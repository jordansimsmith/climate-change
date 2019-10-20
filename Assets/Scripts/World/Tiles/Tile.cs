using System;
using HUD;
using UnityEngine;
using UnityEngine.UI;
using World.Entities;

namespace World.Tiles
{
    public class Tile : MonoBehaviour
    {
        [SerializeField]
        private TileType tileType;

        [SerializeField] 
        private Material originalMaterial;

        private Material highlightMaterial;
        private Text cost;
        private EntityPlacer placer;
        public Vector2Int Pos { get; set; }
        public TileType TileType
        {
            get => tileType;
            set => tileType = value;
        }

        public int ReclaimCost
        {
            get
            {
                if (TileType == TileType.Water)
                {
                    return 20000;
                }
                if (TileType == TileType.Sand)
                {
                    return 10000;
                }
                
                return Int32.MaxValue;
            }
        }

        private Entity entity;
        public Entity Entity
        {
            get => entity;
            set
            {
                if (entity != null)
                {
                    entity.Destruct();
                    Destroy(entity.gameObject);
                }

                if (value != null)
                {
                    value.transform.SetParent(gameObject.transform, false);
                    entity = value;
                    entity.Construct();
                }
                else
                {
                    entity = null;
                }
            }
        }

        public bool IsTileValid()
        {
            return TileType.Equals(TileType.Grass) && entity == null;
        }

        public static Vector3 Size { get; } = new Vector3(10f, 2.5f, 10f);

        public void OnMouseEnter()
        {
            if (highlightMaterial == null)
            {
                highlightMaterial = new Material(originalMaterial);
//                Debug.Log(highlightMaterial.color.ToString());
                highlightMaterial.EnableKeyword("_EMISSION");
                highlightMaterial.SetColor("_EMISSION", new Color(0.05f, 0.05f, 0.05f));
                highlightMaterial.globalIlluminationFlags = MaterialGlobalIlluminationFlags.RealtimeEmissive;
//                highlightMaterial.color = Color.blue;
            }

            var materials = GetComponent<MeshRenderer>().materials;
            materials[1] = highlightMaterial;
            GetComponent<MeshRenderer>().materials = materials;
            
            Debug.Log("Material changed");
            
            
            if (placer == null)
            {
                placer = FindObjectOfType<EntityPlacer>();
            }

            if (cost == null)
            {
                cost = GameObject.FindWithTag("ReclaimCost").GetComponent<Text>();
            }

            if (placer != null)
            {
                if (placer.Mode == EntityPlacerMode.RECLAIM)
                {
                    if (TileType == TileType.Sand || TileType == TileType.Water)
                    {
                        cost.text = "Cost: " + ReclaimCost;
                    }
                    else
                    {
                        ResetCostText();
                    }
                }
                else
                {
                    ResetCostText();
                }
            }
        }

        public void OnMouseExit()
        {
            ResetCostText();
//            var materials = GetComponent<MeshRenderer>().materials;
//            materials[1] = originalMaterial;
//            GetComponent<MeshRenderer>().materials = materials;
        }

        private void ResetCostText()
        {
            cost.text = "";
        }
    }
}