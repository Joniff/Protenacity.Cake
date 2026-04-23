using System.Net.Http.Json;
using Duende.IdentityModel.Client;

// the base URL of the Umbraco site - change this to fit your setup
const string host = "https://localhost:44302";

var client = new HttpClient();

// request a client credentials token from the Management API token endpoint
var tokenResponse = await client.RequestClientCredentialsTokenAsync(
    new ClientCredentialsTokenRequest
    {
        Address = $"{host}/umbraco/management/api/v1/security/back-office/token",
        ClientId = "umbraco-back-office-claude",
        ClientSecret = "4alexandraroad"
    }
);

if (tokenResponse.IsError || tokenResponse.AccessToken is null)
{
    Console.WriteLine($"Error obtaining a token: {tokenResponse.ErrorDescription}");
    return;
}

// use the access token as Bearer token
client.SetBearerToken(tokenResponse.AccessToken);

// fetch user data from the "current user" Management API endpoint
var apiResponse = await client.GetAsync($"{host}/umbraco/management/api/v1/user/current");
var apiUserResponse = await apiResponse
    .EnsureSuccessStatusCode()
    .Content
    .ReadFromJsonAsync<ApiUserResponse>();

if (apiUserResponse is null)
{
    Console.WriteLine("Could not parse a user from the API response.");
    return;
}

Console.WriteLine($"Hello, {apiUserResponse.Name} ({apiUserResponse.Email})");

public class ApiUserResponse
{
    public required Guid Id { get; set; }

    public required string Name { get; set; }

    public required string Email { get; set; }
}
