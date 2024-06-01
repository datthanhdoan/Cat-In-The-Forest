using UnityEngine;

public class Pasture : MonoBehaviour
{
    private Bounds _bounds;
    [SerializeField] WayPoint _wayPoint;


    private void Start()
    {
        InitBounds();
    }
    private void InitBounds()
    {
        _bounds = new Bounds(_wayPoint.Points[0], Vector3.zero);
        foreach (Vector3 point in _wayPoint.Points)
        {
            _bounds.Encapsulate(point);
        }
    }

    public bool CheckPositionInBounds(Vector3 position)
    {
        if (_bounds.Contains(position))
        {
            return true;
        }
        return false;
    }

    public Vector3 RandomPositionInBounds()
    {
        Vector3 randomPos = new Vector3(
            Random.Range(_bounds.min.x, _bounds.max.x),
            Random.Range(_bounds.min.y, _bounds.max.y),
            Random.Range(_bounds.min.z, _bounds.max.z)
        );

        // Hiển thị vị trí random lên scene và xóa sau 2s
        // GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        // cube.transform.position = randomPos;
        // Destroy(cube, 2);

        // Debug.Log($"Random Position: {randomPos}");
        // Debug.Log($"Bounds Min: {_bounds.min}, Max: {_bounds.max}");

        return randomPos;
    }
    // vẽ hình chữ nhật bao quanh pasture
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(_bounds.center, _bounds.size);
    }
}