using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Data.Common;
using Tournament_Results.Models;
using Tournament_Results.ViewModels;

namespace Tournament_Results.Data
{
    public class AttendanceDataAccess : IAttendanceDataAccess
    {
        readonly IConfiguration _configuration;
        readonly SqlConnection _connection;

        public AttendanceDataAccess(IConfiguration configuration)
        {
            _configuration = configuration;
            _connection = new SqlConnection(_configuration.GetConnectionString("DatabaseConnection"));
        }

        public async Task<IEnumerable<TournamentResultVM>> ReadAllPlayersTournaments(Guid playerID, string? startDate, string? endDate)
        {
            string procedure = "spPlayersTournamentsSELECT";
            var @params = new
            {
                playerID,
                startDate = DateTime.Parse(startDate),
                endDate = DateTime.Parse(endDate)
            };
            IEnumerable<TournamentResultVM> results = _connection
                .QueryAsync<TournamentResultVM>(procedure, @params, commandType: CommandType.StoredProcedure).Result;
            return results;
        }

        public async Task<IEnumerable<PlayerResultVM>> ReadSingleTournamentsPlayers(Guid id)
        {
            string procedure = "spAllTournamentsPlayersSELECT";
            var @params = new { id };
            IEnumerable<PlayerResultVM> attendances = _connection
                .QueryAsync<PlayerResultVM>(procedure, @params, commandType: CommandType.StoredProcedure).Result;
            return attendances;
        }

        public async Task<int> ReadPlayersPointSum(string procedure, Guid id, string? startDate, string? endDate)
        {
            var @params = new
            {
                id,
                startDate,
                endDate
            };
            int sum = _connection
                .QueryFirstOrDefaultAsync<int>(procedure, @params, commandType: CommandType.StoredProcedure).Result;
            return sum;
        }

        public Task<Points> ReadSingleAttendancePlacing(Guid playerID, Guid tournamentID)
        {
            throw new NotImplementedException();
        }

        public Task<Points> ReadSingleAttendancePremierPlacing(Guid playerID, Guid tournamentID)
        {
            throw new NotImplementedException();
        }
    }

    public interface IAttendanceDataAccess
    {
        Task<IEnumerable<PlayerResultVM>> ReadSingleTournamentsPlayers(Guid id);
        Task<IEnumerable<TournamentResultVM>> ReadAllPlayersTournaments(Guid id, string? startDate, string? endDate);
        Task<Points> ReadSingleAttendancePlacing(Guid playerID, Guid tournamentID);
        Task<Points> ReadSingleAttendancePremierPlacing(Guid playerID, Guid tournamentID);
        Task<int> ReadPlayersPointSum(string procedure, Guid id, string? startDate, string? endDate);
    }
}