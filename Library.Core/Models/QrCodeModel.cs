using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Core.Models
{
    public class QrCodeModel
    {
        public string BookQrCodeId { get; set; }
        public byte[] QrImageData { get; set; }
        public string QrImageMimeType { get; set; }
    }
}
