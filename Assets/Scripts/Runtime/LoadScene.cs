using System;
using System.Collections.Immutable;
using System.Linq;
using ComradeVanti.CSharpTools;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GatherCraftDefend
{

    public class LoadScene : MonoBehaviour
    {

        private const string PlayerPrefKey = "SceneLoadParams";

        public static SceneLoadParams Parameters => LoadFromPlayerPrefs();
        

        public void WithName(string sceneName) =>
            WithName(sceneName, SceneLoadParams.empty);

        public void WithName(string sceneName, SceneLoadParams parameters)
        {
            WriteToPlayerPrefs(parameters);
            SceneManager.LoadScene(sceneName);
        }

        private static void WriteToPlayerPrefs(SceneLoadParams parameters)
        {
            var entryStrings =
                parameters.keyValuePairs
                          .Select(entry => $"{entry.Key}={entry.Value}");
            var pref = string.Join(",", entryStrings);
            PlayerPrefs.SetString(PlayerPrefKey, pref);
        }

        private static SceneLoadParams LoadFromPlayerPrefs()
        {
            var pref = PlayerPrefs.GetString(PlayerPrefKey);
            var entryStrings = pref.Split(",", StringSplitOptions.RemoveEmptyEntries);

            SceneLoadParams AddEntry(SceneLoadParams parameters, string entryString)
            {
                var parts = entryString.Split("=");
                var key = parts[0];
                var value = parts[1];

                return parameters.AddEntry(key, value);
            }

            return entryStrings.Aggregate(SceneLoadParams.empty, AddEntry);
        }

    }


    public class SceneLoadParams
    {

        public static readonly SceneLoadParams empty =
            new SceneLoadParams(ImmutableDictionary<string, string>.Empty);

        public readonly ImmutableDictionary<string, string> keyValuePairs;


        private SceneLoadParams(ImmutableDictionary<string, string> keyValuePairs) =>
            this.keyValuePairs = keyValuePairs;


        public SceneLoadParams AddEntry(string key, string value) =>
            new SceneLoadParams(keyValuePairs.Add(key, value));

        private SceneLoadParams AddValue(string name, string type, string value) =>
            AddEntry($"{type}:{name}", value);

        public SceneLoadParams AddInt(string name, int i) =>
            AddValue(name, "int", i.ToString());

        public IOpt<int> TryGetInt(string name)
        {
            var key = $"int:{name}";
            return keyValuePairs.TryGet(key).Map(int.Parse);
        }

    }

}