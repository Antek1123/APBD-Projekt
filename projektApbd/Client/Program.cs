using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using projektApbd.Client;
using projektApbd.Client.Services;
using Syncfusion.Blazor;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ICompanyService, CompanyService>();
builder.Services.AddScoped<IHttpService, HttpService>();
builder.Services.AddScoped<ILocalStorageService, LocalStorageService>();

//blazor
Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NjYyMzE4QDMyMzAyZTMxMmUzMGcyTGJML3hiSjB2cmFEWmRXQUVSVlh1UlVWYWt2REhKQlJLUTRpYk12Tms9");
builder.Services.AddSyncfusionBlazor();

var host = builder.Build();
var authenticatinService = host.Services.GetRequiredService<IAuthenticationService>();
await authenticatinService.Initialize();

await builder.Build().RunAsync();
