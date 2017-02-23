using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotJEM.Json.Validation.Context;
using Newtonsoft.Json.Linq;
using NUnit.Framework;

namespace DotJEM.Json.Validation.Test.Context
{
    [TestFixture]
    public class DynamicContextTest
    {
        //TopContext cast = ((dynamic)context) as TopContext;
        //TopContext cast2 = ((dynamic)context);
        //TopContext cast3 = (TopContext)((dynamic)context);

        [Test]
        public void Cast_TopContextImplicitDynamic_IsCastableToTopContext()
        {
            dynamic context = new DynamicContext(new TopContext(), new JObject());
            TopContext casted = context;
            Assert.That(casted, Is.Not.Null);
        }

        [Test]
        public void Cast_TopContextExplicitDynamic_IsCastableToTopContext()
        {
            dynamic context = new DynamicContext(new TopContext(), new JObject());
            TopContext casted = (TopContext)context;
            Assert.That(casted, Is.Not.Null);
        }

        [Test]
        public void Cast_MiddleContextImplicitDynamic_IsCastableToTopContext()
        {
            dynamic context = new DynamicContext(new MiddleContext(), new JObject());
            TopContext casted = context;
            Assert.That(casted, Is.Not.Null);
        }

        [Test]
        public void Cast_MiddleContextExplicitDynamic_IsCastableToTopContext()
        {
            dynamic context = new DynamicContext(new MiddleContext(), new JObject());
            TopContext casted = (TopContext)context;
            Assert.That(casted, Is.Not.Null);
        }

        [Test]
        public void Cast_BottomContextImplicitDynamic_IsCastableToTopContext()
        {
            dynamic context = new DynamicContext(new BottomContext(), new JObject());
            TopContext casted = context;
            Assert.That(casted, Is.Not.Null);
        }

        [Test]
        public void Cast_BottomContextExplicitDynamic_IsCastableToTopContext()
        {
            dynamic context = new DynamicContext(new BottomContext(), new JObject());
            TopContext casted = (TopContext)context;
            Assert.That(casted, Is.Not.Null);
        }

        [Test]
        public void Cast_NotContextImplicitDynamic_IsNotCastableToTopContext()
        {
            dynamic context = new DynamicContext(new NotContext(), new JObject());
            TopContext casted = context;
            Assert.That(casted, Is.Null);
        }

        [Test]
        public void Cast_NotContextExplicitDynamic_IsNotCastableToTopContext()
        {
            dynamic context = new DynamicContext(new NotContext(), new JObject());
            Assert.That(() => (TopContext)context, Throws.Exception);
        }

        [Test]
        public void Casts_ContextContainingType_Succeeds()
        {
            IJsonValidationContext context = new DynamicContext(new TopContext(), new JObject());
            Assert.That(context as TopContext, Is.Null);
        }

        public class NotContext : IJsonValidationContext{}
        public class TopContext : IJsonValidationContext{}
        public class MiddleContext : TopContext{}
        public class BottomContext : MiddleContext{}
    }


}
