using System.Collections.Generic;
using Tournament_Results.Data;
using Tournament_Results.Models;
using Tournament_Results.ViewModels;
using static MudBlazor.CategoryTypes;

namespace Tournament_Results.Controllers
{
    public class ATPRankingController : IATPRankingController
    {
        readonly ITournamentDataAccess _tournamentAccess;
        readonly IAttendanceDataAccess _attendanceDataAccess;
        readonly IPlayerDataAccess _playerDataAccess;
        readonly IPointsDataAccess _pointsDataAccess;

        public ATPRankingController(ITournamentDataAccess tournamentAccess, IAttendanceDataAccess attendanceDataAccess,
            IPlayerDataAccess playerDataAccess, IPointsDataAccess pointsDataAccess)
        {
            _tournamentAccess = tournamentAccess;
            _attendanceDataAccess = attendanceDataAccess;
            _playerDataAccess = playerDataAccess;
            _pointsDataAccess = pointsDataAccess;
        }
        public async Task<RankingGridViewModel> ReadATPRanking(string startDate, string endDate)
        {
            var _startDate = DateTime.Parse(startDate).ToUniversalTime();
            var _endDate = DateTime.Parse(endDate).ToUniversalTime();
            var tournaments = await _tournamentAccess.ReadAll(_startDate, _endDate); ///Two dates

            List<Player> players = new();
            HashSet<string> tags = new();
            foreach (var tour in tournaments)
            {
                var attendants = await _playerDataAccess.ReadAllTournamentsPlayers(tour.ID);

                foreach (var attendant in attendants)
                {
                    if (tags.Add(attendant.Tag))
                    {
                        players.Add(attendant);
                    }
                }
            }

            PointsDictionary pD = new();

            List<PlayerRankingDTO> playerRankings = new();
            foreach (var player in players)
            {
                PlayerRankingDTO playerRanking = new()
                {
                    Placings = new(),
                    Tag = player.Tag
                };
                foreach (var tournament in tournaments)
                {
                    int fetchedPlacing = await _pointsDataAccess.ReadSingleAttendancePlacing(player.ID, tournament.ID);
                    fetchedPlacing = CheckIfPlacingHasNegativeValue(fetchedPlacing);
                    
                    int pointsValue = 0;

                    if (tournament.IsPremier)
                    {
                        pD.Premier.TryGetValue(fetchedPlacing, out int _pointsValue);
                        pointsValue = _pointsValue;
                    }
                    else
                    {
                        pD.Major.TryGetValue(fetchedPlacing, out int _pointsValue);
                        pointsValue = _pointsValue;
                    }

                    Points points = new(fetchedPlacing, pointsValue);

                    playerRanking.Placings.Add(points);
                }
                playerRanking.SumOfPoints = playerRanking.Placings.Sum(point => point.Value);
                playerRankings.Add(playerRanking);
            }

            return new RankingGridViewModel() { Tournaments = tournaments, Players = playerRankings };
        }

        private static int CheckIfPlacingHasNegativeValue(int attendant)
        {
            bool isNegative = attendant < 0;
            int placing = isNegative ? ConvertToPositive(attendant) : attendant;
            return placing;
        }

        private static int ConvertToPositive(int x)
        {
            return -x;
        }

    //    public async Task<Points> ReadSingleAttendancePlacing(Guid playerID, Guid tournamentID)
    //    {
    //        var tournament = await _tournamentAccess.ReadSingle(tournamentID);
    //        if (tournament.IsPremier)
    //        {
    //            var placing = await _attendanceDataAccess.ReadSingleAttendancePlacing(playerID, tournamentID);
    //            var points = await _pointsDataAccess.ReadPremierPoints(placing.ID);
    //            if (points == null)
    //            {
    //                Points p = new(100, 0);
    //                return p;
    //            }
    //            points.ID = points.ID < 0 ? points.ID * -2 : points.ID;
    //            return points;
    //        }
    //        else
    //        {
    //            var points = await _pointsDataAccess.ReadSingleAttendancePlacing(playerID, tournamentID);
    //            if (points == null)
    //            {
    //                Points p = new(99, 0);
    //                return p;
    //            }
    //            return points;
    //        }
    //    //}


    }
    public interface IATPRankingController
    {
        Task<RankingGridViewModel> ReadATPRanking(string startDate, string endDate);
        //Task<Points> ReadSingleAttendancePlacing(Guid playerID, Guid tournamentID);
        //Task<int> ReadPlayersPointSum(Guid id, string? startDate, string? endDate);
    }
}

//public async Task<int> ReadPlayersPointSum(Guid id, string? startDate, string? endDate)
//{
//    string majorSumProcedure = "spTotalMajorPointsByPlayerSELECT";
//    string premierSumProcedure = "spTotalPremierPointsByPlayerSELECT";

//    int totalMajorPoints = await _attendanceDataAccess.ReadPlayersPointSum(premierSumProcedure, id, startDate, endDate);
//    int totalPremierPoints = await _attendanceDataAccess.ReadPlayersPointSum(majorSumProcedure, id, startDate, endDate);
//    int totalPoints = totalMajorPoints + totalPremierPoints;
//    return totalPoints;
//}