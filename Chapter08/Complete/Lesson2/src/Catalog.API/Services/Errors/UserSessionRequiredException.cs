namespace eShop.Catalog.Services.Errors;

public sealed class UserSessionRequiredException() 
    : Exception("A user session is required. Please log in.");