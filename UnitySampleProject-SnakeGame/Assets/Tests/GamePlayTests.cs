using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class GamePlayTests {


    [UnityTest]
    public IEnumerator GamePlayTests_Pool_HasCreatedInstancesAsMuchAsPoolSize() {
        //define pool size value
        int poolSize = 21;
        //create gameObject with Pool script attached to it
        FruitsPool pool = new GameObject().AddComponent<FruitsPool>();
        pool.ConstructForUnityTest(poolSize);
        //init pool, with dummy data
        pool.InitPool( new IObserver[]{} );
        // Use the Assert class to test conditions.
        Assert.AreEqual(poolSize, pool.pool.Count);
        // yield to skip a frame
        
        yield return null;
    }
}
