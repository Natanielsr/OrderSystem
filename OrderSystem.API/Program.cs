
using FluentValidation;
using OrderSystem.API.Filters;
using OrderSystem.Application.Mappings;
using OrderSystem.Application.Orders.Commands.CreateOrder;
using OrderSystem.Application.Orders.Queries.ListOrders;
using OrderSystem.Application.Validator;
using OrderSystem.Domain.Repository;
using OrderSystem.Domain.UnitOfWork;
using OrderSystem.Infrastructure;
using OrderSystem.Infrastructure.Repository.EntityFramework;
using OrderSystem.Infrastructure.UnitOfWork;

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

// Registra todos os validators do assembly
builder.Services.AddValidatorsFromAssemblyContaining<CreateOrderValidator>();

// Isso fará o AutoMapper procurar por qualquer classe que herde de 'Profile' 
// no assembly onde o seu MappingProfile (ou qualquer classe da Application) está.
builder.Services.AddAutoMapper(typeof(OrderMappingProfile).Assembly);

// Configura o MediatR e adiciona o Behavior
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssemblyContaining<Program>();
    cfg.AddOpenBehavior(typeof(CreateOrderValidationBehavior<,>));
    cfg.RegisterServicesFromAssembly(assembly);
    cfg.RegisterServicesFromAssembly(typeof(ListOrdersQuery).Assembly);

});

builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IOrderUnitOfWork, OrderUnitOfWork>();

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

