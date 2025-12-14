// See https://aka.ms/new-console-template for more information

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PolyhydraGames.Core.Interfaces;
using Spotabot.Companion.Services;
using Spotabot.Services.Site;
using Spotabot.Setup;
using AppDomain = System.AppDomain;
using Directory = System.IO.Directory;

Console.WriteLine("Hello, World!");
// See https://aka.ms/new-console-template for more information


Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);



var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        services.AddConsoleHttpClient()
            .AddTwitch(hostContext.Configuration)
            .AddDiscord(hostContext.Configuration)
            .AddMusic()
            // .AddGeneral(hostContext.Configuration)
            .AddChannelManagerServices()
            .AddConfig();
        //services.AddSingleton<CompanionPortListener>();
        //services.AddSingleton<KeyboardParser>();
        services.AddSingleton<Webserver>();
        services.AddSingleton<IWebsiteRequestor, WebsiteLauncher>();


        //services.AddSingleton<IInputSimulator, InputSimulator>();
        //hostContext.Configuration.Bind("Seq", services);        
        //services.AddLogging(x =>
        //{
        //    x.AddConsole();
        //    x.AddSeq(hostContext.Configuration.GetSection("Seq"));
        //});

    }).Build();



var scope = host.Services;
//var listener = scope.GetRequiredService<CompanionPortListener>();
//await listener.Start();
var web = scope.GetRequiredService<Webserver>();
await web.Start();


await host.RunAsync();
