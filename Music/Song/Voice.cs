using System;
using System.Collections.Generic;
using LostParticles.TicksEngine;
using LostParticles.MusicFramework.Midi.IO;
using LostParticles.MusicFramework.Midi;

using MidiMessages = LostParticles.MusicFramework.Midi.Messages;
using LostParticles.TicksEngine.Manager;

namespace LostParticles.MusicFramework.Song
{
    public class Voice:List<IVoiceNote>
    {

        #region Fields
        public readonly Song ParentSong;
        private string _clef = "violin";
        private string _meter = "4/4";

        private string _key = "";
        #endregion

        #region Properties
        public string Clef
        {
            get { return _clef; }
            set { _clef = value; }
        }

        public string Meter
        {
            get { return _meter; }
            set { _meter = value; }
        }

        #region some stuff based on meter
        public double Misura
        {
            get
            {
                //based on meter we will count the mesura ÈÇáÚÑÈì ãÇÒæÑÉ
                double misura_count = 0;

                string mz = "";
                if (_meter.ToUpperInvariant().ToCharArray()[0] == 'C')
                {
                    mz = "4/4";
                }
                else
                {
                    mz = _meter;
                }

                string[] mis = mz.Split('/');
                double mnum = double.Parse(mis[0], System.Globalization.CultureInfo.InvariantCulture);
                double mden = double.Parse(mis[1], System.Globalization.CultureInfo.InvariantCulture);

                double misura_duration = mnum / mden;
                Fraction fr = new Fraction(0, 1); // fr = 0/1

                foreach (IVoiceNote vnt in AllNotes)
                {
                    Note nt = vnt.GetType() == typeof(Note) ? (Note)vnt : ((Chord)vnt)[0];

                    Fraction fnt = new Fraction(nt.DurationNumerator, nt.DurationDenominator);

                    fr = fr + fnt;

                    if (misura_duration == fr)
                    {
                        //misure is complete count it
                        misura_count++;
                        fr = new Fraction(0, 1);
                    }
                }

                if (fr > 0)
                {
                    //incomplete misura
                    misura_count += fr;
                }

                return misura_count;
            }
        }

        public Note HalfNote
        {
            get
            {
                double m2 = Misura / 2;

                string mz = "";
                if (_meter.ToUpperInvariant().ToCharArray()[0] == 'C')
                {
                    mz = "4/4";
                }
                else
                {
                    mz = _meter;
                }

                string[] mis = mz.Split('/');
                double mnum = double.Parse(mis[0], System.Globalization.CultureInfo.InvariantCulture);
                double mden = double.Parse(mis[1], System.Globalization.CultureInfo.InvariantCulture);

                double misura_duration = mnum / mden;

                double Target_Duration = m2 * misura_duration;   //now 

                Fraction fr = new Fraction(0, 1); // fr = 0/1

                Note HalfNote = null;
                foreach (Note nt in AllNotes)
                {
                    Fraction fnt = new Fraction(nt.DurationNumerator, nt.DurationDenominator);
                    fr = fr + fnt;

                    if (fr  == Target_Duration)
                    {
                        //we reached the desired the half note
                        HalfNote = nt;
                        break;
                    }

                    if (fr > Target_Duration)
                    {
                        HalfNote = nt;
                        break;
                    }
                }

                return HalfNote;
            }
        }

        public int HalfNoteIndex
        {
            get
            {
                double m2 = Misura / 2;

                string mz = "";
                if (_meter.ToUpperInvariant().ToCharArray()[0] == 'C')
                {
                    mz = "4/4";
                }
                else
                {
                    mz = _meter;
                }

                string[] mis = mz.Split('/');
                double mnum = double.Parse(mis[0], System.Globalization.CultureInfo.InvariantCulture);
                double mden = double.Parse(mis[1], System.Globalization.CultureInfo.InvariantCulture);

                double misura_duration = mnum / mden;

                double Target_Duration = m2 * misura_duration;   //now 

                Fraction fr = new Fraction(0, 1); // fr = 0/1

                int HalfNote = 0;
                foreach (Note nt in AllNotes)
                {
                    Fraction fnt = new Fraction(nt.DurationNumerator, nt.DurationDenominator);
                    fr = fr + fnt;

                    if (fr  == Target_Duration)
                    {
                        //we reached the desired the half note
                        
                        break;
                    }

                    if (fr > Target_Duration)
                    {
                        
                        break;
                    }
                    HalfNote++;
                }

                return HalfNote;

            }
        
        }




