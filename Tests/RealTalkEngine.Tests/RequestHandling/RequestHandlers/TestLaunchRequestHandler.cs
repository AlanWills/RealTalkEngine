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
    public class TestLaunchRequestHandler
    {
        #region Is Handler For Request Tests

        [TestMethod]
        public void IsHandlerForRequest_InputtingNull_ReturnsFalse()
        {
            LaunchRequestHandler launchRequestHandler = new LaunchRequestHandler();

            Assert.IsFalse(launchRequestHandler.IsHandlerForRequest(null));
        }

        [TestMethod]
        public void IsHandlerForRequest_InputtingNonLaunchRequest_ReturnsFalse()
        {
            LaunchRequestHandler launchRequestHandler = new LaunchRequestHandler();
            SkillRequest request = new SkillRequest();
            request.Request = new SessionEndedRequest();

            Assert.IsFalse(launchRequestHandler.IsHandlerForRequest(request));

            request.Request = new IntentRequest();

            Assert.IsFalse(launchRequestHandler.IsHandlerForRequest(request));
        }

        [TestMethod]
        public void IsHandlerForRequest_InputtingLaunchRequest_ReturnsTrue()
        {
            LaunchRequestHandler launchRequestHandler = new LaunchRequestHandler();
            SkillRequest request = new SkillRequest();
            request.Request = new LaunchRequest();

            Assert.IsTrue(launchRequestHandler.IsHandlerForRequest(request));
        }

        #endregion
    }
}
