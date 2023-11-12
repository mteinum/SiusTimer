namespace SiusTimer.Model
{
    public class Group
    {
        public string Id { get; set; }
        public int Repeat { get; set; }
        public Step[] Steps { get; set; }
    }
}
