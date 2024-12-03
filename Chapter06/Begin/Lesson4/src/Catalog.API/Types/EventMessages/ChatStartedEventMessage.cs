namespace eShop.Catalog.Types;

public record ChatStartedEventMessage(string SessionId, string CustomerName, int ProductId, DateTime Time);