namespace Hl7.App.Security;

public class ApiKeyValidation : IApiKeyValidation
{
    private const string ApiKeyName = "ApiKey";
    private readonly IConfiguration _configuration;
    public ApiKeyValidation(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    public bool IsValidApiKey(string userApiKey)
    {
        if (string.IsNullOrWhiteSpace(userApiKey))
            return false;
        var apiKey = _configuration.GetValue<string>(ApiKeyName);
        
        if (apiKey != userApiKey)
            return false;
        
        return true;
    }
}
