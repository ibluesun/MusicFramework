using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using LostParticles.MusicFramework.Midi.Messages.Channel;
using LostParticles.MusicFramework.Midi.Messages.Common;


namespace LostParticles.MusicFramework.Midi.IO
{
    public class MidiTrackStream : MemoryStream
    {
        public static Dictionary<byte, Type> ChannelMessageTypes;

        public static Dictionary<byte, Type> MetaTypes;

        static MidiTrackStream()
        {

            #region Message Status Codes
            ChannelMessageTypes = new Dictionary<byte, Type>();

            for (byte bk = 0x80; bk < 0x90; bk++)
                ChannelMessageTypes.Add(bk, typeof(NoteOff));

            for (byte bk = 0x90; bk < 0xA0; bk++)
                ChannelMessageTypes.Add(bk, typeof(NoteOn));

            for (byte bk = 0xA0; bk < 0xB0; bk++)
                ChannelMessageTypes.Add(bk, typeof(KeyAftertouch));

            for (byte bk = 0xB0; bk < 0xC0; bk++)
                ChannelMessageTypes.Add(bk, typeof(ControlChange));

            for (byte bk = 0xC0; bk < 0xD0; bk++)
                ChannelMessageTypes.Add(bk, typeof(ProgramChange));

            for (byte bk = 0xD0; bk < 0xE0; bk++)
                ChannelMessageTypes.Add(bk, typeof(ChannelAftertouch));

            for (byte bk = 0xE0; bk < 0xF0; bk++)
                ChannelMessageTypes.Add(bk, typeof(PitchBend));

            #endregion

            #region Meta Messages
            //The meta even types

            //Cach the message types 

            MetaTypes = new Dictionary<byte, Type>();

            foreach (Type type in MetaMessage.MetaMessagesTypes)
            {
                MetaTypes.Add(MetaMessage.GetMetaStatus(type)[0], type);
            }
            

            #endregion
        }

        public MidiTrackStream()
            : base()
        {
        }

        public MidiTrackStream(byte[] data)
            : base(data)
        {
        }



        /// <summary>
        /// Discovere the next status byte and reading it.
        /// </summary>
        /// <returns></returns>
        public MidiEvent ReadMidiEvent()
        {
            MidiEvent me = new MidiEvent();

            me.DeltaTime = ReadVariableValue();


            me.Message = DiscoverMessage();


            return me;
        }


        /// <summary>
        /// Write Midi Even in the current track stream
        /// </summary>
        /// <param name="midiEvent"></param>
        public void WriteMidiEvent(MidiEvent midiEvent)
        {
            //write the delta time first

            WriteVariableValue(midiEvent.DeltaTime);


            //then write the midi message itself

            Write(midiEvent.Message.ToArray(), 0, midiEvent.Message.Length);


        }


        //A recommended approach for a receiving device is to maintain its "running status buffer" as so:
        //    1. Buffer is cleared (ie, set to 0) at power up.
        //    2. Buffer stores the status when a Voice Category Status (ie, 0x80 to 0xEF) is received.
        //    3. Buffer is cleared when a System Common Category Status (ie, 0xF0 to 0xF7) is received.
        //    4. Nothing is done to the buffer when a RealTime Category message is received.
        //    5. Any data bytes are ignored when the buffer is 0.

        byte runningStatus = 0;

        public MidiMessage DiscoverMessage()
        {
            
            byte newStatus = (byte)ReadByte();

            
            if (ChannelMessageTypes.ContainsKey(newStatus))
            {
                Type cmType = ChannelMessageTypes[newStatus];

                ChannelMessage cm = (ChannelMessage)Activator.CreateInstance(cmType);
                cm.Status = newStatus;

                if (cmType == typeof(ChannelAftertouch) || cmType == typeof(ProgramChange))
                {
                    cm[1] = (byte)ReadByte();
                }
                else
                {
                    cm[1] = (byte)ReadByte();
                    cm[2] = (byte)ReadByte();
                }

                runningStatus = newStatus;
               
                return cm;
            }
            else if (newStatus >= 0xF0 && newStatus <= 0xF7)
            {
                //System message

                //the running state buffer should be cleared here
                runningStatus = 0;

                //the system message could be exclusive or common.

                MidiMessage mm =  DiscoverSystemMessage(newStatus);


                return mm;

            }
            else if (newStatus >= 0xF8 && newStatus <= 0xFE)
            {
                //real system message.
                throw new NotImplementedException("System Real Message");
            }
            else if (newStatus == 0xFF)
            {
                return DiscoverMetaMessage();
            }
            else
            {
                //may be or should be a channel message with a running status.
                Type cmType = ChannelMessageTypes[runningStatus];

                ChannelMessage cm = (ChannelMessage)Activator.CreateInstance(cmType);
                cm.Status = runningStatus;

                if (cmType == typeof(ChannelAftertouch) || cmType == typeof(ProgramChange))
                {
                    cm[1] = newStatus;   //this is not status its first byte of channel message
                }
                else
                {
                    cm[1] = newStatus;
                    cm[2] = (byte)ReadByte();
                }

                return cm;

            }
        }


