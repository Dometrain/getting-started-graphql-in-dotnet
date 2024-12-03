using eShop.Catalog.Services.Errors;
using ISession = eShop.Catalog.Sessions.ISession;

namespace eShop.Catalog.Services;

public sealed class ChatService(CatalogContext context, ISession session)
{
    public async Task<IReadOnlyList<CustomerChat>> GetOpenChatsAsync(
        CancellationToken cancellationToken = default) 
        => await context.Chats
            .Where(t => t.AssignedTo == 0)
            .OrderBy(t => t.Id)
            .ToListAsync(cancellationToken);

    public async Task<CustomerChat> CreateChatAsync(
        int productId,
        CancellationToken cancellationToken = default)
    {
        if (session.CurrentUser is null)
        {
            throw new UserSessionRequiredException();
        }

        var chat = new CustomerChat
        {
            SessionId = Guid.NewGuid(),
            CustomerName = session.CurrentUser.Name,
            Time = DateTime.UtcNow,
            ProductId = productId
        };
        
        context.Chats.Add(chat);
        await context.SaveChangesAsync(cancellationToken);

        return chat;
    }

    public async Task<bool> AssignChatAsync(
        Guid sessionId, 
        CancellationToken cancellationToken = default)
    {
        if (!(session.CurrentUser?.IsSupportAgent ?? false))
        {
            throw new InvalidUserPrivilegesException();
        }

        var chat = await context.Chats.FirstOrDefaultAsync(t => t.SessionId == sessionId, cancellationToken);

        if (chat is null)
        {
            return false;
        }

        chat.AssignedTo = session.CurrentUser.Id;
        await context.SaveChangesAsync(cancellationToken);
        return true;
    }

    public async Task<IReadOnlyList<CustomerChatMessage>> GetMessagesAsync(
        Guid sessionId,
        CancellationToken cancellationToken = default) 
        => await context.Chats
            .Where(t => t.SessionId == sessionId)
            .Include(t => t.Messages)
            .SelectMany(t => t.Messages)
            .ToListAsync(cancellationToken);

    public async Task<CustomerChatMessage> CreateMessageAsync(
        Guid sessionId,
        string text,
        CancellationToken cancellationToken)
    {
        if (session.CurrentUser is null)
        {
            throw new UserSessionRequiredException();
        }

        var message = new CustomerChatMessage
        {
            Chat = await context.Chats.SingleAsync(t => t.SessionId == sessionId, cancellationToken),
            From = session.CurrentUser.Name,
            Text = text
        };

        context.ChatMessages.Add(message);
        await context.SaveChangesAsync(cancellationToken);

        return message;
    }
}