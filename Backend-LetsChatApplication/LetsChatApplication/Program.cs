using LetsChatApplication.Hub;
using LetsChatApplication.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("chatCon");

builder.Services.AddCors(cors => cors.AddPolicy("myPolicy", builder =>{
    builder.AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin().AllowCredentials().WithOrigins("http://localhost:4200");
}));
builder.Services.AddSignalR();
builder.Services.AddDbContext<ChatContext>(db => db.UseSqlServer(connectionString));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();
app.UseHttpsRedirection();
app.UseCors("myPolicy");

app.UseEndpoints(endpoints =>
{
    
    endpoints.MapHub<BroadcastHub>("/notify");
});
app.UseAuthorization();

app.MapControllers();

app.Run();
