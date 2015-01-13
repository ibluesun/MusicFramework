using System;
using System.Collections.Generic;
using System.Text;
using LostParticles.MusicFramework.Midi.Messages.Common;

namespace LostParticles.MusicFramework.Midi.Messages.Meta
{
    /// <summary>
    /// FF 51 03 tt tt tt
    ///  three bytes expressing  tempo   in microseconds  per quarter note
    /// </summary>
    [MidiStatus(0xFF)]
    [MetaStatus(0x51, 0x03)]
    public class Tempo : MetaMessage
    {

        public int Value
        {
            get
            {
                int v = messageData[3];
                v = v << 8;
                v = v + messageData[4];
                v = v << 8;
                v = v + messageData[5];
                return v;
            }
            set
            {
                //write it in 3 bytes
                byte[] data = BitConverter.GetBytes(value);
                this[3] = data[2];
                this[4] = data[1];
                this[5] = data[0];

            }
        }
        public double Bpm
        {
            get
            {
                return 60000000.0 / Value;
            }
            set
            {
                // t= 60/val
                // val = 60/t
                Value = (int)(60000000.0 / value);
            }
        }
    }
}
