using LostParticles.MusicFramework.Midi.Messages.Common;

namespace LostParticles.MusicFramework.Midi.IO
{
    /// <summary>
    /// Midi Event is holding Delta Time + MidiMessage
    /// Because in MIDI file there is always Delta Time + MidiMessage
    /// </summary>
    public sealed class MidiEvent
    {

        public ulong DeltaTime { get; set; }


        public MidiMessage Message {get;set;}


    }


}
