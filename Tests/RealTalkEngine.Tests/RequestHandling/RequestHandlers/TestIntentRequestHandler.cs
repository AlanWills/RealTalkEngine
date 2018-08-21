using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace RealTalkEngine.Tests.RequestHandling.RequestHandlers
{
    [TestClass]
    public class TestIntentRequestHandler
    {
        #region Is Handler For Request Tests

        [TestMethod]
        public void IsHandlerForRequest_InputtingNull_ReturnsFalse()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void IsHandlerForRequest_InputtingNonIntentRequest_ReturnsFalse()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void IsHandlerForRequest_InputtingIntentRequest_WithNoIntentSet_ReturnsFalse()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void IsHandlerForRequest_InputtingIntentRequest_WithIntentSet_ReturnsTrue()
        {
            Assert.Fail();
        }

        #endregion

        #region Handle Request Tests

        [TestMethod]
        public void HandleRequest_InputtingIntentRequestWithMatchingHandlerInFactory_ReturnsHandlerResponse()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void HandleRequest_InputtingIntentRequestWithNoMatchingHandlerInFactory_ButIntentNameInFactory_ReturnsCorrectResponseFromStory()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void HandleRequest_InputtingIntentRequestWithNoMatchingHandlerInFactory_NoMatchingIntentNameInFactory_ReturnsEmptyResponse()
        {
            Assert.Fail();
        }

        #endregion
    }
}
