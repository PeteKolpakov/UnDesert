using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeBehaviour : MonoBehaviour
{
    private string _name;
    private float _lifeTime = 3f;
    private float timeLeft = 0f;
    private bool _isAlive = false;

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
    }

    private void LifeCycle()
    {
        _isAlive = true;
        timeLeft = _lifeTime;
        while (_isAlive)
        {
            timeLeft -= Time.deltaTime;
            if (timeLeft < 0)
            {
                _isAlive = false;
                Destroy(this.gameObject);
            }
        }       
    }
}
