using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GameObjectData
{
    public string name;
    public Vector2 position;
    public float rotation;
    public string spriteName;  // Assuming you want to save sprite information
}

[Serializable]
public class LevelData
{
    public List<GameObjectData> gameObjects = new List<GameObjectData>();
}