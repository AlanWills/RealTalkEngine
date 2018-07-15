using Alexa.NET;
using Alexa.NET.Request;
using Alexa.NET.Request.Type;
using Alexa.NET.Response;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RealTalkEngine.RequestHandling;
using RealTalkEngine.Tests.RequestHandling.RequestHandlers;
using System;
using System.Collections.Generic;
using System.Text;

namespace RealTalkEngine.Tests.RequestHandling
{
    [TestClass]
    public class TestRequestContext
    {
        #region Constructor Tests

        [TestMethod]
        public void Constructor_SetsRequest_ToInputtedValue()
        {
            SkillRequest skillRequest = new SkillRequest();
            skillRequest.Request = new LaunchRequest();
            RequestContext requestContext = new RequestContext(skillRequest, null, null);

            Assert.AreSame(skillRequest, requestContext.Request);
        }

        [TestMethod]
        public void Constructor_SetsSession_ToInputtedValue()
        {
            SkillRequest skillRequest = new SkillRequest();
            skillRequest.Request = new LaunchRequest();
            skillRequest.Session = new Session();
            RequestContext requestContext = new RequestContext(skillRequest, null, null);

            Assert.AreSame(skillRequest.Session, requestContext.Session);
        }

        [TestMethod]
        public void Constructor_SetsRequestHandler_ToInputtedValue()
        {
            MockSkillRequestHandler handler = new MockSkillRequestHandler();
            RequestContext requestContext = new RequestContext(null, null, handler);

            Assert.AreSame(handler, requestContext.SkillRequestHandler);
        }

        [TestMethod]
        public void Constructor_InputtingNullRequestHandler_DoesNothing()
        {
            MockSkillRequestHandler handler = new MockSkillRequestHandler();

            // This should not throw an exception
            RequestContext requestContext = new RequestContext(null, null, null);
        }

        [TestMethod]
        public void Constructor_InputtingNonNullRequestHandler_SetsRequestHandlerRequestContext_ToCreatedContext()
        {
            MockSkillRequestHandler handler = new MockSkillRequestHandler();
            RequestContext requestContext = new RequestContext(null, null, handler);

            Assert.AreSame(requestContext, handler.RequestContext);
        }
        
        #endregion

        #region Handle Request Tests

        [TestMethod]
        public void HandleRequest_WithNullSkillRequestHandler_ReturnsFallbackReponse()
        {
            SkillResponse response = ResponseBuilder.Tell("Test");
            RequestContext requestContext = new RequestContext(null, null, null);
            requestContext.FallbackResponse = response;

            Assert.IsNull(requestContext.SkillRequestHandler);
            Assert.AreSame(response, requestContext.HandleRequest());
        }

        [TestMethod]
        public void HandleRequest_WithNonNullSkillRequestHandler_ReturnsHandlerReponse()
        {
            SkillResponse response = ResponseBuilder.Tell("Test");
            MockSkillRequestHandler requestHandler = new MockSkillRequestHandler();
            requestHandler.HandleRequest_Result = response;
            RequestContext requestContext = new RequestContext(null, null, requestHandler);

            Assert.AreSame(requestHandler, requestContext.SkillRequestHandler);
            Assert.AreSame(response, requestContext.HandleRequest());
        }

        #endregion

        #region Disposable Tests

        [TestMethod]
        public void Dispose_WithNullSkillRequestHandler_DoesNothing()
        {
            RequestContext requestContext = new RequestContext(null, null, null);

            Assert.IsNull(requestContext.SkillRequestHandler);

            // This should do nothing
            requestContext.Dispose();
        }

        [TestMethod]
        public void Dispose_WithNonNullSkillRequestHandler_SetsSkillRequestHandlerRequestContext_ToNull()
        {
            MockSkillRequestHandler requestHandler = new MockSkillRequestHandler();
            RequestContext requestContext = new RequestContext(null, null, requestHandler);

            Assert.AreSame(requestHandler, requestContext.SkillRequestHandler);
            Assert.AreSame(requestContext, requestHandler.RequestContext);

            requestContext.Dispose();

            Assert.IsNull(requestHandler.RequestContext);
        }

        #endregion
    }
}
