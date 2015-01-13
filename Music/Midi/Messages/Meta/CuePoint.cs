using System;
using System.Collections.Generic;
using System.Text;
using LostParticles.MusicFramework.Midi.Messages.Common;

namespace LostParticles.MusicFramework.Midi.Messages.Meta
{
    /// <summary>
    /// FF 07 len text
    /// </summary>
    [MidiStatus(0xFF)]
    [MetaStatus(0x07)]
    public class CuePoint : Text
    {
    }
}
