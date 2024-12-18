using CleanArchMvc.Infra.Ioc;

var builder = WebApplication.CreateBuilder(args);

// Configuração de logging para ajudar no diagnóstico de autenticação
builder.Services.AddLogging(logging =>
{
    logging.AddConsole();
    logging.SetMinimumLevel(LogLevel.Debug);
});

// Criar um logger de teste para verificar se o log funciona
var logger = builder.Logging.Services.BuildServiceProvider().GetRequiredService<ILogger<Program>>();
logger.LogDebug("O sistema de logging foi configurado com sucesso.");

builder.Services.AddInfrastructureAPI(builder.Configuration);
builder.Services.AddInfraStructureJWT(builder.Configuration);
builder.Services.AddInfrastructureSwagger();


// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddAuthorization();
builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStatusCodePages();

// Configure o pipeline para usar os controladores.
app.UseRouting();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers().RequireAuthorization();


app.Run();