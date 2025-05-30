using AudioSynthesis.Synthesis;
using LostParticles.MusicFramework.Midi.IO;
using LostParticles.MusicFramework.Midi.Messages.Common;
using SharpDX;
using SharpDX.Multimedia;
using SharpDX.XAudio2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LostParticles.MusicFramework.XAudioMidi
{
    public class XMidiSynthesizer : IMidiOutputDevice, IDisposable
    {
        Synthesizer MainSynth;

        XAudio2 xaudio;
        MasteringVoice MainMasteringVoice;

        SourceVoice MainSourceVoice;
        byte[] MidiVoiceDataFragment;
        DataStream MidiVoiceDataStream;

        WaveFormat VoiceWaveFormat;


        public XMidiSynthesizer(string soundFont)
        {
            // initialize synthesizer
            MainSynth = new Synthesizer(44100, 2);
            MainSynth.LoadBank(new SoundFontFile(soundFont));

            MidiVoiceDataFragment = new byte[MainSynth.RawBufferSize];

            // initialize xaudio
            xaudio = new XAudio2();
            MainMasteringVoice = new MasteringVoice(xaudio);

            VoiceWaveFormat = new WaveFormat();
            MainSourceVoice = new SourceVoice(xaudio, VoiceWaveFormat, true);

            MainSourceVoice.BufferEnd += MainSourceVoice_BufferEnd;
            MainSourceVoice.StreamEnd += MainSourceVoice_StreamEnd;

        }


        void MainSourceVoice_BufferEnd(IntPtr obj)
        {
            FillMainSourceVoice();
        }

        void MainSourceVoice_StreamEnd()
        {
            //System.Diagnostics.Debug.WriteLine("Main Voice at " + Environment.TickCount);            

        }

        int callTimes = 0;
        void FillMainSourceVoice()
        {
            if (MidiVoiceDataStream != null) MidiVoiceDataStream.Dispose();

            try
            {
                MainSynth.GetNext(MidiVoiceDataFragment);
            }
            catch(Exception exo)
            {
                // do nothing and continue
                System.Diagnostics.Debug.WriteLine(exo.Message);
                //MainSynth.ResetSynthControls();
            }


            MidiVoiceDataStream = DataStream.Create(MidiVoiceDataFragment, true, false);


            var buffer = new AudioBuffer
            {
                Stream = MidiVoiceDataStream,
                AudioBytes = (int)MidiVoiceDataStream.Length,
                Flags = BufferFlags.EndOfStream
            };

            MainSourceVoice.SubmitSourceBuffer(buffer, null);
        }


        bool TurnedOn = false;
        private bool disposedValue;

        public void TurnOn()
        {
            TurnedOn = true;

            FillMainSourceVoice();
            MainSourceVoice.Start();

        }

        public void TurnOff()
        {
            TurnedOn = false;
            MainSourceVoice.Stop();
        }


        public void Send(ChannelMessage channelMessage)
        {
            MainSynth.ProcessMidiMessage(channelMessage.Channel, channelMessage.Command, channelMessage[1], channelMessage[2]);
        }



        public void Send(byte status, byte data_1)
        {

            int channel = ChannelMessage.StatusChannel(status);
            int command = ChannelMessage.StatusCommand(status);

            MainSynth.ProcessMidiMessage(channel, command, data_1, 0);
        }

        public void Send(byte status, byte data_1, byte data_2)
        {
            int channel = ChannelMessage.StatusChannel(status);
            int command = ChannelMessage.StatusCommand(status);

            MainSynth.ProcessMidiMessage(channel, command, data_1, data_2);
        }

        public void Send(byte status, byte data_1, byte data_2, byte data_3)
        {
            throw new NotImplementedException();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                TurnOff();
                xaudio.Dispose();

                // TODO: set large fields to null
                disposedValue = true;
            }
        }


        ~XMidiSynthesizer()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: false);
        }



        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
