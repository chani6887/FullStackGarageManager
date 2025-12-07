using MongoDB.Driver;
using WorkshopManagement.Api.Services;

var builder = WebApplication.CreateBuilder(args);

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";


var mongoConnectionString = builder.Configuration.GetConnectionString("MongoConnection");
var databaseName = "WorkshopsDB";
builder.Services.AddSingleton<IMongoClient>(sp => new MongoClient(mongoConnectionString));


builder.Services.AddMemoryCache();

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
        policy =>
        {
            policy.WithOrigins("http://localhost:4200")
                  .AllowAnyHeader() 
                  .AllowAnyMethod(); 
        });
});
builder.Services.AddScoped<IWorkshopRepository, MongoWorkshopRepository>(sp =>
{
    var client = sp.GetRequiredService<IMongoClient>();
    return new MongoWorkshopRepository(client, databaseName);
});

builder.Services.AddHttpClient();


builder.Services.AddHostedService<ApiDataLoadService>();



builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


app.UseCors(MyAllowSpecificOrigins);

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();

    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Workshops API V1");
        c.RoutePrefix = string.Empty;
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.MapControllers();

app.Run();