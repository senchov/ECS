using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;



public class MovementSystemTest
{
    // A UnityTest behaves like a coroutine in PlayMode
    // and allows you to yield null to skip a frame in EditMode
    [UnityTest]
    public IEnumerator MovementTest()
    {
      
        yield return null;
        
        yield return new WaitForSeconds(20.0f);
        Debug.Log("op");
    }

    private static Object LoadTestPrefab()
    {
        return Resources.Load("test", typeof(GameObject));
    }
}