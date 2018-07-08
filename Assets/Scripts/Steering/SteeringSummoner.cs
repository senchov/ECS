using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteeringSummoner : MonoBehaviour
{
    [SerializeField] private GameObject SteerEntity;
	
	void Update ()
    {
        if (Input.GetMouseButton(1))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);            
            mousePos.z = 0;
            Instantiate(SteerEntity,mousePos,Quaternion.identity);
        }
	}
}
