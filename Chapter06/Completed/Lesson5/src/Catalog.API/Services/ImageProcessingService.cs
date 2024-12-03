using HotChocolate.Subscriptions;

namespace eShop.Catalog.Services;

public class ImageProcessingService(
    IServiceProvider services, 
    ITopicEventSender topicEventSender, 
    ITopicEventReceiver topicEventReceiver) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var eventStream = await topicEventReceiver.SubscribeAsync<ProductImageEventMessage>(
            TopicNames.ImageProcessing, 
            stoppingToken);

        await foreach (var message in eventStream.ReadEventsAsync().WithCancellation(stoppingToken))
        {
            await ProcessImageAsync(message, stoppingToken);
        }
    }

    private async Task ProcessImageAsync(ProductImageEventMessage message, CancellationToken cancellationToken)
    {
        await using var scope = services.CreateAsyncScope();
        
        var productService = scope.ServiceProvider.GetRequiredService<ProductService>();
        var imageStorage = scope.ServiceProvider.GetRequiredService<ImageStorage>();

        var topic = $"{TopicNames.ImageProcessing}_{message.TransactionId}";
        await topicEventSender.SendAsync(topic, "Started ...", cancellationToken);

        await Task.Delay(5000, cancellationToken);
        await topicEventSender.SendAsync(topic, "Validated Image ...", cancellationToken);
        
        await Task.Delay(3000, cancellationToken);
        await topicEventSender.SendAsync(topic, "Reformatted Image ...", cancellationToken);

        await topicEventSender.SendAsync(topic, "Fetch Product ...", cancellationToken);
        await Task.Delay(3000, cancellationToken);
        var product = await productService.GetProductByIdAsync(message.ProductId, cancellationToken);

        if (product is null)
        {
            await topicEventSender.SendAsync(topic, "Failed: Invalid product ...", cancellationToken);
            return;
        }
        
        await topicEventSender.SendAsync(topic, $"Uploading Image for Product {product.Name} ...", cancellationToken);
        await using var stream = File.OpenRead(message.FileName);
        product.ImageFileName = await imageStorage.SaveImageAsync(message.OriginalFileName, stream, cancellationToken);
        await productService.UpdateProductAsync(product, cancellationToken);

        await topicEventSender.SendAsync(topic, "Completed!", cancellationToken);
        await topicEventSender.CompleteAsync(topic);
    }
}