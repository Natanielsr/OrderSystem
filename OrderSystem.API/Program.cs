
using FluentValidation;
using OrderSystem.Application.Orders.Commands.CreateOrder;

var builder = WebApplication.CreateBuilder(args);
var assembly = typeof(Program).Assembly;

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

// Registra todos os validators do assembly
builder.Services.AddValidatorsFromAssembly(assembly);

// Isso fará o AutoMapper procurar por qualquer classe que herde de 'Profile' 
// no assembly onde o seu MappingProfile (ou qualquer classe da Application) está.
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Configura o MediatR e adiciona o Behavior
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(assembly);
    cfg.AddOpenBehavior(typeof(CreateOrderValidationBehavior<,>));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();


app.Run();

