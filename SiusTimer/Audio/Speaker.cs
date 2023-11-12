using SiusTimer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;

namespace SiusTimer.Audio
{
    internal class Speaker
    {
        public static void Annonce(string sound)
        {
            var resourceName = $"SiusTimer.Audio.en.{sound}.wav";

            using (var stream = typeof(MainForm).Assembly.GetManifestResourceStream(resourceName))
            {
                var soundPlayer = new SoundPlayer(stream);
                soundPlayer.PlaySync();
            }
        }
    }
}
