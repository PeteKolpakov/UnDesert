using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    [SerializeField]
    private float _speed = 5f;
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.W))
        //{
        //    transform.Translate(new Vector3(1f, 0f, 1f) * _speed * Time.deltaTime);
        //}
        //if (Input.GetKeyDown(KeyCode.S))
        //{
        //    transform.Translate(new Vector3(-1f, 0f, -1f) * _speed * Time.deltaTime);
        //}
        //if (Input.GetKeyDown(KeyCode.D))
        //{
        //    transform.Translate(new Vector3(1f, 0f, -1f) * _speed * Time.deltaTime);
        //}
        //if (Input.GetKeyDown(KeyCode.A))
        //{
        //    transform.Translate(new Vector3(-1f, 0f, 1f) * _speed * Time.deltaTime);
        //}
    }
}
