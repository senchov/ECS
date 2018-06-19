using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuntimeTest2 : MonoBehaviour
{
    [RuntimeTestAtribute]
    public IEnumerator Lol()
    {
        Debug.LogError("RuntimeTest2");
        yield return new WaitForSeconds(1);
    }
}