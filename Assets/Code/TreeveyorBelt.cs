using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TreeveyorBelt : MonoBehaviour
{

    [SerializeField]
    private float timer;

    [SerializeField]
    private Sprite[] _sprites;

    [SerializeField]
    private GameObject[] _slots;

    [SerializeField]
    private int _index = 0;

    private float maxTime = 2f;

    private int one = 0, two = 0, three = 0, zero = 0;

    void Start()
    {
        timer = maxTime;

        _index = _slots.Length-1;
    }

    void Update()
    {

        timer -= Time.deltaTime;
        Queue();

    }

    private void Queue()
    {

        if (timer <= 0f)
        {

            if (_index == _slots.Length - 1)
            {
                zero = Random.Range(0, _sprites.Length);
                _slots[_slots.Length - 1].GetComponent<Image>().sprite = _sprites[zero];
                --_index;
            }
            else if (_index == _slots.Length - 2)
            {

                one = zero;
                _slots[_slots.Length - 2].GetComponent<Image>().sprite = _sprites[one];
                --_index;

            }
            else if (_index == _slots.Length - 3)
            {
                two = one;
                _slots[_slots.Length - 3].GetComponent<Image>().sprite = _sprites[two];
                --_index;
            }
            else if (_index == _slots.Length - 4)
            {

                three = two;
                _slots[_slots.Length - 4].GetComponent<Image>().sprite = _sprites[three];
                _index = _slots.Length - 1;
            }

            timer = maxTime;

        }
    }
}
