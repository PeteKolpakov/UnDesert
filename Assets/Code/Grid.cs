using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{

    [SerializeField]
    private GameObject _cubePrefab = null;
    [SerializeField]
    private int _width;
    [SerializeField]
    private int _height;

    private Dictionary<Vector2Int, GameObject> _worldMap;
    private void Awake()
    {
        _worldMap = new Dictionary<Vector2Int, GameObject>();
    }
    private void Start()
    {
        GameObject prefab = _cubePrefab;
        Vector3 pos = new Vector3(0, 0, 0);
        for (int x = 0; x < _width; x++)
        {
            for (int y = 0; y < _height; y++)
            {
                pos = new Vector3(x, 0, y);
                GameObject cube = Instantiate(prefab, pos, Quaternion.identity);
                //print(cube.GetComponent<GroundTile>());
                //_worldMap.Add(new Vector2Int(x, y), cube);
                //spawnedCube.SetName($"{x}, {y}");
                //spawnedCube.SetLifeTime(Random.Range(1f, 5f));
            }
        }
        //print(_worldMap);
    }

    public  GroundTile GetCubeAtCoords(Vector2Int target)
    {
        GroundTile targetCube = _worldMap[target].GetComponent<GroundTile>();
        return targetCube;
    }
}
