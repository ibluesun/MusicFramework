using LostParticles.MusicFramework.Midi.IO;
using LostParticles.MusicFramework.Midi.Messages.Common;
using System;
using System.Runtime.InteropServices;

namespace MusicPlayer
{
    /// <summary>
    /// Represents MIDI output device capabilities.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct MidiOutCaps
    {
        #region MidiOutCaps Members

        /// <summary>
        /// Manufacturer identifier of the device driver for the Midi output 
        /// device. 
        /// </summary>
        public short mid;

        /// <summary>
        /// Product identifier of the Midi output device. 
        /// </summary>
        public short pid;

        /// <summary>
        /// Version number of the device driver for the Midi output device. The 
        /// high-order byte is the major version number, and the low-order byte 
        /// is the minor version number. 
        /// </summary>
        public int driverVersion;

        /// <summary>
        /// Product name.
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
        public string name;

        /// <summary>
        /// Flags describing the type of the Midi output device. 
        /// </summary>
        public short technology;

        /// <summary>
        /// Number of voices supported by an internal synthesizer device. If 
        /// the device is a port, this member is not meaningful and is set 
        /// to 0. 
        /// </summary>
        public short voices;

        /// <summary>
        /// Maximum number of simultaneous notes that can be played by an 
        /// internal synthesizer device. If the device is a port, this member 
        /// is not meaningful and is set to 0. 
        /// </summary>
        public short notes;

        /// <summary>
        /// Channels that an internal synthesizer device responds to, where the 
        /// least significant bit refers to channel 0 and the most significant 
        /// bit to channel 15. Port devices that transmit on all channels set 
        /// this member to 0xFFFF. 
        /// </summary>
        public short channelMask;

        /// <summary>
        /// Optional functionality supported by the device. 
        /// </summary>
        public int support;

        #endregion
    }

    public static class MidiDeviceFunctions
    {
        #region Win32 Midi Device Functions

        [DllImport("winmm.dll")]
        public static extern int midiConnect(int handleA, int handleB, int reserved);

        [DllImport("winmm.dll")]
        public static extern int midiDisconnect(int handleA, int handleB, int reserved);


        [DllImport("winmm.dll")]
        public static extern int midiOutOpen
            (out IntPtr midiOutHandle, int deviceID, int callBack,
            int callBackInstance, int dwFlags);

        [DllImport("winmm.dll")]
        public static extern int midiOutClose(IntPtr midiOutHandle);

        
        [DllImport("winmm.dll")]
        public static extern int midiOutReset(int handle);

        [DllImport("winmm.dll")]
        public static extern int midiOutShortMsg(
            IntPtr midiOutHandle, 
            UInt32 dwMessage);

        [DllImport("winmm.dll")]
        public static extern int midiOutPrepareHeader(int handle,
            IntPtr headerPtr, int sizeOfMidiHeader);

        [DllImport("winmm.dll")]
        public static extern int midiOutUnprepareHeader(int handle,
            IntPtr headerPtr, int sizeOfMidiHeader);

        [DllImport("winmm.dll")]
        public static extern int midiOutLongMsg(int handle,
            IntPtr headerPtr, int sizeOfMidiHeader);

        [DllImport("winmm.dll")]
        public static extern int midiOutGetDevCaps(int deviceID,
            ref MidiOutCaps caps, int sizeOfMidiOutCaps);

        [DllImport("winmm.dll")]
        public static extern int midiOutGetNumDevs();

        public const int MOM_OPEN = 0x3C7;
        public const int MOM_CLOSE = 0x3C8;
        public const int MOM_DONE = 0x3C9;

        public const int CALLBACK_NULL = 0x00000000;
        public const int NULL = 0x0;


        // Represents the method that handles messages from Windows.
        public delegate void MidiOutProc(int handle, int msg, int instance, int param1, int param2);


        #endregion
    }

    public sealed class MidiOutputDevice : IDisposable, IMidiOutputDevice
    {
        private readonly int deviceID;

        public int DeviceID
        {
            get { return deviceID; }
        }

        private readonly IntPtr midiOutHandle;

        public IntPtr MidiOutHandle
        {
            get { return midiOutHandle; }
        } 

        private readonly MidiOutCaps deviceCaps = new MidiOutCaps();


        public MidiOutputDevice(int deviceID)
        {
            MidiDeviceFunctions.midiOutGetDevCaps(
                deviceID,
                ref deviceCaps,
                Marshal.SizeOf(deviceCaps)
                );


            this.deviceID = deviceID;

            int result = MidiDeviceFunctions.midiOutOpen(
                out midiOutHandle,
                this.deviceID,
                MidiDeviceFunctions.NULL,
                MidiDeviceFunctions.NULL,
                MidiDeviceFunctions.CALLBACK_NULL);

            if (result > 0) throw new Exception("ERROR In opening midi device");


        }


        public void Send(ChannelMessage channelMessage)
        {

            if (channelMessage.Length == 2)
                Send(channelMessage.Status, channelMessage[1]);
            else if (channelMessage.Length == 3)
                Send(channelMessage.Status, channelMessage[1], channelMessage[2]);
            else if (channelMessage.Length == 4)
                Send(channelMessage.Status, channelMessage[1], channelMessage[2], channelMessage[3]);
            else
                throw new NotImplementedException("Bytes exceeds the allowable midi data to be sent.");
        }


        public void Send(byte status, byte data_1)
        {
            //from low order to higher order  remember MSX HL register

            UInt32 dwData = data_1;
            dwData = (dwData << 8) + status ;


            MidiDeviceFunctions.midiOutShortMsg(midiOutHandle, dwData);
        }

        public void Send(byte status, byte data_1, byte data_2)
        {
            UInt32 dwData = data_2;
            dwData = (dwData << 8) + data_1;
            dwData = (dwData << 8) + status;

            MidiDeviceFunctions.midiOutShortMsg(midiOutHandle, dwData);
        }

        public void Send(byte status, byte data_1, byte data_2, byte data_3)
        {
            UInt32 dwData = data_3;
            dwData = (dwData << 8) + data_2;
            dwData = (dwData << 8) + data_1;
            dwData = (dwData << 8) + status;

            MidiDeviceFunctions.midiOutShortMsg(midiOutHandle, dwData);
        }


        ~MidiOutputDevice()
        {
            Dispose(false);
        }


        #region IDisposable Members

        bool disposed;

        public void Dispose()
        {
            Dispose(true);

            GC.SuppressFinalize(this);
        }

        protected void Dispose(bool disposing)
        {

            // Check to see if Dispose has already been called.
            if (!this.disposed)
            {
                // If disposing equals true, dispose all managed
                // and unmanaged resources.
                if (disposing)
                {
                    // Dispose managed resources.

                }

                // Call the appropriate methods to clean up
                // unmanaged resources here.
                // If disposing is false,
                // only the following code is executed.

                MidiDeviceFunctions.midiOutClose(midiOutHandle);
                
                // Note disposing has been done.
                disposed = true;

            }

        }

        #endregion
    }
}
