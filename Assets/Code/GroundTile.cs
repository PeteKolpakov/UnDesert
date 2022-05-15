using Assets.Code;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GroundTile : MonoBehaviour
{
    private string _name;
    [SerializeField]
    private float _lifeTime = 1f;
    private float timeLeft = 0f;

    [SerializeField]
    private Renderer _renderer;
    [SerializeField]
    private Gradient _moistureGradient;
    [SerializeField]
    private float _selectHeight;
    [SerializeField][Range(0f, 1f)]
    private float _desertCutoff = 0.2f;

    private bool _isSelected = false;
    private bool _isHovered = false;

    private Vector3 _generatedPos;
    private Vector3 _selectedPos;

    private List<GroundTile> _myNeighbors;

    private TileState _iAmType = TileState.Soil;
    private HydrationState _hydrationState = HydrationState.stable;
    public float _hydration = 1f;

    private HydrationInfo _moistInfo;

    [SerializeField, Range(0.001f, 4f)]
    private float _hydrationSpeed = .1f;

    public TreeRequirements _myTree;

    public string GetName()
    {
        return _name;
    }

    public void SetName(string target)
    {
        _name = target;
    }

    public void SetLifeTime(float target)
    {
        timeLeft = target;
    }

    public void SetHidration(float target)
    {
        _hydration = target;
    }

    public void SetTree(TreeRequirements target)
    {
        _myTree = target;
    }

    public void UpdateHidrationColor()
    {
        if (_iAmType.Equals(TileState.Desert))
        {
            _renderer.material.color = _moistureGradient.Evaluate(0);
        }
        _renderer.material.color = _moistureGradient.Evaluate(_hydration);
    }

    private void Awake()
    {
        timeLeft = _lifeTime += Random.Range(1f, 3f);
        _renderer.material.color = _moistureGradient.Evaluate(1f);
        _generatedPos = transform.position;
        _selectedPos = new Vector3(_generatedPos.x, _generatedPos.y + _selectHeight, _generatedPos.z);
        _myNeighbors = new List<GroundTile>();

        _moistInfo = FindObjectOfType<HydrationInfo>();

    }

    private void Start()
    {
        _moistInfo.gameObject.SetActive(false);
    }

    public void SetNeighbors(List<GroundTile> targetList)
    {
        _myNeighbors.Clear();
        foreach (var item in targetList)
        {
            _myNeighbors.Add(item);
        }
    }

    public List<GroundTile> GetNeighbors()
    {
        return _myNeighbors;
    }

    private void Update()
    {
        //CountdownTimer();   
        if (!_iAmType.Equals(TileState.Water))
        {
            CheckIfDry();
            UpdateTileState();
        }
        UpdateSelectedState();
        ResolveNeighbourInteraction();
        UpdateHydration();
        UpdateHidrationColor();
    }

    private void UpdateSelectedState()
    {
        if (!_isSelected)
        {
            transform.position = _generatedPos;
        }
        if (_isSelected)
        {
            transform.position = _selectedPos;
        }
    }

    private bool CheckIfDry()
    {
        return _hydration <= _desertCutoff;
    }
    private void UpdateTileState()
    {
        switch (CheckIfDry())
        {
            case true:
                _iAmType = TileState.Desert;
                break;
            case false:
                _iAmType = TileState.Soil;
                break;
        }
    }
    public void SetTileState(TileState target)
    {
        _iAmType = target;
    }

    public TileState GetTileState()
    {
        return _iAmType;
    }
    private void UpdateHydration()
    {

        if (_hydrationState.Equals(HydrationState.up))
        {
            _hydration += _hydrationSpeed * Time.deltaTime;
        }
        if (_hydrationState.Equals(HydrationState.down))
        {
            _hydration -= _hydrationSpeed * Time.deltaTime;
        }
        _hydration = Mathf.Clamp(_hydration, 0f, .98f);
    }
    private void ResolveNeighbourInteraction()
    {
        int desertCount = 0;
        int waterCount = 0;
        int soilCount = 0;
        int treeCount = 0;

        foreach (var tile in _myNeighbors)
        {
            if (tile.GetTileState() == TileState.Desert)
            {
                desertCount++;
            }
            else if (tile.GetTileState() == TileState.Water)
            {
                waterCount++;
            }
            else if (tile.GetTileState() == TileState.Soil)
            {
                soilCount++;
            }
            if (tile._myTree != null)
            {
                treeCount++;
            }
        }

        switch (_iAmType)
        {
            case TileState.Desert:
                if (waterCount >= 1)
                    _hydrationState = HydrationState.up;
                if (treeCount >= 1)
                    _hydrationState = HydrationState.up;
                break;
            case TileState.Soil:
                if (desertCount >= 2)
                    _hydrationState = HydrationState.down;
                if (treeCount >= 2)
                    _hydrationState = HydrationState.up;
                break;
            case TileState.Water:
                    _hydrationState = HydrationState.stable;
                if (desertCount >= 3)
                    _hydrationState = HydrationState.down;
                break;
            default:
                break;
        }
        //// if soil
        //if (desertCount >= 1 && _iAmType.Equals(TileState.Soil))
        //{
        //    _hydrationState = HydrationState.down;
        //    //_renderer.material.color = Color.black;
        //}
        //// if desert
        //else if (waterCount >= 1 && !_iAmType.Equals(TileState.Desert))
        //{
        //    if (!(desertCount >= 2))
        //    {
        //        _hydrationState = HydrationState.up;
        //        //_renderer.material.color = Color.blue;
        //    }
        //}
        //else if (soilCount >= 3 && !_iAmType.Equals(TileState.Desert))
        //{
        //    if (!(desertCount >= 2))
        //    {
        //        _hydrationState = HydrationState.up;
        //        //_renderer.material.color = Color.blue;
        //    }
        //}
        //// if water
        //else if (desertCount >= 3 && _iAmType.Equals(TileState.Water))
        //{
        //    _hydrationState = HydrationState.down;
        //}
        //// if trees
        //if (_iAmType.Equals(TileState.Desert) && treeCount >= 1)
        //{
        //    _hydrationState = HydrationState.up;
        //}
        if (_myTree != null)
        {
            _hydrationState = HydrationState.up;
        }
    }

    private void OnMouseEnter()
    {
        _isSelected = true;
        _isHovered = true;

        _moistInfo.SetValue(_hydration);
        _moistInfo.ShowUI(true);

    }
    private void OnMouseExit()
    {
        _isSelected = false;
        _isHovered = false;

        _moistInfo.ShowUI(false);

    }
    private void OnMouseDown()
    {
        _isSelected = false;
        Debug.Log($"Coord: ({_name}) | Hydration Level: {_hydration * 100:F2}%  | Tile State: {_iAmType} | Has Tree: {_myTree}");

    }
    private void OnMouseUp()
    {
        if (_isHovered)
        {
            _isSelected = true;
        }
    }
}
