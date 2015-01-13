using System.Text;

namespace LostParticles.MusicFramework.Midi.Messages.Common
{
    [MidiStatus(0xF0)]
    public class SystemExclusiveMessage : MidiMessage
    {

        public int MessageLength
        {
            get
            {
                return messageData.Count - 1;
            }
        }

        public byte[] SystemExlusiveMessageData
        {
            get
            {
                //get from the third byte to the end of bytes

                return messageData.GetRange(1, MessageLength).ToArray();

            }
            set
            {
                //set the length to the length of passed array.
                for (int i = 0; i < value.Length; i++)
                {
                    this[1 + i] = value[i];
                }

            }
        }


        public string SystemExlusiveMessageMessage
        {
            get
            {
                //get all bytes but cut first and last one
                
                //return Encoding.ASCII.GetString(SystemExlusiveMessageData);
                return Encoding.UTF8.GetString(SystemExlusiveMessageData, 0, SystemExlusiveMessageData.Length);
            }
        }
    }
}
