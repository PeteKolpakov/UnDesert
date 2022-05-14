using Assets.Code;
using System.Collections.Generic;
using UnityEngine;

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

    private TileState _myState = TileState.soil;
    private float _hydration = 1f;

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
        if (_myState.Equals(TileState.desert))
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
        if (!_myState.Equals(TileState.water))
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
                _myState = TileState.desert;
                break;
            case false:
                _myState = TileState.soil;
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
    }
    private void OnMouseExit()
    {
        _isSelected = false;
        _isHovered = false;
    }
    private void OnMouseDown()
    {
        _isSelected = false;
        Debug.Log($"hidration level at {_name} is {_hydration} and state is {_myState}");

    }
    private void OnMouseUp()
    {
        if (_isHovered)
        {
            _isSelected = true;
        }
    }
}
