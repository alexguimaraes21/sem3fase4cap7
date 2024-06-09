using Asp.Versioning;
using AutoMapper;
using Fiap.Api.Alunos;
using Fiap.CidadesInteligentes.ColetaResiduos.Api.Context;
using Fiap.CidadesInteligentes.ColetaResiduos.Api.Handlers;
using Fiap.CidadesInteligentes.ColetaResiduos.Api.Libs;
using Fiap.CidadesInteligentes.ColetaResiduos.Api.Models;
using Fiap.CidadesInteligentes.ColetaResiduos.Api.Repositories;
using Fiap.CidadesInteligentes.ColetaResiduos.Api.ResponseModels;
using Fiap.CidadesInteligentes.ColetaResiduos.Api.Services;
using Fiap.CidadesInteligentes.ColetaResiduos.Api.ViewModels;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
#region DB Configuration
var connectionString = builder.Configuration.GetConnectionString("DbConnection");
builder.Services.AddDbContext<DatabaseContext>(
    opt => opt.UseOracle(connectionString).EnableSensitiveDataLogging(true)
);
#endregion

#region Repositories
builder.Services.AddTransient<UserRepository>();
builder.Services.AddTransient<TruckRepository>();
builder.Services.AddTransient<ContainerRepository>();
builder.Services.AddTransient<CollectionRepository>();
builder.Services.AddTransient<RouteRepository>();
#endregion

#region Services
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ITruckService, TruckService>();
builder.Services.AddScoped<IContainerService, ContainerService>();
#endregion

#region AutoMapper
var mapperConfig = new AutoMapper.MapperConfiguration(mc =>
{
    mc.AllowNullCollections = true;
    mc.AllowNullDestinationValues = true;

    /* UserModel, UserViewModel, RegisterViewModel */
    // UserModel -> UserResponseModel || UserResponseModel -> UserModel
    mc.CreateMap<UserModel, UserResponseModel>();
    mc.CreateMap<UserResponseModel, UserModel>();
    // UserModel -> UserViewModel || UserViewModel -> UserModel
    mc.CreateMap<UserModel, UserViewModel>();
    mc.CreateMap<UserViewModel, UserModel>();
    // UserModel -> RegisterViewModel || RegisterViewModel -> UserModel
    mc.CreateMap<UserModel, RegisterViewModel>();
    mc.CreateMap<RegisterViewModel, UserModel>();

    /* TruckModel, TruckViewModel, TruckResponseModel */
    // TruckModel -> TruckViewModel || TruckViewModel -> TruckModel
    mc.CreateMap<TruckModel, TruckViewModel>();
    mc.CreateMap<TruckViewModel, TruckModel>();
    // TruckModel -> TruckResponseModel || TruckResponseModel -> TruckModel
    mc.CreateMap<TruckModel, TruckResponseModel>();
    mc.CreateMap<TruckResponseModel, TruckModel>();

    /* ContainerModel, ContainerViewModel, ContainerResponseModel */
    // ContainerModel -> ContainerViewModel || ContainerViewMode -> ContainerModel
    mc.CreateMap<ContainerModel, ContainerViewModel>();
    mc.CreateMap<ContainerViewModel, ContainerModel>();
    // ContainerModel -> ContainerResponseModel || ContainerResponseModel -> ContainerModel
    mc.CreateMap<ContainerModel, ContainerResponseModel>();
    mc.CreateMap<ContainerResponseModel, ContainerModel>();
});
IMapper mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);
#endregion

#region CORS Configuration
builder.Services.AddCors(opts =>
{
    opts.AddDefaultPolicy(builder =>
        builder.WithOrigins("*")
            .AllowAnyMethod()
            .AllowAnyHeader());
});
#endregion

#region Authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Util.JWT_KEY_STR)),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });
#endregion

builder.Services.AddControllers(options =>
{
    options.Filters.Add<HttpResponseExceptionFilter>();
})
    .ConfigureApiBehaviorOptions(options =>
    {
        options.InvalidModelStateResponseFactory = context =>
            new BadRequestObjectResult(context.ModelState)
            {
                ContentTypes =
                {
                    // using static System.Net.Mime.MediaTypeNames;
                    Application.Json,
                    Application.Xml
                }
            };
    })
    .AddXmlSerializerFormatters();

#region Versioning
builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new ApiVersion(1);
    options.ReportApiVersions = true;
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.ApiVersionReader = ApiVersionReader.Combine(
        new UrlSegmentApiVersionReader(),
        new HeaderApiVersionReader("X-Api-Version"));
}).AddApiExplorer(options =>
{
    options.GroupNameFormat = "'v'V";
    options.SubstituteApiVersionInUrl = true;
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
builder.Services.AddSwaggerGen(options => options.OperationFilter<SwaggerDefaultValues>());
#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

if (app.Environment.IsDevelopment())
{
    //app.UseExceptionHandler("/error-development");
    app.UseExceptionHandler(new ExceptionHandlerOptions()
    {
        AllowStatusCode404Response = true,
        ExceptionHandlingPath = "/error-development"
    });
}
else
{
    app.UseExceptionHandler(new ExceptionHandlerOptions()
    {
        AllowStatusCode404Response = true,
        ExceptionHandlingPath = "/error"
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
