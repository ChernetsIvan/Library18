using System.Drawing;
using MessagingToolkit.QRCode.Codec;

namespace Library.Core.Utility
{
    public static class QRcodeGenerator
    {
        private const int MaxLenStrToEncode = 858;
        public static Image GenerateQrImage(string strToEncode)
        {
            string str = strToEncode.Length > MaxLenStrToEncode ? strToEncode.Substring(0, MaxLenStrToEncode) : strToEncode;
            Image img = QrGen(str, GetQrLevel(str));
            return img;
        }
        private static Image QrGen(string input, int qrLevel)
        {
            string toenc = input;
            QRCodeEncoder qe = new QRCodeEncoder
            {
                QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE,
                QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.L,
                QRCodeVersion = qrLevel
            };
            Bitmap bm = qe.Encode(toenc);
            return bm;
        }

        private static int GetQrLevel(string strToEncode)
        {
            int len = strToEncode.Length;
            if (len <= 17) return 1;
            if (len <= 32) return 2;
            if (len <= 53) return 3;
            if (len <= 78) return 4;
            if (len <= 108) return 5;
            if (len <= 134) return 6;
            if (len <= 154) return 7;
            if (len <= 192) return 8;
            if (len <= 230) return 9;
            if (len <= 271) return 10;
            if (len <= 321) return 11;
            if (len <= 367) return 12;
            if (len <= 425) return 13;
            if (len <= 458) return 14;
            if (len <= 520) return 15;
            if (len <= 586) return 16;
            if (len <= 644) return 17;
            if (len <= 718) return 18;
            if (len <= 792) return 19;
            if (len <= MaxLenStrToEncode) return 20;
            return 21;
        }
    }
}

