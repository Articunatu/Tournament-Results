namespace Tournament_Results.ViewModels
{
    public class PlayerResultVM
    {
        public Guid PlayerID { get; set; }
        public string? Tag { get; set; }
        public int Placing { get; set; }
        public int Points { get; set; }

        //public PlayerResult(int placing)
        //{
        //    bool isNegative = placing < 0;
        //    if (isNegative)
        //    {
        //        Placing = ConvertToPositive(placing);
        //    }
        //}

        //private static int ConvertToPositive(int x)
        //{
        //    return -x;
        //}
    }
}
