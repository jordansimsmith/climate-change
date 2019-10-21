using Audio;
using UnityEngine;
using World.Entities;
using World.Resource;
using World.Tiles;

namespace World
{
    /// <summary>
    /// EntityPlacer provides some interactions with the game board. It has four
    /// different modes: NONE, DELETE, BUILD, RECLAIM.
    ///
    /// BUILD and RECLAIM uses ray cast to select tiles to build entities and
    /// to reclaim tiles
    /// </summary>
    public class EntityPlacer : MonoBehaviour
    {
        [SerializeField] private EntityFactory factory;
        [SerializeField] private ResourceSingleton resources;
        private Entity entity;
        private Plane tilePlane;
        private EntityPlacerMode mode = EntityPlacerMode.NONE;
        private GameBoard board;

        // Stores the mode of the EntityPlacer
        public EntityPlacerMode Mode
        {
            get => mode;
            set
            {
                switch (value)
                {
                    case EntityPlacerMode.DELETE:
                        if (entity != null)
                        {
                            DestroyCurrentEntity();
                        }

                        mode = EntityPlacerMode.DELETE;
                        
                        // clear entity if there is currently one 
                        entity = null;
                        enabled = true;
                        break;
                    
                    case EntityPlacerMode.NONE:
                        mode = EntityPlacerMode.NONE;
                        enabled = false;
                        break;
                    
                    case EntityPlacerMode.BUILD:
                        mode = EntityPlacerMode.BUILD;
                        enabled = true;
                        break;
                    
                    case EntityPlacerMode.RECLAIM:
                        mode = EntityPlacerMode.RECLAIM;
                        enabled = true;
                        break;
                }
            }
        }
        private void Start()
        {
            tilePlane = new Plane(Vector3.up, 0);
            board = GetComponentInChildren<GameBoard>();
        }

        public void Spawn(EntityType type)
        {
            if (entity != null)
            {
                DestroyCurrentEntity();
            }
            
            Mode = EntityPlacerMode.BUILD;
            entity = factory.Get(type);
            enabled = true;
        }


        private void Update()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Mode == EntityPlacerMode.BUILD)
            {
                // Places object at mouse, deposits object on grid when clicked
                Transform buildingTransform = entity.transform;
                if (Physics.Raycast(ray, out var hitInfo, Mathf.Infinity, 1 << 10))
                {
                    GameObject gameTile = hitInfo.collider.gameObject;
                    Tile tile = gameTile.GetComponent<Tile>();

                    buildingTransform.SetParent(gameTile.transform);
                    buildingTransform.localPosition = Vector3.zero;

                    var canBePlaced = EntityCanBePlacedOn(tile);
                    entity.ShowOutline(canBePlaced);
                    if (Input.GetMouseButtonDown(0) && canBePlaced)
                    {
                        tile.Entity = entity;
                        enabled = false;
                        entity.HideOutline();
                        entity = null;
                        Mode = EntityPlacerMode.NONE;
                        AudioManager.Instance.Play(gameTile);
                    }
                }
                else
                {
                    tilePlane.Raycast(ray, out var enter);
                    Vector3 hitPoint = ray.GetPoint(enter);
                    buildingTransform.parent = null;
                    buildingTransform.position = hitPoint;
                    entity.HideOutline();
                }

            }
            else if (Mode == EntityPlacerMode.RECLAIM)
            {
                //  Reclaims a tile on click
                if (Physics.Raycast(ray, out var hitInfo, Mathf.Infinity, 1 << 10))
                {
                    if (Input.GetMouseButton(0))
                    {
                        GameObject gameTile = hitInfo.collider.gameObject;
                        Tile tile = gameTile.GetComponent<Tile>();
                        if (TileCanBeReclaimed(tile)) {
                            int reclaimCost = tile.ReclaimCost;
                            if (resources.Money >= reclaimCost) {
                                resources.Money -= reclaimCost;
                                board.ReclaimTile(tile);
                            }
                        } 
                    }
                }
            }
        }

        private bool EntityCanBePlacedOn(Tile tile)
        {
            return tile.IsTileValid() && resources.Money >= entity.Stats.cost;
        }

        public bool TileCanBeReclaimed(Tile tile)
        {
            return tile.Entity == null && board.GetNumberOfAdjacentGrassTiles(tile) >= 2;
        }

        public bool EntityCanBeDeleted(Tile tile)
        {
            return tile.Entity != null && tile.Entity.Type != EntityType.TownHall;
        }

        private void DestroyCurrentEntity()
        {
            if (entity != null)
            {
                Destroy(entity.transform.gameObject);
            }
        }
    }
}