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
        //LifeCycle();
        timeLeft = _lifeTime += Random.Range(1f, 3f);
        _renderer.material.color = Random.ColorHSV();
    }
    private void Update()
    {
        LifeCycle();       
    }

    private void LifeCycle()
    {
    
        timeLeft -= Time.deltaTime;
        _renderer.material.color += Color.white * Time.deltaTime * Random.Range(0.01f, 0.1f);
        if (timeLeft < 0)
        {
            Destroy(this.gameObject);
        }
    }
}
