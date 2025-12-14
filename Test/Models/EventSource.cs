namespace Spotabot.Test.Models;

internal class EventSource
{
    // Define an asynchronous event
    public event EventHandler MyEvent;

    // Method to trigger the asynchronous event
    public async Task TriggerEventAsync()
    {
        await Task.Delay(1000); // Simulate some asynchronous operation
        MyEvent?.Invoke(this, EventArgs.Empty);
    }
}