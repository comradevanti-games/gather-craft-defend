using System.Collections.Immutable;
using ComradeVanti.CSharpTools;

namespace GatherCraftDefend
{

    public class EnemyTypeRepo
    {

        private const string ResourcePath = "EnemyTypes";

        private readonly ImmutableDictionary<string, EnemyType> typesByName;


        private EnemyTypeRepo(ImmutableDictionary<string, EnemyType> typesByName) =>
            this.typesByName = typesByName;


        public static EnemyTypeRepo LoadFromResources()
        {
            var types = UnityEngine.Resources.LoadAll<EnemyType>(ResourcePath);
            var typesByName = types.ToImmutableDictionary(it => it.name);
            return new EnemyTypeRepo(typesByName);
        }


        public IOpt<EnemyType> TryGetByName(string typeName) =>
            typesByName.TryGet(typeName);

    }

}