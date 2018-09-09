using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.Mathematics;

public class CameraMovementSettings : MonoBehaviour
{
    [SerializeField] public RectSettings RectangleSettings;
 
    [Serializable]
    public class RectSettings
    {
        public float HighOffset;
        public float DownOffset;
        public float RightOffset;
        public float LeftOffset;
    }

    [Serializable]
    private class CameraBorder
    {
        public float3 HighPoint;
        public float3 LowPoint;
        public float3 RightPoint;
        public float3 LeftPoint;
    }
}
