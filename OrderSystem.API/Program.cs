
using OrderSystem.API.Filters;
using OrderSystem.Application.DependencyInjection;
using OrderSystem.Domain.Services;
using OrderSystem.Infrastructure;
using OrderSystem.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);
var assembly = typeof(Program).Assembly;

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers(options =>
{
    options.Filters.Add<BadRequestFilter>();
    options.Filters.Add<ValidationExceptionFilter>();
});

builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

// --- INÍCIO DO BLOCO DE SEED ---
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<AppDbContext>();
        var passwordService = services.GetRequiredService<IPasswordService>();

        // Chama o método estático que criamos
        DbInitializer.Seed(context, passwordService);
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "Ocorreu um erro ao popular o banco de dados.");
    }
}
// --- FIM DO BLOCO DE SEED ---

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();


app.Run();

