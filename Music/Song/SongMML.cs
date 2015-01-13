using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;


namespace LostParticles.MusicFramework.Song
{
    public partial class Song : List<Voice>
    {

        static int octave = 4;
        static int num = 1;
        static int den = 4;

        public static Song SongFromMML(string mml)
        {

            octave = 4;
            num = 1;
            den = 4;

            Song NewSong = new Song(120);

            NewSong.SongName = "Auto Song";

            Voice vo = new Voice(NewSong, 0);

            vo.Clef = "violin";
            vo.Meter = "4/4";


            vo.AddRange(FromMML(mml));
            


            NewSong.Add(vo);

            return NewSong;
        }

        public static Note NoteMML(string note)
        {
            // Note [Octave|Duration]  
            // C#>[4|1/4]

            string nt = note.Trim(); 

            Match m = Regex.Match(nt, @"(.*)\[(.*)\]");

            if (m.Success)
            {
                string mod = m.Groups[2].Value;
                nt = m.Groups[1].Value;

                string oct = mod.Split('|')[0];
                if (string.IsNullOrEmpty(oct))
                    octave = 4;
                else
                {
                    //parse x/y

                    if (oct.Split('/').Length == 2)
                    {
                        num = int.Parse(oct.Split('/')[0]);
                        den = int.Parse(oct.Split('/')[1]);
                    }
                    else
                        octave = int.Parse(oct);
                }

                if (mod.Split('|').Length == 2)
                {
                    string dur = mod.Split('|')[1];
                    if (dur.Split('/').Length == 2)
                    {
                        num = int.Parse(dur.Split('/')[0]);
                        den = int.Parse(dur.Split('/')[1]);
                    }
                }
            }

            Note n = new Note
            {
                Octave = octave,
                Name = nt,
                Duration = num.ToString(CultureInfo.InvariantCulture) + "/" + den.ToString(CultureInfo.InvariantCulture)
                

            };

            return n;
        }




        public static Note[] FromMML(string mml)
        {
            MatchCollection notes = Regex.Matches(mml, @"(^\s*.+?\s+|.+?\s+|.+\s*$)", RegexOptions.IgnoreCase | RegexOptions.Singleline);

            List<Note> vo = new List<Note>();
            foreach (Match note in notes)
            {

                vo.Add(NoteMML(note.Value.Trim()));
            }

            return vo.ToArray();
        }


        public void AddMMLTrack(string mml)
        {

            Voice vo = new Voice(this, this.Count);

            vo.Clef = "violin";
            vo.Meter = "4/4";


            vo.AddRange(FromMML(mml));
            


            this.Add(vo);

        }
    
    }
}