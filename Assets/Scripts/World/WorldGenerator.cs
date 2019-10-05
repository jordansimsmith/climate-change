using UnityEngine;
using World.Tiles;

namespace World
{
    public class WorldGenerator : MonoBehaviour
    {
        [SerializeField] private float generationOffset;
        [SerializeField] private float generationFidelity;
        [SerializeField] private float forestFidelity;

        // Randomly generates the layout of the tile map 
        /// <param name="length">The side length of the square tile map</param>
        public TileType[,] Generate(int length)
        {
            TileType[,] worldArray = new TileType[length, length];
            generationOffset += Random.Range(0, 100);

            for (int x = 0; x < length; x += 1)
            {
                for (int z = 0; z < length; z += 1)
                {
                    var centerRelOffsetX = Mathf.Abs((float) (x - length / 2) / length);
                    var centerRelOffsetZ = Mathf.Abs((float) (z - length / 2) / length);

                    var centerRelOffset = Mathf.Sqrt(Mathf.Pow(centerRelOffsetX, 2) + Mathf.Pow(centerRelOffsetZ, 2));

                    var num = Mathf.PerlinNoise(generationOffset + x * generationFidelity,
                        generationOffset + z * generationFidelity);

                    num += (float) 0.5;
                    num -= centerRelOffset * 1.5f;

                    worldArray[x, z] = num > 0.4 ? TileType.Grass : TileType.Water;
                }
            }


            // Add mountains
            int bound = length / 6;
            int mCount = 0;
            for (int x = 2 * bound; x <= 4 * bound; x++)
            {
                for (int z = 2 * bound; z <= 4 * bound; z++)
                {
                    if (mCount >= 3) break;
                    if (worldArray[x, z] != TileType.Grass || Random.value <= 0.90f) continue;
                    worldArray[x, z] = TileType.Mountain;
                    mCount++;
                }
            }

            // Add water borders
            for (int x = 0; x < length; x++)
            {
                for (int z = 0; z < length; z++)
                {
                    if (x == 0 || z == 0 || x == length - 1 || z == length - 1)
                    {
                        worldArray[x, z] = TileType.Water;
                    }
                }
            }

            // Add beaches
            for (int x = 0; x < length; x++)
            {
                for (int z = 0; z < length; z++)
                {
                    if (worldArray[x, z] != TileType.Grass)
                    {
                        continue;
                    }

                    if (z < length - 1 && worldArray[x, z + 1] == TileType.Water)
                    {
                        worldArray[x, z] = TileType.Sand;
                    }

                    if (z > 0 && worldArray[x, z - 1] == TileType.Water)
                    {
                        worldArray[x, z] = TileType.Sand;
                    }

                    if (x < length - 1 && worldArray[x + 1, z] == TileType.Water)
                    {
                        worldArray[x, z] = TileType.Sand;
                    }

                    if (x > 0 && worldArray[x - 1, z] == TileType.Water)
                    {
                        worldArray[x, z] = TileType.Sand;
                    }
                }
            }

            return worldArray;
        }
    }
}