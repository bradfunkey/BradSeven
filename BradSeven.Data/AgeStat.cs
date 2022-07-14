namespace BradSeven.Data
{
    public class AgeStat
    {
        public int Age { get; set; }
        public int FemaleCount { get; set; }
        public int MaleCount { get; set; }
        public int TotalCount { get; set; }

        public int OtherCount
        {
            get { return TotalCount - (MaleCount + FemaleCount); }
        }

    }
}
