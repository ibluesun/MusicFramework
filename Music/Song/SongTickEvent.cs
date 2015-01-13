using System;
using LostParticles.MusicFramework.Midi;
using LostParticles.TicksEngine;
using LostParticles.MusicFramework.Midi.IO;
using LostParticles.MusicFramework.Midi.Messages.Channel;

namespace LostParticles.MusicFramework.Song
{

    /// <summary>
    /// ToneTick Class is inherited from Tick Class
    /// which entered into the TickEventsManager Class
    /// to customize the procedures of event start stop 
    /// </summary>
    public sealed class SongTickEvent : TickEvent
    {
        private readonly MidiNote midi_note;

        /// <summary>
        /// The originating voice index in this song
        /// </summary>
        private readonly Voice SourceVoice;


        public readonly IMidiOutputDevice MidiOutputDevice;

        public SongTickEvent(IMidiOutputDevice midiOutputDevice, Voice sourceVoice, MidiNote midiNote, double holdBeat, double durationBeat)
            : base(holdBeat, durationBeat)
        {
            //Remember that null NoteData is expressing REST

            midi_note = midiNote;

            SourceVoice = sourceVoice;
            MidiOutputDevice = midiOutputDevice;
        }


        public override void Begin()
        {
            if (midi_note != null)
            {
                midi_note.Channel = 2 * SourceVoice.VoiceIndex;  //selecting channel based on voiceindex in song every voice has two channels

                int channel = 0;
                if (midi_note.SemiTone != 64) channel = 1;

                NoteOn non = new NoteOn();
                non.Channel = channel;
                non.NoteNumber = (byte)midi_note.NoteNumber;
                non.Velocity = 127;

                MidiOutputDevice.Send(non.Status, non.NoteNumber, non.Velocity);

                PitchBend pb = new PitchBend();
                pb.Channel = channel;
                pb.Channel = channel;
                pb.LSB = 0;
                pb.MSB = (byte)midi_note.SemiTone;

                MidiOutputDevice.Send(pb[0], pb[1], pb[2]);
            }
                                
        }

        public override void End()
        {
            if (midi_note != null)
            {
                midi_note.Channel = 2 * SourceVoice.VoiceIndex;  //selecting channel based on voiceindex in song every voice has two channels

                int channel = 0;
                if (midi_note.SemiTone != 64) channel = 1;

                NoteOff non = new NoteOff();
                non.Channel = channel;
                non.NoteNumber = (byte)midi_note.NoteNumber;
                non.Velocity = 127;

                MidiOutputDevice.Send(non.Status, non.NoteNumber, non.Velocity);

            }
            
        }
    }
}
