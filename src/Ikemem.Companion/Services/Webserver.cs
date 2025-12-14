using EmbedIO;
using EmbedIO.Actions;
using Microsoft.Extensions.Configuration;

namespace Spotabot.Companion.Services;

public class Webserver
{
    public Webserver(IConfiguration configuration)
    {
        _port = configuration["WebPort"];
        _address = configuration["WebAddress"];
    }
    private WebServer server;
    private readonly string _port;
    private readonly string _address;
    private string Url => _address + ":" + _port;
    //private readonly object _webpage;

    public async Task Start()
    {
        server = CreateWebServer();
        {
            await server.RunAsync();

        }
    }


    private WebServer CreateWebServer()
    {
        var server = new WebServer(o => o
                .WithUrlPrefix(Url)
                .WithMode(HttpListenerMode.EmbedIO))
            // First, we will configure our web server by adding Modules.
            .WithLocalSessionManager()
            //.WithWebApi("/milkdrop", m => m.WithController<MilkdropController>())
            //.WithModule(new WebSocketChatModule("/chat"))
            //.WithModule(new WebSocketTerminalModule("/terminal"))
            //.WithStaticFolder("/", HtmlRootPath, true, m => m.WithContentCaching(UseFileCache)) // Add static files after other modules to avoid conflicts
            .WithModule(new ActionModule("/", HttpVerbs.Any, ctx => ctx.SendDataAsync(new { Message = "Error" })));

        // Listen for state changes.
        //server.StateChanged += (s, e) => $"WebServer New State - {e.NewState}".Info();

        return server;
    }
}