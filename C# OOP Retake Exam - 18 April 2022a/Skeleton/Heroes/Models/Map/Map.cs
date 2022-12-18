using Heroes.Models.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Heroes.Models.Map
{
    public class Map : IMap
    {
        public string Fight(ICollection<IHero> players)
        {
            List<IHero> knights = new List<IHero>();
            List<IHero> barbarians = new List<IHero>();

            foreach (var hero in players)
            {
                if (hero.GetType().Name == "Knight")
                {
                    knights.Add(hero);
                }
                else if (hero.GetType().Name == "Barbarian")
                {
                    barbarians.Add(hero);
                }
            }

            int numOfDeatsForTheKnights = 0;
            int numOfDeatsForTheBarbarians = 0;

            while (barbarians.Count != 0 && knights.Count != 0)
            {

                foreach (var hero in knights)
                {

                    foreach (var hero1 in barbarians)
                    {

                        hero1.TakeDamage(hero.Weapon.DoDamage());

                    }
                }

                numOfDeatsForTheKnights += knights.RemoveAll(h => h.Health <= 0);

                foreach (var hero in barbarians)
                {

                    foreach (var hero1 in knights)
                    {

                        hero1.TakeDamage(hero.Weapon.DoDamage());

                    }

                }
                numOfDeatsForTheBarbarians += barbarians.RemoveAll(h => h.Health <= 0);

            }

            if (barbarians.Any(b => b.IsAlive))
            {
                return $"The barbarians took {numOfDeatsForTheBarbarians} casualties but won the battle.";
            }
            else
            {
                return $"The knights took {numOfDeatsForTheKnights} casualties but won the battle.";
            }
        }
    }
}
