
namespace LostParticles.MusicFramework.Midi
{

    public delegate void MidiNoteProcedure(MidiNote midiNote);

    /// <summary>
    /// Represents Midi Note that hold information ready to be sent to the midi device.
    /// Also hold information about text notation of the tone.
    /// </summary>
    public class MidiNote
    {
        /// <summary>
        /// specify channel if this particular note
        /// need different channel to be processed
        /// but not used until now :)
        /// </summary>
        public int Channel { get; set; }

        public int NoteNumber { get; set; }
        public int SemiTone { get; set; }


        /// <summary>
        /// return the tone octave based on Tone value
        /// </summary>
        public int Octave
        {
            get
            {
                int oct = -1;

                if (NoteNumber >= 120) oct = 9;
                else if (NoteNumber >= 108) oct = 8;
                else if (NoteNumber >= 96) oct = 7;
                else if (NoteNumber >= 84) oct = 6;
                else if (NoteNumber >= 72) oct = 5;
                else if (NoteNumber >= 60) oct = 4;
                else if (NoteNumber >= 48) oct = 3;
                else if (NoteNumber >= 36) oct = 2;
                else if (NoteNumber >= 24) oct = 1;
                else if (NoteNumber >= 12) oct = 0;
                else oct = -1;
                return oct;
            }
        }

        /// <summary>
        /// return the tone scientific notation
        /// </summary>
        public string ScientificNotation
        {
            get
            {
                int oct = Octave;
                int abs_position = NoteNumber - MusicScale.GetOctaveFirstKey(oct);
                string ns = "_";  //sakta

                double noffset = abs_position / 2.0;

                double st = SemiTone / 32.0 / 2.0;

                if (st < 1)
                {
                    st = 1 - st;
                    noffset -= st;
                }
                else if (st > 1)
                {
                    st = st - 1;
                    noffset += st;
                }

                int o;

                ns = MusicScale.NoteFromOffset(noffset, out o);



                return ns + "" + oct.ToString(System.Globalization.CultureInfo.InvariantCulture) + "";
            }
        }

        /// <summary>
        /// Notation without octave number
        /// </summary>
        public string AbsoluteNotation
        {
            get
            {
                string refnote = ScientificNotation.ToUpperInvariant();
                refnote = refnote.Substring(0, refnote.Length - 1);
                return refnote;

            }
        }
    }


    
}
