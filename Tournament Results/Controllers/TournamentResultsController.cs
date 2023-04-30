using Tournament_Results.Data;
using Tournament_Results.Models;
using Tournament_Results.ViewModels;

namespace Tournament_Results.Controllers
{
    public class TournamentResultsController : ITournamentResultsController
    {
        readonly IAttendanceDataAccess _attendanceDataAccess;
        readonly ITournamentDataAccess _tournamentDataAccess;
        readonly IPointsDataAccess _pointsDataAccess;

        public TournamentResultsController(
            IAttendanceDataAccess attendanceDataAccess, 
            ITournamentDataAccess tournamentDataAccess,
            IPointsDataAccess pointsDataAccess)
        {
            _attendanceDataAccess = attendanceDataAccess;
            _tournamentDataAccess = tournamentDataAccess;
            _pointsDataAccess = pointsDataAccess;
        }

        public async Task<TournamentInfoVM> ReadSingleTournamentsPlayers(Guid tournamentID)
        {
            var tournament = await _tournamentDataAccess.ReadSingle(tournamentID);

            var players = await _attendanceDataAccess.ReadSingleTournamentsPlayers(tournamentID);
            players.Select(async p => p.Points = await ReadPoints(p.Placing, tournament.IsPremier));
            players.Select(p => p.Placing = CheckIfPlacingHasNegativeValue(p.Placing));

            TournamentInfoVM tournamentInfo = new(tournament.Title, players);
            return tournamentInfo ?? throw new ArgumentException("Tihi!");
        }

        private async Task<int> ReadPoints(int placing, bool isPremier) 
        {
            if (isPremier)
            {
                return await _pointsDataAccess.ReadPremierPpoints(placing);
            }
            return await _pointsDataAccess.ReadMajorPoints(placing);
        }

        private static int CheckIfPlacingHasNegativeValue(int attendant)
        {
            bool isNegative = attendant < 0;
            var placing = isNegative ? ConvertToPositive(attendant) : attendant;
            return placing;
        }

        private static int ConvertToPositive(int x)
        {
            return -x;
        }
    }
    public interface ITournamentResultsController
    {
        Task<TournamentInfoVM> ReadSingleTournamentsPlayers(Guid tournamentID);
    }
}
