using eShop.Catalog.Services.Errors;
using HotChocolate.Subscriptions;
using eShop.Catalog.Sessions;
using eShop.Catalog.Types.Errors;
using ISession = eShop.Catalog.Sessions.ISession;

namespace eShop.Catalog.Types;

[MutationType]
public static class MessageMutations
{
    [UseMutationConvention(PayloadFieldName = "sessionId")]
    public static async Task<FieldResult<string, UserSessionRequiredError>> StartChatAsync(
        [ID<Product>] int productId,
        ChatService chatService,
        ITopicEventSender topicEventSender,
        CancellationToken cancellationToken)
    {
        var chat = await chatService.CreateChatAsync(productId, cancellationToken);
        var sessionId = chat.SessionId.ToString("N");
        
        await topicEventSender.SendAsync(
            TopicNames.ChatStarted, 
            new ChatStartedEventMessage(chat.Id, sessionId, chat.CustomerName, productId, chat.Time), 
            cancellationToken);
        return sessionId;
    }

    [UseMutationConvention(PayloadFieldName = "success")]
    public static async Task<bool> AssignChatAsync(
        string sessionId, 
        ChatService chatService,
        CancellationToken cancellationToken) 
        => await chatService.AssignChatAsync(Guid.Parse(sessionId), cancellationToken);

    [UseMutationConvention(PayloadFieldName = "success")]
    public static async Task<FieldResult<bool, UserSessionRequiredError>> SendMessageAsync(
        string sessionId,
        string text,
        ChatService chatService,
        ITopicEventSender topicEventSender,
        CancellationToken cancellationToken)
    {
        var chatMessage = await chatService.CreateMessageAsync(Guid.Parse(sessionId), text, cancellationToken);
        var topicName = TopicNames.Chat + sessionId;
        var eventMessage = new ChatMessageEventMessage(chatMessage.Id, text, chatMessage.From);
        await topicEventSender.SendAsync(topicName, eventMessage, cancellationToken);
        return true;
    }
}