using Alexa.NET;
using Alexa.NET.Request;
using Alexa.NET.Response;
using Amazon.Lambda.Core;
using RealTalkEngine.RequestHandling.RequestHandlers;
using System;
using System.Collections.Generic;
using System.Text;

namespace RealTalkEngine.RequestHandling
{
    public class RequestContext : IDisposable
    {
        #region Properties and Fields

        /// <summary>
        /// The current request that the runtime is processing.
        /// </summary>
        public SkillRequest Request { get; private set; }

        /// <summary>
        /// The current session that the runtime is maintaining.
        /// </summary>
        public Session Session { get; private set; }

        /// <summary>
        /// The current request handler which has been chosen to handle the current request.
        /// </summary>
        private SkillRequestHandler SkillRequestHandler { get; set; }

        /// <summary>
        /// The default response we will send back if we could not properly process the current context.
        /// Default value is an empty skill response.
        /// </summary>
        public SkillResponse FallbackResponse { get; set; } = ResponseBuilder.Empty();

        #endregion

        public RequestContext(SkillRequest request, ILambdaContext lambdaContext, SkillRequestHandler requestHandler)
        {
            Request = request;
            Session = request.Session;
            SkillRequestHandler = requestHandler;

            if (SkillRequestHandler != null)
            {
                SkillRequestHandler.RequestContext = this;
            }

            // Set up the logging here
            Logger.Initialize(lambdaContext?.Logger);
            Logger.Log("Request Type: " + request.GetRequestType().Name);
        }

        #region Request Handling

        /// <summary>
        /// Uses the current context and skill request handler to return an appropriate response to the current request.
        /// If the request could not be handled, the FallbackResponse is used.
        /// </summary>
        /// <returns></returns>
        public SkillResponse HandleRequest()
        {
            return SkillRequestHandler != null ? SkillRequestHandler.HandleRequest() : FallbackResponse;
        }

        #endregion

        #region IDisposable Implementation

        /// <summary>
        /// Perform cleanup to reset the current state
        /// </summary>
        public void Dispose()
        {
            if (SkillRequestHandler != null)
            {
                SkillRequestHandler.RequestContext = null;
            }
        }

        #endregion
    }
}
