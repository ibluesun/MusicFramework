using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LostParticles.MusicFramework.Midi.Messages.Channel;

namespace MusicFrameworkTestingProject
{
    [TestClass]
    public class MidiUnitTest
    {
        [TestMethod]
        public void TestChannelMessage()
        {
            NoteOn no = new NoteOn();

            no.Channel = 12;

            Assert.AreEqual<int>(0x90, no.Command);

        }
    }
}
