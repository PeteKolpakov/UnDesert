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

    [SerializeField]
    private GameObject _canvas;
    [SerializeField]
    private Text _info;

    private TileState _myState = TileState.Soil;
    public float _hydration = 1f;

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

    public void UpdateHidrationColor()
    {
        if (_myState.Equals(TileState.Desert))
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
        _selectedPos = new Vector3(_generatedPos.x, _generatedPos.y + _selectHeight ,_generatedPos.z);
        _myNeighbors = new List<GroundTile>();

        //_info = FindObjectOfType<TileInfo>();

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
        UpdateSelectedState();
        UpdateHidrationColor();
        if (!_myState.Equals(TileState.Water))
        {
            CheckIfDry();
            UpdateTileState();
        }
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
                _myState = TileState.Desert;
                break;
            case false:
                _myState = TileState.Soil;
                break;
        }
    }

    private void CountdownTimer()
    {   
        timeLeft -= Time.deltaTime;
        float samplePoint = 0f + timeLeft;
        _renderer.material.color = _moistureGradient.Evaluate(samplePoint);
        if (timeLeft < 0f)
        {
            //Destroy(this.gameObject);
        }
    }

    private void OnMouseEnter()
    {
        _isSelected = true;
        _isHovered = true;

        _info.text = $"{ _hydration * 100:F0}%";
        _canvas.SetActive(true);

    }
    private void OnMouseExit()
    {
        _isSelected = false;
        _isHovered = false;

        _canvas.SetActive(false);
    }
    private void OnMouseDown()
    {
        _isSelected = false;
        Debug.Log($"Coord: ({_name}) | Hydration Level: {_hydration * 100:F2}%  | Tile State: {_myState}");

    }
    private void OnMouseUp()
    {
        if (_isHovered)
        {
            _isSelected = true;
        }
    }
}
