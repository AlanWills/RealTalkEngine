using Alexa.NET.Request;
using Alexa.NET.Response;
using Amazon.Lambda.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace RealTalkEngine.RequestHandling
{
    public static class RequestListener
    {
        /// <summary>
        /// Initialize the context ready to handle the inputted skill request.
        /// Will return an appropriate response based on the current state and new input.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public static SkillResponse HandleRequest(SkillRequest input, ILambdaContext context)
        {
            // Set up the logging here
            Logger.Initialize(context.Logger);

            return null;
        }
    }
}
