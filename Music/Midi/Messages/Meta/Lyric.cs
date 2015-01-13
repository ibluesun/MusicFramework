using LostParticles.MusicFramework.Midi.Messages.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace LostParticles.MusicFramework.Midi.Messages.Meta
{
    /// <summary>
    /// FF 05 len text
    /// </summary>
    [MidiStatus(0xFF)]
    [MetaStatus(0x05)]
    public class Lyric : Text
    {
    }
}
