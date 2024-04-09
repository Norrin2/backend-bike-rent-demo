using BikeRent.Domain.Entities;
using BikeRent.Domain.ValueObject;

namespace BikeRent.Tests.Domain.Deliveryman
{
    public class RentTests
    {
        [Fact]
        public void Should_Create_Rent_Instance()
        {
            // Arrange
            var bike = new Bike("4fcde0d3-4202-4ffc-8f57-adb7fc6dab8f", "test", 2000);
            var plan = RentPlan.Days15;

            // Act
            var rent = new Rent(bike, plan);

            // Assert
            Assert.NotNull(rent);
            Assert.Equal(bike, rent.Bike);
            Assert.Equal(plan, rent.Plan);
            Assert.True(rent.StartDate > DateTime.Now);
            Assert.Null(rent.EndDate);
        }

        [Fact]
        public void Should_Return_Notifications_Rent_With_No_Bike()
        {
            // Arrange
            var plan = RentPlan.Days15;

            // Act
            var rent = new Rent(null, plan);

            // Assert
            Assert.NotNull(rent);
            Assert.False(rent.IsValid);
            Assert.Equal(nameof(Bike), rent.Notifications.First().Key);
        }

        [Fact]
        public void Should_Calculate_Cost_Without_Extra_Days()
        {
            // Arrange
            var bike = new Bike("4fcde0d3-4202-4ffc-8f57-adb7fc6dab8f", "test", 2000);
            var plan = RentPlan.Days7;
            var rent = new Rent(bike, plan);
            var returnDate = rent.StartDate.AddDays(10);

            // Act
            var cost = rent.FinishRentAndGetCost(returnDate);

            // Assert
            Assert.Equal(7 * (int)plan + 3 * 50, cost);
        }

        [Fact]
        public void Should_Calculate_Cost_With_Extra_Days()
        {
            // Arrange
            var bike = new Bike("4fcde0d3-4202-4ffc-8f57-adb7fc6dab8f", "test", 2000);
            var plan = RentPlan.Days15;
            var rent = new Rent(bike, plan);
            var returnDate = rent.StartDate.AddDays(20);

            // Act
            var cost = rent.FinishRentAndGetCost(returnDate);

            // Assert
            Assert.Equal(15 * (int)plan + 5 * 50, cost);
        }

        [Fact]
        public void Should_Calculate_Cost_With_Return_Date_Earlier_Than_End_Date()
        {
            // Arrange
            var bike = new Bike("4fcde0d3-4202-4ffc-8f57-adb7fc6dab8f", "test", 2000);
            var plan = RentPlan.Days15;
            var rent = new Rent(bike, plan);
            var returnDate = rent.StartDate.AddDays(10);

            // Act
            var cost = rent.FinishRentAndGetCost(returnDate);
            decimal penalityCost = 0.4m;

            // Assert
            Assert.Equal((10 * (int)plan) + (5 * ((int)plan) * penalityCost), cost);
        }

        [Fact]
        public void Should_Calculate_Cost_With_Return_Date_Later_Than_End_Date()
        {
            // Arrange
            var bike = new Bike("4fcde0d3-4202-4ffc-8f57-adb7fc6dab8f", "test", 2000);
            var plan = RentPlan.Days15;
            var rent = new Rent(bike, plan);
            var returnDate = rent.StartDate.AddDays(20);

            // Act
            var cost = rent.FinishRentAndGetCost(returnDate);

            // Assert
            Assert.Equal(15 * (int)plan + 5 * 50, cost);
        }
    }
}
