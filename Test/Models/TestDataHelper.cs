using Newtonsoft.Json;

namespace Spotabot.Test.Models;

[Obsolete(
    "Please move this to a shared library so I have a standard way to do this and stop repeating this since i forget how to do this every few months.")]
public static class TestDataHelper
{
    [Obsolete] private static string RootDirectory => AppDomain.CurrentDomain.BaseDirectory;

    private static string TestDataDirectory => "test_data";

    private static string GetTestFile(string name)
    {
        return Path.Join(RootDirectory, TestDataDirectory, name);
    }

    public static T GetTestFile<T>(string filename)
    {
        var completeName = GetTestFile(filename);
        var txt = File.ReadAllText(completeName);
        var games = JsonConvert.DeserializeObject<T>(txt);
        return games;
    }
}