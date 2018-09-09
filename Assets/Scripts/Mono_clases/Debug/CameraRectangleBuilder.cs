using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRectangleBuilder : MonoBehaviour
{

    [SerializeField] private CameraMovementSettings CameraSettings;
    [SerializeField] private LineRenderer Line;

    private Camera MainCamera;

    private Camera GetMainCamera
    {
        get
        {
            if (MainCamera == null)
                MainCamera = Camera.main;
            return MainCamera;
        }
    }

    private void Update()
    {
        BuildRect();
    }

    [ContextMenu("BuildRect")]
    public void BuildRect()
    {
        Vector3 leftPoint, rightPoint, downPoint, upPoint;
        GetBorderPoints(out leftPoint, out rightPoint, out downPoint, out upPoint);

        Vector3 downLeft, upLeft, upRight, downRight;
        downLeft = new Vector3(rightPoint.x,downPoint.y,0);
        upLeft = new Vector3(rightPoint.x, upPoint.y, 0);
        upRight = new Vector3(leftPoint.x, upPoint.y,0);
        downRight = new Vector3(leftPoint.x, downPoint.y, 0);

        Line.positionCount = 5;
        Line.SetPosition(0, downLeft);
        Line.SetPosition(1, upLeft);
        Line.SetPosition(2, upRight);
        Line.SetPosition(3, downRight);
        Line.SetPosition(4, downLeft);
    }

    private void GetBorderPoints(out Vector3 leftPoint, out Vector3 rightPoint, out Vector3 downPoint, out Vector3 upPoint)
    {
        float highOffset = CameraSettings.RectangleSettings.HighOffset;
        float downOffset = CameraSettings.RectangleSettings.DownOffset;
        float rightOffset = CameraSettings.RectangleSettings.RightOffset;
        float leftOffset = CameraSettings.RectangleSettings.LeftOffset;

        float halfWidth = GetMainCamera.pixelWidth * 0.5f;
        float halfHeight = GetMainCamera.pixelHeight * 0.5f;

        leftPoint = GetMainCamera.ScreenToWorldPoint(new Vector2(halfWidth - halfWidth * leftOffset, halfHeight));
        rightPoint = GetMainCamera.ScreenToWorldPoint(new Vector2(halfWidth + halfWidth * rightOffset, halfHeight));
        downPoint = GetMainCamera.ScreenToWorldPoint(new Vector2(halfWidth, halfHeight + halfHeight * downOffset));
        upPoint = GetMainCamera.ScreenToWorldPoint(new Vector2(halfWidth, halfHeight - halfHeight * highOffset));
    }
}
