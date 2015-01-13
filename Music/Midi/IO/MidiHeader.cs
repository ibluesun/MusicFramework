using System;

namespace LostParticles.MusicFramework.Midi.IO
{
    public sealed class MidiHeader
    {
        public string Header;

        public UInt32 HeaderLength;

        public UInt16 FormatType;

        public UInt16 NumTracks;


        /// <summary>
        /// Ticks per beat
        /// </summary>
        public UInt16 Division;



        #region static values for header
        public const string ID = "MThd";
        public const UInt32 Length = 6;
        public const UInt16 Format = 1;

        /// <summary>
        /// Ticks Per Beat
        /// or
        /// Pulses Per Quarter Note
        /// </summary>
        public const UInt16 PPQN = 96;

        public static MidiHeader GetHeader(int numTracks)
        {
            MidiHeader header = new MidiHeader();

            //fixed data
            header.Header = ID;
            header.HeaderLength = Length;
            header.FormatType = Format;
            header.NumTracks = (UInt16)numTracks;
            header.Division = PPQN;

            return header;

        }
        
        #endregion

    }
}
