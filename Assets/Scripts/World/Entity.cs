using UnityEngine;

namespace World
{
    public abstract class Entity : MonoBehaviour
    {
        public EntityType Type { get; set; }
    }
}