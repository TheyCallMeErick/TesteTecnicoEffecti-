using Scalar.AspNetCore;
using TesteTecnicoEffecti.Src.Facades;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddDbContext<TesteTecnicoEffecti.Src.Data.ApplicationDbContext>();
builder.Services.AddScoped<TesteTecnicoEffecti.Src.Services.ILicitacaoService, TesteTecnicoEffecti.Src.Services.LicitacoesService>();
builder.Services.AddScoped<IConsultaLicitacoesFacade, ConsultaLicitacoesFacade>();

builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}
app.MapControllers();

app.UseHttpsRedirection();



app.Run();

