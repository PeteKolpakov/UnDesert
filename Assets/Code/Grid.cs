using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{

    [SerializeField]
    private GameObject _cubePrefab;
    [SerializeField]
    private int _height;
    [SerializeField]
    private int _width;

    private Dictionary<Vector2Int, CubeBehaviour> _worldMap;
    private void Awake()
    {
        _worldMap = new Dictionary<Vector2Int, CubeBehaviour>();
    }
    private void Start()
    {
        for (int x = 0; x < _height; x++)
        {
            for (int y = 0; y < _width; y++)
            {
                var cube = Instantiate(_cubePrefab, new Vector3(x, 0, y), Quaternion.identity);
                CubeBehaviour spawnedCube = cube.GetComponent<CubeBehaviour>();
                spawnedCube.SetName($"{x}, {y}");
                spawnedCube.SetLifeTime(Random.Range(1f, 5f));
                _worldMap.Add(new Vector2Int(x, y), spawnedCube);
            }
        }
    }

    public  CubeBehaviour GetCubeAtCoords(Vector2Int target)
    {
        CubeBehaviour targetCube = _worldMap[target];
        return targetCube;
    }
}
