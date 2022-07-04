using System;
using System.Net.Http.Json;
using System.Text.Json;
using clientapp.Infrastructure.Contracts;
using common.exception;
using common.shared;
using common.shared.User.Dto;
using MudBlazor;

namespace clientapp.Infrastructure;

public class VerifyEmailService : BaseService, IVerifyEmailService
{
    private HttpClient httpClient;
    private const string ControllerUrl = "verifyemail";

    public VerifyEmailService(HttpClient httpClient, ISnackbar snackbar)
        : base(snackbar)
    {
        this.httpClient = httpClient;
    }

    public async Task<HttpResponseMessage> CheckVerifyEmailCode(CheckVerifyEmailCodeRequestDto request)
    {
        var response = await httpClient.PostAsJsonAsync($"{ControllerUrl}", request);

        ValidateResponse(response);

        return response;
    }

    public async Task<HttpResponseMessage> SendVerifyEmailCode()
    {
        var response = await httpClient.PostAsync($"{ControllerUrl}/sendverifyemailcode", null);

        ValidateResponse(response);

        return response;
    }
}

