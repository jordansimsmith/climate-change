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

        public static bool highlightEnabled = true;
        
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

        public void ShowHighlight(Color color) {
            // Swap out the material for a temporary material of a different color
            if (highlightMaterial == null) {
                highlightMaterial = new Material(originalMaterial);
            }
            highlightMaterial.color = color;
            var meshRenderer = GetComponent<MeshRenderer>();
            var materials = meshRenderer.materials;
            materials[1] = highlightMaterial;
            meshRenderer.materials = materials;
        }


        public void ShowHighlight() {
            if (placer == null)
            {
                placer = FindObjectOfType<EntityPlacer>();
            }
            if (placer.Mode == EntityPlacerMode.DELETE) {
                if (placer.EntityCanBeDeleted(this)) {
                    ShowHighlight(new Color(0.9f, 0.8f, 0.17f));
                }
            }
            else if (placer.Mode == EntityPlacerMode.RECLAIM) {
                if (placer.TileCanBeReclaimed(this)) {
                    ShowHighlight(new Color(0.35f, 0.77f, 0.39f));
                }
                else {
                    ShowHighlight(new Color(0.77f, 0.27f, 0.23f));
                }
            }
            else {
                var ogColor = originalMaterial.color;
                var color = TileType is TileType.Sand
                    ? new Color(ogColor.r * 0.9f, ogColor.g * 0.9f, ogColor.b * 0.9f)
                    : new Color(ogColor.r * 1.5f, ogColor.g * 1.5f, ogColor.b * 1.5f);
                ShowHighlight(color);
            }
        }


        public void HideHighlight() {
            // Restores the original material
            var meshRenderer = GetComponent<MeshRenderer>();
            var materials = meshRenderer.materials;
            materials[1] = originalMaterial;
            meshRenderer.materials = materials;
        }
        
        public void OnMouseEnter()
        {
            if (highlightEnabled)
            {
                ShowHighlight();
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
            HideHighlight();
        }

        private void ResetCostText()
        {
            cost.text = "";
        }
    }
}