using System.Collections.Generic;

namespace LostParticles.MusicFramework.Song
{
    public class Chord : List<Note>, IVoiceNote
    {
        #region IVoiceElement Members

        public bool CanContainChildVoiceElements
        {
            get { return true; }
        }

        #endregion

        /// <summary>
        /// returns the longest note in the chord.
        /// </summary>
        public Note LongestNote
        {
            get
            {
                Note longest = this[0];   
                foreach (Note nt in this)
                {
                    if (nt.DurationValue >= longest.DurationValue)
                    {
                        longest = nt;
                    }
                }

                return longest;
            }
        }

        private static int CompareNotesByDuration(Note x, Note y)
        {
            if (x == null)
            {
                if (y == null)
                {
                    // If x is null and y is null, they're
                    // equal. 
                    return 0;
                }
                else
                {
                    // If x is null and y is not null, y
                    // is greater. 
                    return -1;
                }
            }
            else
            {
                // If x is not null...
                //
                if (y == null)
                // ...and y is null, x is greater.
                {
                    return 1;
                }
                else
                {
                    // ...and y is not null, compare the 
                    // lengths of the two strings.
                    //
                    int retval = x.DurationValue.CompareTo(y.DurationValue);

                    if (retval != 0)
                    {
                        // If the strings are not of equal length,
                        // the longer string is greater.
                        //
                        return retval;
                    }
                    else
                    {
                        // If the strings are of equal length,
                        // sort them with ordinary string comparison.
                        //
                        return x.DurationValue.CompareTo(y.DurationValue);
                    }
                }
            }

        }

        /// <summary>
        /// gets the notes from the shortet duration to the longest duration.
        /// </summary>
        public List<Note> NotesFromShort
        {
            get
            {
                List<Note> fromShort = new List<Note>(this);

                fromShort.Sort(CompareNotesByDuration);

                return fromShort;
            }
        }
    }
}
