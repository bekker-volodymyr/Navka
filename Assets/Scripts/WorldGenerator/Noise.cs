using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Noise: MonoBehaviour
{
    public int width = 256;
    public int height = 128;
    
    public float scale = 4f;
    
    public float offsetX = 100f;
    public float offsetY = 100f;

    public bool useRandomOffset = false;

    public float GetRawPerlin(int x, int y)
    {
        float xCoord, yCoord;

        if (useRandomOffset)
        {
            xCoord = ((float)x / width * scale) + (float)Random.Range(0, 101);
            yCoord = ((float)y / height * scale) + (float)Random.Range(0, 101);
        }
        else
        {
            xCoord = (float)x / width * scale + offsetX;
            yCoord = (float)y / height * scale + offsetY;
        }

        return Mathf.PerlinNoise(xCoord, yCoord);
    }
}
