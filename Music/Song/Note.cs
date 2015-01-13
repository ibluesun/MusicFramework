
namespace LostParticles.MusicFramework.Song
{
    /// <summary>
    /// Voice Note represent Note node in the song xml format and an  object during program run.
    /// </summary>
    public class Note : IVoiceNote
    {
        private string _Name;


        /// <summary>
        /// The Name of the note c, d, e, f, g, a, b
        /// </summary>
        public string Name
        {
            get
            {
                return _Name.ToUpperInvariant();
            }
            set { _Name = value; }
        }

        /// <summary>
        /// return the note without octave modifier or octave number
        /// </summary>
        public string AbsoluteNotation
        {
            get
            {
                string nn = Name;
                if (nn.EndsWith("<") || nn.EndsWith(">"))
                {
                    nn = nn.Substring(0, nn.Length - 1);
                }

                return nn;

            }
        }

        #region Raw Properties
        private int _Octave;

        public int Octave
        {
            get { return _Octave; }
            set { _Octave = value; }
        }

        public string Accedential
        {
            get
            {
                string g = AbsoluteNotation;
                return g.Substring(1);
            }
        }

        private string _Duration = "";

        public string Duration
        {
            get { return _Duration; }
            set { _Duration = value; }
        }

        private string _NextNoteAfter = "";

        public string NextNoteAfter
        {
            get { return _NextNoteAfter; }
            set { _NextNoteAfter = value; }
        }
        #endregion

        #region Calculated Properties
        public int DurationNumerator
        {
            get
            {
                string[] sd = Duration.Split('/');

                int num = int.Parse(sd[0], System.Globalization.CultureInfo.InvariantCulture);

                return num;
            }
        }
        public int DurationDenominator
        {
            get
            {
                string[] sd = Duration.Split('/');

                int den = int.Parse(sd[1], System.Globalization.CultureInfo.InvariantCulture);

                return den;
            }

        }

        public int NextNumerator
        {
            get
            {
                if (NextNoteAfter != "")
                {
                    string[] sd = NextNoteAfter.Split('/');

                    int num = int.Parse(sd[0], System.Globalization.CultureInfo.InvariantCulture);
                    //int den = int.Parse(sd[1], System.Globalization.CultureInfo.InvariantCulture);

                    return num;
                }
                else
                {
                    return DurationNumerator;
                }
            }
        }
        public int NextDenominator
        {
            get
            {
                if (NextNoteAfter != "")
                {
                    string[] sd = NextNoteAfter.Split('/');

                    int num = int.Parse(sd[0], System.Globalization.CultureInfo.InvariantCulture);
                    int den = int.Parse(sd[1], System.Globalization.CultureInfo.InvariantCulture);

                    return den;
                }
                else
                {
                    return DurationDenominator;
                }
            }

        }


        /// <summary>
        /// The duration of the current note in numerical value.
        /// </summary>
        public double DurationValue
        {
            get
            {
                string[] sd = Duration.Split('/');

                int num = int.Parse(sd[0], System.Globalization.CultureInfo.InvariantCulture);
                int den = int.Parse(sd[1], System.Globalization.CultureInfo.InvariantCulture);

                //get a fraction from whole note
                double duration = ((double)num / (double)den);

                //but the value is expressing fraction of whole note
                // and tick event manager is considering tick as noar
                // so multiply it by 4

                duration *= 4; // correct the value

                return duration;

            }
        }

        /// <summary>
        /// The duration elapsed before the next note begins.
        /// </summary>
        public double NextNoteAfterValue
        {
            get
            {
                if (NextNoteAfter != "")
                {
                    string[] sd = NextNoteAfter.Split('/');

                    int num = int.Parse(sd[0], System.Globalization.CultureInfo.InvariantCulture);
                    int den = int.Parse(sd[1], System.Globalization.CultureInfo.InvariantCulture);

                    //get a fraction from whole note
                    double duration = ((double)num / (double)den);
                    duration *= 4; // correct the value

                    return duration;
                }
                else
                {
                    return DurationValue;
                }

            }
        }

        #endregion


        #region IVoiceElement Members


        public bool CanContainChildVoiceElements
        {
            get { return false; }
        }

        #endregion


        public string MusicalName
        {
            get
            {
                string g = Name;

                g.Replace("C", "Do  ");
                g.Replace("D", "Re  ");
                g.Replace("E", "Mi  ");
                g.Replace("F", "Fa  ");
                g.Replace("G", "Sol ");
                g.Replace("A", "La  ");
                g.Replace("B", "Si  ");

                return g;
            }
        }

        public override string ToString()
        {
            return Name + "[" + Octave + "|" + Duration + "]";
        }


    }
}
