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
        ISession session,
        ITopicEventSender topicEventSender,
        CancellationToken cancellationToken)
    {
        if (session.CurrentUser is null)
        {
            return new UserSessionRequiredError();
        }
        
        var sessionId = Guid.NewGuid().ToString("N");
        await topicEventSender.SendAsync(
            TopicNames.ChatStarted, 
            new ChatStartedEventMessage(sessionId, session.CurrentUser.Name, productId, DateTime.UtcNow), 
            cancellationToken);
        return sessionId;
    }
    
    [UseMutationConvention(PayloadFieldName = "success")]
    public static async Task<FieldResult<bool, UserSessionRequiredError>> SendMessageAsync(
        string sessionId,
        string text,
        ISession session,
        ITopicEventSender topicEventSender,
        CancellationToken cancellationToken)
    {
        if (session.CurrentUser is null)
        {
            return new UserSessionRequiredError();
        }
        
        var topicName = TopicNames.Chat + sessionId;
        var message = new ChatMessageEventMessage(Guid.NewGuid(), text, session.CurrentUser.Name);
        await topicEventSender.SendAsync(topicName, message, cancellationToken);
        return true;
    }
}