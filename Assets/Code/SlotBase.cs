using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotBase : MonoBehaviour
{

    [SerializeField]
    private Sprite[] _sprites;

    [SerializeField]
    private GameObject[] _trees;

    [SerializeField]
    private GameObject _previousSlot;

    [SerializeField]
    private bool random;

    private PlaceTree _selection;

    private void Awake()
    {
        _selection = FindObjectOfType<PlaceTree>();
        gameObject.GetComponent<Image>().sprite = _sprites[Random.Range(0, _sprites.Length)];
    }

    private void Update()
    {
        Take();
    }

    private void Take()
    {
        if (gameObject.GetComponent<Image>().sprite == null)
        {

            if (!random)
            {
                gameObject.GetComponent<Image>().sprite = _previousSlot.GetComponent<Image>().sprite;
                _previousSlot.GetComponent<Image>().sprite = null;
            }
            else
            {
                gameObject.GetComponent<Image>().sprite = _sprites[Random.Range(0, _sprites.Length)];
            }

        }
    }

    public void Button()
    {

        if (gameObject.GetComponent<Image>().sprite.name == "Acacia")
        {
            _selection._selected = _trees[0];
        }
        else if (gameObject.GetComponent<Image>().sprite.name == "Aloe")
        {
            _selection._selected = _trees[1];
        }
        else if (gameObject.GetComponent<Image>().sprite.name == "Baobab")
        {
            _selection._selected = _trees[2];
        }
        else if (gameObject.GetComponent<Image>().sprite.name == "Cactus")
        {
            _selection._selected = _trees[3];
        }
        else if (gameObject.GetComponent<Image>().sprite.name == "Palm")
        {
            _selection._selected = _trees[4];
        }

        gameObject.GetComponent<Image>().sprite = null;

    }

}

