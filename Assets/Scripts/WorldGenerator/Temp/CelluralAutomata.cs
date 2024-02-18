using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CelluralAutomata : MonoBehaviour
{
    [SerializeField] private int density;
    [SerializeField] private int iterations;
    public BiomsEnum[,] MakeNoiseGrid(int width, int height)
    {
        BiomsEnum[,] bioms = new BiomsEnum[width, height];
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                if (Random.value * 100 > density)
                {
                    bioms[i, j] = BiomsEnum.Field;
                }
                else
                {
                    bioms[i, j] = BiomsEnum.Forest;
                }
            }
        }

        return bioms;
    }

    public BiomsEnum[,] ApplyCelluralAutomaton(BiomsEnum[,] bioms, int width, int height)
    {
        for (int i = 1; i < iterations; i++)
        {
            BiomsEnum[,] tempGrid = bioms;
            for (int j = 1; j < width; j++)
            {
                for (int k = 1; k < height; k++)
                {
                    int neighborForest = 0;
                    for (int y = j - 1; y < j + 1; y++)
                    {
                        for (int x = k - 1; x < k + 1; x++)
                        {
                            if (IsWithinMapBounds(x, y, width, height))
                            {
                                if (y != j && x != k)
                                {
                                    if (tempGrid[y, x] == BiomsEnum.Forest)
                                    {
                                        neighborForest++;
                                    }
                                }
                            }
                            else
                            {
                                neighborForest++;
                            }
                        }
                    }
                    if (neighborForest > 4)
                    {
                        bioms[j, k] = BiomsEnum.Forest;
                    }
                    else
                    {
                        bioms[j, k] = BiomsEnum.Field;
                    }
                }
            }
        }

        return bioms;
    }

    private bool IsWithinMapBounds(int x, int y, int boundsX, int boundsY)
    {
        return (x > 0 && x < boundsX - 1) && (y > 0 && y < boundsY - 1);
    }
}