        #endregion

        /// <summary>
        /// Return all notes absolutely
        /// </summary>
        public IVoiceNote[] AllNotes
        {
            get
            {
                List<IVoiceNote> allNotes = new List<IVoiceNote>();

                foreach (IVoiceNote ive in this)
                {
                    Type vtype = ive.GetType();
                    if (vtype == typeof(Note))
                    {
                        allNotes.Add((Note)ive);
                    }

                    if (vtype == typeof(Repeat))
                    {
                        allNotes.AddRange(((Repeat)ive).AllNotes);
                    }

                    if (vtype == typeof(Chord))
                    {
                        allNotes.Add(ive);
                    }
                }

                return allNotes.ToArray();
            }
        }

        public IVoiceNote LastNote
        {
            get
            {
                return AllNotes[AllNotes.Length - 1];
            }
        }


        /// <summary>
        /// Keys that alter the tone to Sharp or Bicar
        /// </summary>
        public string Key
        {
            get { return _key; }
            set { _key = value; }
        }

        /// <summary>
        /// Keys that  modify the song notes.
        /// </summary>
        public string[] Keys
        {
            get
            {
                return Key.ToUpperInvariant().Split(',');
            }
        }

        public Dictionary<string, string> ToneKeys
        {
            get
            {
                if (Key == "") return null;
                else
                {
                    //for every note in the list
                    //if the note is naked means only one character c d e f g a b
                    //then the key will be added to it
                    Dictionary<string, string> TK = new Dictionary<string, string>(Keys.Length);
                    foreach (string k in Keys)
                    {
                        //get first character
                        string ToneVal = k.ToCharArray()[0].ToString();
                        //get the rest of characters
                        string ToneKeyVal = k.Substring(1);
                        TK.Add(ToneVal, ToneKeyVal);
                    }
                    return TK;
                }
            }
        }
        #endregion


        private readonly int voiceIndex;

        public int VoiceIndex
        {
            get
            {
                return voiceIndex;
            }
        }

        public Voice(Song song, int voiceIndex)
        {
            ParentSong = song;
            this.voiceIndex = voiceIndex;
        }

        #region for Events to play the voice

        void FillTicksManager(TicksManager manager)
        {
            //load voice 1

            double LastDuration = 0.0;

            foreach (IVoiceNote note in this)
            {
                Type vtype = note.GetType();

                if (vtype == typeof(Chord))
                {
                    //put notes with 0 hold duration (means multiple tones in the same time)
                    int cc = 0;
                    foreach (Note chordNote in (Chord)note)
                    {
                        if (cc == 0) //first note is having the note at last duration
                            LastDuration = NoteNode(manager, chordNote, LastDuration);
                        else
                            LastDuration = NoteNode(manager, chordNote, 0);

                        cc++;
                    }
                }
                else if (vtype == typeof(Repeat))
                {
                    LastDuration = RepeatNode(manager, note, LastDuration);
                }
                else
                {
                    LastDuration = NoteNode(manager, note, LastDuration);
                }
            }
        }

        const string MidiOutputDevice = "MidiOutputDevice";
        public WindowsTicksPlayer GetWindowsPlayer(IMidiOutputDevice midiOutputDevice)
        {
            WindowsTicksPlayer TickManager = new WindowsTicksPlayer(ParentSong.Tempo);

            TickManager.Store(MidiOutputDevice, midiOutputDevice);

            FillTicksManager(TickManager);

            return TickManager;
        }

        public HardwareTicksPlayer GetHardwarePlayer(IMidiOutputDevice midiOutputDevice)
        {
            HardwareTicksPlayer TickManager = new HardwareTicksPlayer(ParentSong.Tempo);

            TickManager.Store(MidiOutputDevice, midiOutputDevice); 
            
            FillTicksManager(TickManager);

            return TickManager;
        }


