using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using World;
using World.Entities;
using World.Tiles;

public class TornadoMovement : MonoBehaviour
{
    [SerializeField, DefaultValue(15)] public int speed;

    public float movementPeriod;
    public int periodsTillDeath;

    private GameBoard board;
    private Vector3 targetPosition;
    private int periodsElapsed = -1; // Will be incremented to zero on Start()

    private ParticleSystem masterParticleSystem;

    // Start is called before the first frame update
    private void Start()
    {
        board = FindObjectOfType<GameBoard>();
        InvokeRepeating("RefreshTargetPosition", 0, movementPeriod);
        masterParticleSystem = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
        if (transform.position.Equals(targetPosition))
        {
            targetPosition = PickRandomPosition();
        }
    }

    private void RefreshTargetPosition()
    {
        targetPosition = PickRandomPosition();
        periodsElapsed++;
        if (periodsElapsed > this.periodsTillDeath)
        {
            masterParticleSystem.Stop(true);
            Destroy(gameObject, 2.0f);
        }
    }

    private Vector3 PickRandomPosition(TileType type = TileType.Grass)
    {
        Vector3 randomTilePos = board.GetRandomTile(type).transform.position;
        return new Vector3(randomTilePos.x, 0, randomTilePos.z);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Tile"))
        {
            Tile otherTile = other.gameObject.GetComponent<Tile>();

            if (otherTile != null && otherTile.Entity != null && otherTile.Entity.Type != EntityType.TownHall)
            {
                otherTile.Entity = null;
            }
        }
    }
}