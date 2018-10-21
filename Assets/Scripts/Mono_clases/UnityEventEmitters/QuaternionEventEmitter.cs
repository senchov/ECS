using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;

public class QuaternionEventEmitter : MonoBehaviour
{
    [SerializeField] private quaternion TargetQuaternion;
    [SerializeField] private QuaternionUnityEvent Event;
    [SerializeField] private float3 Euler;

    public void Emmit()
    {
        Event.Invoke(TargetQuaternion);
    }

    [ContextMenu("EuelerToQuaternin")]
    public void EuelerToQuaternin()
    {
        quaternion q = quaternion.Euler(Euler);
        Debug.LogError(q.value);
    }

}
