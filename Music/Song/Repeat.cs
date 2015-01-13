using System.Collections.Generic;
using System;

namespace LostParticles.MusicFramework.Song
{
    public class Repeat:List<IVoiceNote>, IVoiceNote
    {
        private int _Times;

        public int Times
        {
            get { return _Times; }
            set { _Times = value; }
        }
        public Repeat(int times)
        {
            this._Times = times;
        }


        public Note[] AllNotes
        {
            get
            {
                List<Note> allNotes = new List<Note>();

                foreach (IVoiceNote ive in this)
                {
                    Type vtype = ive.GetType();

                    if (vtype == typeof(Note))
                    {
                        allNotes.Add((Note)ive);
                    }
                    if (vtype == typeof(Repeat))
                    {
                        allNotes.AddRange(((Repeat)ive).AllNotes);
                    }
                    if (vtype == typeof(Chord))
                    {
                        allNotes.Add(((Chord)ive)[0]);
                    }
                }

                return allNotes.ToArray();
            }
        }

        #region IVoiceElement Members


        public bool CanContainChildVoiceElements
        {
            get { return true; }
        }

        #endregion
    }
}
