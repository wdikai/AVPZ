using System;
using System.Collections.Generic;

namespace Connection
{
    class Request
    {
        private string url;
        private Dictionary<string, string> parametrs;

        public void SetURL(string _url)
        {
            this.url = _url;
        }

        public Request(string _url)
        {
            this.url = _url;
            this.parametrs = new Dictionary<string, string>();
        }

        public void AddParametr(string name, string value)
        {
            this.parametrs.Add(name, value);
        }

        public override string ToString()
        {
            string result = "";
            result = String.Concat(result, url);
            result = String.Concat(result, "?");
            
            ICollection<string> keys = parametrs.Keys;
            foreach (string param in keys)
            {
                result = String.Concat(result, param);
                result = String.Concat(result, "=");
                result = String.Concat(result, parametrs[param]);
                result = String.Concat(result, "&");
            }
           return result.Substring(0, result.Length - 1);
        }
    }
}
