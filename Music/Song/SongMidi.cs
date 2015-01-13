//To Do 

//2- Make the wide sequencer take the data from the Song object model
//1- Make Song Write Midi File
// - Make Song Write mml string


using System.Collections.Generic;
using LostParticles.MusicFramework.Midi.IO;


namespace LostParticles.MusicFramework.Song
{
    public partial class Song : List<Voice>
    {


        //the intent of this file is to write midi file from the current object model


        public MidiFileStream MidiFile
        {
            get
            {
                MidiFileStream mf = new MidiFileStream();

                foreach (Voice v in this)
                {
                    mf.Tracks.Add(v.MidiTrack);
                }

                return mf;
            }
        }
    }
}