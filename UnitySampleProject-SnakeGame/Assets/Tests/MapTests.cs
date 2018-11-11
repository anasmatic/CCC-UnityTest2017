using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using System;

public class MapTests {

    [Test]
    public void MapTests_GetValidPosition_ReturnsPositionThatIsNotBlockedBySnake() {
        int[][] map;
        for (int i = 0; i < 200; i++)
        {
            //reset map
            Map.Instance.InitMap(20, 20);
            map = Map.Instance.GetMapNow();
            //mimic a snake movement that blocks parts of the map, errr let's randomly block some cells in the map
            RandomlyBlock(map);
            //Map.Instance.PrintMap();
            var pos = Map.Instance.GetValidPosition();
            
            float posVal = (float)map[(int)pos.x][(int)pos.z];
            Assert.AreEqual(posVal, 0);
        }
    }

    private void RandomlyBlock(int[][] map)
    {
        System.Random rand = new System.Random();
        for (int i = 0; i < map.Length; i++){
            for (int j = 0; j < map[i].Length; j++){
                map[i][j] = rand.Next(0,2);//randome from 0 to 1
            }
        }
    }
}
