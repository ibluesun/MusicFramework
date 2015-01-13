using System;
using System.Collections.Generic;
using System.Text;
using LostParticles.MusicFramework.Midi.Messages.Common;

namespace LostParticles.MusicFramework.Midi.Messages.Meta
{
    /// <summary>
    /// FF 20 01 cc
    /// </summary>
    [MidiStatus(0xFF)]
    [MetaStatus(0x20, 0x01)]
    public class MidiChannel : MetaMessage
    {

    }
}
