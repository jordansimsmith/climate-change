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

        private void Start()
        {
            tilePlane = new Plane(Vector3.up, 0);
        }

        public void spawn(EntityType type)
        {
            if (enabled)
            {
                return;
            }

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
                    Debug.DrawLine(Camera.main.transform.position, hitInfo.point, Color.red);
                    GameObject gameTile = hitInfo.collider.gameObject;
                    buildingTransform.SetParent(gameTile.transform);
                    buildingTransform.localPosition = Vector3.zero;
                    if (Input.GetMouseButtonDown(0))
                    {
                        Tile tile = gameTile.GetComponent<Tile>();
                        if (tile.TileType.Equals(TileType.Grass)
                            && tile.Entity == null
                            && resources.Money >= entity.Stats.cost)
                        {
                            tile.Entity = entity;
                            enabled = false;
                        }
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
            else
            {
                // Remove target object if clicked
                if (Physics.Raycast(ray, out var hitInfo, Mathf.Infinity, 1 << 10))
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        enabled = false;
                        GameObject gameTile = hitInfo.collider.gameObject;
                        Tile tile = gameTile.GetComponent<Tile>();
                        if (tile.Entity != null && tile.Entity.Type != EntityType.TownHall)
                        {
                            tile.Entity = null;
                        }
                    }
                }
            }
        }

        public void remove()
        {
            entity = null;
            enabled = true;
        }
    }
}