        double RepeatNode(TicksManager eventsManager, IVoiceNote n, double lastDuration)
        {
            Repeat repeatNode = (Repeat)n;

            int times = repeatNode.Times;

            double LastDuration2 = lastDuration;

            while (times > 0)
            {
                foreach (IVoiceNote note in repeatNode)
                {
                    Type vtype = note.GetType();

                    if (vtype == typeof(Chord))
                    {
                        //put notes with 0 hold duration (means multiple tones in the same time)
                        int cc = 0;
                        foreach (Note chordnote in (Chord)note)
                        {
                            if (cc == 0) //first note is having the note at last duration
                                LastDuration2 = NoteNode(eventsManager, chordnote, LastDuration2);
                            else
                                LastDuration2 = NoteNode(eventsManager, chordnote, 0);

                            cc++;
                        }
                    }
                    else if (vtype == typeof(Repeat))
                    {
                        LastDuration2 = RepeatNode(eventsManager, note, LastDuration2);
                    }
                    else
                    {
                        LastDuration2 = NoteNode(eventsManager, note, LastDuration2);
                    }
                }
                times--;

            }

            return LastDuration2;
        }

        /// <summary>
        /// Add note to the tickevents manager
        /// and set its events to be fired later during playing
        /// </summary>
        /// <param name="eventsManager"></param>
        /// <param name="n"></param>
        /// <param name="lastDuration"></param>
        /// <returns></returns>
        double NoteNode(TicksManager eventsManager, IVoiceNote n, double lastDuration)
        {

            Note note = (Note)n;

            //get a fraction from whole note
            double duration = note.DurationValue;

            SongTickEvent toneTick = new SongTickEvent( eventsManager.Retrieve<IMidiOutputDevice>(MidiOutputDevice),
                this,
                GetMidiNote(note),
                lastDuration,
                duration
                );            

            eventsManager.AddEvent(toneTick);

            return note.NextNoteAfterValue;
        }


        /// <summary>
        /// Construct <see cref="MidiNote"/> from <see cref="Note"/>
        /// </summary>
        /// <param name="note"></param>
        /// <returns></returns>
        public MidiNote GetMidiNote(Note note)
        {
            
            //now the note value [c d e f g a b] [[~ #]!? *] [> <]?
            string NoteValue = note.Name;

            if (ToneKeys != null)
            {
                //apply keys to the feeded tone.

                if (NoteValue.Length == 1)
                {
                    if (ToneKeys.ContainsKey(NoteValue))
                        NoteValue += ToneKeys[NoteValue];
                }

                if (NoteValue.Length == 2)
                {
                    //it may be character < or >
                    char o = NoteValue.ToCharArray()[1];
                    if (o == '<' || o == '>')
                    {
                        //insert
                        //apply the key to tone
                        string mk = NoteValue.ToCharArray()[0].ToString();

                        if (ToneKeys.ContainsKey(mk))
                            NoteValue = NoteValue.Insert(1, ToneKeys[mk]);
                    }
                }
            }

            return MusicScale.GenerateMidiNote(note.Octave, NoteValue);
        }

        #endregion






