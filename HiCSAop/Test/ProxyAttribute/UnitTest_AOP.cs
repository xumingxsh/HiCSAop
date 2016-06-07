using System;
using System.Diagnostics;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using HiCSAop.Test.Impl;

namespace HiCSAop.Test
{
    [TestClass]
    public class UnitTest_AOP
    {
        /// <summary>
        /// 正常测试，设置属性成功，并获得期望结果
        /// </summary>
        [TestMethod]
        public void AOP_Method()
        {
            AOPTest.AOP_Test();
            AOPTest test = new AOPTest();
            test.Name = "test";
            test.AOP_Test2();
            test.AOP_Test3(" arg1");


            AOPTest test3 = new AOPTest();
            test3.Name = "test3";
            test3.AOP_Test2();
            test3.AOP_Test3(" arg3");


            AOPTest2.AOP_Test();
            AOPTest2 test2 = new AOPTest2();
            test2.AOP_Test2();
            test2.AOP_Test3(5);
        }
    }

    [MethodInvok]
    public class AOPTest : ContextBoundObject
    {
        public string Name { set; get; }
        public static void AOP_Test()
        {
            System.Diagnostics.Trace.WriteLine("this is a test");
        }

        public void AOP_Test2()
        {
            System.Diagnostics.Trace.WriteLine("this is a test2");
        }

        public void AOP_Test3(string name)
        {
            System.Diagnostics.Trace.WriteLine(string.Format("this is a test3, {0}  Name:", name) + Name);
        }
    }

    [MethodInvok]
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

        public void AOP_Test3(int name)
        {
            System.Diagnostics.Trace.WriteLine(string.Format("this is a test3, {0}", name));
        }
    }
}
