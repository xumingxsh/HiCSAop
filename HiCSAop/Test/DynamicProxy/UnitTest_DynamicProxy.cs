using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
﻿using Castle.DynamicProxy;

using HiCSAop.Test.DynamicProxy.Impl;

namespace HiCSAop.Test.DynamicProxy
{
    [TestClass]
    public class UnitTest_DynamicProxy
    {
        static HiTraceIntercept interceptor = new HiTraceIntercept();

        private static T New<T>() where T: class, new()
        {
            return interceptor.New<T>();
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
