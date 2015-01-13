using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;


namespace LostParticles.MusicFramework.Midi.Messages.Common
{
    

    /// <summary>
    /// oxFF is RESET Message to the MIDI device 
    /// the midi file specs reused this byte to its uses
    /// and had make it other many things.
    /// </summary>
    [MidiStatus(0xFF)]
    public class MetaMessage : MidiMessage
    {
        public MetaMessage()
            : base()
        {

            byte[] data = GetMetaStatus(this.GetType());

            //get first byte as the meta code.
            this.MetaCode = data[0];

            //allow storing for rest of bytes if exist
            for (int ix = 1; ix < data.Length; ix++)
            {
                //[ix+1] is starting from byte 2  <== remember byte[0] status  byte[1] metacode byte[2] length
                this[ix + 1] = data[ix];
            }



        }

        public MetaMessage(byte metaCode)
            :base(0xFF)
        {
            this.MetaCode = metaCode;
        }

        public byte MetaCode 
        {
            get 
            { 
                return this[1];
            }
            set 
            {
                this[1] = value;
            }
        }

        public byte MetaLength
        {
            get
            {
                return this[2];
            }
            set
            {
                this[2] = value;
            }
        }


        public byte[] MetaData 
        {
            get
            {
                //get from the third byte to the end of bytes
                
                return messageData.GetRange(3, MetaLength).ToArray();
                
            }
            set
            {
                //set the length to the length of passed array.
                MetaLength = (byte)value.Length;
                for (int i = 0; i < MetaLength; i++)
                {
                    this[3 + i] = value[i];
                }

            }
        }

        public override string ToString()
        {
            return base.ToString() + "[0x" + MetaCode.ToString("x").ToUpper() + "]";
        }



        public static byte[] GetMetaStatus(Type midiMessageType)
        {
            var cAttribute = midiMessageType.GetTypeInfo().GetCustomAttributes<MetaStatusAttribute>().FirstOrDefault();
            if (cAttribute!=null)
            {
                return cAttribute.Status;
            }
            else
                return new byte[] { 0x00 };
        }

        public static Type[] MetaMessagesTypes
        {
            get
            {
                Type metaMessageType = typeof(MetaMessage);



                var AllTypes = metaMessageType.GetTypeInfo().Assembly.DefinedTypes;
                List<Type> MetaTypes = new List<Type>();

                foreach(var type in AllTypes)
                {
                    if(type.IsSubclassOf(metaMessageType))
                    {
                        MetaTypes.Add(type.AsType());
                    }
                }

                return MetaTypes.ToArray();
            }
        }


    }



}
