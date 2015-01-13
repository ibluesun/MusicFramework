using System;
using System.Collections.Generic;
using System.Text;
using LostParticles.MusicFramework.Midi.Messages.Common;

namespace LostParticles.MusicFramework.Midi.Messages.Meta
{
    /// <summary>
    /// FF 2F 00
    /// not optional and should be in the end of the track
    /// </summary>
    [MidiStatus(0xFF)]
    [MetaStatus(0x2F, 0x00)]
    public class EndOfTrack : MetaMessage
    {

    }

}
