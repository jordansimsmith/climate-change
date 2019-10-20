using Audio;
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
            else if (Mode == EntityPlacerMode.DELETE)
            {
                // Remove target object if clicked
                if (Physics.Raycast(ray, out var hitInfo, Mathf.Infinity, 1 << 10))
                {
                    GameObject deleteTile = hitInfo.collider.gameObject;
                    Tile tile = deleteTile.GetComponent<Tile>();
                    var canBeDeleted = EntityCanBeDeleted(tile);
                    if (Input.GetMouseButtonDown(0) && canBeDeleted) {
                        tile.Entity = null;
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

//        private void PlaceEntityIfValid(GameObject gameTile) {
//            Tile tile = gameTile.GetComponent<Tile>();
//            if (tile.IsTileValid() && resources.Money >= entity.Stats.cost)
//            {
//                tile.Entity = entity;
//                enabled = false;
//                entity.HideOutline();
//                entity = null;
//            }
//        }


        public bool TileCanBeReclaimed(Tile tile) {
            return tile.Entity == null && board.GetNumberOfAdjacentGrassTiles(tile) >= 2;
            
//            
//            if (tile.Entity == null) {
//                if (board.GetNumberOfAdjacentGrassTiles(tile) >= 2) {
//                    int reclaimCost = tile.ReclaimCost;
//                    if (resources.Money >= reclaimCost) {
//                        resources.Money -= reclaimCost;
//                        board.ReclaimTile(tile);
//                    }
//                }
//                else {
//                    Debug.Log("not enough adjacent grass tiles");
//                }
//            }
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