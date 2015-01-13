using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LostParticles.MusicFramework.Midi.IO
{
    public interface IMidiOutputDevice
    {
        void Send(byte status, byte data_1);
        void Send(byte status, byte data_1, byte data_2);
        void Send(byte status, byte data_1, byte data_2, byte data_3);
    }
}
