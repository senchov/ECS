using Unity.Entities;
using UnityEngine;

class InputSystem : ComponentSystem
{
    private struct Group
    {
        public readonly int Length; 
        public ComponentArray<InputData> Inputs;
    }

    [Inject] private Group Data;

    protected override void OnUpdate()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        bool isFire = Input.GetButtonDown("Fire1");

        for (int i = 0; i < Data.Length; i++)
        {
            Data.Inputs[i].Horizontal = x;
            Data.Inputs[i].Vertical = y;
            Data.Inputs[i].IsFire = isFire;
        }
    }
}

