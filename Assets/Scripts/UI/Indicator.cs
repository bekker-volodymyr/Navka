using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Indicator : MonoBehaviour
{
    [SerializeField] private Slider slider;
    public void SetValue(float currentValue, float maxValue)
    {
        slider.value = currentValue * 100 / maxValue;
    }
}
