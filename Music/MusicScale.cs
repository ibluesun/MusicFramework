using System;
using System.Collections.Generic;
using LostParticles.MusicFramework.Midi;
using System.Collections;
using System.Linq;

namespace LostParticles.MusicFramework
{

    /// <summary>
    /// this class for making music scale you want
    /// all values from 0.0 to 1.0
    /// make sure the first number is offset from the begining of Octave
    /// the class base key is always the Do tone
    /// </summary>
    public class MusicScale
    {
        double[] _Offsets;

        public readonly string ScaleName;

        /// <summary>
        /// first number is alwayis the offset from base note
        /// last number is alwayis the first number + octave
        /// its alwayis 9 numbers 
        /// </summary>
        /// <param name="Offsets">depends on the wanted scale</param>
        public MusicScale(string name, string refrenceNote, params double[] offsets)
        {

            ScaleName = name;

            this.RefrenceNote = refrenceNote; //we specified offset

            List<double> otes = new List<double>();

            /* 
             * I insert 0.0 in the beging of the offsets
             * any way the refrence is added to the BaseKey Property
             */
            otes.Add(0.0f);  //if this value is not 0 then the refrence will change
            foreach (double ofz in offsets)
                otes.Add(ofz);




            _Offsets = otes.ToArray();


        }

        public int TonesCount
        {
            get
            {
                return _Offsets.Length;
            }
        }



 

        /// <summary>
        /// this is alwayis the Do on the start of the fourth octave
        /// </summary>
        private int _BaseKey = 60;

        /// <summary>
        /// the value of the begining of the scale
        /// </summary>
        public int BaseKey
        {
            get
            {
                return _BaseKey;
            }
        }

        /// <summary>
        /// this value is added to the _BaseKey to get the refrence note the scale 
        /// is playing from
        /// for example
        /// Major Do  is playing from Do
        /// Major Fa  is playing from Fa 
        /// Minor Sol is playing minor scale from Sol
        /// etc.
        /// </summary>
        private double _RefrenceNoteOffset;

