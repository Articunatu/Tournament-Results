using Tournament_Results.Data;
using Tournament_Results.ViewModels;

namespace Tournament_Results.Controllers
{
    public class PlayerResultsController : IPlayerResultsController
    {
        readonly IAttendanceDataAccess _attendanceDataAccess;
        readonly IPlayerDataAccess _playerDataAccess;
        readonly IPointsDataAccess _pointsDataAccess;

        public PlayerResultsController(IAttendanceDataAccess attendanceDataAccess, IPlayerDataAccess playerDataAccess, IPointsDataAccess pointsDataAccess)
        {
            _attendanceDataAccess = attendanceDataAccess;
            _playerDataAccess = playerDataAccess;
            _pointsDataAccess = pointsDataAccess;
        }

        public async Task<PlayerInfoViewModel> ReadAllPlayersTournaments(Guid playerID, string? startDate, string? endDate)
        {

            var attendances = await _attendanceDataAccess.ReadAllPlayersTournaments(playerID, startDate, endDate);

            PointsDictionary pD = new();

            foreach (var item in attendances)
            {
                if (item.IsPremier)
                {
                    if (pD.Premier.TryGetValue(item.Placing, out int points))
                    {
                        item.Placing = points;
                    }
                }
                else
                {
                    if (pD.Major.TryGetValue(item.Placing, out int points))
                    {
                        item.Placing = points;
                    }
                }
                
            }

            var player = await _playerDataAccess.ReadSingle(playerID);


            PlayerDTO playerModel = new(player);

            PlayerInfoViewModel playerInfo = new(attendances, playerModel);

            return playerInfo;
        }
    }

    public interface IPlayerResultsController
    {
        Task<PlayerInfoViewModel> ReadAllPlayersTournaments(Guid id, string? startDate, string? endDate);
    }
}