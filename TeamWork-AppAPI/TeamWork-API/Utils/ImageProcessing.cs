﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeamWork.Common.ConstantStrings;

namespace TeamWork_API.Utils
{
    public static class ImageProcessing
    {
        public static string DecompressImage(string compressedBase64)
        {
            if (string.IsNullOrEmpty(compressedBase64))
            {
                return string.Empty;
            }

            Chilkat.Compression compress = new Chilkat.Compression
            {
                Algorithm = Constants.Deflate
            };

            Chilkat.BinData binDat = new Chilkat.BinData();
            binDat.AppendEncoded(compressedBase64, Constants.Base64);
            compress.DecompressBd(binDat);

            return binDat.GetEncoded(Constants.Base64);
        }
        public static string CompressImage(string strBase64)
        {
            if (string.IsNullOrEmpty(strBase64))
            {
                return string.Empty;
            }

            Chilkat.Compression compress = new Chilkat.Compression
            {
                Algorithm = Constants.Deflate
            };

            Chilkat.BinData binDat = new Chilkat.BinData();
            // Load the base64 data into a BinData object.
            // This decodes the base64. The decoded bytes will be contained in the BinData.
            binDat.AppendEncoded(strBase64, Constants.Base64);

            // Compress the BinData.
            compress.CompressBd(binDat);

            // Get the compressed data in base64 format:
            return binDat.GetEncoded(Constants.Base64);
        }
    }
}
