using System;
using System.Collections.Generic;
using System.Text;
using Alexa.NET.Request;
using Alexa.NET.Response;
using Alexa.NET.Request.Type;
using Alexa.NET.Response.Ssml;
using Alexa.NET;
using Amazon.Lambda.Core;
using RealTalkEngine.StorySystem;

namespace RealTalkEngine.RequestHandling.RequestHandlers
{
    public class LaunchRequestHandler : SkillRequestHandler
    {
        #region Skill Request Handler Implementations

        /// <summary>
        /// Returns true if the inputted request corresponds to a LaunchRequest.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public override bool IsHandlerForRequest(SkillRequest request)
        {
            return request?.Request is LaunchRequest;
        }

        /// <summary>
        /// Provides a simple introduction when launched about how to play the game.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public override SkillResponse HandleRequest()
        {
            return ResponseBuilder.Empty();
        }

        #endregion
    }
}