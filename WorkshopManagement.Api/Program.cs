var builder = WebApplication.CreateBuilder(args);

// bind settings
builder.Services.Configure<WorkshopManagement.Api.Models.MongoDBSettings>(
    builder.Configuration.GetSection("MongoDb"));

// register services
builder.Services.AddSingleton<WorkshopManagement.Api.Services.GarageService>();
builder.Services.AddHttpClient<WorkshopManagement.Api.Services.ExternalGarageService>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
