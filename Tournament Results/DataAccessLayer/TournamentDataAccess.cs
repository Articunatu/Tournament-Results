using Dapper;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.Common;
using Microsoft.Data.SqlClient;
using Tournament_Results.Models;

namespace Tournament_Results.Data
{
    public class TournamentDataAccess : ITournamentDataAccess
    {
        readonly IConfiguration _configuration;
        readonly SqlConnection _connection;

        public TournamentDataAccess(IConfiguration configuration)
        {
            _configuration = configuration;
            _connection = new SqlConnection(_configuration.GetConnectionString("DatabaseConnection"));
        }

        public async Task<Tournament> Create(Tournament created)
        {
            string procedure = "PlayerINSERT";
            created.ID = Guid.NewGuid();
            var @params = new 
            { 
                created.ID,
                created.Title,
                created.PrizeMoney,
                created.AttendeesAmount
            };

            Tournament player = _connection
                .QueryAsync<Tournament>(procedure, @params, commandType: CommandType.StoredProcedure).Result.FirstOrDefault();
            return player;
        }

        public async Task<IEnumerable<Tournament>> ReadAll(DateTime startDate, DateTime endDate)
        {
            string procedure = "spAllTournamentsSELECT";
            var @params = new
            {
                startDate,
                endDate
            };
            IEnumerable<Tournament> tournaments = _connection
                .QueryAsync<Tournament>(procedure, @params, commandType: CommandType.StoredProcedure).Result;
            return tournaments;
        }

        public async Task<Tournament> ReadSingle(Guid id)
        {
            string procedure = "spSingleTournamentSELECT";
            var param = new { ID = id };
            Tournament player = _connection
                .QueryAsync<Tournament>(procedure, param, commandType: CommandType.StoredProcedure).Result.FirstOrDefault();
            return player;
        }

        public async Task<Tournament> Update(Guid id, Tournament updated)
        {
            string procedure = "PlayerUPDATE";
            var @params = new
            {
                updated.ID,
                updated.Title,
                updated.PrizeMoney,
                updated.AttendeesAmount
            };
            Tournament player = _connection
                .QueryAsync<Tournament>(procedure, @params, commandType: CommandType.StoredProcedure).Result.FirstOrDefault();
            return player;
        }

        public async Task<Tournament> Delete(Guid id)
        {
            string procedure = "PlayerDELETE";
            var param = new { ID = id };
            Tournament player = _connection
                .QueryAsync<Tournament>(procedure, param, commandType: CommandType.StoredProcedure).Result.FirstOrDefault();
            return player;
        }
    }

    public interface ITournamentDataAccess
    {
        Task<IEnumerable<Tournament>> ReadAll(DateTime startDate, DateTime endDate);
        Task<Tournament> ReadSingle(Guid id);
    }
}
