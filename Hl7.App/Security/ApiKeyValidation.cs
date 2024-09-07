namespace Hl7.App.Security;

public class ApiKeyValidation : IApiKeyValidation
{
    private const string ApiKeyName = "ApiKey";
    private readonly string _apiKey;
    public ApiKeyValidation(IConfiguration configuration)
    {
        _apiKey = configuration.GetValue<string>(ApiKeyName); 
    }
    
    public bool IsValidApiKey(string userApiKey)
    {
        if (string.IsNullOrWhiteSpace(userApiKey))
            return false;
        
        if (_apiKey != userApiKey)
            return false;
        
        return true;
    }
}
