using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class WorldPrefab
{
    public GameObject prefab;
    public float spawnWeight;
    public float minHeight;
    public float maxHeight;
}

[System.Serializable]
public class Biome
{
    public string name;
    public WorldPrefab[] biomePrefabs;
}

public class WorldGenerator : MonoBehaviour
{
    [Header("World Settings")]
    public int width = 10;
    public int height = 10;

    [Header("Biomes")]
    public Biome[] biomes;

    [Header("Random Seed")]
    public bool useRandomSeed = true;
    public string seed;
    public InputField customSeedInput;

    void Start()
    {
        if (useRandomSeed)
        {
            seed = Time.time.ToString();
            customSeedInput.interactable = false;
        }
        else
        {
            customSeedInput.interactable = true;
        }

        GenerateWorld();
    }

    void Update()
    {
        if (!useRandomSeed && customSeedInput.isFocused && Input.GetKeyDown(KeyCode.Return))
        {
            seed = customSeedInput.text;
            GenerateWorld();
        }
    }

    void GenerateWorld()
    {
        Random.InitState(seed.GetHashCode());

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Vector3 tilePosition = new Vector3(x, 0, y);
                Biome selectedBiome = ChooseRandomBiome();
                WorldPrefab[] biomePrefabs = GenerateBiome(selectedBiome, width, height);

                foreach (var prefab in biomePrefabs)
                {
                    float heightOffset = Random.Range(prefab.minHeight, prefab.maxHeight);
                    tilePosition.y = heightOffset;
                    Instantiate(prefab.prefab, tilePosition, Quaternion.identity);
                }
            }
        }
    }

    Biome ChooseRandomBiome()
    {
        return biomes[Random.Range(0, biomes.Length)];
    }

    WorldPrefab[] GenerateBiome(Biome biome, int width, int height)
    {

        WorldPrefab[] biomePrefabs = new WorldPrefab[biome.biomePrefabs.Length];

        for (int i = 0; i < biome.biomePrefabs.Length; i++)
        {
            biomePrefabs[i] = new WorldPrefab
            {
                //prefab = GetRandomPrefab(biome.biomePrefabs[i].prefab),
                spawnWeight = biome.biomePrefabs[i].spawnWeight,
                minHeight = biome.biomePrefabs[i].minHeight,
                maxHeight = biome.biomePrefabs[i].maxHeight
            };
        }

        float saturation = CalculateSaturation(biomePrefabs);
        //AddPointsOfInterest(biomePrefabs, saturation);

        return biomePrefabs;
    }

    private GameObject GetRandomPrefab(GameObject[] prefabs)
    {
        return prefabs[Random.Range(0, prefabs.Length)];
    }

    float CalculateSaturation(WorldPrefab[] biomePrefabs)
    {
        float saturation = 0f;

        foreach (var prefab in biomePrefabs)
        {
            saturation += prefab.spawnWeight;
        }

        return saturation;
    }

    void OnValidate()
    {

        width = Mathf.Max(1, width);
        height = Mathf.Max(1, height);
    }

    void OnDrawGizmos()
    {

        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(new Vector3(width * 0.5f, 0.5f, height * 0.5f), new Vector3(width, 1, height));
    }
}