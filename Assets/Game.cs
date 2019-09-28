using UnityEngine;

public class Game : MonoBehaviour
{

    [SerializeField]
    Vector2Int boardSize = new Vector2Int(11, 11);

    private GameBoard board;

    void Start()
    {
        board = GetComponent<GameBoard>();
    }

    void Awake()
    {
        board.Initialize(boardSize);
    }
}