using System;
using EncounterScene;
using NUnit.Framework;
using UnityEngine;

namespace Tests
{
    public class GridControlTest
    {
        [Test]
        public void GetPlateCoordinatesTest()
        {
            Vector2 evenFuncRes = GridControl.GetPlateCoordinates(new Vector2(5, 5),
                new Vector2(10, 10),9);
            Vector2 oddFuncRes = GridControl.GetPlateCoordinates(new Vector2(5, 5),
                new Vector2(9, 9), 9);

            Vector2 evenRes = new Vector2(-4.5f, -4.5f);
            Vector2 oddRes = Vector2.zero;

            Assert.True(evenRes.Equals(evenFuncRes) && oddRes.Equals(oddFuncRes));
        }

        [Test]
        public void GetPlateByCoordinatesTest()
        {
            Vector2 evenFuncRes = GridControl.GetPlateByCoordinates(new Vector2(-4.5f, -4.5f),
                new Vector2(10, 10), 9);
            Vector2 oddFuncRes = GridControl.GetPlateByCoordinates(Vector2.zero, new Vector2(9, 9), 9);
            
            Vector2 evenRes = new Vector2(5, 5);
            Vector2 oddRes = new Vector2(5, 5);
            
            Assert.True(evenRes.Equals(evenFuncRes) && oddRes.Equals(oddFuncRes));
        }

        [Test]
        public void MapToGridCoordinatesTest()
        {
            Vector2 evenFuncRes = GridControl.MapToGridCoordinates(new Vector2(-8.9f, -0.1f),
                new Vector2(10, 10), 9);
            Vector2 oddFuncRes = GridControl.MapToGridCoordinates(new Vector2(-4.33f, 4.33f),
                new Vector2(9, 9), 9);

            Vector2 evenRes = new Vector2(-4.5f, -4.5f);
            Vector2 oddRes = Vector2.zero;

            Assert.True(evenRes.Equals(evenFuncRes) && oddRes.Equals(oddFuncRes));
        }
    }
}
