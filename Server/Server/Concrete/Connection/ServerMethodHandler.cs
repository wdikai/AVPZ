using System;
using System.Linq;
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
      object result = null;
      if (parameters.Count() == 1 && parameters[0] == null)
      {
        result = _handlerMethod.Invoke(handler, null);
        return result.ToString();
      }
      result = _handlerMethod.Invoke(handler, parameters);
      return result.ToString();
    }
  }
}