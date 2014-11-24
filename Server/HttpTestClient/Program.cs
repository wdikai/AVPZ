using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace HttpTestClient
{
  class Program
  {
    static void Main(string[] args)
    {
      //var request = (HttpWebRequest)WebRequest.Create("http://185.50.248.10:313/TestServer/MainHandler.ashx/?methodName=signin&uid=0&data={%22login%22:%22a%22,%22password%22:%22a%22}");
      //var request = (HttpWebRequest)WebRequest.Create("http://185.50.248.10:313/TestServer/MainHandler.ashx/?methodName=getstaticdata&uid=0&data={\"\"}");

      //Создал запрос всё что после Master.ashx это аргументы запроса
      
      //Получил ответ
      Send();
      Console.Read();
    }

    static void RequestCallBack(IAsyncResult result)
    {
      var request = result.AsyncState as HttpWebRequest;
      var stream = request.EndGetRequestStream(result);
      request.BeginGetResponse(ResponceCallBack, request);
    }

    static void ResponceCallBack(IAsyncResult result)
    {
      var request = result.AsyncState as HttpWebRequest;
      var response = request.EndGetResponse(result) as HttpWebResponse;
      using (var sr = new StreamReader(response.GetResponseStream()))
      {
        string a = sr.ReadToEnd();
        Console.WriteLine(a);
      }

    }

    static void Send()
    {
      for (int i = 0; i < 1; i++)
      {
        var request = (HttpWebRequest)WebRequest.Create("http://185.50.248.10:313/TestServer/MainHandler.ashx/?methodName=getstaticdata&data");
        //var request = (HttpWebRequest)WebRequest.Create("http://185.50.248.10:313/TestServer/MainHandler.ashx/?methodName=signin&data={%22login%22:%22Nickitos%22,%22password%22:%2233321%22}");
        //var request = (HttpWebRequest)WebRequest.Create("http://185.50.248.10:313/TestServer/MainHandler.ashx/?methodName=buyunit&data={%22userid%22:%221%22,%22unitid%22:%221%22}");
        var response = (HttpWebResponse)request.GetResponse();
        
        //Получил поток ввода с ответа
        var resStream = response.GetResponseStream();

        //Прочитал поток в буфер байтов
        var buffer = new byte[response.ContentLength];
        resStream.Read(buffer, 0, (int)response.ContentLength);

        //Перевёл в чары учитывая кодировку UTF-8
        var charArr = new char[Encoding.UTF8.GetCharCount(buffer, 0, buffer.Length)];
        Encoding.UTF8.GetChars(buffer, 0, buffer.Length, charArr, 0);

        //Превратил в строку (потом её захерачить в Json объект нужно)
        var res = new string(charArr);
        Console.WriteLine(i + res);
      }
    }
  }
}
