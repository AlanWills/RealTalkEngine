using Alexa.NET;
using Alexa.NET.Response;
using RealTalkEngine.RequestHandling.RequestHandlers;
using System;
using System.Collections.Generic;
using System.Text;
using Alexa.NET.Request;

namespace RealTalkEngine.Tests.Mocks.RequestHandling.IntentHandlers
{
    public class MockIntentHandler : IntentHandler
    {
        #region Properties and Fields

        public override string IntentName { get { return "MockIntentHandler"; } }

        public SkillResponse SkillResponse { get; set; } = ResponseBuilder.Empty();

        #endregion

        public MockIntentHandler(SkillResponse response)
        {
            SkillResponse = response;
        }

        public override SkillResponse HandleIntent(Intent intent) { return SkillResponse; }
    }
}
