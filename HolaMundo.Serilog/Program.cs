using Serilog;
using Serilog.Events;
using Serilog.Sinks.MSSqlServer;
using Serilog.Sinks.MSSqlServer.Sinks.MSSqlServer.Options;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//Serilog
//Consola y archivo
//var logger = new LoggerConfiguration()
//                .MinimumLevel.Debug()
//                .WriteTo.Console(LogEventLevel.Information)
//                .WriteTo.File("log.txt", LogEventLevel.Fatal) //Con esta línea configuramos la salida a fichero
//                .CreateLogger();


//Db
//var connectionString = "Data Source=192.168.1.86; Initial Catalog=Serilog; Persist Security Info=True;User ID=sa;Password=Macross#2012; TrustServerCertificate=True;";
//var sqlLoggerOptions = new MSSqlServerSinkOptions
//{
//    AutoCreateSqlTable = true,
//    SchemaName = "dbo",
//    TableName = "Log",
//    BatchPostingLimit = 1
//};

//var logger = new LoggerConfiguration()
//    .MinimumLevel.Debug()
//    .WriteTo.Console(LogEventLevel.Information)
//    .WriteTo.File("log.txt", LogEventLevel.Fatal, rollingInterval: RollingInterval.Day, retainedFileCountLimit: 10)
//    .WriteTo.MSSqlServer(connectionString, sqlLoggerOptions, null, null, LogEventLevel.Information) //Aquí se añade el Sink
//                .CreateLogger();


//Desde configuracion
//var configuration = new ConfigurationBuilder()
//                .AddJsonFile("appsettings.json")
//                .Build();

//var logger = new LoggerConfiguration()
//                .ReadFrom.Configuration(configuration)
//                .CreateLogger();
//builder.Host.UseSerilog();

//logger.Verbose("Mensaje Verbose");
//logger.Debug("Mensaje Debug");
//logger.Information("Mensaje Information");
//logger.Warning("Mensaje Warning");
//logger.Error("Mensaje Error");
//logger.Fatal("Mensaje Fatal");

//incluir en el _logger del controller
builder.Host.UseSerilog((hostContext, services, configuration) =>
{
    configuration.ReadFrom.Configuration(hostContext.Configuration);
});

//Serilog

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


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
