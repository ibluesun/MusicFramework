using System;
using System.Collections.Generic;
using System.Text;
using LostParticles.MusicFramework.Midi.Messages.Common;

namespace LostParticles.MusicFramework.Midi.Messages.Meta
{
    /// <summary>
    /// FF 00 02 ss ss
    /// Sequence number is from    ss ss 
    /// FF 00 00
    /// Sequence number is from the location of the track in the midi file
    /// </summary>
    [MidiStatus(0xFF)]
    [MetaStatus(0x00, 0x02)]
    public class SequenceNumber : MetaMessage
    {

    }


}
