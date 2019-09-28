using UnityEngine;

public class GameBoard : MonoBehaviour {
	
	[SerializeField]
	public Transform ground;
    public GameObject tilePrefab;

	Vector2Int size;

	public void Initialize (Vector2Int size) {
		this.size = size;
		ground.localScale = new Vector3(size.x, size.y, 1f);
	}
}