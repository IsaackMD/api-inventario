using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MyInventoryApp.src.Application.Mappers;
using MyInventoryApp.src.Application.UseCases.AlertaLowProductCase;
using MyInventoryApp.src.Application.UseCases.Categories;
using MyInventoryApp.src.Application.UseCases.Firebase;
using MyInventoryApp.src.Application.UseCases.InfoData;
using MyInventoryApp.src.Application.UseCases.Notify;
using MyInventoryApp.src.Application.UseCases.Products;
using MyInventoryApp.src.Application.UseCases.Stocks;
using MyInventoryApp.src.Application.UseCases.User;
using MyInventoryApp.src.Domain.Interfaces;
using MyInventoryApp.src.Infraestructure;
using MyInventoryApp.src.Infraestructure.Persistence;
using MyInventoryApp.src.Infraestructure.Persistence.Repositories;
using MyInventoryApp.src.Infraestructure.Service;
using MyInventoryApp.src.Infraestructure.Service.Jwt;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// DB
builder.Services.AddDbContext<MyInventoryDbContext>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("DefaultConnection")
    )
);

// DI
builder.Services.AddScoped<ICategoriaRepository, CategoryRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IStockMovementRepository, StockMovementRepository>();
builder.Services.AddScoped<INotificationTokenRepository, NotificationTokenRepository>();
builder.Services.AddScoped<INotificationService, FirebaseNotificationService>();
builder.Services.AddScoped<IAuthRepository, UserRepository>();
builder.Services.AddScoped<GetInfoRepository>();

builder.Services.AddScoped<TokenService>();
builder.Services.AddScoped<CreateProductUseCase>();
builder.Services.AddScoped<AlertaLowProductCase>();
builder.Services.AddScoped<ListProduct>();
builder.Services.AddScoped<ListStockUseCase>();
builder.Services.AddScoped<GetProductsUseCase>();
builder.Services.AddScoped<ListCategoryUseCase>();
builder.Services.AddScoped<CreateCategoryUseCase>();
builder.Services.AddScoped<IncreaseStockUseCase>();
builder.Services.AddScoped<DecreaseStockUseCase>();
builder.Services.AddScoped<GetInfoUseCase>();
builder.Services.AddScoped<UpdateCategoryUseCase>();
builder.Services.AddScoped<NotifyLowStockUseCase>();
builder.Services.AddScoped<FirebaseUseCase>();
builder.Services.AddScoped<CreateUserUseCase>();
builder.Services.AddScoped<LoginUseCase>();
builder.Services.AddScoped<AuthMeUseCase>();
builder.Services.AddScoped<ITokenValidator, TokenValidator>();

builder.Services.AddScoped<IUnitOfWork, EfUnitOfWork>();

builder.Services.AddAutoMapper(typeof(MappingProfile));
// Controllers
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        // ✅ Ignora propiedades con valor null
        options.JsonSerializerOptions.DefaultIgnoreCondition =
            System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
    });

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// 1. Agregar servicios CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        //policy.WithOrigins(
        //        "http://localhost:3000",
        //        "http://192.168.100.144:3000"
        //      )
        //      .AllowAnyHeader()
        //      .AllowAnyMethod();
        ////.AllowCredentials();

        policy.WithOrigins("http://localhost:5173")
              .AllowCredentials()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Secret"])
            )
        };


        options.Events = new JwtBearerEvents
        {
            OnMessageReceived = context =>
            {
                var token = context.Request.Cookies["access_token"];

                if (!string.IsNullOrEmpty(token))
                {
                    context.Token = token;
                }

                return Task.CompletedTask;
            }
        };
    });

builder.Services.AddAuthorization();

var app = builder.Build();
app.UseRouting();

app.UseCors("AllowFrontend");

// ========================================
// APLICAR MIGRACIONES CON RETRY ✅
// ========================================
//using (var scope = app.Services.CreateScope())
//{
//    var services = scope.ServiceProvider;
//    var logger = services.GetRequiredService<ILogger<Program>>();
//    var context = services.GetRequiredService<MyInventoryDbContext>();

//    var retry = 0;
//    var maxRetries = 10;
//    var delay = TimeSpan.FromSeconds(3);

//    while (retry < maxRetries)
//    {
//        try
//        {
//            logger.LogInformation($"🔄 Intento {retry + 1}/{maxRetries} - Aplicando migraciones...");
//            context.Database.Migrate();
//            logger.LogInformation("✅ Migraciones aplicadas exitosamente");
//            break;
//        }
//        catch (Exception ex)
//        {
//            retry++;
//            if (retry >= maxRetries)
//            {
//                logger.LogError(ex, "❌ Error al aplicar migraciones después de {MaxRetries} intentos", maxRetries);
//                throw;
//            }

//            logger.LogWarning(ex, "⚠️ Error al conectar con la base de datos. Reintentando en {Delay} segundos...", delay.TotalSeconds);
//            Thread.Sleep(delay);
//        }
//    }
//}

// Swagger middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.Run();

Console.WriteLine(builder.Configuration["Jwt:Secret"]);
