using System;
using System.Runtime.Remoting.Messaging;

using System.Diagnostics;

namespace HiCSAop.Test.Impl
{
    [AttributeUsage(AttributeTargets.Class)]
    public class MethodInvokAttribute : HiCSAop.MethInvokBaseAttribute
    {
        public MethodInvokAttribute()
        {
            this.Handle = ExecuteFunction;
        }

        private static IMethodReturnMessage ExecuteFunction(IMethodCallMessage msg, Func<IMethodReturnMessage> evt)
        {
            Trace.WriteLine(string.Format("method {0}:{1} start", msg.TypeName, msg.MethodName));
            if (msg.Args != null && msg.ArgCount > 0)
            {
                //foreach (object obj in msg.Args)
                for (int i = 0; i < msg.ArgCount; i++ )
                {
                    object name = msg.GetArgName(i);
                    string nameStr = (name == null || name is DBNull) ? "" : Convert.ToString(name);
                    Object obj = msg.GetArg(i);
                    Trace.WriteLine(nameStr + ":" + ((obj == null || obj is DBNull) ? "" : Convert.ToString(obj)));
                }
            }
            IMethodReturnMessage retMsg = evt();
            Trace.WriteLine(string.Format("method {0}:{1} finish", msg.TypeName, msg.MethodName));
            return retMsg;
        }
    }
}
