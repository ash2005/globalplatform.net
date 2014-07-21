﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace globalplatform.net
{
    /// <summary>
    /// Represents repsonse APDU
    /// </summary>
    ///
    class ResponseAPDU
    {
        #region Static Fields
        /// <summary>
        /// SW1 offset in repsonse
        /// </summary>
        public static byte OFFSET_SW1 = 0x0;
        /// <summary>
        /// SW2 offset in response
        /// </summary>
        public static byte OFFSET_SW2 = 0x1;
        #endregion

        #region Private Fields
        private int mSW1;
        private int mSW2;
        private byte[] mData;

        #endregion
        
        #region Public Properties
        /// <summary>
        /// SW1
        /// </summary>
        public int SW1 { get { return mSW1; } }
        /// <summary>
        /// SW2
        /// </summary>
        public int SW2 { get { return mSW2; } }
        /// <summary>
        /// Response data
        /// </summary>
        public byte[] Data { get { return mData; } }
        #endregion
        
        #region Constructors
        /// <summary>
        /// Constructs a ResponseAPDU from sw1, sw2 and response data.
        /// </summary>
        /// <param name="sw1">sw1</param>
        /// <param name="sw2">sw2</param>
        /// <param name="data">response data</param>
        public ResponseAPDU(int sw1, int sw2, byte[] data)
        {
            mSW1 = sw1;
            mSW2 = sw2;
            mData = new byte[data.Length];
            System.Array.Copy(data, mData, mData.Length);
        }

        /// <summary>
        /// Constructs a ResponseAPDU from raw response.
        /// </summary>
        /// <param name="response">Raw respose</param>
        /// <exception cref="Exception">If raw response contains less than 2 bytes.</exception>
        public ResponseAPDU(byte[] response)
        {
            if(response.Length < 2)
                throw new Exception("Response APDU must be 2 bytes or more.");
            mSW1 = response[OFFSET_SW1];
            mSW2 = response[OFFSET_SW2];
            mData = new byte[response.Length - 2];
            if(mData.Length > 0)
                System.Array.Copy(response, 2, mData, 0, mData.Length);
        }
        #endregion
        
        #region Methods
        /// <summary>
        /// Converts ResponseAPDU to a byte array.
        /// </summary>
        /// <returns>Byte array corresponding to ResponseAPDU</returns>
        public byte[] ToByteArray()
        {
            byte[] result = new byte[mData.Length + 2];
            result[OFFSET_SW1] = (byte)mSW1;
            result[OFFSET_SW2] = (byte)mSW2;
            if(mData.Length > 0)
                System.Array.Copy(mData, 0, result, 2, mData.Length);
            return result;
        }
        #endregion

    }
}
