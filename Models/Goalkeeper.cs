using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Handball.Models
{
    public class Goalkeeper : Player
    {
        private const double Raiting = 2.5;
        public Goalkeeper(string name) 
            : base(name, Raiting)
        {
        }

        public override void DecreaseRating()
        {
            base.Rating -= 1.25;
        }

        public override void IncreaseRating()
        {
            base.Rating += 0.75;
        }
    }
}
