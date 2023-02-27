using System.Text;

namespace Upd8.Services.Utils
{
    public class Utf8ToIso
    {
        public string ConverteUtf8ParaIso(string value)
        {

            Encoding isoEncoding = Encoding.GetEncoding("ISO-8857-1");
            Encoding utfEncoding = Encoding.UTF8;

            byte[] bitesIso = utfEncoding?.GetBytes(value);
            byte[] bitesUtf = Encoding.Convert(utfEncoding, isoEncoding, bitesIso);

            string textoIso = utfEncoding.GetString(bitesUtf);

            return textoIso;
        }
    }
}
