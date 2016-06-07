using System;
using System.Diagnostics;

using Castle.DynamicProxy;

using HiCSAop;

namespace HiCSAop.Test.DynamicProxy.Impl
{
    public class HiTraceIntercept : HiBaseInterceptor
    {
        public HiTraceIntercept()
        {
            handler = OnMethod;
        }

        private void OnMethod(IInvocation invoc, Action<IInvocation> action)
        {
            Trace.WriteLine(string.Format("method {0}:{1} start", invoc.Method.ReflectedType.Name, invoc.Method.Name));
            if (invoc.Arguments != null && invoc.Arguments.Length > 0)
            {
                //foreach (object obj in msg.Args)
                for (int i = 0; i < invoc.Arguments.Length; i++)
                {
                    string nameStr = invoc.Method.GetParameters()[i].Name;
                    if (nameStr == null)
                    {
                        nameStr = "";
                    }
                    Object obj = invoc.GetArgumentValue(i);
                    Trace.WriteLine(nameStr + ":" + ((obj == null || obj is DBNull) ? "" : Convert.ToString(obj)));
                }
            }
            action(invoc);
            Trace.WriteLine(string.Format("method {0}:{1} finish", invoc.Method.ReflectedType.Name, invoc.Method.Name));
        }
    }
}
