using System;

namespace Server
{
  [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
  public class ServerMethodAttribute: Attribute{}
}