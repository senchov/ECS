using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Float2EventEmitter : MonoBehaviour {

    [SerializeField] private float2 PlayerVelocity;
    [SerializeField] private Float2UnityEvent Event;

    public void Emmit()
    {        
        Event.Invoke(PlayerVelocity);
    }
}
