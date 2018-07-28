using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementSettings : MonoBehaviour
{
    public float MoveBulletSpeed = 3.0f;

    [Header("SmallController")]
    public float SmallControllerForwardSpeed = 15;
    public float SmallControllerArriveSpeed = 1;
    public float SmallControllerBackSpeed = 10;
    public float SmallControllerStopRadius = 0.01f;
}
