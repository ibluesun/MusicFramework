using System;
using System.Collections.Generic;
using System.Text;
using LostParticles.MusicFramework.Midi.Messages.Common;
using System.Runtime.InteropServices;

namespace LostParticles.MusicFramework.Midi.Messages.Meta
{
    /// <summary>
    /// FF 02 len text
    /// </summary>
    [MidiStatus(0xFF)]
    [MetaStatus(0x02)]
    public class Copyright : Text
    {



    }
}
