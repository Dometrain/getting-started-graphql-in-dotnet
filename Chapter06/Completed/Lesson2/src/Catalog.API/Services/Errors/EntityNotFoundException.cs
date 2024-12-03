namespace eShop.Catalog.Services.Errors;

public class EntityNotFoundException(string entityName, int entityId) : Exception
{
    public string EntityName { get; } = entityName;
    
    public int EntityId { get; } = entityId;
}