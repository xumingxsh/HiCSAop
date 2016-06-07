using System;﻿
using System.Collections.Generic;
using Castle.DynamicProxy;

namespace HiCSAop
{
    public class HiBaseInterceptor : StandardInterceptor
    {
        public Action<IInvocation, Action<IInvocation>> handler = null;

        protected override void PreProceed(IInvocation invocation)
        {
            Console.WriteLine("调用前的拦截器，方法名是：{0}。", invocation.Method.Name);
            base.PreProceed(invocation);

        }

        protected override void PerformProceed(IInvocation invocation)
        {

            if (handler != null)
            {
                handler(invocation, base.PerformProceed);
            }
            //base.PerformProceed(invocation);

        }


        protected override void PostProceed(IInvocation invocation)
        {
            Console.WriteLine("调用后的拦截器，方法名是：{0}。", invocation.Method.Name);
            base.PostProceed(invocation);
        }  

        public T New<T>() where T: class, new()
        {
            return generator.CreateClassProxy<T>(this);  
        }
        
        static ProxyGenerator generator = new ProxyGenerator();
    }
}
