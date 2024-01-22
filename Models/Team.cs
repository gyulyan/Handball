using Handball.Models.Contracts;
using Handball.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Handball.Models
{
    public class Team : ITeam
    {
        private string name;
        private int pointsEarned;
        private List<IPlayer> players;

        public Team(string name)
        {
            Name = name;
            players = new List<IPlayer>();
            pointsEarned = 0;
        }

        public string Name
        {
            get => name;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(ExceptionMessages.TeamNameNull);
                }
                name = value;
            }
        }

        public int PointsEarned
        {
            get => pointsEarned;
            private set
            {
                pointsEarned = value;
            }
        }

        public double OverallRating
        {
            get
            {
                if (players.Count == 0)
                {
                    return 0;
                }

                double totalRating = players.Sum(player => player.Rating);
                return Math.Round(totalRating / players.Count, 2);
            }
        }

        public IReadOnlyCollection<IPlayer> Players { get => players; }


        public void Draw()
        {
            pointsEarned++;

            var player = players.FirstOrDefault(x => x.GetType().Name == "Goalkeeper");

            if (player != null)
            {
                player.IncreaseRating();
            }

        }

        public void Lose()
        {
            foreach (var player in players)
            {
                player.DecreaseRating();
            }
        }

        public void SignContract(IPlayer player)
        {
            players.Add(player);
        }

        public void Win()
        {
            pointsEarned += 3;
            foreach (var player in players)
            {
                player.IncreaseRating();
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"Team: {Name} Points: {PointsEarned}");
            sb.AppendLine($"--Overall rating: {OverallRating}");

            if (players.Count > 0)
            {
                sb.Append("--Players: ");
                sb.Append(string.Join(", ", players.Select(x => x.Name)));
               
            }
            else
            {
                sb.AppendLine("--Players: none");
            }

            return sb.ToString().TrimEnd();
        }
    }
}
