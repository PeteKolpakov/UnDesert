using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HydrationInfo : MonoBehaviour
{

    [SerializeField]
    private GameObject _HydrationUI;

    [SerializeField]
    private Text _text;

    public void SetValue(float value)
    {
        _text.text = $"{ value * 100:F0}%";
    }

    public void ShowUI(bool _switch)
    {

        if (!_switch)
        {
            _HydrationUI.SetActive(false);
        }
        else
        {
            _HydrationUI.SetActive(true);
        }

    }

}
