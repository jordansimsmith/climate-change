using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using World;
using World.Resource;
using World.Tiles;
using Random = UnityEngine.Random;

namespace NaturalDisasters
{
    public class NaturalDisasterDispatcher : MonoBehaviour
    {
        public ResourceSingleton resources;
        public GameObject tornadoPrefab;
        public GameObject smokePrefab;
        public float tileConversionDelay;
        public NaturalDisasterPanelController naturalDisasterPanelController;

        private GameObject activeTornado;
        private GameBoard board;

        private void Start()
        {
            //Every 10 Seconds check if we should dispatch an event
            board = FindObjectOfType<GameBoard>();
            if (naturalDisasterPanelController)
            {
                InvokeRepeating("DisasterEventDispatcher", 10f, 10f);

                naturalDisasterPanelController.Hide();
            }
        }
   
        private void DisasterEventDispatcher()
        {
            // If environment drops below 100, i.e. relatively few trees to factories (you start w +300 env thanks trees).  
            var envScore = resources.totalSupply.environment - resources.totalDemand.environment;
            if (envScore < 100)
            {
                // roll dice 10% to have the natural event
                if (Random.Range(0, 10) == 0)
                {
                    switch (Random.Range(0, 5))
                    {
                        case 0:
                            DoSeaLevelRise();
                            return;
                        case 1:
                            DoCyclone();
                            return;
                        case 2:
                            DoCyclone();
                            return;
                        case 3:
                            DoDrought();
                            return;
                        case 4:
                            DoDrought();
                            return;
                        default:
                            return;
                    }
                }
            }

            //generate Rng Event for the subscribe
        }

        public void DoCyclone()
        {
            if (activeTornado != null)
            {
                return;
            }

            activeTornado = Instantiate(tornadoPrefab);
            naturalDisasterPanelController.Show("Cyclone hits your island!",
                "A climate change caused cyclone ravages your island, destroying buildings in its wake.");
        }

        public void DoDrought()
        {
            naturalDisasterPanelController.Show("There has been a drought!",
                "Lush fields of grass have dried out into barren desert wastelands.");
            int tilesToConvert = Random.Range(2, 6);
            ConvertTilesRandomly(TileType.Grass, TileType.Sand, tilesToConvert);
        }

        public void DoSeaLevelRise()
        {
            naturalDisasterPanelController.Show("Sea levels have risen",
                "Melting ice caps have caused the sea to erode your island's outermost regions");

            int tilesToConvert = Random.Range(2, 6);
            ConvertTilesRandomly(TileType.Sand, TileType.Water, tilesToConvert);
        }

        private void ConvertTilesRandomly(TileType startType, TileType endType, int numberOfTiles)
        {
            HashSet<Tuple<int, int>> tiles = new HashSet<Tuple<int, int>>();

            while (numberOfTiles > 0)
            {
                Tuple<int, int> tilePos = board.GetRandomTilePosition(startType);
                numberOfTiles--; // avoids case where there are no tiles of type "from" on board.

                if (tiles.Contains(tilePos))
                {
                    continue;
                }

                Tile tile = board.Tiles[tilePos.Item1, tilePos.Item2];
                Instantiate(smokePrefab, tile.gameObject.transform, false);
                tiles.Add(tilePos);
            }

            StartCoroutine(ConvertTilesToType(tiles, endType, tileConversionDelay));
        }

        private IEnumerator ConvertTilesToType(HashSet<Tuple<int, int>> tiles, TileType conversionType,
            float delayBeforeConversion)
        {
            yield return new WaitForSeconds(delayBeforeConversion);

            foreach (Tuple<int, int> tile in tiles)
            {
                Tile oldTile = board.Tiles[tile.Item1, tile.Item2];
                Destroy(oldTile.gameObject);
                board.CreateTileAt(tile.Item1, tile.Item2, conversionType);
            }

            // rebake nav mesh since underlying tiles have changed 
            board.RebakeNavMesh();
        }
        
        

    }
}