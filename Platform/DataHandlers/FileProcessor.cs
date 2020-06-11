using System.IO;
using System.Text;

namespace Platform.DataHandlers
{
    public class FileProcessor
    {
        
        // internal static string StoreImage(string filePath)
        // {
        //     var type = GetImageType(filePath);
        // }

        public static string GetImageType(string path)
        {
            string headerCode = GetHeaderInfo($"wwwroot/Files/Uploads/{path}").ToUpper();

            if (headerCode.StartsWith("FFD8FFE0"))
            {
                return "JPG";
            }
            else if (headerCode.StartsWith("49492A"))
            {
                return "TIFF";
            }
            else if (headerCode.StartsWith("424D"))
            {
                return "BMP";
            }
            else if (headerCode.StartsWith("474946"))
            {
                return "GIF";
            }
            else if (headerCode.StartsWith("89504E470D0A1A0A"))
            {
                return "PNG";
            }
            else
            {
                return ""; //UnKnown
            }
        }

        public static string GetHeaderInfo(string path)
        {
            byte[] buffer = new byte[8];

            BinaryReader reader = new BinaryReader(new FileStream(path, FileMode.Open));
            reader.Read(buffer, 0, buffer.Length);
            reader.Close();

            StringBuilder sb = new StringBuilder();
            foreach (byte b in buffer)
                sb.Append(b.ToString("X2"));

            return sb.ToString();
        }
    }
}