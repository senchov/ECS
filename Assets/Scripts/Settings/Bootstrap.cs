using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bootstrap
{
    public static PrephabHub PrefabHub;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    public static void InitializeAfterSceneLoad()
    {
        PrefabHub = GameObject.Find("Settings").GetComponent<PrephabHub>();
    }    
}
