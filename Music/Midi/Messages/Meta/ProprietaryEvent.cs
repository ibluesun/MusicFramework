using System;
using System.Collections.Generic;
using System.Text;
using LostParticles.MusicFramework.Midi.Messages.Common;

namespace LostParticles.MusicFramework.Midi.Messages.Meta
{
    /// <summary>
    /// FF 7F len data
    ///  
    /// </summary>
    [MidiStatus(0xFF)]
    [MetaStatus(0x7F)]
    public class ProprietaryEvent : MetaMessage
    {
    }
}
