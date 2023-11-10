using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tournament_Results.Data;

namespace Tournament_Results.Tests
{
    [TestClass]
    public class PointsTest
    {
        readonly IPointsDataAccess _pointsData;

        public PointsTest(IPointsDataAccess pointsData)
        {
            _pointsData = pointsData;
        }

        [TestMethod]
        [Priority(1)]
        public async void Get_Points_2000_When_Player_Places_First_And_Tournament_Is_Premier()
        {
            int expectedPoints = 2000;

            var FirstPlacer = Guid.Parse("b93d767c-a276-4644-974d-78c218f2a989");
            var PremierTour = Guid.Parse("4bd6761b-09ab-4a44-8f4d-32c67b6eb3ab");

            int actualPoints = (await _pointsData.ReadPremierPoints(1)).Value;

            Assert.AreEqual(expectedPoints, actualPoints);
        }
    }
}
