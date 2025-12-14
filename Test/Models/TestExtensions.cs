using System.Diagnostics;
using GamePlayer.Core.Models;
using Newtonsoft.Json;
using PolyhydraGames.Core.Interfaces.Gaming;

namespace Spotabot.Test.Models
{
    public static class TestExtensions
    {
        private static List<BatoceraGame>? _randomGames;

        private static string RootDirectory => AppDomain.CurrentDomain.BaseDirectory;
        private static string TestDataDirectory => "test_data";
        public static IEnumerable<IGameUpdater> Updaters { get; set; }

        public static string GetTestFile(string name)
        {
            return Path.Join(RootDirectory, TestDataDirectory, name);
        }

        public static T Get<T>(string filename)
        {
            var completeName = GetTestFile(filename);
            Debug.WriteLine(completeName);
            var txt = File.ReadAllText(completeName);
            var games = JsonConvert.DeserializeObject<T>(txt);
            return games;
        }
    }
}