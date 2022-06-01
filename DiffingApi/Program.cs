using Application.Commands;
using Application.Extensions;
using Application.Helpers;
using Application.Implementation;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors();
builder.Services.AddScoped<DifferenceChecker>();
builder.Services.AddSingleton<WordStoreAccessor>();
builder.Services.AddScoped<IDifferenceCheckerCommand, DifferenceChecker>();
builder.Services.AddScoped<ISetWordCommand, SetWordCommand>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(x =>
{
    x.AllowAnyOrigin();
    x.AllowAnyMethod();
    x.AllowAnyHeader();
});

app.UseHttpsRedirection();

app.RegisterRoutesForDiffer();

app.Run();