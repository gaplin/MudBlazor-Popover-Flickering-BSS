using Microsoft.AspNetCore.Http.Connections;
using MudBlazor.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddMudServices();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment()) {
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

// For simulating latency
app.Use(async (context, next) => {
    await Task.Delay(100);
    await next.Invoke();
});

// For simulating latency
app.MapBlazorHub(options => 
    options.Transports = HttpTransportType.LongPolling);

app.MapFallbackToPage("/_Host");

app.Run();
