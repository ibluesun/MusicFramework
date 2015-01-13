
using LostParticles.MusicFramework.Midi.Messages.Common;

namespace LostParticles.MusicFramework.Midi.Messages.Channel
{
    [MidiStatus(0x80)]
    public sealed class NoteOff : ChannelMessage
    {

        public byte NoteNumber
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
        
        public byte Velocity
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
