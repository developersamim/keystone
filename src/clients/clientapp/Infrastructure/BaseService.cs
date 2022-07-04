using System;
using MudBlazor;
using System.Net.Http.Json;
using common.exception;

namespace clientapp.Infrastructure;


public class BaseService
{
    private readonly ISnackbar snackbar;

    public BaseService(ISnackbar snackbar)
    {
        this.snackbar = snackbar;
    }

    public async void ValidateResponse(HttpResponseMessage httpResponseMessage)
    {
        if (!httpResponseMessage.IsSuccessStatusCode)
        {
            var apiError = await httpResponseMessage.Content.ReadFromJsonAsync<ApiError>();

            snackbar.Add($"{apiError.Message}", MudBlazor.Severity.Error);
        }
    }
}

