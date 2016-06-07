using System;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Proxies;
using System.Runtime.Remoting.Contexts;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Remoting.Activation;
using System.Runtime.Remoting.Services;

namespace HiCSAop
{
    class InterceptProxy : RealProxy
    {
        private readonly MarshalByRefObject target;
        public OnProcessHandle Handle { set; get; }

        public InterceptProxy(MarshalByRefObject obj, Type type)
            : base(type)
        {
            this.target = obj;
        }

        public override IMessage Invoke(IMessage msg)
        {
            IMethodCallMessage call = (IMethodCallMessage)msg;

            IConstructionCallMessage ctor = call as IConstructionCallMessage;
            if (ctor != null)
            {
                RealProxy default_proxy = RemotingServices.GetRealProxy(this.target);
                default_proxy.InitializeServerObject(ctor);
                MarshalByRefObject tp = (MarshalByRefObject)this.GetTransparentProxy();
                return EnterpriseServicesHelper.CreateConstructionReturnMessage(ctor, tp);
            }

            if (Handle == null)
            {
                return RemotingServices.ExecuteMessage(this.target, call);
            }
            else
            {
                return Handle(call, () =>
                {
                    return RemotingServices.ExecuteMessage(this.target, call);
                });
            }
        }
    }
}
