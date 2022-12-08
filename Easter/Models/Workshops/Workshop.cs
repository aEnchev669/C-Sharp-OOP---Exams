using Easter.Models.Bunnies.Contracts;
using Easter.Models.Eggs.Contracts;
using Easter.Models.Workshops.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Easter.Models.Workshops
{
    public abstract class Workshop : IWorkshop
    {
        public void Color(IEgg egg, IBunny bunny)
        {
            if (bunny.Energy > 0 && bunny.Dyes.Any(d => !d.IsFinished()))
            {
                foreach (var dye in bunny.Dyes.Where(d => !d.IsFinished()))
                {
                    if (!egg.IsDone() && bunny.Energy > 0 && bunny.Dyes.Any(d => !d.IsFinished()))
                    {
                        egg.GetColored();
                        bunny.Work();
                        dye.Use();
                    }
                    else if (egg.IsDone() || bunny.Energy == 0)
                    {
                        break;
                    }
                }
            }
        }
    }
}
