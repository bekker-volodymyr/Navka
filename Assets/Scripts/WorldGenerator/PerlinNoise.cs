using UnityEngine;

public class PerlinNoise : MonoBehaviour
{
    public float scale = 4f;

    public float offsetX = 100f;
    public float offsetY = 100f;

    public bool useRandomOffset = false;

    private void Start()
    {
        GenerateOffset();
    }

    public float GetRawPerlin(int x, int y, int worldWidth, int worldHeight)
    {
        float xCoord, yCoord;

        xCoord = (float)x / worldWidth * scale;
        yCoord = (float)y / worldHeight * scale;

        return Mathf.PerlinNoise(xCoord + offsetX, yCoord + offsetY);
    }

    public void GenerateOffset()
    {
        offsetX = Random.Range(0f, 9999f);
        offsetY = Random.Range(0f, 9999f);
    }

    public BiomsEnum[,] GeneratePerlinNoise(BiomsEnum[,] biomsGrid, int worldWidth, int worldHeight)
    {
        GenerateOffset();
        for (int x = 0; x < worldWidth; x++)
        {
            for (int y = 0; y < worldHeight; y++)
            {
                BiomsEnum biom = GetBiomWithPerlin(x, y, worldWidth, worldHeight);

                biomsGrid[x, y] = biom;
            }
        }
        return biomsGrid;
    }
    private BiomsEnum GetBiomWithPerlin(int x, int y, int worldWidth, int worldHeight)
    {
        float rawPerlin = GetRawPerlin(x, y, worldWidth, worldHeight);

        float scaledPerlin = rawPerlin * 2;

        int flooredPerlin = Mathf.FloorToInt(scaledPerlin);

        switch (flooredPerlin)
        {
            case 0: return BiomsEnum.Field;
            case 1: return BiomsEnum.Forest;
            case 2: return BiomsEnum.Village;
            default: return BiomsEnum.Field;
        }
    }
}