        /// <summary>
        /// The voice converted to miditrack
        /// </summary>
        public MidiTrack MidiTrack
        {
            get
            {
                MidiTrackStream ms = new MidiTrackStream();

                MidiEvent tempo = new MidiEvent
                {
                    DeltaTime = 0,
                    Message = new MidiMessages.Meta.Tempo
                       {
                           Bpm = ParentSong.Tempo
                       }
                };

                ms.WriteMidiEvent(tempo);
                

                double restDuration = 0.0;

                foreach(IVoiceNote vnote  in AllNotes)
                {

                    double tpb = (double)MidiHeader.PPQN;
                    // I want to know how many ticks for the note
                    // duration = x <beats>
                    // note duration = x * tpb

                    if (vnote.GetType() == typeof(Chord))
                    {
                        Chord chord = (Chord)vnote;

                        //Note on all notes
                        foreach (Note note in chord.NotesFromShort)
                        {
                            MidiNote mn = GetMidiNote(note);
                            mn.Channel = voiceIndex * 2;   //so it is for the  0, 2, 4, 6, 8, 10, 12, 14, 16

                            MidiEvent on = new MidiEvent
                            {
                                DeltaTime = (ulong)(restDuration * tpb),
                                Message = new MidiMessages.Channel.NoteOn
                                {
                                    Channel = (mn.SemiTone == 64 ? mn.Channel : mn.Channel + 1),
                                    NoteNumber = (byte)mn.NoteNumber,
                                    Velocity = 127
                                }
                            };
                           
                            MidiEvent bend = new MidiEvent
                            {
                                DeltaTime = 0,
                                Message = new MidiMessages.Channel.PitchBend
                                {
                                    Channel = (mn.SemiTone == 64 ? mn.Channel : mn.Channel + 1),
                                    LSB = 0,
                                    MSB = (byte)mn.SemiTone
                                }
                            };

                            ms.WriteMidiEvent(bend);

                            ms.WriteMidiEvent(on);

                           
                        }

                        //Note Off all notes starting from the shortest note
                        foreach (Note note in chord.NotesFromShort)
                        {
                            MidiNote mn = GetMidiNote(note);
                            mn.Channel = voiceIndex * 2;   //so it is for the  0, 2, 4, 6, 8, 10, 12, 14, 16

                            MidiEvent off = new MidiEvent
                            {
                                DeltaTime = (ulong)(note.DurationValue * tpb),
                                Message = new MidiMessages.Channel.NoteOff
                                {
                                    Channel = (mn.SemiTone == 64 ? mn.Channel : mn.Channel + 1),
                                    NoteNumber = (byte)mn.NoteNumber,
                                    Velocity = 127
                                }
                            };

                            ms.WriteMidiEvent(off);


                        }

                    }
                    else
                    {

                        Note note = (Note)vnote;

                        MidiNote mn = GetMidiNote(note);

                        //to convert the note into midi
                        //
                        //   note on the key plus any hold notes before it.
                        //   wait the duration
                        //   note off the key.
                        //   


                        if (mn != null)
                        {
                            mn.Channel = voiceIndex * 2;   //so it is for the  0, 2, 4, 6, 8, 10, 12, 14, 16

                            MidiEvent on = new MidiEvent
                            {
                                DeltaTime = (ulong)(restDuration * tpb),
                                Message = new MidiMessages.Channel.NoteOn
                                {
                                    Channel = (mn.SemiTone == 64 ? mn.Channel : mn.Channel + 1),
                                    NoteNumber = (byte)mn.NoteNumber,
                                    Velocity = 127
                                }
                            };


                            MidiEvent bend = new MidiEvent
                            {
                                DeltaTime = 0,
                                Message = new MidiMessages.Channel.PitchBend
                                {
                                    Channel = (mn.SemiTone == 64 ? mn.Channel : mn.Channel + 1),
                                    LSB = 0,
                                    MSB = (byte)mn.SemiTone
                                }
                            };

                            MidiEvent off = new MidiEvent
                            {
                                DeltaTime = (ulong)(note.DurationValue * tpb),
                                Message = new MidiMessages.Channel.NoteOff
                                {
                                    Channel = (mn.SemiTone == 64 ? mn.Channel : mn.Channel + 1),
                                    NoteNumber = (byte)mn.NoteNumber,
                                    Velocity = 127
                                }
                            };

                            ms.WriteMidiEvent(bend);

                            ms.WriteMidiEvent(on);

                            ms.WriteMidiEvent(off);

                            restDuration = 0;
                        }
                        else
                        {
                            //hold
                            restDuration = note.DurationValue;

                        }
                    }
                }


                MidiEvent endEvent = new MidiEvent
                {
                    DeltaTime = 0,
                    Message = new MidiMessages.Meta.EndOfTrack()

                };

                ms.WriteMidiEvent(endEvent);
                

                return MidiTrack.FromMidiTrackStream(ms);
            }
        }




    }
}
