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
    }
    private void Update()
    {
        LifeCycle();       
    }

    private void LifeCycle()
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
        transform.position += new Vector3(0, _selectHeight, 0);
    }
    private void OnMouseExit()
    {
        transform.position -= new Vector3(0, _selectHeight, 0);
    }
}
