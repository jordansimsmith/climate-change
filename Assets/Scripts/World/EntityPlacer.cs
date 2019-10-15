using Audio;
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
        private bool deleteMode;
        private EntitySideBarController sideBarController;

        public bool DeleteMode
        {
            get => deleteMode;
            set
            {
                if (entity != null)
                {
                    DestroyCurrentEntity();
                }

                deleteMode = value;
                if (value)
                {
                    entity = null;
                    enabled = true;
                }
                else
                {
                    enabled = false;
                }
            }
        }

        private void Start()
        {
            tilePlane = new Plane(Vector3.up, 0);
            sideBarController = FindObjectOfType<EntitySideBarController>();
        }

        public void Spawn(EntityType type)
        {
            if (entity != null)
            {
                DestroyCurrentEntity();
            }

            DeleteMode = false;
            entity = factory.Get(type);
            enabled = true;
        }


        private void Update()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (entity != null)
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
                        PlaceEntityIfValid(gameTile);
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
            else if (DeleteMode)
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
        }

        private bool EntityCanBePlacedOn(Tile tile)
        {
            return tile.TileType.Equals(TileType.Grass)
                   && tile.Entity == null
                   && resources.Money >= entity.Stats.cost;
        }

        private void PlaceEntityIfValid(GameObject gameTile)
        {
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

                entity.HideOutline();
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