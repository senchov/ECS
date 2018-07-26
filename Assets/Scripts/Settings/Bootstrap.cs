using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bootstrap
{
    public static PrephabHub PrefabHub;
    public static MovementSettings MoveSettings;
    public static DestroySettings DestroySettings;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    public static void InitializeAfterSceneLoad()
    {
        PrefabHub = GameObject.Find("Settings").GetComponent<PrephabHub>();
        MoveSettings = GameObject.Find("Settings").GetComponent<MovementSettings>();
        DestroySettings = GameObject.Find("Settings").GetComponent<DestroySettings>();
    }    
}
