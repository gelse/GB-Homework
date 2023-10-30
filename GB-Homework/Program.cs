using GB_Homework.Filters;
using GB_Homework.Validators;
using GB_Homework.Worker;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<ValidationFilterAttribute>();
builder.Services.AddSingleton<IVatWorker, VatWorker>();

// that registration can easily be switched to a different validator. i know i broke YAGNI with it.
builder.Services.AddSingleton<IVatValueValidator, AustrianVatValueValidator>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();