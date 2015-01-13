using LostParticles.MusicFramework.Midi;
using LostParticles.MusicFramework.Midi.IO;
using LostParticles.MusicFramework.Midi.Messages.Channel;
using LostParticles.MusicFramework.Song;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace MusicPlayer
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private MidiOutputDevice SystemOutputDevice = new MidiOutputDevice(0);

        #region playing note data directly

        private void PlayNote(MidiNote nd)
        {

            int channel = 0;
            if (nd.SemiTone != 64) channel = 1;

            NoteOn non = new NoteOn();
            non.Channel = channel;
            non.NoteNumber = (byte)nd.NoteNumber;
            non.Velocity = 127;

            SystemOutputDevice.Send(non.Status, non.NoteNumber, non.Velocity);

            PitchBend pb = new PitchBend();
            pb.Channel = channel;
            pb.Channel = channel;
            pb.LSB = 0;
            pb.MSB = (byte)nd.SemiTone;

            SystemOutputDevice.Send(pb[0], pb[1], pb[2]);

            toolStripStatusLabel1.Text = nd.ScientificNotation;
            toolStripStatusLabel2.Text = nd.NoteNumber.ToString();
            toolStripStatusLabel3.Text = nd.SemiTone.ToString();
        }

        private void StopNote(MidiNote nd)
        {
            int channel = 0;
            if (nd.SemiTone != 64) channel = 1;

            NoteOff non = new NoteOff();
            non.Channel = channel;
            non.NoteNumber = (byte)nd.NoteNumber;
            non.Velocity = 127;

            SystemOutputDevice.Send(non.Status, non.NoteNumber, non.Velocity);


            toolStripStatusLabel1.Text = "";
        }

        #endregion


        private void MMLPlayButton_Click(object sender, EventArgs e)
        {
            Song sng = new Song(120);

            sng.AddMMLTrack(MMLTextBox.Text);

            //var player = sng[0].GetWindowsPlayer(SystemOutputDevice);
            //player.Play();

            sng.GetPlayer(SystemOutputDevice).PlayAsynchronus();
        }

        private void MidiFileLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var result = MidiFileDialog.ShowDialog(this);

            if (result == System.Windows.Forms.DialogResult.OK)
            {
                MidiFileTextBox.Text = MidiFileDialog.FileName;
            }
        }

        private void MidiPlayButton_Click(object sender, EventArgs e)
        {

            MidiFileStream mfs = new MidiFileStream(File.OpenRead(MidiFileTextBox.Text));

            var player = mfs.GetPlayer(SystemOutputDevice);

            player.PlayAsynchronus();

            
        }

        private void MSongPlayButton_Click(object sender, EventArgs e)
        {
            XDocument xd = XDocument.Load(MSongFileTextBox.Text);

            Song sng = Song.SongFromXmlDocument(xd);

            sng.GetPlayer(SystemOutputDevice).PlayAsynchronus();
        }

        private void MSongFileLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var result = MSongFileDialog.ShowDialog(this);

            if (result == System.Windows.Forms.DialogResult.OK)
            {
                MSongFileTextBox.Text = MSongFileDialog.FileName;
            }

        }
    }
}
