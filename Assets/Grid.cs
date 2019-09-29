using UnityEngine;

public class Grid : MonoBehaviour
{
    [SerializeField] private float size = 1f;

    public Vector3 GetNearestPointOnGrid(Vector3 position)
    {
        position -= transform.position;

        int xCount = Mathf.FloorToInt(position.x / size);
        int yCount = Mathf.FloorToInt(position.y / size);
        int zCount = Mathf.FloorToInt(position.z / size);

        Vector3 result = new Vector3(
            (float) xCount * size,
            (float) yCount * size,
            (float) zCount * size);

        result += transform.position;

        return result;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        for (float x = 0; x < 40; x += size)
        {
            for (float z = 0; z < 40; z += size)
            {
                var point = GetNearestPointOnGrid(new Vector3(x, 0f, z));
                Gizmos.DrawSphere(point, 0.1f);
            }
        }
    }

    public float GetUnitSize()
    {
        return size;
    }
}