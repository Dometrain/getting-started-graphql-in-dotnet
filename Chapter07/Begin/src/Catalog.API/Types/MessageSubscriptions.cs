namespace eShop.Catalog.Types;

[SubscriptionType]
public static class MessageSubscriptions
{
    [Subscribe]
    [Topic(TopicNames.ChatStarted)]
    public static ChatStartedMessage OnChatStarted(
        [EventMessage] ChatStartedEventMessage message)
        => new ChatStartedMessage(message.SessionId, message.ProductId);
    
    [Subscribe]
    [Topic("Chat_{sessionId}")]
    public static ChatMessage OnChatMessage(
        string sessionId,
        [EventMessage] ChatMessageEventMessage message) 
        => new(message.Id, message.Text, message.From);
}

public sealed class ChatStartedMessage(string sessionId, int productId)
{
    public string SessionId => sessionId;

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

public record ChatMessage([ID<ChatMessage>] Guid Id, string Text, string From);