using NUnit.Framework;
using System;

namespace FootballTeam.Tests
{
    public class Tests
    {
        private FootballTeam team;
        [SetUp]
        public void Setup()
        {
            team = new FootballTeam("Botev", 15);
        }

        [Test]
        public void FootballPlayerCtor()
        {
            FootballPlayer footballPlayer = new FootballPlayer("Gosho", 15, "Forward");

            Assert.AreEqual("Gosho", footballPlayer.Name);
            Assert.AreEqual(15, footballPlayer.PlayerNumber);
            Assert.AreEqual("Forward", footballPlayer.Position);
        }
        [TestCase("")]
        [TestCase(null)]
        public void NameShoudlThrowException(string name)
        {
            Assert.Throws<ArgumentException>(() =>
            {
                FootballPlayer footballPlayer = new FootballPlayer(name, 15, "Forward");
            }, "Name cannot be null or empty!");
        }
        [TestCase(0)]
        [TestCase(22)]
        [TestCase(100)]
        [TestCase(-100)]
        public void NumberShoudlThrowException(int number)
        {
            Assert.Throws<ArgumentException>(() =>
            {
                FootballPlayer footballPlayer = new FootballPlayer("Gosho", number, "Forward");
            }, "Player number must be in range [1,21]");
        }
        [TestCase("")]
        [TestCase(null)]
        [TestCase("Ivan")]
        public void PossitionShoudlThrowException(string possition)
        {
            Assert.Throws<ArgumentException>(() =>
            {
                FootballPlayer footballPlayer = new FootballPlayer("Gosho", 15, possition);
            }, "Invalid Position");
        }
        [Test]
        public void Scose()
        {
            FootballPlayer footballPlayer = new FootballPlayer("Gosho", 15, "Forward");
            footballPlayer.Score();
            Assert.AreEqual(1, footballPlayer.ScoredGoals);
        }
        [Test]
        public void GoalScored()
        {
            FootballPlayer footballPlayer = new FootballPlayer("Gosho", 15, "Forward");

            Assert.AreEqual(0, footballPlayer.ScoredGoals);
        }
        [Test]
        public void TeamCtor()
        {
            FootballTeam teamCurr = new FootballTeam("Botev", 15);

            Assert.AreEqual("Botev", teamCurr.Name);
            Assert.AreEqual(15, teamCurr.Capacity);

        }
        [TestCase(null)]
        [TestCase("")]
        public void NameTeamException(string name)
        {
            Assert.Throws<ArgumentException>(() =>
            {
                FootballTeam teamCurr = new FootballTeam(name, 15);

            }, "Name cannot be null or empty!");
        }
        [TestCase(14)]
        [TestCase(-100)]
        public void CapacityTeamException(int capacity)
        {
            Assert.Throws<ArgumentException>(() =>
            {
                FootballTeam teamCurr = new FootballTeam("Gosho", capacity);

            }, "Capacity min value = 15");
        }
        [Test]
        public void AddPlayer()
        {
            FootballPlayer footballPlayer = new FootballPlayer("Gosho", 15, "Forward");
            FootballPlayer footballPlayer2 = new FootballPlayer("Gosho1", 16, "Forward");
            FootballPlayer footballPlayer3 = new FootballPlayer("Gosho2", 17, "Forward");
            FootballPlayer footballPlayer4 = new FootballPlayer("Gosho3", 18, "Forward");
            FootballPlayer footballPlayer5 = new FootballPlayer("Gosho4", 19, "Forward");
            FootballPlayer footballPlayer6 = new FootballPlayer("Gosh5o", 12, "Forward");
            FootballPlayer footballPlayer7 = new FootballPlayer("Gosho6", 13, "Forward");
            FootballPlayer footballPlayer8 = new FootballPlayer("Gosho7", 15, "Forward");
            FootballPlayer footballPlayer9 = new FootballPlayer("Gosho8", 14, "Forward");
            FootballPlayer footballPlayer10 = new FootballPlayer("Gosho9", 5, "Forward");
            FootballPlayer footballPlayer11 = new FootballPlayer("Gosho0", 1, "Forward");
            FootballPlayer footballPlayer12 = new FootballPlayer("Gosho11", 2, "Forward");
            FootballPlayer footballPlayer13 = new FootballPlayer("Gosho12", 3, "Forward");
            FootballPlayer footballPlayer14 = new FootballPlayer("Gosho13", 4, "Forward");
            FootballPlayer footballPlayer15 = new FootballPlayer("Gosho14", 9, "Forward");
            FootballPlayer footballPlayer100 = new FootballPlayer("Gosho124", 8, "Forward");


            FootballTeam teamCurr = new FootballTeam("Gosho", 15);


            teamCurr.AddNewPlayer(footballPlayer);
            teamCurr.AddNewPlayer(footballPlayer2);
            teamCurr.AddNewPlayer(footballPlayer3);
            teamCurr.AddNewPlayer(footballPlayer4);
            teamCurr.AddNewPlayer(footballPlayer5);
            teamCurr.AddNewPlayer(footballPlayer6);
            teamCurr.AddNewPlayer(footballPlayer7);
            teamCurr.AddNewPlayer(footballPlayer8);
            teamCurr.AddNewPlayer(footballPlayer9);
            teamCurr.AddNewPlayer(footballPlayer10);
            teamCurr.AddNewPlayer(footballPlayer11);
            teamCurr.AddNewPlayer(footballPlayer13);
            teamCurr.AddNewPlayer(footballPlayer15);
            teamCurr.AddNewPlayer(footballPlayer14);
            teamCurr.AddNewPlayer(footballPlayer12);


            string expected = "No more positions available!";
            string actual = teamCurr.AddNewPlayer(footballPlayer100);
            Assert.AreEqual(expected, actual);
            Assert.AreEqual(15, teamCurr.Players.Count);

        }
        [Test]
        public void AddPlayerTrue()
        {
            FootballPlayer player = new FootballPlayer("Gosho124", 8, "Forward");


            FootballTeam teamCurr = new FootballTeam("Gosho", 15);


            string expected = $"Added player {player.Name} in position {player.Position} with number {player.PlayerNumber}";
            string actual = teamCurr.AddNewPlayer(player);
        }
        [Test]
        public void PickPlayer()
        {
            FootballPlayer player = new FootballPlayer("Ivan", 8, "Forward");

            FootballTeam teamCurr = new FootballTeam("Gosho", 15);
            teamCurr.AddNewPlayer(player);

            var actual = teamCurr.PickPlayer("Ivan");
            Assert.AreEqual(player, actual);
        }
        [Test]
        public void PickPlayerNull()
        {
            

            FootballTeam teamCurr = new FootballTeam("Gosho", 15);
            

            var actual = teamCurr.PickPlayer(null);
            Assert.AreEqual(null, actual);
        }
        [Test] 
        public void PlayerScore()
        {
            FootballPlayer player = new FootballPlayer("Ivan", 8, "Forward");
            FootballTeam teamCurr = new FootballTeam("Gosho", 15);
            teamCurr.AddNewPlayer(player);

            string exMessages = $"{player.Name} scored and now has {1} for this season!";

            string actual = teamCurr.PlayerScore(8);
            Assert.AreEqual(exMessages, actual);
        }
    }
}