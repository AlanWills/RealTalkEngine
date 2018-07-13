using System;
using System.Collections.Generic;
using System.Text;
using Alexa.NET;
using Alexa.NET.Request;
using Alexa.NET.Response;
using Alexa.NET.Response.Ssml;
using Amazon.Lambda.Core;

namespace RealTalkEngine.RequestHandling.RequestHandlers
{
    public class StopIntentHandler : IntentHandler
    {
        #region Properties and Fields

        /// <summary>
        /// The name of the intent we are handling.
        /// </summary>
        public override string IntentName
        {
            get { return Intents.StopIntentName; }
        }

        #endregion

        #region Intent Handler Abstract Implementations

        /// <summary>
        /// Completely stop the session.
        /// </summary>
        /// <param name="intent"></param>
        /// <param name="session"></param>
        /// <param name="lambdaContext"></param>
        /// <returns></returns>
        public override SkillResponse HandleIntent(Intent intent)
        {
            SkillResponse response = ResponseBuilder.Tell(new Speech(new Sentence("Thank you for playing.")));
            response.Response.ShouldEndSession = true;

            return response;
        }

        #endregion
    }
}
