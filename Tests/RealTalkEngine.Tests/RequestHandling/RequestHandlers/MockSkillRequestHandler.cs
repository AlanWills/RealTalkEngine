using Alexa.NET;
using Alexa.NET.Request;
using Alexa.NET.Response;
using RealTalkEngine.RequestHandling.RequestHandlers;
using System;
using System.Collections.Generic;
using System.Text;

namespace RealTalkEngine.Tests.RequestHandling.RequestHandlers
{
    public class MockSkillRequestHandler : SkillRequestHandler
    {
        #region Properties and Fields

        public SkillResponse HandleRequest_Result { get; set; } = ResponseBuilder.Empty();

        public bool IsHandlerForRequest_Result { get; set; } = true;

        #endregion

        #region Skill Request Handler Implementation

        public override SkillResponse HandleRequest()
        {
            return HandleRequest_Result;
        }

        public override bool IsHandlerForRequest(SkillRequest request)
        {
            return IsHandlerForRequest_Result;
        }

        #endregion
    }
}