﻿using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    [SerializeField] public float Speed;
    [SerializeField] public Queue<Vector3> Path;
}