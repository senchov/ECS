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
    
    [RuntimeTestAtribute]
    public IEnumerator Test4()
    {
        Debug.LogError("op");
        yield return new WaitForSeconds(1);
        Debug.LogError("op1");
    }
    
    [RuntimeTestAtribute]
    public IEnumerator Test5()
    {
        Debug.LogError("op");
        yield return new WaitForSeconds(1);
        Debug.LogError("op1");
    }
    
    [RuntimeTestAtribute]
    public IEnumerator Test6()
    {
        Debug.LogError("op");
        yield return new WaitForSeconds(1);
        Debug.LogError("op1");
    }

    [RuntimeTestAtribute]
    public IEnumerator Test7()
    {
        Debug.LogError("op");
        yield return new WaitForSeconds(1);
        Debug.LogError("op1");
    }

    [RuntimeTestAtribute]
    public IEnumerator Test8()
    {
        Debug.LogError("op");
        yield return new WaitForSeconds(1);
        Debug.LogError("op1");
    }

    [RuntimeTestAtribute]
    public IEnumerator Test9()
    {
        Debug.LogError("op");
        yield return new WaitForSeconds(1);
        Debug.LogError("op1");
    }

    [RuntimeTestAtribute]
    public IEnumerator Test10()
    {
        Debug.LogError("op");
        yield return new WaitForSeconds(1);
        Debug.LogError("op1");
    }

    [RuntimeTestAtribute]
    public IEnumerator Test11()
    {
        Debug.LogError("op");
        yield return new WaitForSeconds(1);
        Debug.LogError("op1");
    }

    [RuntimeTestAtribute]
    public IEnumerator Test12()
    {
        Debug.LogError("op");
        yield return new WaitForSeconds(1);
        Debug.LogError("op1");
    }

    [RuntimeTestAtribute]
    public IEnumerator Test13()
    {
        Debug.LogError("op");
        yield return new WaitForSeconds(1);
        Debug.LogError("op1");
    }

    [RuntimeTestAtribute]
    public IEnumerator Test14()
    {
        Debug.LogError("op");
        yield return new WaitForSeconds(1);
        Debug.LogError("op1");
    }

    [RuntimeTestAtribute]
    public IEnumerator Test15()
    {
        Debug.LogError("op");
        yield return new WaitForSeconds(1);
        Debug.LogError("op1");
    }

    [RuntimeTestAtribute]
    public IEnumerator Test16()
    {
        Debug.LogError("op");
        yield return new WaitForSeconds(1);
        Debug.LogError("op1");
    }

    [RuntimeTestAtribute]
    public IEnumerator Test17()
    {
        Debug.LogError("op");
        yield return new WaitForSeconds(1);
        Debug.LogError("op1");
    }

    [RuntimeTestAtribute]
    public IEnumerator Test18()
    {
        Debug.LogError("op");
        yield return new WaitForSeconds(1);
        Debug.LogError("op1");
    }

    [RuntimeTestAtribute]
    public IEnumerator Test19()
    {
        Debug.LogError("op");
        yield return new WaitForSeconds(1);
        Debug.LogError("op1");
    }

    [RuntimeTestAtribute("lol")]
    public IEnumerator Test20()
    {
        Debug.LogError("op");
        yield return new WaitForSeconds(1);
        Debug.LogError("op1");
    }

    [RuntimeTestAtribute]
    public IEnumerator Test21()
    {
        Debug.LogError("op");
        yield return new WaitForSeconds(1);
        Debug.LogError("op1");
    }

    [RuntimeTestAtribute]
    public IEnumerator Test22()
    {
        Debug.LogError("op");
        yield return new WaitForSeconds(1);
        Debug.LogError("op1");
    }

    

}