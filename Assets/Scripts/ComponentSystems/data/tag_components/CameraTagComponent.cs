using Unity.Entities;
using System;

[Serializable]
public struct CameraTag : IComponentData
{    
}

public class CameraTagComponent : ComponentDataWrapper<CameraTag>
{
}
