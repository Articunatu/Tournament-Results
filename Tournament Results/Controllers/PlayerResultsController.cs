using Tournament_Results.Data;
using Tournament_Results.Models;
using Tournament_Results.ViewModels;

namespace Tournament_Results.Controllers
{
    public class PlayerResultsController : IPlayerResultsController
    {
        readonly IAttendanceDataAccess _attendanceDataAccess;
        readonly IPlayerDataAccess _playerDataAccess;

        public PlayerResultsController(IAttendanceDataAccess attendanceDataAccess, IPlayerDataAccess playerDataAccess)
        {
            _attendanceDataAccess = attendanceDataAccess;
            _playerDataAccess = playerDataAccess;
        }

        public async Task<PlayerInfoVM> ReadAllPlayersTournaments(Guid playerID, string? startDate, string? endDate)
        {
            var attendances = await _attendanceDataAccess.ReadAllPlayersTournaments(playerID, startDate, endDate);

            var player = await _playerDataAccess.ReadSingle(playerID);

            PlayerVM playerModel = new(player);

            PlayerInfoVM playerInfo = new(attendances, playerModel);

            return playerInfo;
        }
    }

    public interface IPlayerResultsController
    {
        Task<PlayerInfoVM> ReadAllPlayersTournaments(Guid id, string? startDate, string? endDate);
    }
}