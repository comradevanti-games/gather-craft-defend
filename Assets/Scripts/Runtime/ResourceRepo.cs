using System;
using System.Collections.Immutable;
using ComradeVanti.CSharpTools;
using UnityEngine;

namespace GatherCraftDefend
{

    public class ResourceRepo<TId, TResource>
        where TResource : ScriptableObject
    {

        private readonly ImmutableDictionary<TId, TResource> resourcesById;


        private ResourceRepo(ImmutableDictionary<TId, TResource> resourcesById) =>
            this.resourcesById = resourcesById;


        public static ResourceRepo<TId, TResource> Load(string path, Func<TResource, TId> idSelector)
        {
            var resources = UnityEngine.Resources.LoadAll<TResource>(path);
            var resourcesById = resources.ToImmutableDictionary(idSelector);
            return new ResourceRepo<TId, TResource>(resourcesById);
        }


        public IOpt<TResource> TryGetById(TId id) =>
            resourcesById.TryGet(id);

    }

}