using Newtonsoft.Json.Serialization;
using TicketBookingAPIs.Filters;
using NLog.Web;
using Autofac.Extensions.DependencyInjection;
using Autofac;
using TicketBookingAPIs.Services;
using Microsoft.AspNetCore.Diagnostics;
using NLog;
using Newtonsoft.Json;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using TicketBookingAPIs.Config;

var builder = WebApplication.CreateBuilder(args);
Logger _logger = LogManager.GetCurrentClassLogger();

// Add services to the container.
builder.Services.AddControllers().AddNewtonsoftJson(options => 
{
    //���÷�������json��Сд
    options.SerializerSettings.ContractResolver = new DefaultContractResolver();
    //�������ڸ�ʽ
    options.SerializerSettings.DateFormatString = "yyyy-MM-dd";
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen( options =>
{
    //�����������Ĭ��ֵ
    options.SchemaFilter<DefaultValueSchemaFilter>();
});
//����
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", opt => opt.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
});

//��־
builder.Logging.ClearProviders();
builder.Logging.AddNLog("nlog.config");
builder.Host.UseNLog();

SymmetricSecurityKey _symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(AppSettingsJson.SecretKey));

//�����֤
builder.Services.AddAuthentication(auth =>
{
    auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(token =>
{
    //token.RequireHttpsMetadata = false;
    token.SaveToken = true;
    token.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = _symmetricSecurityKey,
        ValidateIssuer = true,
        ValidIssuer = "https://localhost:7137",
        ValidateAudience = true,
        ValidAudience = "https://localhost:7137",
        RequireExpirationTime = true,
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero
    };
});
//��Ȩ
builder.Services.AddAuthorization();
//��Ȩ��Χ
builder.Services.AddAuthorizationBuilder()
  .AddPolicy("ticket_booking", policy => policy
  .RequireRole("Admin")  
  .RequireClaim("scope", "ticket_booking_api"));
//��汾
builder.Services.AddApiVersioning(options =>
{
    options.ReportApiVersions = true;   
    options.DefaultApiVersion = new Asp.Versioning.ApiVersion(1);   
});
//����ע�� Autofac
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
{
    //˲ʱע��
    containerBuilder.RegisterType<JwtService>().As<IJwtService>().InstancePerDependency();

});
//����Session
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options => 
{
    options.IdleTimeout = TimeSpan.FromMinutes(60);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("CorsPolicy");

app.UseSession();
app.Use(async (context, next) =>
{
    var jwtoken = context.Session.GetString("JWToken");
    if (!string.IsNullOrEmpty(jwtoken))
    {
        context.Request.Headers.Add("Authorization", $"Bearer {jwtoken}");
    }
    await next();
});

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers()
.RequireAuthorization();
//.RequireAuthorization("ticket_booking");




//ȫ���쳣����
app.UseExceptionHandler(errors =>
{
    errors.Run(async context =>
    {
        var feature = context.Features.Get<IExceptionHandlerPathFeature>();
        var error = feature?.Error;
        var result = new ApiResult(ApiResultStatusCode.ServerError);
        if (error != null)
        {
            _logger.Error($"Application error��{error.Message}-{error.StackTrace}");
            result.Errors = new List<string>();
            result.Errors.Add(error.Message);
            result.Errors.Add(error.StackTrace);
        }

        context.Response.StatusCode = StatusCodes.Status200OK;
        context.Response.ContentType = "application/json";
        await context.Response.WriteAsync(JsonConvert.SerializeObject(result));
    });
});

app.Run();
