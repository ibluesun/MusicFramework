
using LostParticles.MusicFramework.Midi.Messages.Common;

namespace LostParticles.MusicFramework.Midi.Messages.Channel
{
    [MidiStatus(0xE0)]
    public sealed class PitchBend : ChannelMessage
    {

        public byte LSB
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
        
        public byte MSB
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
