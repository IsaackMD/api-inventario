using Microsoft.EntityFrameworkCore;
using MyInventoryApp.src.Application.Mappers;
using MyInventoryApp.src.Application.UseCases.Categories;
using MyInventoryApp.src.Application.UseCases.InfoData;
using MyInventoryApp.src.Application.UseCases.Products;
using MyInventoryApp.src.Application.UseCases.Stocks;
using MyInventoryApp.src.Domain.Interfaces;
using MyInventoryApp.src.Infraestructure;
using MyInventoryApp.src.Infraestructure.Persistence;
using MyInventoryApp.src.Infraestructure.Persistence.Repositories;

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
builder.Services.AddScoped<GetInfoRepository>();

builder.Services.AddScoped<CreateProductUseCase>();
builder.Services.AddScoped<ListProduct>();
builder.Services.AddScoped<ListStockUseCase>();
builder.Services.AddScoped<GetProductsUseCase>();
builder.Services.AddScoped<ListCategoryUseCase>();
builder.Services.AddScoped<CreateCategoryUseCase>();
builder.Services.AddScoped<IncreaseStockUseCase>();
builder.Services.AddScoped<DecreaseStockUseCase>();
builder.Services.AddScoped<GetInfoUseCase>();   

builder.Services.AddScoped<IUnitOfWork, EfUnitOfWork>();

builder.Services.AddAutoMapper(typeof(MappingProfile));
// Controllers
builder.Services.AddControllers();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// 1. Agregar servicios CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("http://localhost:3000") // SIN slash
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});

var app = builder.Build();


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