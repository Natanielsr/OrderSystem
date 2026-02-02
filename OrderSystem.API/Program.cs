
using FluentValidation;
using Microsoft.AspNetCore.Http.HttpResults;
using OrderSystem.API.Filters;
using OrderSystem.Application.Mappings;
using OrderSystem.Application.Orders.Commands.CreateOrder;
using OrderSystem.Application.Orders.Queries.ListOrders;
using OrderSystem.Domain.Repository;
using OrderSystem.Domain.UnitOfWork;
using OrderSystem.Infrastructure.Repository;
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
});

// Registra todos os validators do assembly
builder.Services.AddValidatorsFromAssembly(assembly);

// Isso fará o AutoMapper procurar por qualquer classe que herde de 'Profile' 
// no assembly onde o seu MappingProfile (ou qualquer classe da Application) está.
builder.Services.AddAutoMapper(typeof(OrderMappingProfile).Assembly);

// Configura o MediatR e adiciona o Behavior
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(assembly);
    cfg.RegisterServicesFromAssembly(typeof(ListOrdersQuery).Assembly);
    cfg.AddOpenBehavior(typeof(CreateOrderValidationBehavior<,>));
});

//fake repository and unitofwork
builder.Services.AddSingleton<IOrderRepository, OrderRepositoryTEST>();
builder.Services.AddSingleton<IProductRepository, ProductRepositoryTEST>();
builder.Services.AddScoped<IOrderUnitOfWork, OrderUnitOfWorkTEST>();

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

