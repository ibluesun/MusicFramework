using System;
using System.Collections.Generic;
using System.Text;

namespace LostParticles.MusicFramework.Song
{
    public interface IVoiceNote
    {
        bool CanContainChildVoiceElements { get; }
    }

}
