using System;
using System.Collections.Generic;
using System.Xml.Linq;


namespace LostParticles.MusicFramework.Song
{
    public partial class Song : List<Voice>
    {


        #region Data Manipulation


        public static Song SongFromXmlDocument(XDocument xd)
        {
            var Song = xd.Root;

            Song NewSong = new Song(int.Parse(Song.Attribute("Tempo").Value, System.Globalization.CultureInfo.InvariantCulture));

            if (Song.Attribute("Title") != null) NewSong.Title = Song.Attribute("Title").Value;
            if (Song.Attribute("Composer") != null) NewSong.Composer = Song.Attribute("Composer").Value;

            if(Song.Attribute("Name")!=null) NewSong.SongName = Song.Attribute("Name").Value;


            int CurrentVoiceIndex = 0;
            foreach (var xVoice in Song.Elements())
            {

                Voice vo = new Voice(NewSong, CurrentVoiceIndex);

                if (xVoice.Attribute("key") != null)
                {
                    vo.Key = xVoice.Attribute("key").Value;
                }
                if (xVoice.Attribute("clef") != null)
                {
                    vo.Clef = xVoice.Attribute("clef").Value;
                }
                if (xVoice.Attribute("meter") != null)
                {
                    vo.Meter = xVoice.Attribute("meter").Value;
                }



                //load voice 1
                foreach (var note in xVoice.Elements())
                {
                    IVoiceNote ive;
                    if (note.Name.LocalName == "Chord")
                    {
                        ive = new Chord();
                        foreach (var chordnote in note.Elements())
                        {
                            ((Chord)ive).Add(NoteNode(chordnote));
                        }
                    }
                    else if (note.Name.LocalName == "Repeat")
                    {
                        ive = RepeatNode(note);
                    }
                    else
                    {
                        ive = NoteNode(note);
                    }

                    vo.Add(ive);
                }


                NewSong.Add(vo);

                CurrentVoiceIndex++;

            }

            return NewSong;
        }

        private static Repeat RepeatNode(XElement repeatNode)
        {
            int times = int.Parse(repeatNode.Attribute("Times").Value, System.Globalization.CultureInfo.InvariantCulture);
            Repeat NewRepeat = new Repeat(times);

            foreach (XElement note in repeatNode.Elements())
            {
                IVoiceNote ive;
                if (note.Name.LocalName == "Chord")
                {
                    ive = new Chord();
                    foreach (XElement chordnote in note.Elements())
                    {
                        ((Chord)ive).Add(NoteNode(chordnote));
                    }
                }
                else if (note.Name.LocalName == "Repeat")
                {
                    ive = RepeatNode(note);
                }
                else
                {
                    ive = NoteNode(note);
                }
                NewRepeat.Add(ive);
            }

            return NewRepeat;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="TickManager"></param>
        /// <returns>Duration of note</returns>
        private static Note NoteNode(XElement note)
        {
            Note NewNote = new Note();

            NewNote.Octave = int.Parse(note.Attribute("Octave").Value, System.Globalization.CultureInfo.InvariantCulture);

            NewNote.Duration = note.Attribute("Duration").Value;

            //now the note value [c d e f g a b] [[~ #]!? *] [> <]?
            NewNote.Name = note.Attribute("Tone").Value.ToUpperInvariant();

            if (note.Attribute("NextNoteAfter") != null)
            {
                NewNote.NextNoteAfter = note.Attribute("NextNoteAfter").Value;
            }

            return NewNote;
        }

        #endregion


        #region Data Store

        public string SongName = "";
        
        public override string ToString()
        {
            return this.SongName;
        }

        public XDocument XmlDocumentFromSong()
        {
            XDocument xd = new XDocument();

            
            //xd.AppendChild(xd.CreateXmlDeclaration("1.0", "utf-8", String.Empty));
            xd.Declaration = new XDeclaration("1.1", "utf-8", "yes");

            //XmlElement xmlSong = xd.CreateElement("Song");
            XElement xmlSong = new XElement("Song");

            xmlSong.SetAttributeValue("Name", SongName);
            

            xmlSong.SetAttributeValue("Tempo", this.Tempo.ToString(System.Globalization.CultureInfo.InvariantCulture));
            xmlSong.SetAttributeValue("xmlns", "http://music.lostparticles.net/song");
            xmlSong.SetAttributeValue("Title", Title);
            xmlSong.SetAttributeValue("Composer", Composer);


            xd.Add(xmlSong);

            foreach (Voice vc in this)
            {
                XElement xmlVoice = new XElement("Voice");
                xmlVoice.SetAttributeValue("clef", vc.Clef);
                xmlVoice.SetAttributeValue("meter", vc.Meter);
                xmlVoice.SetAttributeValue("key", vc.Key);

                foreach (IVoiceNote ive in vc)
                {
                    
                    Type vtype = ive.GetType();
                    if (vtype == typeof(Note))
                    {
                        SaveXMLNote(xmlVoice, (Note)ive);
                    }

                    if (vtype == typeof(Repeat))
                    {

                        SaveXMLRepeat(xmlVoice, (Repeat)ive);
                    }

                    if (vtype == typeof(Chord))
                    {
                        
                        SaveXMLChord(xmlVoice, (Chord)ive);
                    }

                }
                xmlSong.Add(xmlVoice);
            }

            return xd;

        }



        private void SaveXMLNote(XElement xmlContainer, Note note)
        {
            XElement xmlVE = new XElement("Note");

            xmlVE.SetAttributeValue("Tone", note.Name);
            xmlVE.SetAttributeValue("Octave", note.Octave.ToString(System.Globalization.CultureInfo.InvariantCulture));
            xmlVE.SetAttributeValue("Duration", note.Duration);

            xmlContainer.Add(xmlVE);

        }

        private void SaveXMLChord(XElement xmlContainer, Chord chord)
        {
            XElement xmlVE = new XElement("Chord");

            foreach (Note note in chord)
            {
                SaveXMLNote(xmlVE, note);
            }
            xmlContainer.Add(xmlVE);
        }

        private void SaveXMLRepeat(XElement xmlContainer, Repeat repeat)
        {
            XElement xmlVE = new XElement("Repeat");
            xmlVE.SetAttributeValue("Times", repeat.Times.ToString(System.Globalization.CultureInfo.InvariantCulture));


            foreach (IVoiceNote ive in repeat)
            {
                Type vtype = ive.GetType();

                if (vtype == typeof(Note))
                    SaveXMLNote(xmlVE, (Note)ive);

                if (vtype == typeof(Repeat))
                    SaveXMLRepeat(xmlVE, (Repeat)ive);

                if (vtype == typeof(Chord))
                    SaveXMLChord(xmlVE, (Chord)ive);
                    
                
            }

            xmlContainer.Add(xmlVE);

        }


        #endregion

    }
}
