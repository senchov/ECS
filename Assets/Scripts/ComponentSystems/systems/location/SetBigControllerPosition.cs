﻿using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;
using UnityEngine;
using Assets.Scripts.Core;

public class SetBigControllerPosition : ComponentSystem
{
    private struct Data
    {
        public readonly int Length;
        public ComponentDataArray<BigController> Controller;
        public ComponentDataArray<Position> Positions;
    }

    [Inject] Data ControllerData;

    public static float2 Pos;

    protected override void OnUpdate()
    {
        Vector3 bottomLeftCorner = Camera.main.ScreenToWorldPoint(new Vector3(0,0, -20.0f));

        for (int i = 0; i < ControllerData.Length; i++)
        {
            float x = bottomLeftCorner.x + ControllerData.Controller[i].Offset.x;
            float y = bottomLeftCorner.y + ControllerData.Controller[i].Offset.y;
            Position pos = new Position { Value = new float3(x, y, ControllerData.Positions[i].Value.z) };
            IsometricConverter isometricConverter = new IsometricConverter();

            ControllerData.Positions[i] = isometricConverter.Convert(pos);
            Pos = new float2(x,y);
        }
    }
}
