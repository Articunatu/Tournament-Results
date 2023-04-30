//using Tournament_Results.Data;
//using Tournament_Results.Models;
//using Tournament_Results.ViewModels;

//namespace Tournament_Results.Controllers
//{
//    public class ATPRankingController : IATPRankingController
//    {
//        readonly ITournamentDataAccess _tournamentAccess;
//        readonly IAttendanceDataAccess _attendanceDataAccess;
//        readonly IPlayerDataAccess _playerDataAccess;
//        readonly IPointsDataAccess _pointsDataAccess;

//        public ATPRankingController(ITournamentDataAccess tournamentAccess, IAttendanceDataAccess attendanceDataAccess,
//            IPlayerDataAccess playerDataAccess, IPointsDataAccess pointsDataAccess)
//        {
//            _tournamentAccess = tournamentAccess;
//            _attendanceDataAccess = attendanceDataAccess;
//            _playerDataAccess = playerDataAccess;
//            _pointsDataAccess = pointsDataAccess;
//        }
//        public async Task<ATPRankingVM> ReadATPRanking(string startDate, string endDate)
//        {
//            //var n = _poiDataAccess.ReadSingleAttendancePremierPlacing(Guid.NewGuid(), Guid.NewGuid());

//            var _startDate = DateTime.Parse(startDate).ToUniversalTime();
//            var _endDate = DateTime.Parse(endDate).ToUniversalTime();
//            var tournaments = await _tournamentAccess.ReadAll(_startDate, _endDate); ///Two dates

//            List<Player> players = new();
//            foreach (var tour in tournaments)
//            {
//                var attendants = await _playerDataAccess.ReadAllTournamentsPlayers(tour.ID);
//                foreach (var attendant in attendants)
//                {
//                    if(!players.Contains(attendant))
//                        players.Add(attendant);
//                }
//            }

//            //var players = await _attendanceDataAccess.ReadAllTournamentsPlayers(Guid.NewGuid()); ///Two dates

//            List<PlayerRankingVM> playerRankings = new();
//            foreach (var player in players)
//            {
//                PlayerRankingVM playerRanking = new() {  Placings = new() };
//                foreach (var tournament in tournaments)
//                {
//                    var fetchedPlacing = await _pointsDataAccess.ReadSingleAttendancePlacing(player.ID, tournament.ID);
//                    Points points = fetchedPlacing ?? new(0, 0);
//                    bool didEnter = fetchedPlacing != null;
//                    if (didEnter)
//                       points.ID = CheckIfPlacingHasNegativeValue(points.ID);
//                    playerRanking.Placings.Add(points);
//                }
//                playerRankings.Add(playerRanking);
//                //playerRanking.SumOfPoints = ReadPlayersPointSum();
//            }

//            return new ATPRankingVM() { Tournaments = tournaments, Players = playerRankings };
//        }

//        private static int CheckIfPlacingHasNegativeValue(int attendant)
//        {
//            bool isNegative = attendant < 0;
//            var placing = isNegative ? ConvertToPositive(attendant) : attendant;
//            return placing;
//        }

//        private static int ConvertToPositive(int x)
//        {
//            return -x;
//        }

//        //public async Task<Points> ReadSingleAttendancePlacing(Guid playerID, Guid tournamentID)
//        //{
//        //    var tournament = await _tournamentAccess.ReadSingle(tournamentID);
//        //    if (tournament.IsPremier)
//        //    {
//        //        var points = await _pointsDataAccess.ReadSingleAttendancePlacing(playerID, tournamentID);
//        //        if (points == null)
//        //        {
//        //            Points p = new()
//        //            {
//        //                ID = 100,
//        //                Value = 0
//        //            };
//        //            return p;
//        //        }
//        //        points.ID = points.ID < 0 ? points.ID * -2 : points.ID;
//        //        return points;
//        //    }
//        //    else
//        //    {
//        //        var points = await _pointsDataAccess.ReadSingleAttendancePlacing(playerID, tournamentID);
//        //        if (points == null)
//        //        {
//        //            Points p = new()
//        //            {
//        //                ID = 99,
//        //                Value = 0
//        //            };
//        //            return p;
//        //        }
//        //        return points;
//        //    }
//        //}

//        public async Task<int> ReadPlayersPointSum(Guid id, string? startDate, string? endDate)
//        {
//            string majorSumProcedure = "spTotalMajorPointsByPlayerSELECT";
//            string premierSumProcedure = "spTotalPremierPointsByPlayerSELECT";

//            int totalMajorPoints = await _attendanceDataAccess.ReadPlayersPointSum(premierSumProcedure, id, startDate, endDate);
//            int totalPremierPoints = await _attendanceDataAccess.ReadPlayersPointSum(majorSumProcedure, id, startDate, endDate);
//            int totalPoints = totalMajorPoints + totalPremierPoints;
//            return totalPoints;
//        }
//    }
//    public interface IATPRankingController
//    {
//        Task<ATPRankingVM> ReadATPRanking(string startDate, string endDate);
//        //Task<Points> ReadSingleAttendancePlacing(Guid playerID, Guid tournamentID);
//        Task<int> ReadPlayersPointSum(Guid id, string? startDate, string? endDate);
//    }
//}
