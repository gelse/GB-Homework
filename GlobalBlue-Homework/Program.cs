using GlobalBlue_Homework.Filters;
using GlobalBlue_Homework.Validators;
using GlobalBlue_Homework.Worker;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<ValidationFilterAttribute>();
builder.Services.AddTransient<IVatWorker, VatWorker>();
builder.Services.AddTransient<IVatValueValidator, AustrianVatValueValidator>();

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