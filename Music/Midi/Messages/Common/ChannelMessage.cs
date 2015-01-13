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

        public int Command
        {
            get
            {
                return Status & 0xF0;    // command is in the high bits so we multiply it by 1111 0000
            }
        }

        public string HexCommand
        {
            get
            {
                return Command.ToString("x");
            }
        }


        public static int StatusCommand(byte status)
        {
            return status & 0xF0;
        }

        public static int StatusChannel(byte status)
        {
            return status & DataMask & CommandMask;
        }
    }
}
