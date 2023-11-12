namespace SiusTimer.Model
{
    public class Step
    {
        public string Sound { get; set; }
        public TimeSpan? Wait { get; set; }
        public TimeSpan? Timer { get; set; }
        public string Light { get; set; }
        public Group Group { get; set; }
    }
}
