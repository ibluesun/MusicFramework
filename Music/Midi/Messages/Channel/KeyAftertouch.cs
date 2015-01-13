
using LostParticles.MusicFramework.Midi.Messages.Common;

namespace LostParticles.MusicFramework.Midi.Messages.Channel
{
    [MidiStatus(0xA0)]
    public sealed class KeyAftertouch:ChannelMessage
    {
        public byte Note
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

        public byte Pressure
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
