using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.WSA;

public class WorldGenerator : MonoBehaviour
{
    [SerializeField] BiomeSO fieldBiomeSO;

    [SerializeField] Tilemap baseLayer;
    [SerializeField] Tilemap decorLayer;

    [SerializeField] int worldWidth;
    [SerializeField] int worldHeight;

    private void Start()
    {
        int[,] noise = Noise.Generate(worldWidth, worldHeight);

        baseLayer.ClearAllTiles();
        decorLayer.ClearAllTiles();

        for (int x = 0; x < worldWidth; x++)
        {
            for (int y = 0; y < worldHeight; y++)
            {
                if (noise[x, y] == 1)
                {
                    baseLayer.SetTile(new Vector3Int(x - (worldWidth / 2), y - worldHeight / 2, 0), fieldBiomeSO.baseLayer[Random.Range(0, fieldBiomeSO.baseLayer.Length)]);

                    if (Random.Range(0, 50) == 0)
                    {
                        decorLayer.SetTile(new Vector3Int(x - (worldWidth / 2), y - worldHeight / 2, 0), fieldBiomeSO.decorLayer[Random.Range(0, fieldBiomeSO.decorLayer.Length)]);
                    }
                }
            }
        }
    }
}
