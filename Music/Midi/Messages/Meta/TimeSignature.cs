using System;
using System.Collections.Generic;
using System.Text;
using LostParticles.MusicFramework.Midi.Messages.Common;

namespace LostParticles.MusicFramework.Midi.Messages.Meta
{
    /// <summary>
    /// FF 58 04 nn dd cc bb
    ///  
    /// </summary>
    [MidiStatus(0xFF)]
    [MetaStatus(0x58, 0x04)]
    public class TimeSignature : MetaMessage
    {
    }
}