        public string RefrenceNote
        {
            get
            {
                int o;
                return (NoteFromOffset(_RefrenceNoteOffset, out o));
            }
            set
            {
                _RefrenceNoteOffset = OffsetFromNote(value);
                CachedMidiNotes.Clear();
               
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="refrenceNoteOffset"></param>
        /// <param name="octaveOverflow">out parameter if offset exceeds the octave boundary</param>
        /// <returns></returns>
        public static string NoteFromOffset(double refrenceNoteOffset, out int octaveOverflow)
        {
            string ns = "C";

            double _RNote = refrenceNoteOffset;

            if (refrenceNoteOffset < 0)
            {
                _RNote += 6;

            }
            // 6.25

            if (refrenceNoteOffset > 5.75)
            {
                _RNote -= 6;
            }

            if (_RNote == 0) ns = "C";
            else if (_RNote == 0.125) ns = "C'";
            else if (_RNote == 0.25) ns = "C/";
            else if (_RNote == 0.375) ns = "C/'";
            else if (_RNote == 0.5) ns = "C#";
            else if (_RNote == 0.625) ns = "D!.";
            else if (_RNote == 0.75) ns = "D!";
            else if (_RNote == 0.875) ns = "D.";


            else if (_RNote == 1) ns = "D";
            else if (_RNote == 1.125) ns = "D'";
            else if (_RNote == 1.25) ns = "D/";
            else if (_RNote == 1.375) ns = "D/'";
            else if (_RNote == 1.5) ns = "D#";
            else if (_RNote == 1.625) ns = "E!.";
            else if (_RNote == 1.75) ns = "E!";
            else if (_RNote == 1.875) ns = "E.";

            else if (_RNote == 2) ns = "E";
            else if (_RNote == 2.125) ns = "E'";
            else if (_RNote == 2.25) ns = "E/";
            else if (_RNote == 2.375) ns = "F.";
            else if (_RNote == 2.5) ns = "F";
            else if (_RNote == 2.625) ns = "F'";
            else if (_RNote == 2.75) ns = "F/";
            else if (_RNote == 2.875) ns = "F/'";

            else if (_RNote == 3) ns = "F#";
            else if (_RNote == 3.125) ns = "F#'";
            else if (_RNote == 3.25) ns = "G!";
            else if (_RNote == 3.375) ns = "G.";
            else if (_RNote == 3.5) ns = "G";
            else if (_RNote == 3.625) ns = "G'";
            else if (_RNote == 3.75) ns = "G/";
            else if (_RNote == 3.875) ns = "G/'";

            else if (_RNote == 4) ns = "G#";
            else if (_RNote == 4.125) ns = "G#'";
            else if (_RNote == 4.25) ns = "A!";
            else if (_RNote == 4.375) ns = "A.";
            else if (_RNote == 4.5) ns = "A";
            else if (_RNote == 4.625) ns = "A'";
            else if (_RNote == 4.75) ns = "A/";
            else if (_RNote == 4.875) ns = "A/'";

            else if (_RNote == 5) ns = "A#";
            else if (_RNote == 5.125) ns = "A#'";
            else if (_RNote == 5.25) ns = "B!";
            else if (_RNote == 5.375) ns = "B.";
            else if (_RNote == 5.5) ns = "B";
            else if (_RNote == 5.625) ns = "B'";
            else if (_RNote == 5.75) ns = "B/";
            else if (_RNote == 5.875) ns = "B/'";


            else throw new Exception("Offset Not Found");

            octaveOverflow = 0;

            if (refrenceNoteOffset < 0)
            {
                octaveOverflow = -1;

                ns += "<";

            }

            if (refrenceNoteOffset > 5.75)
            {
                octaveOverflow = 1;

                ns += ">";

            }

            return ns;
        }

        /// <summary>
        /// Returns the offset based on zero
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static double OffsetFromNote(string value)
        {
            //fast regex for the string
            // [C D E F G A B] [[~ #]!?] [> <]

            //C~!  -0.75
            //C~   -0.5
            //C!   -0.25
            //C.   -0.125
            //C or C*     0
            //C'   +0.125
            //C/   +0.25
            //C#   +0.5
            //C#/  +0.75
            // so the conclusion is that  
            //       (# raise 0.5) (/ raise 0.25) (' raise 0.125)  
            //       (~ flat 0.5) (! flat 0.25) (. flat 0.125)


            double _RefrenceNoteOffset = 0;


            if (value == null) return _RefrenceNoteOffset;

            string note = value.ToUpperInvariant();

            //look for last character
            char s = value.ToCharArray()[value.Length - 1];

            if (s == '>' || s == '<')
            {
                //octave modifier
                //remove the character from the string to return string
                // to only note string
                note = value.ToUpperInvariant().Substring(0, value.Length - 1);
            }

            char picar = note.ToCharArray()[note.Length - 1];
            if (picar == '*')
            {
                //remove the character so the tone when evaulated 
                // it will return to the original state
                // it should be noted that there is no C*!
                note = note.ToUpperInvariant().Substring(0, note.Length - 1);
            }

            #region determine the character refrence
            char[] ntcs = note.ToCharArray();
            switch (ntcs[0])
            {
                case 'C':
                    _RefrenceNoteOffset = 0;
                    break;
                case 'D':
                    _RefrenceNoteOffset = 1;
                    break;
                case 'E':
                    _RefrenceNoteOffset = 2;
                    break;
                case 'F':
                    _RefrenceNoteOffset = 2.5;
                    break;
                case 'G':
                    _RefrenceNoteOffset = 3.5;
                    break;
                case 'A':
                    _RefrenceNoteOffset = 4.5;
                    break;
                case 'B':
                    _RefrenceNoteOffset = 5.5;
                    break;
            }

            if (ntcs.Length > 1)
            {

                for (int ix = 1; ix < ntcs.Length;ix++ )
                {
                    char c = ntcs[ix];
                    switch (c)
                    {
                        case '#':
                            _RefrenceNoteOffset += 0.5;
                            break;
                        case '/':
                            _RefrenceNoteOffset += 0.25;
                            break;
                        case '\'':
                            _RefrenceNoteOffset += 0.125;
                            break;
                        default:
                            _RefrenceNoteOffset = _RefrenceNoteOffset + 0;    // this is silly ofcourse because picar has nothing
                            break;
                        case '.':
                            _RefrenceNoteOffset -= 0.125;
                            break;
                        case '!':
                            _RefrenceNoteOffset -= 0.25;
                            break;
                        case '~':
                            _RefrenceNoteOffset -= 0.5;
                            break;
                    }

                }

            }
            

            #endregion


            //check last letter
            //if '>' note layed on the next octave
            //if '<' note layed in the previous octave
            if (s == '>')
            {
                _RefrenceNoteOffset = _RefrenceNoteOffset + 6;
            }

            if (s == '<')
            {
                _RefrenceNoteOffset = _RefrenceNoteOffset - 6;
            } 
            
            
            return _RefrenceNoteOffset;
        }



        private int _Octave = 4;
        public int Octave
        {
            get
            {
                return _Octave;
            }
            set
            {

                _BaseKey = GetOctaveFirstKey(value);
                _Octave = value;
            }
        }



        /// <summary>
        /// getting the first do of the required octave
        /// </summary>
        /// <param name="_Octave">required octave</param>
        /// <returns>midi note index number of Do</returns>
        static public int GetOctaveFirstKey(int octave)
        {
            int _BaseKey = 0;
            switch (octave)
            {
                case 0:
                    _BaseKey = 12;  //C0
                    break;
                case 1:
                    _BaseKey = 24;  //C1
                    break;
                case 2:
                    _BaseKey = 36;  //C2
                    break;
                case 3:
                    _BaseKey = 48;  //C3
                    break;
                case 4:
                    _BaseKey = 60;  //C4
                    break;
                case 5:
                    _BaseKey = 72;  //C5
                    break;
                case 6:
                    _BaseKey = 84;  //C6
                    break;
                case 7:
                    _BaseKey = 96;  //C7
                    break;
                case 8:
                    _BaseKey = 108;  //C8
                    break;
                case 9:
                    _BaseKey = 120;  //C9
                    break;

            }
            return _BaseKey;

        }


        /// <summary>
        /// get the total offsets value from the first tone
        /// to the last tone
        /// </summary>
        public double ScaleTotalOffset
        {
            get
            {
                double tot = 0;
                for (int i = 1; i < TonesCount; i++)
                {
                    tot += _Offsets[i];
                }

                return tot;
            }
        }




    /* Algorithm for extracting tone data key
     * and semi tone data key
     * --------------------------------------
     * 0   1    1    0.5    1    1    1    0.5
     * every key in MIDI is 0.5 tone interval
     * 60 middle do 'C4'
     * 61 is do + 0.5 tone
     * so
     * we make correlation to the entered index to produce tone key
     * y = 2 * x   -- first pass to produce tones data
     * 0   2    2    1      2    2     2     1
     * ---------------------------------------
     * second pass check if number is having a decimal value
     * take the decimal value and convert it to semitone data
     * 
     * 
     */

        private Dictionary<int, MidiNote> CachedMidiNotes = new Dictionary<int, MidiNote>();
        private bool CacheMidiNotes = false;


        /// <summary>
        /// 
        /// </summary>
        /// <param name="NoteIndex">Numbers From 1 ~ .., ...</param>
        /// <returns></returns>
        public MidiNote GetNoteData(int noteIndex)
        {

            MidiNote ND;
            int originalNoteIndex = noteIndex;

            if (CacheMidiNotes)
            {
                if (CachedMidiNotes.TryGetValue(originalNoteIndex, out ND))
                    return ND;
            }

            // if no cache occur then we will return after calculation


            double ExtraOffset = 0;

            if (noteIndex == 0)
            {
                throw new Exception("NoteIndex should be > 0 or < 0 NO ZERO ALLLOWED");
            }

            if (noteIndex > TonesCount)
            {

                //calculate the offset
                int times = (int)Math.Floor( (double)noteIndex / (TonesCount - 1));

                ExtraOffset = (times * ScaleTotalOffset);
                noteIndex -= (times * (TonesCount - 1));

            }

            if (noteIndex < 0)
            {

                //calculate the total offsets and decrease offsets

                int times = (Math.Abs(noteIndex) / (TonesCount - 1)) + 1;

                ExtraOffset = 0 - (times * ScaleTotalOffset);
                noteIndex += (times * (TonesCount - 1)) + 1;
            }



            //change the first value of the offsets array 
            //with the value in the refrence note offset

            _Offsets[0] = _RefrenceNoteOffset;

            double RequiredKeyValue = 0;

            if (noteIndex == 0)
            {
                // this case occurd in the japanease scale when hitting the last note of second octave
                // we end with noteindex as 0  which is not allowed ofcourse

                RequiredKeyValue -= (_Offsets[TonesCount - 1] - _Offsets[0]);
                // RequiredKeyValue += _Offsets[0];
                //RequiredKeyValue = _Offsets.Sum();
            }
            else
            {
                while (noteIndex > 0)
                {
                    //lower it to 0 to accumulate all offsets
                    noteIndex--;

                    RequiredKeyValue += _Offsets[noteIndex];

                }
            }

            RequiredKeyValue += ExtraOffset;

            
            double tone = 2 * RequiredKeyValue;

            int tonedata = (int)tone;

            
            ND = new MidiNote();
            ND.NoteNumber = tonedata + BaseKey;
            ND.SemiTone = 64; //no tone change this is neutral value.


            //32 in pitch message is half tone
            // so 100% = 32
            if (Math.Abs(tone) > Math.Abs(tonedata))
            {
                double sm = tone - tonedata; //get the decimal part

                double spt = sm * 32;

                ND.SemiTone += (int)spt;

            }

            if (CacheMidiNotes) CachedMidiNotes.Add(originalNoteIndex, ND);
            
            return ND;
            
        }

        public override string ToString()
        {
            return ScaleName;
        }

        public bool IsBelongToScale(string notation)
        {
            if (notation == null) return false;

            if (notation == "_") return true;   //hold

            bool Belongs = false;

            for (int u = 1; u < this.TonesCount; u++)
            {
                //remove the octave number at the end of the string

                string refnote = GetNoteData(u).AbsoluteNotation;

                if (notation.EndsWith("<") || notation.EndsWith(">"))
                {
                    notation = notation.Substring(0, notation.Length - 1);
                }
                notation = notation.ToUpperInvariant();

                if (refnote == notation)
                {
                    Belongs = true;
                    break;

                }
            }

            return Belongs;
        }

        /// <summary>
        /// based on the input the midi NoteData will be generated
        /// if Notation == "_"  then null will be returned
        /// </summary>
        /// <param name="Octave">FROM 1 TO 9</param>
        /// <param name="Notation">C D ... B  (#)sharp (~)Flat (*)Picar (/) Half Sharp (!) Half Flat (') Half Half Sharp (.) Half Half Flat </param>
        /// <returns></returns>
        public static MidiNote GenerateMidiNote(int octave, string notation)
        {

            if (notation == "_") return null;

            int BaseKey = MusicScale.GetOctaveFirstKey(octave);

            double offset = MusicScale.OffsetFromNote(notation);

            //convert to midi keys
            double tone = 2 * offset;

            //get the integer part to know the required key
            int tonedata = (int)tone;

            MidiNote ND = new MidiNote();
            ND.NoteNumber = tonedata + BaseKey;  //add to the octave base
            ND.SemiTone = 64; //no tone change this is neutral value.


            // 32 in pitch message is half tone
            // so 100% = 32
            if (Math.Abs(tone) > Math.Abs(tonedata))
            {
                double sm = tone - tonedata; //get the decimal part

                double spt = sm * 32;

                ND.SemiTone += (int)spt;

            }

            return ND;

        }

    }
}
