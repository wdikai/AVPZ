using System.Net;
using System.Text;

namespace Connection
{
    class HTTPConnection
    {

        public static string Execute(Request _request)
        {
            var request = (HttpWebRequest)WebRequest.Create(_request.ToString());
            var response = (HttpWebResponse)request.GetResponse();
            var responseStream = response.GetResponseStream();
            
            var buffer = new byte[response.ContentLength];
            responseStream.Read(buffer, 0, (int)response.ContentLength);
            var chars = new char[Encoding.UTF8.GetCharCount(buffer, 0, buffer.Length)];
            Encoding.UTF8.GetChars(buffer, 0, buffer.Length, chars, 0);
            var res = new string(chars);

            return res;
        }

    }
}
