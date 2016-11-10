using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SplitBill;
using System.Collections.Generic;

namespace SplitBill
{
    [TestClass]
    public class SplitBill_Test
    {
        [TestMethod]
        public void Test_cost_per_person()
        {
            //Arrange
            SplitBill a = new SplitBill();
            List<float> Bills_of_participants_per_trip = new List<float>();
            Bills_of_participants_per_trip.Add(30.00f); //10.00+20.00 for first trip first participant
            Bills_of_participants_per_trip.Add(36.02f); //15+15.01+3.00+3.01 Second Participant
            Bills_of_participants_per_trip.Add(18.00f); // 5.00+9.00+4.00  Third Participant

            List<float> result = new List<float>();

            //Act
            result.AddRange(a.Cost_per_person(Bills_of_participants_per_trip));

            List<float> expected_result = new List<float>();
            //Assert
            expected_result.Add(-1.99f);
            expected_result.Add(-8.01f);
            expected_result.Add(+10.01f);

            CollectionAssert.AreEqual(expected_result, result);

        }
    }
}
