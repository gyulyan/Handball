using Handball.Core.Contracts;
using Handball.Models;
using Handball.Models.Contracts;
using Handball.Repositories;
using Handball.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Handball.Core
{
    public class Controller : IController
    {
        private readonly PlayerRepository playerRepository;
        private readonly TeamRepository teamRepository;
        public Controller()
        {
            playerRepository = new PlayerRepository();
            teamRepository = new TeamRepository();
        }

        public string LeagueStandings()
        {
            StringBuilder sb = new StringBuilder();

            var orderedTeams = teamRepository.Models
                .OrderByDescending(x=>x.PointsEarned)
                .ThenByDescending(x=>x.OverallRating)
                .ThenBy(x=>x.Name)
                .ToList();

            sb.AppendLine("***League Standings***");
            foreach (var team in orderedTeams)
            {
                sb.AppendLine(team.ToString());
            }

            return sb.ToString().TrimEnd();
        }

        public string NewContract(string playerName, string teamName)
        {
            if (!playerRepository.ExistsModel(playerName))
            {
                return string.Format(OutputMessages.PlayerNotExisting, playerName, playerRepository.GetType().Name);
            }

            if(!teamRepository.ExistsModel(teamName))
            {
                return string.Format(OutputMessages.TeamNotExisting, teamName, teamRepository.GetType().Name);
            }

            var player = playerRepository.GetModel(playerName);
            if (player.Team != null)
            {
                return string.Format(OutputMessages.PlayerAlreadySignedContract, playerName, player.Team);
            }
            else
            {
                player.JoinTeam(teamName);
                var team = teamRepository.GetModel(teamName);
                team.SignContract(player);
                return string.Format(OutputMessages.SignContract, playerName, teamName);
            }
        }

        public string NewGame(string firstTeamName, string secondTeamName)
        {
           var firstTeam = teamRepository.GetModel(firstTeamName);
            var secondTeam = teamRepository.GetModel(secondTeamName);
            var firstTeamRating = firstTeam.OverallRating;
            var secondTeamRating = secondTeam.OverallRating;
            ITeam winner = null;
            ITeam loser= null;

            if (firstTeamRating == secondTeamRating)
            {
                firstTeam.Draw();
                secondTeam.Draw();

                return string.Format(OutputMessages.GameIsDraw, firstTeamName, secondTeamName);
            }
            else
            {
                if (firstTeamRating > secondTeamRating)
                {
                    winner = firstTeam;
                    loser = secondTeam;
                }
                else
                {
                    winner = secondTeam;
                    loser = firstTeam;
                }
            }

            winner.Win();
            loser.Lose();

            return string.Format(OutputMessages.GameHasWinner, winner.Name, loser.Name);
            
        }

        public string NewPlayer(string typeName, string name)
        {
            IPlayer player = null;

            if (typeName != "Goalkeeper" && typeName != "CenterBack" && typeName != "ForwardWing")
            {
                return string.Format(OutputMessages.InvalidTypeOfPosition, typeName);
            }
            else
            {
                if (playerRepository.ExistsModel(name))
                {
                    return string.Format(OutputMessages.PlayerIsAlreadyAdded, name, playerRepository.GetType().Name, playerRepository.GetModel(name).GetType().Name);
                }

                if (typeName == "Goalkeeper")
                {
                    player = new Goalkeeper(name);
                }
                else if (typeName == "CenterBack")
                {
                    player = new CenterBack(name);
                }
                else if (typeName == "ForwardWing")
                {
                    player = new ForwardWing(name);
                }
            }

            playerRepository.AddModel(player);
            return string.Format(OutputMessages.PlayerAddedSuccessfully, name);
        }

        public string NewTeam(string name)
        {
            if (teamRepository.ExistsModel(name))
            {
                return string.Format(OutputMessages.TeamAlreadyExists, name, teamRepository.GetType().Name);
            }

            ITeam team = new Team(name);
            teamRepository.AddModel(team);
            return string.Format(OutputMessages.TeamSuccessfullyAdded, name, teamRepository.GetType().Name);

        }

        public string PlayerStatistics(string teamName)
        {
            StringBuilder stringBuilder = new StringBuilder();
           ITeam team = teamRepository.GetModel(teamName);
           var orderedList = team
                .Players
                .OrderByDescending(x=>x.Rating)
                .ThenBy(x=>x.Name)
                .ToList();
            stringBuilder.AppendLine($"***{teamName}***");

            foreach (var player in orderedList)
            {
                stringBuilder.AppendLine(player.ToString());
            }

            return stringBuilder.ToString().TrimEnd();
        }
    }
}
