using System;
using System.Collections.Generic;
using System.Text;
using LostParticles.MusicFramework.Midi.Messages.Common;

namespace LostParticles.MusicFramework.Midi.Messages.Meta
{
    /// <summary>
    /// FF 21 01 pp
    /// </summary>
    [MidiStatus(0xFF)]
    [MetaStatus(0x21, 0x01)]
    public class MidiPort : MetaMessage
    {
    }
}
