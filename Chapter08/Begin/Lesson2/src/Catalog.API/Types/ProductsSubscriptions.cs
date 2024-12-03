using System.Runtime.CompilerServices;
using HotChocolate.Subscriptions;

namespace eShop.Catalog.Types;

[SubscriptionType]
public static class ProductsSubscriptions
{
    public static async IAsyncEnumerable<NewProductArrivalEventMessage> OnNewProductArrivalStream(
        [ID<Brand>] int[]? brandIds, 
        ITopicEventReceiver receiver,
        [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        var eventStream = await receiver.SubscribeAsync<NewProductArrivalEventMessage>(
            TopicNames.NewProductArrival, 
            cancellationToken);

        var allowedBrands = brandIds is null ? null : new HashSet<int>(brandIds);

        await foreach (var message in eventStream.ReadEventsAsync().WithCancellation(cancellationToken))
        {
            if (allowedBrands?.Contains(message.BrandId) ?? true)
            {
                yield return message;
            }
        }
    } 
    
    [Subscribe(With = nameof(OnNewProductArrivalStream))]
    public static async Task<Product> OnNewProductArrival(
        [EventMessage] NewProductArrivalEventMessage message,
        ProductService productService,
        CancellationToken cancellationToken) 
        => (await productService.GetProductByIdAsync(message.ProductId, cancellationToken))!;


    [Subscribe]
    [Topic("ImageProcessing_{transactionId}")]
    public static string OnUploadProductStatusChange(
        string transactionId,
        [EventMessage] string message)
        => message;
}