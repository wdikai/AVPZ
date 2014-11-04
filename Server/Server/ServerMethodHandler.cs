using System;
using System.Reflection;

namespace Server
{
  public class ServerMethodHandler
  {
    public string Name { get { return _handlerMethod.Name; } }

    private readonly Type _handerType;
    private readonly MethodInfo _handlerMethod;

    public ServerMethodHandler(MethodInfo methodInfo)
    {
      _handlerMethod = methodInfo;
      _handerType = _handlerMethod.DeclaringType;
    }

    public string Process(params object[] parameters)
    {
      var handler = Activator.CreateInstance(_handerType);
      var result = _handlerMethod.Invoke(handler, parameters);
      return result.ToString();
    }
  }
}