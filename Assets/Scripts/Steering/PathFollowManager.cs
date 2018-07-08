using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFollowManager : MonoBehaviour
{
    [SerializeField] private List<PathHolder> Holders;
    [SerializeField] private Transform[] Points;
    [SerializeField] private float PointsRadius = 3.0f;
    
    public Vector2[] Path;

    private void Start()
    {
        ChangePath();
    }

    [ContextMenu("ChangePath")]
    private void ChangePath()
    {
        Path = Holders[Random.Range(0, Holders.Count)].Path;
    }

    [ContextMenu ("SavePath")]
    public void SavePath()
    {
        Vector2[] pointsToSave = new Vector2[Points.Length];
        for (int i = 0; i < Points.Length; i++)
        {
            pointsToSave[i] = Points[i].position;
        }
        Holders.Add(new PathHolder(pointsToSave));
    }

    private void OnDrawGizmos()
    {
        if (Path == null || Path.Length <2)
            return;

        Gizmos.color = Color.magenta;

        for (int i = 0; i < Path.Length -1; i++)
        {
            Gizmos.color = Color.magenta;
            Gizmos.DrawLine(Path[i], Path[i + 1]);
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(Path [i], PointsRadius);     
        }

        Gizmos.DrawWireSphere(Path[Path.Length - 1], PointsRadius);
    }

    public float GetPointRadius()
    {
        return PointsRadius;
    }

    [System.Serializable]
    private class PathHolder
    {
        public PathHolder(Vector2[] path)
        {
            Path = path;
        }
        public Vector2[] Path;
    }
	
}
