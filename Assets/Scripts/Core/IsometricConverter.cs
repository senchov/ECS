using System;
using Unity.Transforms;
using UnityEngine;

namespace Assets.Scripts.Core
{
    struct IsometricConverter
    {
        public Position Convert(Position  cartesianPos)
        {
            Position isometric = new Position();
            isometric.Value.x = cartesianPos.Value.x - cartesianPos.Value.y;
            isometric.Value.z = (cartesianPos.Value.x - cartesianPos.Value.y) * 0.5f;
            return isometric;
        }
    }
}
