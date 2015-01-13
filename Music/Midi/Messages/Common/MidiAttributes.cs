using System;

namespace LostParticles.MusicFramework.Midi.Messages.Common
{
    public enum MidiStatus
    {
        None                    = 0x00,

        //Channel Messages
        NoteOff                 = 0x80,
        NoteOn                  = 0x90,
        KeyAftertouch           = 0xA0,
        ControlChange           = 0xB0,
        ProgramChange           = 0xC0,
        ChannelAftertouch       = 0xD0,
        PitchBend               = 0xE0,

        //RESET for midi device. and meta data for midi file.
        MetaMessage        = 0xFF
    }


    public class MidiStatusAttribute : Attribute
    {
        protected readonly byte status;
        public byte Status
        {
            get { return status; }
        }

        public MidiStatusAttribute(byte status)
        {
            this.status = status;
        }

    }

    public enum MetaStatus
    {
            SequenceNumber =0x00,

            Text =0x01,
            Copyright = 0x02,
            SequenceName = 0x03,
            Instrument = 0x04,
            Lyric = 0x05,
            Marker = 0x06,
            CuePoint = 0x07,
            ProgramName = 0x08,
            DevicePortName = 0x09,

            EndOfTrack = 0x2F

    }

    public class MetaStatusAttribute:Attribute
    {
        protected readonly byte[] status;
        public byte[] Status
        {
            get { return status; }
        }

        public MetaStatusAttribute(params byte[] status)
        {
            this.status = status;
        }

    }
}
