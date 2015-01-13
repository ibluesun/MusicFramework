using System;
using System.Collections.Generic;
using System.Text;
using LostParticles.MusicFramework.Midi.Messages.Common;

namespace LostParticles.MusicFramework.Midi.Messages.Meta
{
    /// <summary>
    /// FF 59 02 sf mi
    ///  
    /// </summary>
    [MidiStatus(0xFF)]
    [MetaStatus(0x59, 0x02)]
    public class KeySignature : MetaMessage
    {
    }
}
