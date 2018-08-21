using Alexa.NET.Request;
using Alexa.NET.Request.Type;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RealTalkEngine.RequestHandling.RequestHandlers;
using System;
using System.Collections.Generic;
using System.Text;

namespace RealTalkEngine.Tests.RequestHandling.RequestHandlers
{
    [TestClass]
    public class TestSessionEndedRequestHandler
    {
        #region Is Handler For Request Tests

        [TestMethod]
        public void IsHandlerForRequest_InputtingNull_ReturnsFalse()
        {
            SessionEndedRequestHandler handler = new SessionEndedRequestHandler();

            Assert.IsFalse(handler.IsHandlerForRequest(null));
        }

        [TestMethod]
        public void IsHandlerForRequest_InputtingNonSessionEndedRequest_ReturnsFalse()
        {
            SessionEndedRequestHandler handler = new SessionEndedRequestHandler();
            SkillRequest request = new SkillRequest();
            request.Request = new LaunchRequest();

            Assert.IsFalse(handler.IsHandlerForRequest(request));

            request.Request = new IntentRequest();

            Assert.IsFalse(handler.IsHandlerForRequest(request));
        }

        [TestMethod]
        public void IsHandlerForRequest_InputtingSessionEndedRequest_ReturnsTrue()
        {
            SessionEndedRequestHandler handler = new SessionEndedRequestHandler();
            SkillRequest request = new SkillRequest();
            request.Request = new SessionEndedRequest();

            Assert.IsTrue(handler.IsHandlerForRequest(request));
        }

        #endregion
    }
}
