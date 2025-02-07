using WebApi.Services;
var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//API endpointleri i�in controller
builder.Services.AddControllers();

// HTTP istemcisi olu�turmak i�in
builder.Services.AddHttpClient();

//Token s�n�f� 
builder.Services.AddScoped<TokenService>();

var app = builder.Build();
app.MapControllers();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.Run();

