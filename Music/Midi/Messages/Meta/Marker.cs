using System;
using System.Collections.Generic;
using System.Text;
using LostParticles.MusicFramework.Midi.Messages.Common;

namespace LostParticles.MusicFramework.Midi.Messages.Meta
{
    /// <summary>
    /// FF 06 len text
    /// </summary>
    [MidiStatus(0xFF)]
    [MetaStatus(0x06)]
    public class Marker  : Text
    {
    }
}
