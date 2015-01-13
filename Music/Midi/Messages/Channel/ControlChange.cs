
using LostParticles.MusicFramework.Midi.Messages.Common;

namespace LostParticles.MusicFramework.Midi.Messages.Channel
{
    [MidiStatus(0xB0)]
    public sealed class ControlChange : ChannelMessage
    {

        public byte Control
        {
            get
            {
                return this[1];
            }
            set
            {
                this[1] = value;
            }
        }

        public byte Value
        {
            get
            {
                return this[2];
            }
            set
            {
                this[2] = value;
            }
        }


    }
}
