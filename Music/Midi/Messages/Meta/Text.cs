using System;
using System.Collections.Generic;
using System.Text;
using LostParticles.MusicFramework.Midi.Messages.Common;
using System.Runtime.InteropServices;

namespace LostParticles.MusicFramework.Midi.Messages.Meta
{
    /// <summary>
    /// FF 01 length text
    /// </summary>
    [MidiStatus(0xFF)]
    [MetaStatus(0x01)]
    public class Text : MetaMessage
    {

        public string Value
        {
            get
            {
                //get the text from MetaData bytes
                //return Encoding.ASCII.GetString(MetaData);
                return Encoding.UTF8.GetString(MetaData, 0, MetaData.Length);

            }
            set
            {
                //MetaData = Encoding.ASCII.GetBytes(value);
                MetaData = Encoding.UTF8.GetBytes(value);
            }
        }

        public override string ToString()
        {
            return base.ToString() + ": " + Value;
        }

    }
}
