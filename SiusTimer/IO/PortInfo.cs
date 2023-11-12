namespace SiusTimer.IO
{
    public class PortInfo
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public string Display => Description ?? Name;
    }
}
