using clientaggregator.application;
using clientaggregator.infrastructure;
using clientaggregator.infrastructure.Settings;
using common.api.authentication;
using common.api.swagger;
using common.exception;
using common.infrastructure.Settings;
using IdentityModel;
using IdentityModel.Client;
using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;

var builder = WebApplication.CreateBuilder(args);

ApiSetting apiSetting = builder.Configuration.GetSection("ApiSetting").Get<ApiSetting>();
AuthenticationSetting authenticationSetting = builder.Configuration.GetSection("AuthenticationSetting").Get<AuthenticationSetting>();


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "Client Aggregator API", Version = "v1" });
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.OAuth2,
        Flows = new OpenApiOAuthFlows
        {
            AuthorizationCode = new OpenApiOAuthFlow
            {
                AuthorizationUrl = new Uri(builder.Configuration["SwaggerConfiguration:Auth:AuthorizeEndpoint"]),
                TokenUrl = new Uri(builder.Configuration["SwaggerConfiguration:Auth:TokenEndpoint"]),
                Scopes = new Dictionary<string, string>
                {
                    {builder.Configuration["SwaggerConfiguration:Auth:Scopes:0"], "Client Aggregator API - full access" }
                }
            }
        }
    });
    options.OperationFilter<AuthorizeCheckOperationFilter>("oauth2", builder.Configuration.GetSection("SwaggerConfiguration:Auth:Scopes")?.GetChildren()?.Select(x => x.Value)?.ToArray());
});

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = IdentityServerAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(options =>
    {
        options.Authority = authenticationSetting.Authority;

        // if you are using API resources, you can specify the name here
        //options.Audience = "resource1";
        options.TokenValidationParameters.ValidateAudience = false;

        // IdentityServer emits a typ header by default, recommended extra check
        options.TokenValidationParameters.ValidTypes = new[] { "at+jwt" };

        options.TokenValidationParameters.NameClaimType = JwtClaimTypes.Name;
    });

builder.Services.AddAuthorization(e =>
{
    e.AddPolicy(AuthConstant.KnownAuthorizationPolicyName.ClientAccess, policy =>
    {
        policy.RequireAuthenticatedUser();
        policy.RequireClaim("scope", AuthConstant.KnownScope.ClientAccess);
    });
});

const string clientCredentialsTokenKey = "ClientAggregatorClientToken";

builder.Services.AddAccessTokenManagement(options =>
{
    //var discovery = Common.Authentication.DiscoveryDocumentHelpers.GetDiscoveryDocument(services, AuthenticationSettings.Authority);

    var request = new ClientCredentialsTokenRequest
    {
        //Address = discovery.TokenEndpoint,
        Address = authenticationSetting.TokenEndpoint,
        ClientId = authenticationSetting.ClientId,
        ClientSecret = authenticationSetting.ClientSecret,
        Scope = authenticationSetting.Scopes
    };
    options.Client.Clients.Add(clientCredentialsTokenKey, request);
});

builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureSerivces(apiSetting, clientCredentialsTokenKey);

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
        policy =>
        {
            //policy.WithOrigins("https://localhost:7187");
            policy.AllowAnyOrigin();
            policy.AllowAnyHeader();
            policy.AllowAnyMethod();
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "My Client Aggregator API V1");

        options.OAuthClientId("clientaggregator_api_swagger");
        options.OAuthAppName("Client Aggregator API - Swagger");
        options.OAuthUsePkce();
    });
}
app.ConfigureCustomExceptionHandler();

//app.UseHttpsRedirection();
app.UseRouting();

app.UseCors(MyAllowSpecificOrigins);

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
