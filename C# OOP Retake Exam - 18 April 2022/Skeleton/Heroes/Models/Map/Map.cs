using Heroes.Models.Contracts;
using Heroes.Models.Heroes;
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

            foreach (var player in players)
            {
                if (player.GetType() == typeof(Knight)) //!!!!!!!!!!!!!!!!!!!!!!!!! typeOf(Knight)
                {
                    knights.Add(player);
                }
                else if (player.GetType() == typeof(Barbarian))
                {
                    barbarians.Add(player);
                }
            }
            int deadBarbrarians = 0;
            int deadKnights = 0;
           
            while (barbarians.Count != 0 && knights.Count != 0)
            {
                foreach (var knight in knights)
                {
                    foreach (var barbarian in barbarians)
                    {
                        barbarian.TakeDamage(knight.Weapon.DoDamage());
                    }
                }

                deadBarbrarians += barbarians.RemoveAll(h => h.Health <= 0);

                foreach (var barabrian in barbarians)
                {
                    foreach (var knight in knights)
                    {
                        knight.TakeDamage(barabrian.Weapon.DoDamage());
                    }
                }

                deadKnights += knights.RemoveAll(h => h.Health <= 0);           //!!!!!!!!!!!!!!!!!!!!!!!!!!!  += !
            }

            string winner = knights.Count <= 0 ? $"The barbarians took {deadBarbrarians} casualties but won the battle." : $"The knights took { deadKnights} casualties but won the battle.";

            return winner;
        }
    }
}
