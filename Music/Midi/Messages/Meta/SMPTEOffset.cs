using System;
using System.Collections.Generic;
using System.Text;
using LostParticles.MusicFramework.Midi.Messages.Common;

namespace LostParticles.MusicFramework.Midi.Messages.Meta
{
    /// <summary>
    /// FF 54 05 hr mn se fr ff
    ///  
    /// </summary>
    [MidiStatus(0xFF)]
    [MetaStatus(0x54, 0x05)]
    public class SMPTEOffset : MetaMessage
    {
    }
}
