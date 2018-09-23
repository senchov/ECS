using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class RectangleUISetter : MonoBehaviour
{
    [SerializeField] private CameraMovementSettings Settings;
    [SerializeField] private Sliders UISliders;
    [SerializeField] private GameObjectEntity PlayerEntity;
    [SerializeField] private GameObjectEntity CameraEntity;
    [SerializeField] private EntityManagerProviderSO ManagerProvider;
    [SerializeField] private InputFields MaxSpeedInput;

    private void Awake()
    {
        MaxSpeedInput.Player.onEndEdit.AddListener(SetPlayerMaxSpeed);
        MaxSpeedInput.Camera.onEndEdit.AddListener(SetCameraMaxSpeed);
    }

    private void Update()
    {
        Settings.RectangleSettings.HighOffset = UISliders.Up.value;
        Settings.RectangleSettings.DownOffset = UISliders.Down.value;
        Settings.RectangleSettings.RightOffset = UISliders.Right.value;
        Settings.RectangleSettings.LeftOffset = UISliders.Left.value;
    }

    public void SetPlayerMaxSpeed(string input)
    {
        VelocityData velData = ManagerProvider.GetEntityManager.GetComponentData<VelocityData>(PlayerEntity.Entity);
        float maxSpeed = float.Parse(MaxSpeedInput.Player.text);       
        velData.MaxSpeed = maxSpeed;  
        ManagerProvider.GetEntityManager.SetComponentData(PlayerEntity.Entity, velData);
    }

    public void SetCameraMaxSpeed(string input)
    {
        VelocityData velData = ManagerProvider.GetEntityManager.GetComponentData<VelocityData>(CameraEntity.Entity);
        float maxSpeed = float.Parse(MaxSpeedInput.Camera.text);
        velData.MaxSpeed = maxSpeed;
        ManagerProvider.GetEntityManager.SetComponentData(CameraEntity.Entity, velData);
    }

    [Serializable]
    private class Sliders
    {
        public Slider Up;
        public Slider Down;
        public Slider Left;
        public Slider Right;
    }

    [Serializable]
    private class InputFields
    {
        public InputField Player;
        public InputField Camera;
    }

}
