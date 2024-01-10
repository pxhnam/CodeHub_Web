using System.Security.Cryptography;
using System.Text;
namespace CodeHub.Service
{
    public class MD5
    {
        public static string hasd(string input)
        {
            using (MD5CryptoServiceProvider md5Hash = new MD5CryptoServiceProvider())
            {
                byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < data.Length; i++)
                {
                    builder.Append(data[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}