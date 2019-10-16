using HUD;
using UnityEngine;
using World.Entities;
using World.Resource;
using World.Tiles;

namespace World
{
    public class EntityPlacer : MonoBehaviour
    {
        [SerializeField] private EntityFactory factory;
        [SerializeField] private ResourceSingleton resources;
        private Entity entity;
        private Plane tilePlane;
        private EntityPlacerMode mode;
        private GameBoard board;

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
                    buildingTransform.SetParent(gameTile.transform);
                    buildingTransform.localPosition = Vector3.zero;
                    if (Input.GetMouseButtonDown(0))
                    {
                        PlaceEntityIfValid(gameTile);
                    }
                }
                else
                {
                    tilePlane.Raycast(ray, out var enter);
                    Vector3 hitPoint = ray.GetPoint(enter);
                    buildingTransform.parent = null;
                    buildingTransform.position = hitPoint;
                }
            }
            else if (Mode == EntityPlacerMode.DELETE)
            {
                // Remove target object if clicked
                if (Physics.Raycast(ray, out var hitInfo, Mathf.Infinity, 1 << 10))
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        GameObject gameTile = hitInfo.collider.gameObject;
                        DeleteEntityifValid(gameTile);
                    }
                }
            }
            else if (Mode == EntityPlacerMode.RECLAIM)
            {
                // reclaim code 
                if (Physics.Raycast(ray, out var hitInfo, Mathf.Infinity, 1 << 10))
                {
                    if (Input.GetMouseButton(0))
                    {
                        GameObject gameTile = hitInfo.collider.gameObject;
                        Tile tile = gameTile.GetComponent<Tile>();
                        if (tile == null)
                        {
                            Debug.Log("tile is null");
                        }
                        if (tile.Entity == null)
                        {
                            int reclaimCost = tile.ReclaimCost;
                            if (tile.TileType == TileType.Grass)
                            {   
                                Debug.Log("grass");
                            }
                            else
                            {
                                Debug.Log("something else");
                            }
                            if (resources.Money >= reclaimCost)
                            {
                                Debug.Log("bruh");
                                resources.Money -= reclaimCost;
                                board.ReclaimTile(tile);
                            }
                            
                        }
                       
                    }
                }
            }
        }
        
        private void PlaceEntityIfValid(GameObject gameTile) {
            Tile tile = gameTile.GetComponent<Tile>();
            if (tile.IsTileValid() && resources.Money >= entity.Stats.cost)
            {
                tile.Entity = entity;
                enabled = false;
                
                if (entity.Type == EntityType.TownHall)
                {
                    //GameObject.Find("TownHall").SetActive(false);
                    FindObjectOfType<ShopScrollList>().DisableTownHall();
                }

                entity = null;
            }
            
    
        }

        private void DeleteEntityifValid(GameObject gameTile)
        {
            Tile tile = gameTile.GetComponent<Tile>();
            if (tile.Entity != null && tile.Entity.Type != EntityType.TownHall)
            {
                tile.Entity = null;
            }
            
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