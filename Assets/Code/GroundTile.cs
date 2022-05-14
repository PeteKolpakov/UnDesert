using System.Collections;
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

    private bool _isSelected = false;
    private bool _isHovered = false;

    private Vector3 _generatedPos;
    private Vector3 _selectedPos;

    private List<GroundTile> _myNeighbors;

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
        CountdownTimer();   
        UpdateSelectedState();
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
    /// <summary>
    /// returns True when timer when timer runs out
    /// </summary>
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

    }
    private void OnMouseUp()
    {
        if (_isHovered)
        {
            _isSelected = true;
        }
    }
}
