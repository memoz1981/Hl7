namespace Hl7.App.Security;

public interface IApiKeyValidation
{
    bool IsValidApiKey(string userApiKey);
}
