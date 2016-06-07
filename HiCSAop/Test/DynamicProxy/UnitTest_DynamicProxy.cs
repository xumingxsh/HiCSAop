using System;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
﻿using Castle.DynamicProxy;

namespace HiCSAop.Test.DynamicProxy
{
    [TestClass]
    public class UnitTest_DynamicProxy
    {
        static HiBaseInterceptor interceptor = new HiBaseInterceptor();

        private static T New<T>() where T: class, new()
        {
            return interceptor.New<T>();
        }

        public UnitTest_DynamicProxy()
        {
            interceptor.handler = OnMethod;
        }

        /// <summary>
        /// 正常测试，设置属性成功，并获得期望结果
        /// </summary>
        [TestMethod]
        public void DynamicProxy_AOP_Method()
        {

            AOPTest.AOP_Test();
            AOPTest test = New<AOPTest>();  
            test.Name = "test";
            test.AOP_Test2();
            test.AOP_Test3(" arg1");


            AOPTest test3 = New<AOPTest>(); 
            test3.Name = "test3";
            test3.AOP_Test2();
            test3.AOP_Test3(" arg3");


            AOPTest2.AOP_Test();
            AOPTest2 test2 = New<AOPTest2>(); 
            test2.AOP_Test2();
            test2.AOP_Test3(5);
        }


        private static void OnMethod(IInvocation invoc, Action<IInvocation> action)
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

    public class AOPTest 
    {
        public string Name { set; get; }
        public static void AOP_Test()
        {
            System.Diagnostics.Trace.WriteLine("this is a test");
        }

        public virtual void AOP_Test2()
        {
            System.Diagnostics.Trace.WriteLine("this is a test2");
        }

        public virtual void AOP_Test3(string name)
        {
            System.Diagnostics.Trace.WriteLine(string.Format("this is a test3, {0}  Name:", name) + Name);
        }
    }

    public class AOPTest2
    {
        public static void AOP_Test()
        {
            System.Diagnostics.Trace.WriteLine("this is a test");
        }

        public void AOP_Test2()
        {
            System.Diagnostics.Trace.WriteLine("this is a test2");
        }

        public virtual void AOP_Test3(int argTest)
        {
            System.Diagnostics.Trace.WriteLine(string.Format("this is a test3, {0}", argTest));
        }
    }
}
