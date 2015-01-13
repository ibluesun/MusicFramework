using System;
using System.Collections.Generic;
using System.Reflection;


namespace LostParticles.MusicFramework.Midi
{
    public static class Instruments
    {
        public static class GM
        {
            public static class Piano
            {
                public const int AcousticGrandPiano = 0;
                public const int BrightAcousticPiano = 1;
                public const int ElectricGrandPiano = 2;
                public const int HonkyTonkPiano = 3;
                public const int ElectricPiano1 = 4;
                public const int ElectricPiano2 = 5;
                public const int Harpsichord = 6;
                public const int Clavinet = 7;

            }

            public static class ChromaticPercussion
            {
                public const int Celesta = 8;
                public const int Glockenspiel = 9;
                public const int MusicBox = 10;
                public const int Vibraphone = 11;
                public const int Marimba = 12;
                public const int Xylophone = 13;
                public const int TubularBells = 14;
                public const int Dulcimer = 15;

            }
            public static class Organ
            {
                public const int DrawbarOrgan = 16;
                public const int PercussiveOrgan = 17;
                public const int RockOrgan = 18;
                public const int ChurchOrgan = 19;
                public const int ReedOrgan = 20;
                public const int Accordion = 21;
                public const int Harmonica = 22;
                public const int TangoAccordion = 23;

            }
            public static class Guitar
            {
                public const int AcousticGuitarNylon = 24;
                public const int AcousticGuitarSteel = 25;
                public const int ElectricGuitarJazz = 26;
                public const int ElectricGuitarClean = 27;
                public const int ElectricGuitarMuted = 28;
                public const int OverdrivenGuitar = 29;
                public const int DistortionGuitar = 30;
                public const int GuitarHarmonics = 31;
            }
            public static class Bass
            {
                public const int AcousticBass = 32;
                public const int ElectricBassFinger = 33;
                public const int ElectricBassPick = 34;
                public const int FretlessBass = 35;
                public const int SlapBass1 = 36;
                public const int SlapBass2 = 37;
                public const int SynthBass1 = 38;
                public const int SynthBass2 = 39;
            }
            public static class Strings 
            {
                public const int Violin = 40;
                public const int Viola = 41;
                public const int Cello = 42;
                public const int Contrabass = 43;
                public const int TremoloStrings = 44;
                public const int PizzicatoStrings = 45;
                public const int OrchestralHarp = 46;
                public const int Timpani = 47;
            }
            public static class Ensemble 
            {
                public const int StringEnsemble1 = 48;
                public const int StringEnsemble2 = 49;
                public const int SynthStrings1 = 50;
                public const int SynthStrings2 = 51;
                public const int ChoirAahs = 52;
                public const int VoiceOohs = 53;
                public const int SynthVoice = 54;
                public const int OrchestraHit = 55;
            }
            public static class Brass 
            {
                public const int Trumpet = 56;
                public const int Trombone = 57;
                public const int Tuba = 58;
                public const int MutedTrumpet = 59;
                public const int FrenchHorn = 60;
                public const int BrassSection = 61;
                public const int SynthBrass1 = 62;
                public const int SynthBrass2 = 63;
            }
            public static class Reed
            {
                public const int SopranoSax = 64;
                public const int AltoSax = 65;
                public const int TenorSax = 66;
                public const int BaritoneSax = 67;
                public const int Oboe = 68;
                public const int EnglishHorn = 69;
                public const int Bassoon = 70;
                public const int Clarinet = 71;
            }


            public static class Pipe 
            {
                public const int Piccolo = 72;
                public const int Flute = 73;
                public const int Recorder = 74;
                public const int PanFlute = 75;
                public const int BlownBottle = 76;
                public const int Shakuhachi = 77;
                public const int Whistle = 78;
                public const int Ocarina = 79;
            }
            public static class SynthLead 
            {
                public const int Lead1Square = 80;
                public const int Lead2Sawtooth = 81;
                public const int Lead3Calliope = 82;
                public const int Lead4Chiff = 83;
                public const int Lead5Charang = 84;
                public const int Lead6Voice = 85;
                public const int Lead7Fifths = 86;
                public const int Lead8BassAndLead = 87;
            }
            public static class SynthPad 
            {
                public const int Pad1NewAge = 88;
                public const int Pad2Warm = 89;
                public const int Pad3Polysynth = 90;
                public const int Pad4Choir = 91;
                public const int Pad5Bowed = 92;
                public const int Pad6Metallic = 93;
                public const int Pad7Halo = 94;
                public const int Pad8Sweep = 95;
            }
            public static class SynthEffects 
            {
                public const int Fx1Rain = 96;
                public const int Fx2Soundtrack = 97;
                public const int Fx3Crystal = 98;
                public const int Fx4Atmosphere = 99;
                public const int Fx5Brightness = 100;
                public const int Fx6Goblins = 101;
                public const int Fx7Echoes = 102;
                public const int Fx8SciFi = 103;
            }
            public static class Ethnic 
            {
                public const int Sitar = 104;
                public const int Banjo = 105;
                public const int Shamisen = 106;
                public const int Koto = 107;
                public const int Kalimba = 108;
                public const int BagPipe = 109;
                public const int Fiddle = 110;
                public const int Shanai = 111;
            }
            public static class Percussive 
            {
                public const int TinkleBell = 112;
                public const int Agogo = 113;
                public const int SteelDrums = 114;
                public const int Woodblock = 115;
                public const int TaikoDrum = 116;
                public const int MelodicTom = 117;
                public const int SynthDrum = 118;
                public const int ReverseCymbal = 119;
            }
            public static class SoundEffects 
            {
                public const int GuitarFretNoise = 120;
                public const int BreathNoise = 121;
                public const int Seashore = 122;
                public const int BirdTweet = 123;
                public const int TelephoneRing = 124;
                public const int Helicopter = 125;
                public const int Applause = 126;
                public const int Gunshot = 127;
            }


            public static string[] Instruments
            {
                get
                {
                    TypeInfo tp = Type.GetType("LostParticles.Music.Midi.Instruments+GM").GetTypeInfo();

                    List<string> allinstruments = new List<string>();
                    
                    foreach (TypeInfo ntp in tp.DeclaredNestedTypes)
                    {                        
                        foreach(FieldInfo fi in ntp.DeclaredFields)
                        {
                            allinstruments.Add(fi.Name);
                        }
                    }

                    return allinstruments.ToArray();
                }
            }

        }
    }
}
