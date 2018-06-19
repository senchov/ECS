using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuntimeTest : MonoBehaviour
{
    [RuntimeTestAtribute]
    public IEnumerator Test()
    {
        Debug.LogError("chick0");
        yield return new WaitForSeconds(1);
        Debug.LogError("chick");
    }

    [RuntimeTestAtribute]
    public IEnumerator Test2()
    {
        Debug.LogError("op");
        yield return new WaitForSeconds(1);
        Debug.LogError("op1");
    }
}