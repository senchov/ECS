using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class Bootstrap
{
    public static PrephabHub PrefabHub;
    public static MovementSettings MoveSettings;
    public static DestroySettings DestroySettings;
    public static CameraMovementSettings CameraMoveSettings;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    public static void InitializeAfterSceneLoad()
    {
        GameObject settingsGO = GameObject.Find("Settings");
        PrefabHub = settingsGO.GetComponent<PrephabHub>();
        MoveSettings = settingsGO.GetComponent<MovementSettings>();
        DestroySettings = settingsGO.GetComponent<DestroySettings>();
        CameraMoveSettings = settingsGO.GetComponent<CameraMovementSettings>();

        DisableSystems();
    }

    private static void DisableSystems()
    {
        World.Active.GetExistingManager<SetPursuitBehaviorDataSystem>().Enabled = false;
        World.Active.GetExistingManager<SetDistanceToPlayerSystem>().Enabled = false;
        World.Active.GetExistingManager<PlayerVelocitySystem>().Enabled = false;
    }
}
