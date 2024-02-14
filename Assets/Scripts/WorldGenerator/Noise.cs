using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Noise
{
    static private int[,] noise;

    static private int biomePointRadiusRange = 20;

    static public int[,] Generate(int width, int height)
    {
        //Vector2Int villagePoint = new (Random.Range(0, width), Random.Range(0,height));

        noise = new int[width, height];

        Vector2Int fieldPoint = new Vector2Int(Random.Range(0, width), Random.Range(0, height));

        for (int x = fieldPoint.x - biomePointRadiusRange; x < fieldPoint.x + biomePointRadiusRange; x++)
        {
            if (x < 0) continue;

            if (x >= width) break;

            for (int y = fieldPoint.y - Random.Range(biomePointRadiusRange - 6, biomePointRadiusRange + 6); y < fieldPoint.y + Random.Range(biomePointRadiusRange - 6, biomePointRadiusRange + 6); y++)
            {
                if (y < 0) continue;

                if (y >= height) break;

                if(x == fieldPoint.x - biomePointRadiusRange || 
                    x == fieldPoint.x - biomePointRadiusRange + 1 ||
                    x == fieldPoint.x + biomePointRadiusRange || 
                    x == fieldPoint.x + biomePointRadiusRange - 1)
                {
                    noise[x, y] = Random.value > 0.7 ? 1 : 0;
                }
                else
                {
                    noise[x, y] = 1;
                }
            }
        }

        return noise;
    }
}
