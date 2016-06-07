using System;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Proxies;
using System.Runtime.Remoting.Contexts;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Remoting.Activation;
using System.Runtime.Remoting.Services;

namespace HiCSAop
{
    /// <summary>
    /// 方法执行前的处理
    /// </summary>
    /// <param name="call"></param>
    public delegate IMethodReturnMessage OnProcessHandle(IMethodCallMessage msg, Func<IMethodReturnMessage> evt);

    /// <summary>
    /// 方法AOP定制特性
    /// 用于添加日志,事务等
    /// </summary>
    public class MethInvokBaseAttribute : ProxyAttribute
    {
        private MarshalByRefObject target = null;
        public OnProcessHandle Handle { set; get; }

       
        //得到透明代理
        public override MarshalByRefObject CreateInstance(Type serverType)
        {
            //未初始化的实例
            if (target == null)
            {
                target = base.CreateInstance(serverType);
            }
            InterceptProxy rp = GetProxy(serverType);
            rp.Handle = Handle;
            return (MarshalByRefObject)rp.GetTransparentProxy();
        }

        public override RealProxy CreateProxy(ObjRef objRef, Type serverType, object serverObject, Context serverContext)
        {
            return GetProxy(serverType);
        }

        private InterceptProxy GetProxy(Type serverType)
        {
            //未初始化的实例
            if (target == null)
            {
                target = base.CreateInstance(serverType);
            }

            InterceptProxy rp = new InterceptProxy(target, serverType);
            rp.Handle = Handle;
            return rp;
        }
    }
}
