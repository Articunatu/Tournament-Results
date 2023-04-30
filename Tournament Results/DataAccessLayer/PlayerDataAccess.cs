using Dapper;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.Common;
using Microsoft.Data.SqlClient;
using Tournament_Results.Models;
using Tournament_Results.ViewModels;

namespace Tournament_Results.Data
{
    public class PlayerDataAccess : IPlayerDataAccess
    {
        readonly IConfiguration _configuration;
        readonly SqlConnection _connection;

        public PlayerDataAccess(IConfiguration configuration)
        {
            _configuration = configuration;
            _connection = new SqlConnection(_configuration.GetConnectionString("DatabaseConnection"));
        }

        public async Task<IEnumerable<Player>> ReadAllTournamentsPlayers(Guid ID)
        {
            string procedure = "spAllTournamentsPlayersSELECT";
            var @params = new { ID };
            IEnumerable<Player> players = _connection.QueryAsync<Player>
                (procedure, @params, commandType: CommandType.StoredProcedure).Result;
            return players;
        }

        public async Task<Player> Create(Player created)
        {
            string procedure = "PlayerINSERT";
            created.ID = Guid.NewGuid();
            var @params = new 
            { 
                created.ID,
                created.Tag, 
                created.FirstName, 
                created.LastName 
            };

            Player player = _connection.QueryAsync<Player>
                (procedure, @params, commandType: CommandType.StoredProcedure).Result.FirstOrDefault();
            return player;
        }

        public async Task<IEnumerable<Player>> ReadAll()
        {
            string procedure = "AllPlayersSELECT";
            IEnumerable<Player> players = _connection.QueryAsync<Player>
                (procedure, commandType: CommandType.StoredProcedure).Result;
            return players;
        }

        public async Task<Player> ReadSingle(Guid id)
        {
            string procedure = "SinglePlayerSELECT";
            var param = new { ID = id };
            Player player = _connection.QueryAsync<Player>
                (procedure, param, commandType: CommandType.StoredProcedure).Result.FirstOrDefault();
            return player;
        }

        public async Task<Player> Update(Guid id, Player updated)
        {
            string procedure = "PlayerUPDATE";
            var @params = new
            {
                updated.ID,
                updated.Tag,
                updated.FirstName,
                updated.LastName
            };
            Player player = _connection.QueryAsync<Player>
                (procedure, @params, commandType: CommandType.StoredProcedure).Result.FirstOrDefault();
            return player;
        }

        public async Task<Player> Delete(Guid id)
        {
            string procedure = "PlayerDELETE";
            var param = new { ID = id };
            Player player = _connection.QueryAsync<Player>
                (procedure, param, commandType: CommandType.StoredProcedure).Result.FirstOrDefault();
            return player;
        }
    }

    public interface IPlayerDataAccess
    {
        Task<IEnumerable<Player>> ReadAllTournamentsPlayers(Guid ID);
        Task<Player> ReadSingle(Guid id);
    }
}
