using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectEnabler : MonoBehaviour {

    [SerializeField] private GameObject GO;
    private bool IsEnable = false;


    public void Click()
    {
        IsEnable = !IsEnable;
        GO.SetActive(IsEnable);
    }

}
