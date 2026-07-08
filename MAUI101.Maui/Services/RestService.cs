using System.Diagnostics;
using System.Text.Json;
using MAUI101.Maui.Models;
using Microsoft.Extensions.Configuration;

namespace MAUI101.Maui.Services;
public class RestService : IRestService
{
    HttpClient _client;
    JsonSerializerOptions _serializerOptions;
    IConfiguration _configuration;

    public List<Pet> Pets { get; private set; } = [];

    public RestService(IConfiguration configuration)
    {
#if DEBUG
        HttpClientHandler insecureHandler = GetInsecureHandler();
        _client = new HttpClient(insecureHandler);
#else
        _client = new HttpClient();
#endif
        _serializerOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true
        };

            this._configuration = configuration;
    }

    private HttpClientHandler GetInsecureHandler()
    {
        HttpClientHandler handler = new HttpClientHandler();
        handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) =>
        {
            if (cert != null && cert.Issuer.Equals("CN=localhost"))
                return true;
            return errors == System.Net.Security.SslPolicyErrors.None;
        };
        return handler;
    }

    public async Task<List<Pet>> GetPetsAsync()
    {
        Uri uri = new Uri($"{_configuration["ConfigurationHelper:APIUrl"]}/v1/images/search?limit=20&has_breeds=1&api_key={_configuration["ConfigurationHelper:APIKey"]}");
        try
        {
            HttpResponseMessage response = await _client.GetAsync(uri);
            response.EnsureSuccessStatusCode();

            string content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<Pet>>(content, _serializerOptions);
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"ERROR {ex.Message}");
            throw;
        }
    }

    public async Task<Pet?> GetPetByIdAsync(string id)
    {

        Uri uri = new Uri($"{_configuration["ConfigurationHelper:APIUrl"]}/v1/images/{id}");
        try
        {
            HttpResponseMessage response = await _client.GetAsync(uri);
            response.EnsureSuccessStatusCode();

            string content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<Pet>(content, _serializerOptions);
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"ERROR {ex.Message}");
            throw;
        }

    }

}