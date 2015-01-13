using System;
using System.Collections.Generic;
using System.Text;
using LostParticles.MusicFramework.Midi.Messages.Common;

namespace LostParticles.MusicFramework.Midi.Messages.Meta
{
    /// <summary>
    /// FF 08 len text
    /// </summary>
    [MidiStatus(0xFF)]
    [MetaStatus(0x08)]
    public class ProgramName : Text
    {
    }
}
