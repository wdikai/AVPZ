using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Server.Abstract;

namespace Server
{
  public class ServerMethodManager
  {
    public static readonly  Lazy<ServerMethodManager> Instance = new Lazy<ServerMethodManager>(() => new ServerMethodManager());
    private static List<ServerMethodHandler> _handlers;

    private ServerMethodManager()
    {
      
    }

    static ServerMethodManager()
    {
      var assemblies = AppDomain.CurrentDomain.GetAssemblies();
      var types = assemblies.SelectMany(assembly => assembly.GetTypes().Where(type => type.IsClass && type.GetInterface(typeof(IServerListener).Name) != null)).ToList();
      var methods = types.SelectMany(type => type.GetMethods().Where(method => method.GetCustomAttributes(typeof(ServerMethodAttribute), false).Length > 0)).ToList();
      _handlers = new List<ServerMethodHandler>();
      foreach (var method in methods)
      {
        _handlers.Add(new ServerMethodHandler(method));
      }
    }
    

    public ServerMethodHandler ResolveHandler(string name)
    {
      return _handlers.FirstOrDefault(method => method.Name.ToLower() == name);
    }
  }
}