        /// <summary>
        /// Discover the Meta Message.
        /// </summary>
        /// <returns></returns>
        protected MetaMessage DiscoverMetaMessage()
        {

            byte metaCode = (byte)this.ReadByte();

            MetaMessage msg;
            if (MetaTypes.ContainsKey(metaCode))
            {
                msg = (MetaMessage)Activator.CreateInstance(MetaTypes[metaCode]);
            }
            else
            {
                msg = new MetaMessage(metaCode);
            }

            msg.MetaLength = (byte)ReadByte();
            byte[] buffer = new byte[msg.MetaLength];
            Read(buffer, 0, msg.MetaLength);

            msg.MetaData = buffer;


            return msg;

            
        }

        /// <summary>
        /// Dicovers and Reads the exlusive message.
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public MidiMessage DiscoverSystemMessage(byte status)
        {
            
            if (status == 0xF0)
            {

                SystemExclusiveMessage sem = new SystemExclusiveMessage();

                int size = (int)ReadVariableValue() + 1;  // I can't store this size because its variable in length
                byte[] message = new byte[size];

                Read(message, 0, size);

                sem.SystemExlusiveMessageData = message; //but I can store the message data itself


                return sem;
            }
            else if (status == 0xF7)
            {
                //this status message have two meaning ;)
                // First meaning as a termination for the exlusive message.
                // Second as a continuation to the exlusive message.

                //and to discover which one we should get the second byte and test it 


                // If this is an escaped message rather than a system exclusive 
                // continuation message.
                byte st = (byte)ReadByte();
                if ((st & 0x80) == 0x80)
                {
                    //then it is normal termination byte go for further investigation to the next message.
                    return DiscoverMessage();
                }
                else
                {
                    //contunation message.
                    SystemExclusiveMessage sem = new SystemExclusiveMessage();

                    sem.Status = 0xF7;  //the continuation message.

                    this.Position--; // I am standing on the a byte that needs to be taken in consideration again.
                    // and the ReadVariable Value will consume it again.
                    // you may ask why I didn't use peek byte instead of read
                    //      :)  none of your business. :P :P :P .

                    int size = (int)ReadVariableValue();  // I can't store this size because its variable in length
                    byte[] message = new byte[size];

                    Read(message, 0, size);

                    sem.SystemExlusiveMessageData = message; //but I can store the message data itself


                    return sem;

                }


            }
            else
            {
                
                throw new Exception("Unknown Systrem Exclusive MEssage: " + status.ToString("x"));
            }


        }

        public ulong ReadVariableValue()
        {
            ulong value;
            byte c;

            value = (ulong)this.ReadByte();
            

            if ((value & 0x80) != 0)
            {
                value &= 0x7F;
                do
                {
                    c = (byte)this.ReadByte();

                    value = (value << 7) + (ulong)((c) & 0x7F);

                }
                while ((c & 0x80) != 0);
            }

            return (value);
        }



        public void WriteVariableValue(ulong value)
        { 
            ulong buffer; 
            buffer = value & 0x7F;

            while ( (value >>= 7) != 0 )
            { 
                buffer <<= 8; buffer |= ((value & 0x7F) | 0x80); 
            }

            while (true) 
            {
                this.WriteByte((byte)buffer);

                if ((buffer & 0x80) != 0)
                {
                    buffer >>= 8;
                }
                else
                {
                    break;
                }
            }
        }







        public static object MessageDeserialize(MidiTrackStream trackStream, Type anyType)
        {
            int rawsize = Marshal.SizeOf(anyType);

            byte[] rawData = new Byte[rawsize];
            trackStream.Read(rawData, 0, rawsize);



            
            IntPtr buffer = Marshal.AllocHGlobal(rawsize);

            Marshal.Copy(rawData, 0, buffer, rawsize);

            object retobj = Marshal.PtrToStructure(buffer, anyType);

            Marshal.FreeHGlobal(buffer);

            return retobj;
        }





        public static byte[] RawSerialize(object anything)
        {

            int rawsize = Marshal.SizeOf(anything);

            IntPtr buffer = Marshal.AllocHGlobal(rawsize);

            Marshal.StructureToPtr(anything, buffer, false);

            byte[] rawdatas = new byte[rawsize];

            Marshal.Copy(buffer, rawdatas, 0, rawsize);

            Marshal.FreeHGlobal(buffer);

            return rawdatas;

        }
        public static object RawDeserialize(byte[] rawdatas, Type anytype)
        {

            int rawsize = Marshal.SizeOf(anytype);

            if (rawsize > rawdatas.Length)

                return null;

            IntPtr buffer = Marshal.AllocHGlobal(rawsize);

            Marshal.Copy(rawdatas, 0, buffer, rawsize);

            object retobj = Marshal.PtrToStructure(buffer, anytype);

            Marshal.FreeHGlobal(buffer);

            return retobj;



        }

    }
}
