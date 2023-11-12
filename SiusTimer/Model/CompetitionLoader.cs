using System.Xml.Linq;

namespace SiusTimer.Model
{
    public class CompetitionLoader
    {
        static int TryParseAttribute(XElement e, string name, int ifMissing)
        {
            var str = e.Attribute(name)?.Value;
            return Int32.TryParse(str, out var result) ? result : ifMissing;
        }

        static TimeSpan? TryParseTimeSpan(XElement e, string name)
        {
            var str = e.Attribute(name)?.Value;
            if (str == null)
                return null;
            return TimeSpan.Parse(str);
        }

        private static Competition AddParents(Competition competition)
        {
            foreach (var group in competition.Groups)
            {
                foreach (var step in group.Steps)
                {
                    step.Group = group;
                }
            }

            return competition;
        }

        public static Competition Load(string path)
        {
            var doc = XDocument.Load(path);
            var root = doc.Root;

            return AddParents(
                new Competition
                {
                    Name = root.Attribute("name").Value,
                    Groups = root.Elements("group")
                        .Select(
                            e =>
                                new Group
                                {
                                    Id = e.Attribute("id").Value,
                                    Repeat = TryParseAttribute(e, "repeat", 1),
                                    Steps = e.Elements("step")
                                        .Select(
                                            s =>
                                                new Step
                                                {
                                                    Wait = TryParseTimeSpan(s, "wait"),
                                                    Sound = s.Attribute("sound").Value,
                                                    Light = s.Attribute("light")?.Value,
                                                    Timer = TryParseTimeSpan(s, "timer")
                                                }
                                        )
                                        .ToArray()
                                }
                        )
                        .ToArray()
                }
            );
        }
    }
}
