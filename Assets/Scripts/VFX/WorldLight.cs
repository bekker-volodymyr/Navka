using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using System;
using UnityEngine.UI;

[RequireComponent(typeof(Light2D))]
public class WorldLight : MonoBehaviour
{
    private new Light2D light;
    [SerializeField] private WorldTime worldTime;
    [SerializeField] private Gradient gradient;
    //[SerializeField] private Image panelImage; 

    private void Awake()
    {
        light = GetComponent<Light2D>();
        //panelImage = GetComponent<Image>();
        worldTime.WorldTimeChanged += OnWorldTimeChanged;
    }

    private void OnDestroy()
    {
        worldTime.WorldTimeChanged -= OnWorldTimeChanged;
    }

    private void OnWorldTimeChanged(object sender, TimeSpan newTime)
    {
        light.color = gradient.Evaluate(PercentOfTheDay(newTime));
        //panelImage.color = gradient.Evaluate(PercentOfTheDay(newTime));
    }

    private float PercentOfTheDay(TimeSpan timeSpan)
    {
        return (float)timeSpan.TotalMinutes % WorldTimeConstants.minutesInDay / WorldTimeConstants.minutesInDay;
    }
}
