using System;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Events;

[Serializable] public class Float2UnityEvent : UnityEvent<float2> { }
[Serializable] public class Float3UnityEvent : UnityEvent<float3> { }
[Serializable] public class QuaternionUnityEvent : UnityEvent<quaternion> { }

