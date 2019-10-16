using System;
using World.Tiles;

namespace Persistence.Serializables
{
    [Serializable]
    public class SerializableTile
    {
        public SerializableTile()
        {
        }

        public SerializableTile(Tile tile)
        {

            TileType = tile.TileType;
            if (tile.Entity != null)
            {
                Entity = new SerializableEntity(tile.Entity);
            }
        }
            
        public TileType TileType { get; set; }
        public SerializableEntity Entity { get; set; }
            
    }
}