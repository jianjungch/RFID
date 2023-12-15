using Microsoft.AspNetCore.Hosting;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);

//20231215 append for  CORS    test result :fail
//builder.Services.AddCors(options =>
//{
//    options.AddPolicy("MyCorsPolicy", policy =>
//    {
//        policy.WithOrigins("https://rfid-ccu-group8.azurewebsites.net")
//              .AllowAnyHeader()
//              .AllowAnyMethod();
//    });
//});


//20231215 append for  CORS 
builder.Services.AddCors(options =>
{
    options.AddPolicy("MyCorsPolicy", policy =>
    {
        policy.WithOrigins("http://localhost:3000","https://rfid-ccu-group8.azurewebsites.net", "https://rfid-ccu-group8.azurewebsites.net/api/RFID/", "https://rfid-ccu-group8.azurewebsites.net/api/RFID/TimeAlarm")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});



//20231215 append for  CORS   test result :Pass
//builder.Services.AddCors(options =>
//{
//    options.AddPolicy("MyCorsPolicy",
//        builder => builder.AllowAnyOrigin()
//                          .AllowAnyMethod()
//                          .AllowAnyHeader());
//});




builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


// ¨Ï¥ÎCORS
app.UseCors("MyCorsPolicy");

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


