﻿using Alexa.NET;
using Alexa.NET.Request;
using Alexa.NET.Response;
using Amazon.Lambda.Core;
using RealTalkEngine.RequestHandling.RequestHandlers;
using System;
using System.Collections.Generic;
using System.Text;

namespace RealTalkEngine.RequestHandling
{
    public static class RequestListener
    {
        #region Properties and Fields

        /// <summary>
        /// All of the available skill request handlers that can handle incoming skill requests.
        /// </summary>
        private static List<SkillRequestHandler> SkillRequestHandlers { get; set; } = new List<SkillRequestHandler>()
        {
            // Intent request is most likely
            new IntentRequestHandler(),
            new LaunchRequestHandler(),
            new SessionEndedRequestHandler()
        };

        #endregion

        /// <summary>
        /// Initialize the context ready to handle the inputted skill request.
        /// Will return an appropriate response based on the current state and new input.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public static SkillResponse HandleRequest(SkillRequest request, ILambdaContext lambdaContext)
        {
            using (RequestContext context = new RequestContext(request, lambdaContext, SkillRequestHandlers.Find(x => x.IsHandlerForRequest(request))))
            {
                return context.HandleRequest();
            }
        }
    }
}
