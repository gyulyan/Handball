using Handball.Models.Contracts;
using Handball.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Handball.Repositories
{
    public class PlayerRepository : IRepository<IPlayer>
    {
        private readonly List<IPlayer> players;

        public PlayerRepository()
        {
            players = new List<IPlayer>();
        }

        public IReadOnlyCollection<IPlayer> Models => players.AsReadOnly();

        public void AddModel(IPlayer model)
        {
           players.Add(model);
        }

        public bool ExistsModel(string name)
        {
            var player = players.Where(x => x.Name == name).FirstOrDefault();
            return players.Contains(player);
        }

        public IPlayer GetModel(string name)
        {
           if(players.Any(x => x.Name == name))
            {
                return players.Where(x => x.Name == name).FirstOrDefault();
            }
           else 
            {
                return null; 
            }
        }

        public bool RemoveModel(string name)
        {
            var player = players.Where(x => x.Name == name).FirstOrDefault();
            return players.Remove(player);
        }
    }
}
