using Alexa.NET;
using Alexa.NET.Request;
using Alexa.NET.Request.Type;
using Alexa.NET.Response;
using CelTestSharp;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RealTalkEngine.RequestHandling;
using RealTalkEngine.RequestHandling.RequestHandlers;
using RealTalkEngine.Tests.RequestHandling.RequestHandlers;
using System;
using System.Collections.Generic;
using System.Text;

namespace RealTalkEngine.Tests.RequestHandling
{
    [TestClass]
    public class TestRequestListener
    {
        #region Static Constructor

        [TestMethod]
        public void RequestListener_SkillRequestHandlerFactory_SetToDefaultValue()
        {
            Assert.IsNotNull(RequestListener.SkillRequestHandlerFactory);
        }

        #endregion

        #region Handle Request Tests

        [TestMethod]
        public void HandleRequest_NoMatchingSkillRequestHandler_ReturnsRequestContextFallbackResponse()
        {
            SkillResponse response = ResponseBuilder.Tell("Test");
            RequestListener.SkillRequestHandlerFactory = new DefaultSkillRequestHandlerFactory();
            RequestContext.FallbackResponse = response;
            SkillRequest request = new SkillRequest();
            request.Request = new IntentRequest();

            AssertExt.IsEmpty(RequestListener.SkillRequestHandlerFactory.SkillRequestHandlers);
            Assert.AreSame(response, RequestListener.HandleRequest(request, null));
        }

        [TestMethod]
        public void HandleRequest_WithMatchingSkillRequestHandler_ReturnsMatchingSkillRequestHandlerResponse()
        {
            SkillResponse response = ResponseBuilder.Tell("Test");
            MockSkillRequestHandler skillRequestHandler = new MockSkillRequestHandler();
            skillRequestHandler.HandleRequest_Result = response;

            RequestListener.SkillRequestHandlerFactory = new DefaultSkillRequestHandlerFactory();
            RequestListener.SkillRequestHandlerFactory.SkillRequestHandlers.Add(skillRequestHandler);
            SkillRequest request = new SkillRequest();
            request.Request = new IntentRequest();

            Assert.IsNotNull(RequestListener.SkillRequestHandlerFactory.SkillRequestHandlers.Exists(x => x.IsHandlerForRequest(request)));
            Assert.AreSame(response, RequestListener.HandleRequest(request, null));
        }

        #endregion
    }
}
