namespace IceCream.Model
{
    internal class Station
    {
        public string StationID { get; set; }

        public string Date { get; set; }

        public int Target { get; set; }

        public int Actual { get; set; }

        public int Variance { get; set; }
    }
}
