namespace eShop.Catalog.Types;

public record ChatStartedEventMessage(int Id, string SessionId, string CustomerName, int ProductId, DateTime Time);