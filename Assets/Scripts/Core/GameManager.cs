using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region To be or not to be...
    // public static GameManager Instance { get; private set; }
    // private void Awake()
    //{
    //    if (Instance != null && Instance != this)
    //    {
    //        Destroy(this);
    //    }
    //    else
    //    {
    //        Instance = this;
    //    }
    //}
    #endregion

    public static string worldScene;

    public static float health = 0;
    public static float hunger = 0;
    public static float mana = 0;

    #region Game State Values
    public static bool isPaused = false;
    public static bool isDead = false;
    public static bool isSave = false;
    #endregion

    #region Death Event
    public static Action DeathEvent;
    #endregion

    #region Transition Values
    public static HideoutStruct hideoutStruct;
    public static string saveFile;
    #endregion

    #region Dialog System
    public static Action<IDialog> DialogStartEvent;
    public static Action DialogStopEvent;
    #endregion

    #region Quest System
    public static Action<QuestsSO> QuestStartEvent;
    public static Action<QuestsSO> QuestStopEvent;
    #endregion

    #region Inventory System
    [SerializeField] private InventoryController inventoryController;
    public InventoryController InventoryController => inventoryController;
    #endregion

    #region Sound Values
    public static float sfxVolume = 1f;
    public static float musicVolume = 1f;
    #endregion
}
