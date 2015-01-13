
using LostParticles.MusicFramework.Midi.Messages.Common;

namespace LostParticles.MusicFramework.Midi.Messages.Channel
{
    [MidiStatus(0xC0)]
    public sealed class ProgramChange : ChannelMessage
    {
        public byte Program
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
