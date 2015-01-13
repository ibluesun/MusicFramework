using System;
using System.Collections.Generic;
using System.Text;

namespace LostParticles.MusicFramework.Midi.Messages.Common
{

    /// <summary>
    /// 
    /// </summary>
    public abstract class ChannelMessage : MidiMessage
    {
        private const int CommandMask = ~240;
        private const int StatusMask = ~255;
        protected const int DataMask = ~StatusMask;

        public int Channel
        {
            get
            {
                return Status & DataMask & CommandMask;

            }
            set
            {
                //because channel is in status byte we should alter the status field
                Status = (byte)(Status | value);
            }
        }
    }
}
