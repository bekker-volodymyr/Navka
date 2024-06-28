using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class WorldTime : MonoBehaviour
{
    public event EventHandler<TimeSpan> WorldTimeChanged;
    [SerializeField] private float dayLength;
    private TimeSpan currentTime;
    private float minuteLength => dayLength / WorldTimeConstants.minutesInDay;

    private void Start()
    {
        StartCoroutine(AddMinute());
    }


    private IEnumerator AddMinute()
    {
        currentTime += TimeSpan.FromMinutes(1);
        WorldTimeChanged?.Invoke(this, currentTime);
        yield return new WaitForSeconds(minuteLength);
        StartCoroutine(AddMinute());

        //if (GameManager.isPaused)
        //{
        //    StopCoroutine(AddMinute());
        //}
        //else
        //{
        //    StartCoroutine(AddMinute());
        //}
    }
}
