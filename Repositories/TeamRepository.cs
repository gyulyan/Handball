using Handball.Models;
using Handball.Models.Contracts;
using Handball.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Handball.Repositories
{
    public class TeamRepository : IRepository<ITeam>
    {
        private readonly List<ITeam> teams;

        public TeamRepository()
        {
            teams = new List<ITeam>();
        }

        public IReadOnlyCollection<ITeam> Models => teams.AsReadOnly();

        public void AddModel(ITeam model)
        {
            teams.Add(model);
        }

        public bool ExistsModel(string name)
        {
            var team = teams.Where(x => x.Name == name).FirstOrDefault();
            return teams.Contains(team);
        }

        public ITeam GetModel(string name)
        {
            if (teams.Any(x => x.Name == name))
            {
                return teams.Where(x => x.Name == name).FirstOrDefault();
            }
            else
            {
                return null;
            }
        }

        public bool RemoveModel(string name)
        {
            var team = teams.FirstOrDefault(x => x.Name == name);
            if (team != null)
            {
                return teams.Remove(team);
            }
            return false;
        }
    }
}
