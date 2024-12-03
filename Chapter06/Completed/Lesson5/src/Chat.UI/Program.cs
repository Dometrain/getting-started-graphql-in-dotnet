using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using eShop.Chat.UI;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services
    .AddCatalogClient()
    .ConfigureHttpClient(client =>
    {
        client.BaseAddress = new Uri("http://localhost:5224/graphql");
        client.DefaultRequestHeaders.Add("userId", UserContext.UserId);
    })
    .ConfigureWebSocketClient(client =>
    {
        client.Uri = new Uri("ws://localhost:5224/graphql");
    });

await builder.Build().RunAsync();
