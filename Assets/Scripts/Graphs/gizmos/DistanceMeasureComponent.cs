using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Graphs
{
    public class DistanceMeasureComponent : MonoBehaviour
    {
        [SerializeField] private Transform Target0;
        [SerializeField] private Transform Target1;

        [ContextMenu("Measure")]
        public void Measure()
        {
            Debug.LogError(Vector3.Distance(Target0.position, Target1.position));
        }
    }
}