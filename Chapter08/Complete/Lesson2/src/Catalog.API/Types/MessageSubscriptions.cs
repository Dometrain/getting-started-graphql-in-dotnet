using System.Runtime.CompilerServices;
using HotChocolate.Subscriptions;

namespace eShop.Catalog.Types;

[SubscriptionType]
public static class MessageSubscriptions
{
    [Subscribe(With = nameof(OnChatStartedStream))]
    public static ChatStartedMessage OnChatStarted(
        [EventMessage] ChatStartedEventMessage message)
        => new ChatStartedMessage(message.SessionId, message.CustomerName, message.ProductId, message.Time);

    public static async IAsyncEnumerable<ChatStartedEventMessage> OnChatStartedStream(
        IServiceProvider services,
        ITopicEventReceiver topicEventReceiver,
        [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        var lastChatId = 0;
        var eventStream = await topicEventReceiver.SubscribeAsync<ChatStartedEventMessage>(
            TopicNames.ChatStarted,
            cancellationToken);
        var eventMessages = eventStream.ReadEventsAsync();
        
        foreach (var chat in await GetOpenChatsAsync(services, cancellationToken))
        {
            yield return new ChatStartedEventMessage(
                chat.Id,
                chat.SessionId.ToString("N"),
                chat.CustomerName,
                chat.ProductId,
                chat.Time);
            lastChatId = chat.Id;
        }

        await foreach (var eventMessage in eventMessages)
        {
            if (lastChatId < eventMessage.Id)
            {
                yield return eventMessage;
            }
        }
    }

    private static async Task<IReadOnlyList<CustomerChat>> GetOpenChatsAsync(
        IServiceProvider services,
        CancellationToken cancellationToken)
    {
        await using var scope = services.CreateAsyncScope();
        var chatService = scope.ServiceProvider.GetRequiredService<ChatService>();
        return await chatService.GetOpenChatsAsync(cancellationToken);
    }
    
    [Subscribe(With = nameof(OnChatMessageStream))]
    public static ChatMessage OnChatMessage(
        [EventMessage] ChatMessageEventMessage message) 
        => new(message.Id, message.Text, message.From);

    public static async IAsyncEnumerable<ChatMessageEventMessage> OnChatMessageStream(
        string sessionId,
        ITopicEventReceiver topicEventReceiver,
        IServiceProvider services,
        [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        var lastMessageId = 0;
        var eventStream = await topicEventReceiver.SubscribeAsync<ChatMessageEventMessage>(
            TopicNames.Chat + sessionId,
            cancellationToken);
        var eventMessages = eventStream.ReadEventsAsync();
        
        foreach (var chatMessage in await GetChatMessagesAsync(sessionId, services, cancellationToken))
        {
            yield return new ChatMessageEventMessage(
                chatMessage.Id,
                chatMessage.Text,
                chatMessage.From);
            lastMessageId = chatMessage.Id;
        }

        await foreach (var eventMessage in eventMessages)
        {
            if (lastMessageId < eventMessage.Id)
            {
                yield return eventMessage;
            }
        }
    }

    private static async Task<IReadOnlyList<CustomerChatMessage>> GetChatMessagesAsync(
        string sessionId,
        IServiceProvider services,
        CancellationToken cancellationToken)
    {
        await using var scope = services.CreateAsyncScope();
        var chatService = scope.ServiceProvider.GetRequiredService<ChatService>();
        return await chatService.GetMessagesAsync(Guid.Parse(sessionId), cancellationToken);
    }
    
}

public sealed class ChatStartedMessage(string sessionId, string customerName, int productId, DateTime time)
{
    public string SessionId => sessionId;
    
    public string CustomerName => customerName;
    
    public DateTime Time => time;

    public async Task<Product> GetProductAsync(
        ProductService productService, 
        CancellationToken cancellationToken)
    {
        var product = await productService.GetProductByIdAsync(productId, cancellationToken);

        if (product is null)
        {
            product = new Product { Id = productId, Name = "NOT FOUND" };
        }

        return product;
    } 
}

public record ChatMessage([property: ID<ChatMessage>] int Id, string Text, string From);