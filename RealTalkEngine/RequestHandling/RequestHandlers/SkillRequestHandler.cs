using Alexa.NET.Request;
using Alexa.NET.Request.Type;
using Alexa.NET.Response;
using Amazon.Lambda.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace RealTalkEngine.RequestHandling.RequestHandlers
{
    public abstract class SkillRequestHandler
    {
        #region Properties and Fields

        /// <summary>
        /// The context of the current request that this request handler is running in.
        /// </summary>
        public RequestContext RequestContext { get; set; }

        #endregion

        #region Request Handler Abstract Functions

        /// <summary>
        /// A CanRun function for request handlers.
        /// Returns true if this request handler can successfully process the inputted request.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public abstract bool IsHandlerForRequest(SkillRequest request);

        /// <summary>
        /// Handles the current request context and returns the appropriate response.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public abstract SkillResponse HandleRequest();

        #endregion
    }
}
