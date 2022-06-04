using System;
using System.Net.Http.Json;

namespace clientapp.Services;

public class UserService
{
	private HttpClient httpClient;
	private const string ControllerUrl = "user";

	public UserService(HttpClient httpClient)
	{
		this.httpClient = httpClient;
	}

	public async Task<List<UserProfileDto>> GetUsers()
    {
		var response = await httpClient.GetAsync($"{ControllerUrl}/getusers");
		response.EnsureSuccessStatusCode();

		return await response.Content.ReadFromJsonAsync<List<UserProfileDto>>();
    }
}

