using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;


namespace LAB2
{
    class GetTypeInfo
    {
        private Type _type;

        public GetTypeInfo(string type, bool throwOnErorr = true, bool ignoreCase = true)
        {

            try
            {
                _type = Type.GetType(type, throwOnErorr, ignoreCase);
            }
            catch (Exception e)
            {
                Console.WriteLine("Incorrect type name");
                
            }
        }

        public object InvokeMethod(string name, string[] args = null)
        {
            object item = new object();
            var methods = _type.GetMethods();
            foreach (var method in methods)
            {
                if (method.Name.Contains(name))
                {
                    item = Activator.CreateInstance(_type);

                    if (method.GetParameters().Length == args.Length)
                    {
                        try
                        {
                            var newArgs = ChangeParameters(method.GetParameters(), args);
                            return method.Invoke(item, newArgs);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e);
                            throw;
                        }
                    }
                }
            }
            return item;
        }

        public object InvokeMethod(string name,object[] args=null)
        {
            object item=new object();
            var methods = _type.GetMethods();
            foreach (var method in methods)
            {
                if (method.Name.Contains(name))
                {
                    item = Activator.CreateInstance(_type);
                    if(method.GetParameters().Length==args.Length)
                        try
                        {
                            return method.Invoke(item, args);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e);
                            throw;
                        }
                }
            }
            return item;
        }

        private object[] ChangeParameters(ParameterInfo[] parametersInfo, string[] parameters)
        {
            if (parametersInfo.Length != parameters.Length) return null;

            List<object> objParameters = new List<object>();

            for (int i = 0; i < parametersInfo.Length; i++)
            {
                try
                {
                    objParameters.Add(Convert.ChangeType(parameters[i], parametersInfo[i].ParameterType));
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
            return objParameters.ToArray();
        }

        public string GetInfo()
        {
            if (_type == null) return "error";
            string output = String.Empty;
            output += "Type Name:" + _type.Name + "\n";
            output += "Interfaces:\n";
            output += GetPartsInfo(_type.GetInterfaces());           
            output += "Constructors:\n";
            output += GetPartsInfo(_type.GetConstructors());
            output += "Fields:\n";
            output += GetPartsInfo(_type.GetFields());
            output += "Members:\n";
            output += GetPartsInfo(_type.GetMembers());
            output += "Methods:\n";
            output += GetPartsInfo(_type.GetMethods());
            output += "Events:\n";
            output += GetPartsInfo(_type.GetEvents());

            return output;
        }

        private string GetPartsInfo(object[] objs)
        {
            string output= String.Empty;

            foreach (var obj in objs)
            {
                output += obj+"\n";
            }
            return output;
        }

        public object Create()
        {
            object item=null;

            try
            {
                 item = Activator.CreateInstance(_type);
            }
            catch (Exception e)
            {
                Console.WriteLine("There is no default constructor");
            }
            return item;
        }

    }
}
