using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

[assembly: InternalsVisibleTo("GetValedPositionTest1")]
public class Map : IObserver
{
    System.Random rand;
    private static Map instance;
    public static Map Instance
    {
        get
        {
            if (instance == null)
                instance = new Map();

            return instance;
        }
    }

    int[][] map;
    public void InitMap(int width,int height)
    {
        map = new int[height][];
        for(int i = 0; i<height; i ++)
        {
            map[i] = new int[width];
        };

        rand = new System.Random();

        //PrintMap();
    }

    private void PrintMap()
    {
        string arr = "";
        for (int i = 0; i < map.Length; i++)
        {
            for (int j = 0; j < map[i].Length; j++)
                arr += map[i][j] + ", ";
            arr += "\n";
        }
        //it is upside down
        UnityEngine.Debug.Log("arr w:" + map[0].Length + "*h" + map.Length + ":\n" + arr);
    }

    internal Vector3 GetValedPosition()
    {
        //first choose a random block;
        /*
          0 1 2 3 4 5 6 7
        0[       |       ]
        1[ (0,0) | (1,0) ]
        2[_______|_______]
        3[ (0,1) | (1,1) ]
        4[       |       ]
        */
        int blockX, blockY;
        GetRandomBlock(out blockX, out blockY);

        //get random point in this block
        //i and j index in multidim array
        GetRandomIndex(ref blockX, ref blockY);
        //UnityEngine.Debug.Log("blockX:" + blockX + ", blockY:" + blockY);
        if (map[blockX][blockY] > 0)//is blocked
            return GetValedPosition();//go recursive !

        return new Vector3(blockX, 0, blockY);
    }

    private void GetRandomBlock( out int blockX, out int blockY)
    {
        blockX = rand.Next(0, 2);//return 0 or 1
        blockY = rand.Next(0, 2);//return 0 or 1
    }

    private void GetRandomIndex(ref int blockX, ref int blockY)
    {
        var width = map[0].Length;
        var height = map.Length;
        var fisrtX = GetBlockFirstCell(width, blockX);
        var lastX = GetBlockLastCell(width, blockX);
        var firstY = GetBlockFirstCell(height, blockY);
        var lastY = GetBlockLastCell(height, blockX);
        //UnityEngine.Debug.Log("X:"+ fisrtX+">"+lastX+", Y:"+ firstY+">"+lastY);
        blockX = rand.Next(fisrtX, lastX);
        blockY = rand.Next(firstY, lastY);
    }

    private int GetBlockFirstCell(int total, int blockIndex )
    {
        int returnVal = blockIndex * (int)(total * .5);
        if (returnVal == 0) returnVal++;
        return returnVal;
    }
    private int GetBlockLastCell(int total , int blockIndex)
    {
        var half = total * .5;
        int returnValue = (int)(half + (blockIndex * half));
        if (returnValue == total) returnValue--;
        return returnValue;
    }

    public void NotifyWith(Vector3 incomingPos, Vector3 lastPos)
    {
        //Debug.Log("lastPos: " + (int)lastPos.z + "," + (int)lastPos.x);
        //Debug.Log("incomingPos: " + (int)incomingPos.z + "," + (int)incomingPos.x);

        map[map.Length - (int)lastPos.z][(int)lastPos.x] = 0;
        map[map.Length - (int)incomingPos.z][(int)incomingPos.x] = 1;

        //PrintMap();
    }

    public void Notify(){}

    // this is root level singlton class, it won't be distroyed with scene change,
    // it might be useful for game genral settings more than map tracking, but this is an example of what it can do
    public void DeleteSingleton() {
        instance = null;
    }
}

//replica of same logic in Map class to test the private functions that returns available postition to new fruit
public class MapForTest
{
    
    public static void GetRandomBlock_Test(out int blockX, out int blockY)
    {
        System.Random rand = new System.Random();
        blockX = rand.Next(0, 2);//return 0 or 1
        blockY = rand.Next(0, 2);//return 0 or 1
    }
    //same function in Map but chnage the params to be easier for testing
    public static void GetRandomIndex_Test(out int X, out int Y, int blockX, int blockY, int width, int height)
    {
        System.Random rand = new System.Random();
        
        X = rand.Next(GetBlockFirstCell_Test(width, blockX), GetBlockLastCell_Test(width, blockX));
        Y = rand.Next(GetBlockFirstCell_Test(height, blockY), GetBlockLastCell_Test(height, blockY));
    }

    public static int GetBlockFirstCell_Test(int total, int blockIndex)
    {
        return blockIndex * (int)(total * .5);
    }
    public static int GetBlockLastCell_Test(int total, int blockIndex)
    {
        float half = total * .5f;
        return (int) (half + (blockIndex * half));
    }
}