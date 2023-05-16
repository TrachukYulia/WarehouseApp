using DAL.Interfaces;
using DAL;
using BLL;
using BLL.Services;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using BLL.Interfaces;
using WebApi.ExceptionHandler;
using BLL.Validation;
using FluentValidation.AspNetCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<WarehouseContext>
    (options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("WarehouseDb"));
});

builder.Services.AddControllers()
.AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
})
.AddFluentValidation(validator =>
{
    _ = validator.RegisterValidatorsFromAssemblyContaining<CustomerRequestValidator>(lifetime: ServiceLifetime.Singleton);
    _ = validator.RegisterValidatorsFromAssemblyContaining<GoodRequestValidator>(lifetime: ServiceLifetime.Singleton);
    _ = validator.RegisterValidatorsFromAssemblyContaining<OrderRequestValidator>(lifetime: ServiceLifetime.Singleton);
    _ = validator.RegisterValidatorsFromAssemblyContaining<OrderGoodRequestValidator>(lifetime: ServiceLifetime.Singleton);
    _ = validator.RegisterValidatorsFromAssemblyContaining<QueueRequestValidator>(lifetime: ServiceLifetime.Singleton);
    _ = validator.RegisterValidatorsFromAssemblyContaining<TypeOfGoodRequestValidator>(lifetime: ServiceLifetime.Singleton);
}); ;
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddScoped<IGoodService, GoodService>();

builder.Services.AddScoped<ITypeOfGoodService, TypeOfGoodService>();

builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<IOrderService, OrderService>();

builder.Services.AddScoped<IQueueService, QueueService>();
builder.Services.AddTransient<ExceptionMiddleware>();


var mapperConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new AutoMapperProfile());
});
IMapper mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseMiddleware<ExceptionMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
