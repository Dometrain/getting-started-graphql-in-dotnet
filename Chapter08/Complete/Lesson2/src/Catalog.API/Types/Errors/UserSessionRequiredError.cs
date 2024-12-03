using eShop.Catalog.Services.Errors;

namespace eShop.Catalog.Types.Errors;

public sealed class UserSessionRequiredError
{
    public UserSessionRequiredError()
    {
    }

    public UserSessionRequiredError(UserSessionRequiredException ex)
    {
    }
    
    public string Message => "A user session is required. Please log in.";
}