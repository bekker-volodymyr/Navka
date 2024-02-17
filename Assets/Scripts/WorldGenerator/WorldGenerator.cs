using System;
using UnityEngine;
using UnityEngine.Tilemaps;

public class WorldGenerator : MonoBehaviour
{
    [SerializeField] private PerlinNoise perlinNoise;
    [SerializeField] private CelluralAutomata celluralAutomata;

    [SerializeField] private BiomeSO fieldBiomeSO;

    [SerializeField] private Tilemap biomLayer;
    [SerializeField] private Tilemap baseLayer;
    [SerializeField] private Tilemap decorLayer;

    [SerializeField] private int worldWidth;
    [SerializeField] private int worldHeight;

    [SerializeField] private TileBase[] biomTiles;

    public bool generatePerlin = true;

    private BiomsEnum[,] biomsGrid;

    private void Start()
    {
        perlinNoise = GetComponent<PerlinNoise>();
        celluralAutomata = GetComponent<CelluralAutomata>();

        biomsGrid = new BiomsEnum[worldWidth, worldHeight];

        biomLayer.ClearAllTiles();
        baseLayer.ClearAllTiles();
        decorLayer.ClearAllTiles();
    }

    public void ChangeAlgorithm(bool toPerlin)
    {
        generatePerlin = toPerlin;
        if (generatePerlin)
        {
            perlinNoise.GenerateOffset();
            biomsGrid = perlinNoise.GeneratePerlinNoise(biomsGrid, worldWidth, worldHeight);
        }
        else
        {
            // biomsGrid = celluralAutomata.MakeNoiseGrid(worldWidth, worldHeight);
            perlinNoise.GenerateOffset();
            biomsGrid = perlinNoise.GeneratePerlinNoise(biomsGrid, worldWidth, worldHeight);
            biomsGrid = celluralAutomata.ApplyCelluralAutomaton(biomsGrid, worldWidth, worldHeight);
        }
        RegenerateGrid();
    }

    private void RegenerateGrid()
    {
        for (int x = 0; x < worldWidth; x++)
        {
            for (int y = 0; y < worldHeight; y++)
            {
                Vector3Int tilePos = new Vector3Int(x - (worldWidth / 2), y - worldHeight / 2, 0);

                biomLayer.SetTile(tilePos, biomsGrid[x, y] == BiomsEnum.Field ? biomTiles[0] : biomTiles[1]);

                if (biomsGrid[x, y] == BiomsEnum.Field)
                {
                    baseLayer.SetTile(tilePos, fieldBiomeSO.baseLayer[UnityEngine.Random.Range(0, fieldBiomeSO.baseLayer.Length)]);
                    decorLayer.SetTile(tilePos, fieldBiomeSO.decorLayer[UnityEngine.Random.Range(0, fieldBiomeSO.decorLayer.Length)]);
                }
                else
                {
                    baseLayer.SetTile(tilePos, biomsGrid[x, y] == BiomsEnum.Field ? biomTiles[0] : biomTiles[1]);
                }
            }
        }
    }
}
