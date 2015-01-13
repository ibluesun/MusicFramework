using LostParticles.MusicFramework.Midi;
using LostParticles.MusicFramework.Midi.IO;
using LostParticles.TicksEngine;
using System.Collections.Generic;




namespace LostParticles.MusicFramework.Song
{
    /// <summary>
    /// Song class holds many tracks of voices .. and each voice holds many musical notes to be played
    /// </summary>
    public partial class Song:List<Voice>
    {

        private string _Title = "No Title";

        /// <summary>
        /// The song title
        /// </summary>
        public string Title 
        {
            get
            {
                return _Title;
            }
            set
            {
                _Title = value;
            }
        }

        private string _Composer = "No Composer";

        public string Composer
        {
            get { return _Composer; }
            set { _Composer = value; }
        }

        private int _Tempo = 120;

        /// <summary>
        /// The song tempo
        /// </summary>
        public int Tempo
        {
            get { return _Tempo; }
            set { _Tempo = value; }
        }

        /// <summary>
        /// Song Constrcuctor
        /// </summary>
        /// <param name="Tempo">Beats Per Minute</param>
        public Song(int tempo)
        {
            this.Tempo = tempo;
        }


        public MultiPlayer GetPlayer(IMidiOutputDevice midiOutputDevice)
        {
            MultiPlayer mp = new MultiPlayer();

            foreach(Voice v in this)
            {
                mp.AddTicksPlayer(v.GetHardwarePlayer(midiOutputDevice));
            }

            return mp;
        }


    }
}
