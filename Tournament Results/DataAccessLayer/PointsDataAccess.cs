using Dapper;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using Tournament_Results.Models;
using Tournament_Results.ViewModels;

namespace Tournament_Results.Data
{
    public class PointsDataAccess : IPointsDataAccess
    {
        readonly IConfiguration _configuration;
        readonly SqlConnection _connection;

        public PointsDataAccess(IConfiguration configuration)
        {
            _configuration = configuration;
            _connection = new SqlConnection(_configuration.GetConnectionString("DatabaseConnection"));
        }

        public async Task<int> ReadMajorPoints(int placing)
        {
            string query = "SELECT * FROM Points WHERE PointsMajor.ID = @placing";

            var result = await _connection.QueryFirstOrDefaultAsync<int>(query, new { placing });

            return result;
        }

        public async Task<int> ReadPremierPpoints(int placing)
        {
            string query = "SELECT * FROM Points WHERE PremierPoints.ID = @placing";

            var result = await _connection.QueryFirstOrDefaultAsync<int>(query, new { placing });

            return result;
        }

        public async Task<int> ReadSingleAttendancePlacing(Guid playerID, Guid tournamentID)
        {
            string procedure = "spSingleAttendancePlacementSELECT";
            var @params = new
            {
                playerID,
                tournamentID
            };
            var points = await _connection
                .QueryFirstOrDefaultAsync<int>(procedure, @params, commandType: CommandType.StoredProcedure);
            return points;
        }

        public async Task<Points> ReadSingleAttendancePremierPlacing(Guid playerID, Guid tournamentID)
        {
            string procedure = "spSingleAttendancePremierPlacementSELECT";
            var @params = new
            {
                playerID,
                tournamentID
            };
            Points attendance = _connection
                .QueryFirstOrDefaultAsync<Points>(procedure, @params, commandType: CommandType.StoredProcedure).Result;
            return attendance;
        }
    }

    public interface IPointsDataAccess
    {
        Task<int> ReadMajorPoints(int Placing);
        Task<int> ReadPremierPpoints(int Placing);
        //Task<Points> ReadSingleAttendancePlacing(Guid playerID, Guid tournamentID);
        //Task<Points> ReadSingleAttendancePremierPlacing(Guid playerID, Guid tournamentID);
    }
}
