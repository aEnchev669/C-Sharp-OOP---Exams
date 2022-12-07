using CarRacing.Models.Maps.Contracts;
using CarRacing.Models.Racers.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarRacing.Models.Maps
{
    public class Map : IMap
    {
        public string StartRace(IRacer racerOne, IRacer racerTwo)
        {
            if (!racerOne.IsAvailable() && !racerTwo.IsAvailable())
            {
                return "Race cannot be completed because both racers are not available!";
            }
            if (!racerOne.IsAvailable())
            {
                return $"{racerTwo} wins the race! {racerOne} was not available to race!";
            }
            else if (!racerTwo.IsAvailable())
            {
                return $"{racerOne} wins the race! {racerTwo} was not available to race!";
            }

            racerOne.Race();
            racerTwo.Race();

            double racingBehaviorMultiplierRacerOne = 0;
            if (racerOne.RacingBehavior == "strict")
            {
                racingBehaviorMultiplierRacerOne = 1.2;
            }
            else if (racerOne.RacingBehavior == "aggressive")
            {
                racingBehaviorMultiplierRacerOne = 1.1;
            }
            double chanceOfWinningForRacerOne = racerOne.Car.HorsePower * racerOne.DrivingExperience * racingBehaviorMultiplierRacerOne;

            double racingBehaviorMultiplierRacerTwo = 0;
            if (racerTwo.RacingBehavior == "strict")
            {
                racingBehaviorMultiplierRacerTwo = 1.2;
            }
            else if (racerTwo.RacingBehavior == "aggressive")
            {
                racingBehaviorMultiplierRacerTwo = 1.1;
            }
            double chanceOfWinningForRacerTwo = racerTwo.Car.HorsePower * racerTwo.DrivingExperience * racingBehaviorMultiplierRacerTwo;

            IRacer winner = null;
            if (chanceOfWinningForRacerOne > chanceOfWinningForRacerTwo)
            {
                winner = racerOne;
            }
            else
            {
                winner = racerTwo;
            }

            return ($"{racerOne.Username} has just raced against {racerTwo.Username}! {winner.Username} is the winner!");
        }
    }
}
