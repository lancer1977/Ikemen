using System.Reactive.Linq;

namespace Spotabot.Test.Models;

[TestFixture]
public class AsyncEventTests
{
    [Test]
    public async Task TestAsync()
    {
        var source = new EventSource();

        // Create an observable sequence from the asynchronous event handler
        var observable = Observable.FromEventPattern<EventHandler, EventArgs>(
            handler => source.MyEvent += handler,
            handler => source.MyEvent -= handler
        );

        // Subscribe to the observable sequence
        observable.Subscribe(ep => Console.WriteLine("Event occurred"));

        // Trigger the asynchronous event
        await source.TriggerEventAsync();
    }
}