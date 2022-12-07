namespace Robots.Tests
{
    using NUnit.Framework;
    using System;

    public class RobotsTests
    {

        [Test]
        public void Test_If_Constructor_Returns_Correct_Values()
        {
            RobotManager robotManager = new RobotManager(5);
            Assert.AreEqual(5, robotManager.Capacity);
            Assert.AreEqual(0, robotManager.Count);
        }
        [Test]
        public void Test_If_Constructor_Capacity_Recieves_Less_Than_Zero_Should_Throw_ArgumentException()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                RobotManager robotManager = new RobotManager(-5);
            });
        }
        [Test]
        public void Test_Add_If_Two_Robots_With_The_Same_Name_Are_Added_Should_Throw_InvalidOperationException()
        {
            RobotManager robotManager = new RobotManager(5);
            Robot robot = new Robot("Peshkata", 100);
            Robot robot2 = new Robot("Peshkata", 150);
            robotManager.Add(robot);
            Assert.Throws<InvalidOperationException>(() =>
            {
                robotManager.Add(robot2);
            });
        }
        [Test]
        public void Test_Add_If_Trying_To_Add_When_There_Is_No_Capacity_Should_Throw_InvalidOperationException()
        {
            RobotManager robotManager = new RobotManager(0);
            Robot robot = new Robot("Peshkata", 100);
            Assert.Throws<InvalidOperationException>(() =>
            {
                robotManager.Add(robot);
            });
        }
        [Test]
        public void Test_Add_Should_Increase_Count_If_Add_Works_Correctly()
        {
            RobotManager robotManager = new RobotManager(5);
            Robot robot = new Robot("Peshkata", 100);
            Robot robot2 = new Robot("Goshkata", 420);
            robotManager.Add(robot);
            robotManager.Add(robot2);
            Assert.AreEqual(2, robotManager.Count);
        }
        [Test]
        public void Test_Remove_If_Robot_With_That_Name_Is_Not_Found_Should_Throw_InvalidOperationException()
        {
            RobotManager robotManager = new RobotManager(5);
            Robot robot = new Robot("Peshkata", 100);
            robotManager.Add(robot);
            Assert.Throws<InvalidOperationException>(() =>
            {
                robotManager.Remove("WKEOI");
            });
        }
        [Test]
        public void Test_Remove_If_Robot_Decreases_Count_Remove_Works_Correctly()
        {
            RobotManager robotManager = new RobotManager(5);
            Robot robot = new Robot("Peshkata", 100);
            Robot robot2 = new Robot("Goshkata", 123);
            Robot robot3 = new Robot("Chichkaat", 123);
            robotManager.Add(robot);
            robotManager.Add(robot2);
            robotManager.Add(robot3);
            robotManager.Remove("Peshkata");
            Assert.AreEqual(2, robotManager.Count);
        }
        [Test]
        public void Test_Work_If_Robot_Name_Is_Null_Should_Throw_InvalidOperationException()
        {
            RobotManager robotManager = new RobotManager(5);
            Robot robot = new Robot("Peshkata", 100);
            robotManager.Add(robot);
            Assert.Throws<InvalidOperationException>(() =>
            {
                robotManager.Work("asdwqasdqwqwas", "dsakdsa", 50);
            });
        }
        [Test]
        public void Test_Work_If_Robot_Battery_Is_Less_Than_BatteryUsage_Should_Throw_InvalidOperationException()
        {
            RobotManager robotManager = new RobotManager(5);
            Robot robot = new Robot("Peshkata", 100);
            robotManager.Add(robot);
            Assert.Throws<InvalidOperationException>(() =>
            {
                robotManager.Work("Peshkata", "Bachkai", 500);
            });
        }
        [Test]
        public void Test_Work_If_Battery_Is_Correctly_Reduced()
        {
            RobotManager robotManager = new RobotManager(5);
            Robot robot = new Robot("Peshkata", 100);
            robotManager.Add(robot);
            robotManager.Work("Peshkata", "Bachkai", 25);
            Assert.AreEqual(75, robot.Battery);
        }
        [Test]
        public void Test_Charge_If_Recieves_Robot_Name_That_Doesnt_Exist_Should_Throw_InvalidOperationException()
        {
            RobotManager robotManager = new RobotManager(5);
            Robot robot = new Robot("Peshkata", 100);
            robotManager.Add(robot);
            Assert.Throws<InvalidOperationException>(() =>
            {
                robotManager.Charge("NESASHTESTVUVA");
            });
        }
        [Test]
        public void Test_Charge_If_Recharges_The_Robots_Battery_After_Usage()
        {
            RobotManager robotManager = new RobotManager(5);
            Robot robot = new Robot("Peshkata", 100);
            robotManager.Add(robot);
            robotManager.Work("Peshkata", "Bachkai", 75);
            robotManager.Charge("Peshkata");
            Assert.AreEqual(100, robot.Battery);
        }
    }
}