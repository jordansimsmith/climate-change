using UnityEngine;

namespace World
{
  [CreateAssetMenu]
  public class EntityFactory : ScriptableObject
  {
    [SerializeField] private ForestEntity forestPrefab;
    [SerializeField] private HouseEntity housePrefab;
    [SerializeField] private TownHallEntity townHallPreFab;
    [SerializeField] private FactoryEntity factoryEntity;
    [SerializeField] private FarmEntity farmEntity;

    [SerializeField] private PowerStationEntity powerStationEntity;
    public Entity Get(EntityType entityType)
    {
      switch (entityType)
      {
        case EntityType.Forest: return Instantiate(forestPrefab);
        case EntityType.House: return Instantiate(housePrefab);
        case EntityType.TownHall: return Instantiate(townHallPreFab);
        case EntityType.Factory: return Instantiate(factoryEntity);
        case EntityType.Farm: return Instantiate(farmEntity);
        case EntityType.PowerStation: return Instantiate(powerStationEntity);
        default:
          Debug.Log("Unknown entity type");
          return null;
      }
    }
  }
}