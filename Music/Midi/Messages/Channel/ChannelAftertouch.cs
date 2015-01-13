
using LostParticles.MusicFramework.Midi.Messages.Common;

namespace LostParticles.MusicFramework.Midi.Messages.Channel
{
    [MidiStatus(0xD0)]
    public sealed class ChannelAftertouch : ChannelMessage
    {
        public byte Pressure
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
    }
}
