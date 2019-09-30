using UnityEngine;

namespace World
{
    [CreateAssetMenu]
    public class EntityFactory : ScriptableObject
    {
        [SerializeField] private ForestEntity forestPrefab;

        public Entity Get(EntityType entityType)
        {
            switch (entityType)
            {
                case EntityType.Forest: return Instantiate(forestPrefab);
                default:
                    Debug.Log("Unknown entity type");
                    return null;
            }
        }
    }
}