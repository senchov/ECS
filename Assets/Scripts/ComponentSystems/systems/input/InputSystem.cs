using Unity.Entities;
using Unity.Mathematics;
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
        bool isFire = Input.GetButtonDown("Fire2");
        bool isMouse0pressed = GetMouse0Input();
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(GetMousePosition());


        for (int i = 0; i < Data.Length; i++)
        {
            Data.Inputs[i].Horizontal = x;
            Data.Inputs[i].Vertical = y;
            Data.Inputs[i].IsFire = isFire;
            Data.Inputs[i].MousePos = new float2(mousePos.x, mousePos.y);
            Data.Inputs[i].IsMouse0Pressed = isMouse0pressed;
        }
    }

    private static Vector3 GetMousePosition()
    {
        Vector3 mousePos = Vector3.zero;
#if UNITY_IOS || UNITY_ANDROID
        for (int i = 0; i < Input.touchCount; i++)
        {
            mousePos = Input.GetTouch(0).position;
        }
#else
        mousePos = Input.mousePosition;
#endif
#if UNITY_EDITOR
        mousePos = Input.mousePosition;
#endif

        return mousePos;
    }

    private bool GetMouse0Input()
    {
        bool isPressed = false;
#if UNITY_IOS || UNITY_ANDROID
        for (int i = 0; i < Input.touchCount; i++)
        {
            isPressed = true;
        }
#else
isPressed = Input.GetMouseButton(0);
#endif

#if UNITY_EDITOR
        isPressed = Input.GetMouseButton(0);
#endif
        return isPressed;
    }
}

