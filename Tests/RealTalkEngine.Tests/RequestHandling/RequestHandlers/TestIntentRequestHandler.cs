using Alexa.NET;
using Alexa.NET.Request;
using Alexa.NET.Request.Type;
using Alexa.NET.Response;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RealTalkEngine.RequestHandling;
using RealTalkEngine.RequestHandling.RequestHandlers;
using RealTalkEngine.Tests.Mocks.RequestHandling.IntentHandlers;

namespace RealTalkEngine.Tests.RequestHandling.RequestHandlers
{
    [TestClass]
    public class TestIntentRequestHandler
    {
        #region Is Handler For Request Tests

        [TestMethod]
        public void IsHandlerForRequest_InputtingNull_ReturnsFalse()
        {
            IntentRequestHandler intentRequestHandler = new IntentRequestHandler();

            Assert.IsFalse(intentRequestHandler.IsHandlerForRequest(null));
        }

        [TestMethod]
        public void IsHandlerForRequest_InputtingNonIntentRequest_ReturnsFalse()
        {
            IntentRequestHandler intentRequestHandler = new IntentRequestHandler();
            SkillRequest skillRequest = new SkillRequest();
            skillRequest.Request = new SessionEndedRequest();

            Assert.IsFalse(intentRequestHandler.IsHandlerForRequest(skillRequest));

            skillRequest.Request = new LaunchRequest();

            Assert.IsFalse(intentRequestHandler.IsHandlerForRequest(skillRequest));
        }

        [TestMethod]
        public void IsHandlerForRequest_InputtingIntentRequest_WithNoIntentSet_ReturnsFalse()
        {
            IntentRequestHandler intentRequestHandler = new IntentRequestHandler();
            SkillRequest skillRequest = new SkillRequest();
            IntentRequest intentRequest = new IntentRequest();
            skillRequest.Request = intentRequest;

            Assert.IsNull(intentRequest.Intent);
            Assert.IsFalse(intentRequestHandler.IsHandlerForRequest(skillRequest));
        }

        [TestMethod]
        public void IsHandlerForRequest_InputtingIntentRequest_WithIntentSet_ReturnsTrue()
        {
            IntentRequestHandler intentRequestHandler = new IntentRequestHandler();
            SkillRequest skillRequest = new SkillRequest();
            IntentRequest intentRequest = new IntentRequest();
            intentRequest.Intent = new Intent();
            skillRequest.Request = intentRequest;

            Assert.IsNotNull((skillRequest.Request as IntentRequest).Intent);
            Assert.IsTrue(intentRequestHandler.IsHandlerForRequest(skillRequest));
        }

        #endregion

        #region Handle Request Tests

        [TestMethod]
        public void HandleRequest_InputtingIntentRequestWithMatchingHandlerInFactory_ReturnsHandlerResponse()
        {
            IntentRequestHandler intentRequestHandler = new IntentRequestHandler();
            SkillResponse expectedResponse = ResponseBuilder.Empty();
            MockIntentHandler mockIntentHandler = new MockIntentHandler(expectedResponse);
            IntentRequestHandler.IntentHandlerFactory = new MockIntentHandlerFactory(mockIntentHandler);
            SkillRequest skillRequest = new SkillRequest();
            IntentRequest intentRequest = new IntentRequest();
            intentRequest.Intent = new Intent() { Name = mockIntentHandler.IntentName };
            skillRequest.Request = intentRequest;
            intentRequestHandler.RequestContext = new RequestContext(skillRequest, null, null);

            Assert.IsNotNull((skillRequest.Request as IntentRequest).Intent);
            Assert.IsTrue(IntentRequestHandler.IntentHandlerFactory.CustomIntentHandlers.Exists(x => x.IsHandlerForIntent(intentRequest.Intent)));
            Assert.AreSame(expectedResponse, intentRequestHandler.HandleRequest());
        }

        [TestMethod]
        public void HandleRequest_InputtingIntentRequestWithMatchingHandlerInFactory_KeepsHandlerContextAsNull()
        {
            IntentRequestHandler intentRequestHandler = new IntentRequestHandler();
            SkillResponse expectedResponse = ResponseBuilder.Empty();
            MockIntentHandler mockIntentHandler = new MockIntentHandler(expectedResponse);
            IntentRequestHandler.IntentHandlerFactory = new MockIntentHandlerFactory(mockIntentHandler);
            SkillRequest skillRequest = new SkillRequest();
            IntentRequest intentRequest = new IntentRequest();
            intentRequest.Intent = new Intent() { Name = mockIntentHandler.IntentName };
            skillRequest.Request = intentRequest;
            intentRequestHandler.RequestContext = new RequestContext(skillRequest, null, null);

            Assert.IsNull(mockIntentHandler.RequestContext);
            Assert.IsNotNull((skillRequest.Request as IntentRequest).Intent);
            Assert.IsTrue(IntentRequestHandler.IntentHandlerFactory.CustomIntentHandlers.Exists(x => x.IsHandlerForIntent(intentRequest.Intent)));

            intentRequestHandler.HandleRequest();

            Assert.IsNull(mockIntentHandler.RequestContext);
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
