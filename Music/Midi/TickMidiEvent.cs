using System;
using LostParticles.MusicFramework.Midi.IO;
using LostParticles.MusicFramework.Midi.Messages.Channel;
using LostParticles.MusicFramework.Midi.Messages.Common;
using LostParticles.TicksEngine;

namespace LostParticles.MusicFramework.Midi
{
    /// <summary>
    /// This class used for playing midi messages.
    /// </summary>
    public class MidiTickEvent : TickEvent
    {

        public readonly IMidiOutputDevice MidiOutputDevice;

        public MidiMessage Message { get; set; }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="deltaTime">delta time before midi message sent to sequencer</param>
        public MidiTickEvent(IMidiOutputDevice midiOutputDevice, ulong deltaTime)
            : base((double)deltaTime, 0.1)
        {
            MidiOutputDevice = midiOutputDevice;
        }



        public override void Begin()
        {
            //send the message after the hold time is finished
            if (Message.GetType() == typeof(ProgramChange) || Message.GetType() == typeof(ChannelAftertouch))
            {
                if (MidiOutputDevice != null)
                    MidiOutputDevice.Send(Message[0], Message[1]);
            }
            else
            {
                if (MidiOutputDevice != null)
                    MidiOutputDevice.Send(Message[0], Message[1], Message[2]);
            }

            //Console.WriteLine(Message + "| Sent);
            
        }


    }
}
