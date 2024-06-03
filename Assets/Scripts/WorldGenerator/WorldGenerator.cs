using System;
using UnityEngine;
using UnityEngine.Tilemaps;

public class WorldGenerator : MonoBehaviour
{
    [SerializeField] private PerlinNoise perlinNoise;

    [SerializeField] private BiomeSO fieldBiomeSO;
    [SerializeField] private BiomeSO forestBiomeSO;

    [SerializeField] private Tilemap biomLayer;
    [SerializeField] private Tilemap baseLayer;
    [SerializeField] private Tilemap decorLayer;

    [SerializeField] private int worldWidth;
    [SerializeField] private int worldHeight;

    [SerializeField] private TileBase[] biomTiles;

    //private BiomsEnum[,] biomsGrid;

    public BiomsEnum[,] biomsGrid
    {
        get
        {
            return biomsGrid;
        }
        set
        {
            biomsGrid = value;
        }
    }

    private void Start()
    {
        perlinNoise = GetComponent<PerlinNoise>();

        biomsGrid = new BiomsEnum[worldWidth, worldHeight];

        biomLayer.ClearAllTiles();
        baseLayer.ClearAllTiles();
        decorLayer.ClearAllTiles();

        Generate();
    }

    public void Generate()
    {
        biomsGrid = perlinNoise.GeneratePerlinNoise(biomsGrid, worldWidth, worldHeight);
        UpdateGrid();
    }

    private void UpdateGrid()
    {
        for (int x = 0; x < worldWidth; x++)
        {
            for (int y = 0; y < worldHeight; y++)
            {
                Vector3Int tilePos = new Vector3Int(x - (worldWidth / 2), y - worldHeight / 2, 0);

                biomLayer.SetTile(tilePos, biomsGrid[x, y] == BiomsEnum.Field ? biomTiles[0] : biomTiles[1]);
                
                if (UnityEngine.Random.Range(0, 500) == 0)
                {
                    decorLayer.SetTile(tilePos, fieldBiomeSO.decorLayer[UnityEngine.Random.Range(0, fieldBiomeSO.decorLayer.Length)]);
                }

                if (biomsGrid[x, y] == BiomsEnum.Field)
                {
                    baseLayer.SetTile(tilePos, fieldBiomeSO.baseLayer[UnityEngine.Random.Range(0, fieldBiomeSO.baseLayer.Length)]);
                }
                else
                {
                    baseLayer.SetTile(tilePos, forestBiomeSO.baseLayer[UnityEngine.Random.Range(0, forestBiomeSO.baseLayer.Length)]);
                }
            }
        }
    }
}
