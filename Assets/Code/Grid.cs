using Assets.Code;
using NoiseTest;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    [SerializeField]
    private GameObject _cubePrefab;
    [SerializeField]
    private int _width;
    [SerializeField]
    private int _height;
    [SerializeField]
    private GameObject _gridContainer;
    [SerializeField]
    private float _heightScale = 4f;
    [SerializeField]
    private float _groundHeightOffset = -6f;
    [SerializeField][Range(-1f, 1f)]
    private float _noiseBias = 1f;

    private Dictionary<Vector2Int, GroundTile> _worldMap;

    private OpenSimplexNoise _noise2;

    private void Awake()
    {
        _worldMap = new Dictionary<Vector2Int, GroundTile>();
        _noise2 = new OpenSimplexNoise();
    }
    private void Start()
    {
        GenerateMap();
        SetupTileNeighbors();
    }

    private void SetupTileNeighbors()
    {
        foreach (var currentTile in _worldMap)
        {

            List<GroundTile> currentNeighbors = new List<GroundTile>();
            Vector2Int pos = currentTile.Key;
            Vector2Int index = new Vector2Int(pos.x, pos.y);
            if (_worldMap.ContainsKey(index + Vector2Int.up))
            {
                currentNeighbors.Add(_worldMap[index + Vector2Int.up]);
            }
            if (_worldMap.ContainsKey(index + Vector2Int.right))
            {
                currentNeighbors.Add(_worldMap[index + Vector2Int.right]);
            }
            if (_worldMap.ContainsKey(index + Vector2Int.down))
            {
                currentNeighbors.Add(_worldMap[index + Vector2Int.down]);
            }
            if (_worldMap.ContainsKey(index + Vector2Int.left))
            {
                currentNeighbors.Add(_worldMap[index + Vector2Int.left]);
            }

            currentTile.Value.SetNeighbors(currentNeighbors);
            //Debug.Log($"{currentTile.Value.GetName()} has {currentNeighbors.Count} neigbors");
        }
    }

    private void GenerateMap()
    {
        GameObject prefab = _cubePrefab;
        Vector3 pos = new Vector3(0, 0, 0);
        float maxDist = Vector2.Distance(new Vector2(0, 0), new Vector2(_width / 2, _height / 2));
        for (int x = 0; x < _width; x++)
        {
            for (int y = 0; y < _height; y++)
            {
                //pos = new Vector3(x, -6 + (_noise.GetSampleAt(new Vector2(x,y)) * 5), y);
                //pos = new Vector3(x, -6 + (float)(_noise2.Evaluate(x, y) * _heightScale), y);

                pos = new Vector3(x, _groundHeightOffset, y);
                //pos = new Vector3(x, _groundOffset, y);

                GameObject cube = Instantiate(prefab, pos, Quaternion.identity, _gridContainer.transform);
                _worldMap.Add(new Vector2Int(x, y), cube.GetComponent<GroundTile>());
                GroundTile spawnedCube = null;

                if (cube.TryGetComponent<GroundTile>(out GroundTile target))
                {
                    spawnedCube = target;
                    spawnedCube.SetName($"{x}, {y}");
                    spawnedCube.SetLifeTime(Random.Range(1f, 5f));

                    float distanceToCenter = Vector2.Distance(new Vector2(x, y), new Vector2(_width / 2, _height / 2));
                    float mod = 1 - distanceToCenter / maxDist;

                    float noiseSample = GetNoiseSample(x, y);
                    spawnedCube.SetHidration(((noiseSample + _noiseBias) * .6f) * mod);

                    //print((float)_noise2.Evaluate(x, y));
                    //print($"generated Ground at: {spawnedCube.GetName()}");
                }
            }
        }
        GroundTile centerTile;
        _worldMap.TryGetValue(new Vector2Int(_width / 2, _height / 2), out centerTile);
        centerTile.SetHidration(1f);
        centerTile.SetState(TileState.water);
    }

    private float GetNoiseSample(int x, int y)
    {
        float noiseSample = (float)_noise2.Evaluate(x, y);
        if (noiseSample <= 0f)
        {
            noiseSample = 0f;
        }
        if (noiseSample >= 1.0f)
        {
            noiseSample = 1f;
        }

        return noiseSample;
    }

    public  GroundTile GetCubeAtCoords(Vector2Int target)
    {
        GroundTile value;
        if (_worldMap.TryGetValue(target, out value))
        {
            return value;
        }
        print($"No Ground Tile at Target Lovation ({target})");
        return null;       
    }
}
