using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DiContainer;

namespace DiContainer.Tests
{
    [TestClass]
    public class DiContainerTests
    {
        [TestMethod]
        public void SingleComponent_DefaultConstructor()
        {
            var container = new Container();

            container.Register<TestD>();

            var result = container.Resolve<TestD>();

            Assert.IsNotNull(result);
            Assert.IsTrue(result is TestD);
        }

        [TestMethod]
        public void SingleComponent_UnresolvedDependency()
        {
            var container = new Container();
            container.Register<TestE>();

            try
            {
                var type = container.Resolve<TestE>();
            }
            catch(Exception exception)
            {
                Assert.IsTrue(exception.Message.StartsWith("Could not resolve", StringComparison.InvariantCultureIgnoreCase));
                return;
            }

            Assert.Fail("Should have caught this");
        }

        [TestMethod]
        public void SingleComponent_NotRegistered()
        {
            var container = new Container();

            try
            {
                var result = container.Resolve<ITest>();
            }
            catch (Exception exception)
            {
                Assert.IsTrue(exception.Message.StartsWith("Type not registered", StringComparison.InvariantCultureIgnoreCase));
                return;
            }

            Assert.Fail("Should have failed");
        }

        [TestMethod]
        public void Interface_Single()
        {
            var container = new Container();
            container.Register<ITest, TestA>();

            var result = container.Resolve<ITest>();

            Assert.IsNotNull(result);
            Assert.IsTrue(result is TestA);
        }

        [TestMethod]
        public void Interface_Multiple()
        {
            var container = new Container();

            container.Register<ITest, TestA>();
            container.Register<ITest2, TestC>();

            var result1 = container.Resolve<ITest>();
            Assert.IsNotNull(result1);
            Assert.IsTrue(result1 is TestA);

            var result2 = container.Resolve<ITest2>();
            Assert.IsNotNull(result2);
            Assert.IsTrue(result2 is TestC);
        }
    }

    public interface ITest
    {
    }

    public interface ITest2 : ITest
    {
    }

    public class TestA : ITest
    {
    }

    public class TestB : ITest
    {
    }

    public class TestC : ITest2
    {
    }

    public class TestD
    {
    }

    public class TestE
    {
        public TestE(TestD d)
        {
        }
    }
}
