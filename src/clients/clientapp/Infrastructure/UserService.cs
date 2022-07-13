using clientapp.Infrastructure.Contracts;
using clientapp.Models;
using MudBlazor;
using System;
using System.Net.Http.Json;

namespace clientapp.Infrastructure;

public class UserService : BaseService, IUserService
{
	private HttpClient httpClient;
	private const string ControllerUrl = "user";

	public UserService(HttpClient httpClient, ISnackbar snackbar)
		: base(snackbar)
	{
		this.httpClient = httpClient;
	}

	public async Task<List<UserProfileDto>> GetUsers()
    {
		var response = await httpClient.GetAsync($"{ControllerUrl}/getusers");
		response.EnsureSuccessStatusCode();

		return await response.Content.ReadFromJsonAsync<List<UserProfileDto>>();
    }

	public async Task UpdateProfile(UserUpdateProfileDto request)
    {
		await httpClient.PutAsJsonAsync($"{ControllerUrl}", request);
    }

    public async Task<UserProfileDto> GetProfile()
    {
		var response = await httpClient.GetAsync($"{ControllerUrl}/getprofile");
		return await ValidateResponse<UserProfileDto>(response);
	}
}

