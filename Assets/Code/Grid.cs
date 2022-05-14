using Sirenix.OdinInspector;
using Sirenix.Utilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(NoiseGenerator))]
public class Grid : MonoBehaviour
{
    [SerializeField]
    private int _groundOffset = -6;
    [SerializeField]
    private GameObject _cubePrefab;
    [SerializeField]
    private int _width;
    [SerializeField]
    private int _height;
    [SerializeField]
    private GameObject _gridContainer;
    private NoiseGenerator _noise;

    private Dictionary<Vector2Int, GameObject> _worldMap;

    private void Awake()
    {
        _worldMap = new Dictionary<Vector2Int, GameObject>();
        _noise = GetComponent<NoiseGenerator>();
    }
    private void Start()
    {
        GameObject prefab = _cubePrefab;
        Vector3 pos = new Vector3(0, 0, 0);
        for (int x = 0; x < _width; x++)
        {
            for (int y = 0; y < _height; y++)
            {
                //pos = new Vector3(x, -6 + (_noise.GetSampleAt(new Vector2(x,y)) * 5), y);
                pos = new Vector3(x, _groundOffset, y);
                GameObject cube = Instantiate(prefab, pos, Quaternion.identity, _gridContainer.transform);
                //print(cube.GetComponent<GroundTile>());
                _worldMap.Add(new Vector2Int(x, y), cube);
                GroundTile spawnedCube = null;
                if (cube.TryGetComponent<GroundTile>(out GroundTile target))
                {
                    spawnedCube = target;
                    spawnedCube.SetName($"{x}, {y}");
                    spawnedCube.SetLifeTime(Random.Range(1f, 5f));
                    print($"generated Ground at: {spawnedCube.GetName()}");
                }
            }
        }      
    }

    public  GroundTile GetCubeAtCoords(Vector2Int target)
    {
        return _worldMap[target].GetComponent<GroundTile>();
    }
}
