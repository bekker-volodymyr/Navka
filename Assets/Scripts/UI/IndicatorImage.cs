using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Indicator: MonoBehaviour
{
    [SerializeField] private Image image;
    public void SetValue(float currentValue, float maxValue)
    {
        image.fillAmount = currentValue / maxValue;

        Debug.Log(image.fillAmount);
    }
}
