using Entrytask_Urban;
using Entrytask_Urban.Controllers;
using Entrytask_Urban.Models;
using Entrytask_Urban.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Text;
using System.Text.Json;
using Entrytask_Urban.Repository;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Mvc;
using Azure.Core;
using Entrytask_Urban.Pages;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo { Title = "Demo API", Version = "v1" });

    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });

    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new List<string>()
        }
    });

});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters()
    {

        ValidateActor = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };

});



builder.Services.AddAuthorization();
/*builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AuthorizedOnly", policy =>
    {
        policy.RequireAuthenticatedUser();
        // Add additional requirements if needed
    });
});*/
//builder.Services.AddControllers();


builder.Services.AddHttpClient();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddHostedService<BackgroundPrinter>();
//builder.Services.AddScoped<BackgroundPrinter>();
builder.Services.AddScoped<myloginModel>();
//builder.Services.AddScoped <TokenService > ();
//builder.Services.AddSingleton<tokenRepository>();
//builder.Services.AddScoped<TokenRepository>();
builder.Services.AddScoped<GetSalesTransactionsDB>();
builder.Services.AddScoped<ISales, GetSalesTransactionsController>();
builder.Services.AddScoped<ISalelines, GetSalelinesTransactionsController>();

builder.Services.AddRazorPages();
builder.Configuration.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
builder.Services.AddDbContext<GetSalesTransactionsDB>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
var app = builder.Build();

app.UseStaticFiles();
app.UseRouting();
app.UseSwagger();
app.UseAuthentication();
app.UseAuthorization();

//app.MapRazorPages();
/*
app.MapPost("/login",
(User loginDTO) => Login(loginDTO))
    .Accepts<User>("application/json")
    .Produces<string>();*/ //tukoment

IResult Login(User loginDTO)
{
    if (loginDTO == null)
    {
        //return BadRequest("Invalid client request");
        return Results.NotFound("User not found");
    }

    if (loginDTO.Username.Equals("michal") && loginDTO.Password.Equals("123"))
    {
        var token = new JwtSecurityToken
       (
           issuer: builder.Configuration["Jwt:Issuer"],
           audience: builder.Configuration["Jwt:Audience"],

           expires: DateTime.UtcNow.AddDays(60),
           notBefore: DateTime.UtcNow,
           signingCredentials: new SigningCredentials(
               new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
               SecurityAlgorithms.HmacSha256)
       );



        loginDTO.authorized = true;

        return Results.Ok(new JwtSecurityTokenHandler().WriteToken(token));

    }
    else
    {
        loginDTO.authorized = false;
        return Results.BadRequest("Invalid user credentials");
    }


}

/*
app.MapPost("/create",
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
(GetSalesTransactions getsalestransactions,  ISales service) => Create(getsalestransactions, service))
    .Accepts < GetSalesTransactions>("application/json")
    .Produces<GetSalesTransactions>(statusCode: 200, contentType: "application/json");
    

app.MapGet("/get",
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
(int transactionkey, ISales service) => Get(transactionkey, service))
    .Produces<GetSalesTransactions>();

app.MapGet("/list",
   [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
(ISales service) => List(service))
    .Produces<List<GetSalesTransactions>>(statusCode: 200, contentType: "application/json");*/ //tukoment


IResult Create(GetSalesTransactions getsalestransactions, ISales service)
{
    var result = service.Create(getsalestransactions);
    return Results.Ok(result);
}
IResult Get(int transactionkey, ISales service)
{
    var getsalestransactions = service.Get(transactionkey);

    if (getsalestransactions is null) return Results.NotFound("not found");

    return Results.Ok(getsalestransactions);
}

IResult List(ISales service)
{
    var getsalestransactionss = service.List();

    return Results.Ok(getsalestransactionss);
}
/*app.MapPost("/createsalines",
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
(GetSalelinesTransactions getsalelinestransactions, ISalelines service) => Createsalelines(getsalelinestransactions, service))
    .Accepts<GetSalelinesTransactions>("application/json")
    .Produces<GetSalelinesTransactions>(statusCode: 200, contentType: "application/json");*/ //tukoment

IResult Createsalelines(GetSalelinesTransactions getsalelinestransactions, ISalelines service)
{
    var result = service.Createsalelines(getsalelinestransactions);
    return Results.Ok(result);
}
/*
app.MapGet("/listsalelines",
   [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
(ISalelines service) => Listsalelines(service))
    .Produces<List<GetSalelinesTransactions>>(statusCode: 200, contentType: "application/json");*/ //tukoment
IResult Listsalelines(ISalelines service)
{
    var getsalelinestransactionss = service.Listsalelines();

    return Results.Ok(getsalelinestransactionss);
}



app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json:", "v1");
    options.RoutePrefix = string.Empty;
    options.DocumentTitle = "My Swagger";

});

app.UseEndpoints(endpoints =>
{
    //endpoints.MapControllers();
    endpoints.MapPost("/lo", () => "kiojoh")
    .ExcludeFromDescription();

    endpoints.MapPost("/login",
(User loginDTO) => Login(loginDTO))
.Accepts<User>("application/json")
.Produces<string>();

    endpoints.MapPost("/create",
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    (GetSalesTransactions getsalestransactions, ISales service) => Create(getsalestransactions, service))
    .Accepts<GetSalesTransactions>("application/json")
    .Produces<GetSalesTransactions>(statusCode: 200, contentType: "application/json");

    endpoints.MapGet("/get",
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    (int transactionkey, ISales service) => Get(transactionkey, service))
    .Produces<GetSalesTransactions>();

    endpoints.MapGet("/list",
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    (ISales service) => List(service))
    .Produces<List<GetSalesTransactions>>(statusCode: 200, contentType: "application/json");

    endpoints.MapPost("/createsalines",
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    (GetSalelinesTransactions getsalelinestransactions, ISalelines service) => Createsalelines(getsalelinestransactions, service))
    .Accepts<GetSalelinesTransactions>("application/json")
    .Produces<GetSalelinesTransactions>(statusCode: 200, contentType: "application/json");

    endpoints.MapGet("/listsalelines",
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    (ISalelines service) => Listsalelines(service))
    .Produces<List<GetSalelinesTransactions>>(statusCode: 200, contentType: "application/json");

    endpoints.MapControllerRoute("Default", "/{controller}/{action}/{id?}",
        new { conroller = "Pages", action = "mylogin" });



});

/*
app.MapPost("/processResponse", async (HttpClient httpClient) =>
{
    using (HttpClient client = new HttpClient())
    {
        // Set the bearer token in the Authorization header


        // Prepare the request payload
        var requestData = new { };
        var jsonPayload = JsonSerializer.Serialize(requestData);
        //var jsonPayload = NewtonsoftJson.Serialize(requestData);
        var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

        // Send the POST request
        HttpResponseMessage response = await client.PostAsync("http://localhost:27807/processResponse", content);


        // Check the response status
        response.EnsureSuccessStatusCode();
        Console.WriteLine(response.StatusCode);
        Console.WriteLine(response.Content);

    }


});*/
/*app.MapPost("/processResponse", async (HttpClient httpClient) =>
{
    var responseContent = await httpClient.GetStringAsync("http://localhost:27807/processResponse");

    return Results.Ok(responseContent);
});*/

app.MapRazorPages();
//app.UseSwaggerUI();
app.Run();


