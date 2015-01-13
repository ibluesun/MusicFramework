using System;


namespace LostParticles.MusicFramework.Midi.IO
{
    public struct MidiTrack
    {
        public string Header { get; set; }

        public UInt32 DataLength { get; set; }

        internal byte[] Data;    //The track data


        public MidiTrackStream DataStream
        {   
            get
            {
                MidiTrackStream ts = new MidiTrackStream(Data);
                return ts;
            }
        }


        public static MidiTrack FromMidiTrackStream(MidiTrackStream trackStream)
        {

            MidiTrack track = new MidiTrack();

            track.Header = "MTrk";
            track.DataLength = (UInt32)trackStream.Length;
            track.Data = trackStream.ToArray();

            return track;

        }


    }
}
