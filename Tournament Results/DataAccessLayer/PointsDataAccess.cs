using Dapper;
using System.Data;
using System.Data.SqlClient;
using Tournament_Results.Models;

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

        public async Task<Points> ReadPremierPoints(int placing)
        {
            string query = "SELECT * FROM Points WHERE PremierPoints.ID = @placing";

            var result = await _connection.QueryFirstOrDefaultAsync<Points>(query, new { placing });

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
            var placing = await _connection
                .QueryFirstOrDefaultAsync<Attendance>(procedure, @params, commandType: CommandType.StoredProcedure);

            if (placing is null)
            {
                return 111;
            }

            return placing.Placing;
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
        Task<Points> ReadPremierPoints(int Placing);
        Task<int> ReadSingleAttendancePlacing(Guid playerID, Guid tournamentID);
        Task<Points> ReadSingleAttendancePremierPlacing(Guid playerID, Guid tournamentID);
    }

    public class PointsDictionary
    {
        public Dictionary<int, int> Major = new()
        {
            { 1, 1000},
            { 2, 600},
            { 3, 430},
            { 4, 290},
            { 5, 215},
            { 7, 145},
            { 9, 90},
            { 13, 60},
            { 17, 45},
            { 25, 30},
            { 33, 20},
            { 49, 10 },
            { 65, 5 },
            { -1, 900},
            { -2, 700},
            { -5, 240},
            { -7, 160},
            { 6, 190},
            { 8, 130}
        };

        public Dictionary<int, int> Premier = new()
        {
            { 1, 2000},
            { 2, 1200},
            { 3, 860},
            { 4, 580},
            { 5, 430},
            { 7, 290},
            { 9, 215},
            { 13, 145},
            { 17, 90},
            { 25, 60},
            { 33, 30},
            { 49, 20},
            { 65, 10},
            { 129, 5},
            { -1, 1800},
            { -2, 1400},
            { -5, 470},
            { -7, 330},
            { 6, 390},
            { 8, 260}
        };
    }
}
