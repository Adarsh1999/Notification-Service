using NotificationConsumer.Services;
using NotificationConsumer.Hubs;
using Microsoft.Extensions.Logging;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle


builder.Services.AddControllersWithViews();
builder.Services.AddSignalR();


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHostedService<ConsumerService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapHub<ChatHub>("/chathub");
app.MapControllers();

app.Lifetime.ApplicationStarted.Register(() =>
{
	var logger = app.Services.GetRequiredService<ILogger<Program>>();
	logger.LogInformation("Application has started.");
});

app.Lifetime.ApplicationStopping.Register(() =>
{
	var logger = app.Services.GetRequiredService<ILogger<Program>>();
	logger.LogInformation("Application is stopping.");
});

app.Lifetime.ApplicationStopped.Register(() =>
{
	var logger = app.Services.GetRequiredService<ILogger<Program>>();
	logger.LogInformation("Application has stopped.");
});

app.Run();
