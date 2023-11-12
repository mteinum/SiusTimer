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

        public static Competition Load(string path)
        {
            var doc = XDocument.Load(path);
            var root = doc.Root;

            return new Competition
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
                                                DelaySeconds = s.Attribute("delay-seconds")?.Value,
                                                Sound = s.Attribute("sound").Value,
                                                Light = s.Attribute("light")?.Value,
                                                Timer = s.Attribute("timer")?.Value
                                            }
                                    )
                                    .ToArray()
                            }
                    )
                    .ToArray()
            };
        }
    }
}
