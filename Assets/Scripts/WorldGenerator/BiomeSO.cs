using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "Biome", menuName = "World Generation/Biome")]
public class BiomeSO : ScriptableObject
{
    [SerializeField] public TileBase[] baseLayer;
    [SerializeField] public TileBase[] decorLayer;
}
