using System;
using System.Collections.Generic;
using System.IO;
using LostParticles.MusicFramework.Midi.Messages.Meta;
using LostParticles.TicksEngine;
using LostParticles.TicksEngine.Manager;


namespace LostParticles.MusicFramework.Midi.IO
{

    /// <summary>
    /// the class is used to play midi file
    /// its a sequencer
    /// </summary>
    public class MidiFileStream
    {
        BinaryReader midiReader;

        MidiHeader header;

        List<MidiTrack> tracks = new List<MidiTrack>();

        /// <summary>
        /// The midi file header and should be MThd
        /// </summary>
        public MidiHeader Header
        {
            get
            {
                return header;
            }
        }

        public IList<MidiTrack> Tracks
        {
            get
            {
                return tracks;
            }
        }

        public MidiFileStream()
        {
        }


        public MidiFileStream(Stream fileStream)
        {
           using (midiReader = new BinaryReader(fileStream))
           {

                header = new MidiHeader();

                header.Header = new string(midiReader.ReadChars(4));

                if (header.Header != MidiHeader.ID) throw new Exception("Not midi file");


                //all numbers are stord big endian ya3ny mesh el 3koosat beta3et intel

                header.HeaderLength = BitConverter.ToUInt32(ReadBigEndianBytes(midiReader, 4), 0);

                header.FormatType = BitConverter.ToUInt16(ReadBigEndianBytes(midiReader, 2), 0);
                header.NumTracks = BitConverter.ToUInt16(ReadBigEndianBytes(midiReader, 2), 0);
                header.Division = BitConverter.ToUInt16(ReadBigEndianBytes(midiReader, 2), 0);


                for (int i = 1; i <= header.NumTracks; i++)
                {
                    MidiTrack mtr = new MidiTrack();

                    mtr.Header = new string(midiReader.ReadChars(4));
                    mtr.DataLength = BitConverter.ToUInt32(ReadBigEndianBytes(midiReader, 4), 0);
                    mtr.Data = midiReader.ReadBytes((int)mtr.DataLength);

                    tracks.Add(mtr);

                }
            }


        }

        

        /// <summary>
        /// The function read bytes and return them reversible
        /// </summary>
        /// <param name="br"></param>
        /// <returns></returns>
        public byte[] ReadBigEndianBytes(BinaryReader br, int count)
        {
            byte[] b = br.ReadBytes(count);

            Stack<Byte> rb = new Stack<byte>(count);

            foreach (byte x in b)
            {
                rb.Push(x);
            }

            return rb.ToArray(); //return reversed bytes

        }

        public void WriteBigEndianBytes(BinaryWriter bw, byte[] data)
        {
            int ix = data.Length - 1;

            while (ix >= 0)
            {
                bw.Write(data[ix]);
                ix--;
            }
        }


        public MultiPlayer GetPlayer(IMidiOutputDevice outputDevice)
        {
            MultiPlayer MidiPlayer = new MultiPlayer();

            double LastBPM = 120.0 * this.Header.Division;

            foreach (MidiTrack track in this.Tracks)
            {

                MidiTrackStream ts = track.DataStream;


                HardwareTicksPlayer TrackPlayer = new HardwareTicksPlayer(LastBPM);

                while (1 == 1)
                {
                    MidiEvent me = ts.ReadMidiEvent();

                    MidiTickEvent tme = new MidiTickEvent(outputDevice, me.DeltaTime);

                    tme.Message = me.Message;

                    if (tme.Message is Tempo)
                    {
                        //set the tempo of the track.
                        LastBPM = ((Tempo)tme.Message).Bpm * this.Header.Division;
                        TrackPlayer.Tempo = LastBPM;
                    }

                    TrackPlayer.AddEvent(tme);

                    //Console.WriteLine(me.Message);

                    //sw.WriteLine(me.Message);

                    if (me.Message.GetType() == typeof(EndOfTrack))
                        break;
                }
                MidiPlayer.AddTicksPlayer(TrackPlayer);

            }

            return MidiPlayer;
        }


        public MemoryStream MidiStream
        {
            get
            {
                MemoryStream ms = new MemoryStream();

                BinaryWriter midiWriter = new BinaryWriter(ms);

                if (header == null)
                {
                    header = MidiHeader.GetHeader(tracks.Count);
                }

                midiWriter.Write(Header.Header.ToCharArray());

                WriteBigEndianBytes(midiWriter, BitConverter.GetBytes(Header.HeaderLength));

                WriteBigEndianBytes(midiWriter, BitConverter.GetBytes(Header.FormatType));

                WriteBigEndianBytes(midiWriter, BitConverter.GetBytes(Header.NumTracks));

                WriteBigEndianBytes(midiWriter, BitConverter.GetBytes(Header.Division));

                foreach (MidiTrack track in Tracks)
                {
                    midiWriter.Write(track.Header.ToCharArray());
                    WriteBigEndianBytes(midiWriter, BitConverter.GetBytes(track.DataLength));
                    midiWriter.Write(track.Data, 0, (int)track.DataLength);

                }

                return ms;
            }
        }



        public void Save(byte[] fileStream)
        {
            
            fileStream = MidiStream.ToArray();

        }

    }

}

