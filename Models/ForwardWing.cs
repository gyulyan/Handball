using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Handball.Models
{
    public class ForwardWing : Player
    {
        private const double Raiting = 5.5;
        public ForwardWing(string name) 
            : base(name, Raiting)
        {
        }

        public override void DecreaseRating()
        {
            base.Rating -= 0.75;
        }

        public override void IncreaseRating()
        {
            base.Rating += 1.25;
        }
    }
}
