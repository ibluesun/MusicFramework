using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;

namespace LostParticles.MusicFramework.Midi.Messages.Common
{
    /// <summary>
    /// MidiMessage Class represent a midi message that accept two or more bytes.
    /// 
    /// </summary>
    public class MidiMessage
    {

        protected List<byte> messageData = new List<byte>();

        protected List<byte> MessageData
        {
            get { return messageData; }
        }

        /// <summary>
        /// Modify data of the given index
        /// expand the under data to contain the index.
        /// </summary>
        /// <param name="index"></param>
        /// <param name="dataByte"></param>
        private void Modify(int index, byte dataByte)
        {
            //increase the buffer to the specified index
            while (messageData.Count < (index + 1))
            {
                messageData.Add(0x00);
            }

            messageData[index] = dataByte;
            
        }

        public byte this[int index]
        {
            get
            {
                return messageData[index];
            }
            set
            {
                Modify(index, value);
            }
        }


        public byte Status
        {
            get
            {
                return this[0];
            }
            set
            {
                this[0] = value;
            }
        }


        public MidiMessage()
        {
            //try to get the status attribute that declared above the running type
            // and put it in status

            Status = GetStatus(this.GetType());

        }

        public MidiMessage(byte status)
        {
            this.Status = status;
        }

        public override string ToString()
        {
            return this.GetType().Name + "[0x" + Status.ToString("x").ToUpper() + "]";
        }


        public static byte GetStatus(Type midiMessageType)
        {
            
            
            var cAttribute = midiMessageType.GetTypeInfo().GetCustomAttributes<MidiStatusAttribute>().FirstOrDefault();


            if (cAttribute!=null)
            {
                return cAttribute.Status;
            }
            else
                return 0x00;

        }


        public int Length
        {
            get
            {
                return messageData.Count;
            }
        }
        public byte[] ToArray()
        {
            return MessageData.ToArray();
        }


    }
}
