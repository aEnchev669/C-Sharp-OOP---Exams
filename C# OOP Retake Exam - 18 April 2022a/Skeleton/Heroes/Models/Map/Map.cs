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

            while (barbarians.Any(b => b.IsAlive) || knights.Any(b => b.IsAlive))
            {

                foreach (var hero in knights)
                {
                    if (hero.IsAlive)
                    {
                        foreach (var hero1 in barbarians)
                        {
                            if (hero1.Health > 0)
                            {
                                hero1.TakeDamage(hero.Weapon.DoDamage());
                                if (hero1.Health <= 0)
                                {
                                    numOfDeatsForTheBarbarians++;
                                }
                            }
                        }
                    }
                }


                foreach (var hero in barbarians)
                {
                    if (hero.IsAlive)
                    {
                        foreach (var hero1 in knights)
                        {
                            if (hero1.Health > 0)
                            {
                                hero1.TakeDamage(hero.Weapon.DoDamage());
                                if (hero1.Health <= 0)
                                {
                                    numOfDeatsForTheKnights++;
                                }
                            }
                        }
                    }
                }
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
