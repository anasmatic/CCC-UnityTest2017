using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProject1
{
    [TestClass]
    public class GetValedPositionTest1
    {
        [TestMethod]
        public void GetRandomBlock_WhenCalled_ReturnValidBlock()
        {
            //valid block means (0,0) , (0,1) , (1,0) , (1,1)
            for (int i = 0; i < 10; i++)
            {
                int row, col;
                MapForTest.GetRandomBlock_Test(out row, out col);
                
                Assert.IsTrue(row == 0 || row == 1, "row");
                Assert.IsTrue(col == 0 || col == 1, "col");
                //Assert.IsTrue(row == 1 && col == 0, "always");//should fail
                //Assert.IsTrue(row == 0 && col == 0, "always");//should fail
                //Assert.IsTrue(row == 1 && col == 1, "always");//should fail

            }
        }

        [TestMethod]
        public void GetBlockFirstCell_Pass0AsBlock_Return0Always()
        {
            //valid block means (0,0) , (0,1) , (1,0) , (1,1)
            for (int i = 10; i < 100; i+=10)
            {
                int firstCell = MapForTest.GetBlockFirstCell_Test(i, 0);
                Assert.IsTrue(firstCell == 0 );
            }
        }
        [TestMethod]
        public void GetBlockFirstCell_Pass1AsBlock_ReturnHalf()
        {
            var q = new int[]{ 10, 21 , 30 , 45 };
            var a = new int[] { 5, 10, 15, 22 };
            for (int i = 0; i < q.Length; i ++)
            {
                int firstCell = MapForTest.GetBlockFirstCell_Test(q[i], 1);
                Assert.IsTrue(firstCell == a[i]);
            }
        }
        [TestMethod]
        public void GetBlockLastCell_Pass0AsBlock_ReturnHalf()
        {
            var q = new int[] { 10, 20, 30, 45 };
            var a = new int[] { 5, 10, 15, 22 };
            for (int i = 0; i < q.Length; i++)
            {
                int lastCell = MapForTest.GetBlockLastCell_Test(q[i], 0);
                Assert.IsTrue(lastCell == a[i]);
            }
        }
        [TestMethod]
        public void GetBlockLastCell_Pass1AsBlock_ReturnTotalLength()
        {
            var q = new int[] { 10, 20, 30, 45 };
            var a = new int[] { 10, 20, 30, 45 };
            for (int i = 0; i < q.Length; i++)
            {
                int lastCell = MapForTest.GetBlockLastCell_Test(a[i], 1);
                Assert.IsTrue(lastCell == a[i]);
            }
        }
        [TestMethod]
        public void GetRandomIndex_PassRange_ReturnValInRange()
        {
            var blockX = new int[] { 0 , 0, 1, 1 };
            var blockY = new int[] { 0 , 1, 0, 1 };
            var Width = new int[] { 10 , 10, 10, 10 };
            var Height = new int[]{ 10 , 10, 10, 10 };
            //real results
            var _X_from = new int[] { 0, 0 , 5, 5 };
            var _X_to = new int[]  { 4 , 4 , 9, 9 };
            var _Y_from = new int[]{ 0 , 5 , 0, 5 };
            var _Y_to = new int[]  { 4 , 9 , 4, 9 };
            for (int i = 0; i < blockX.Length; i++)
            {
                int X, Y;
                MapForTest.GetRandomIndex_Test(out X, out Y, blockX[i], blockY[i],
                                                        Width[i], Height[i]);
                Assert.IsTrue(X >= _X_from[i]);
                Assert.IsTrue(X <= _X_to[i]);
                Assert.IsTrue(Y >= _Y_from[i]);
                Assert.IsTrue(Y <= _Y_to[i]);
            }
        }
    }
}
