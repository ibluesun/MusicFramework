using System;
using System.Collections.Generic;
using System.Text;
using LostParticles.MusicFramework.Midi.Messages.Common;
using System.Runtime.InteropServices;

namespace LostParticles.MusicFramework.Midi.Messages.Meta
{
    /// <summary>
    /// Sequence/Track Name
    /// FF 03 len text
    /// </summary>
    [MidiStatus(0xFF)]
    [MetaStatus(0x03)]
    public class SequenceName : Text
    {
    }
}
