using System.Collections.Generic;
using Alexa.NET.Request;
using Alexa.NET.Response;
using Amazon.Lambda.Core;
using Alexa.NET.Request.Type;
using Alexa.NET;
using RealTalkEngine.StorySystem;
using System.IO;
using RealTalkEngine.RequestHandling.IntentHandlers;

namespace RealTalkEngine.RequestHandling.RequestHandlers
{
    public class IntentRequestHandler : SkillRequestHandler
    {
        #region Properties and Fields

        /// <summary>
        /// A list of custom intent handlers we will check first when processing an intent.
        /// </summary>
        public static IIntentHandlerFactory IntentHandlerFactory { get; set; }

        /// <summary>
        /// All of the currently supported intents.
        /// </summary>
        private static IIntentNameFactory IntentNameFactory { get; set; }

        #endregion

        #region Skill Request Handler Implementations

        /// <summary>
        /// Return true if the inputted request is an IntentRequest and it's Intent is not null.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public override bool IsHandlerForRequest(SkillRequest request)
        {
            return request?.Request is IntentRequest &&
                   (request.Request as IntentRequest).Intent != null;
        }

        /// <summary>
        /// Find an appropriate intent handler and use it to process the incoming intent.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="lambdaContext"></param>
        /// <returns></returns>
        public override SkillResponse HandleRequest()
        {
            IntentRequest intentRequest = RequestContext.Request.Request as IntentRequest;
            Logger.Log("Request Intent: " + intentRequest.Intent.Name);

            IntentHandler handler = IntentHandlerFactory.CustomIntentHandlers.Find(x => x.IsHandlerForIntent(intentRequest.Intent));
            if (handler != null)
            {
                // Set the context for the process of handling the intent and then reset it afterwards to avoid dangling references.
                handler.RequestContext = RequestContext;
                SkillResponse response = handler.HandleIntent(intentRequest.Intent);
                handler.RequestContext = null;

                return response;
            }
            else if (IntentNameFactory.IntentNames.Contains(intentRequest.Intent.Name))
            {
                // Otherwise we have a story intent
                StoryRuntime runtime = new StoryRuntime(RequestContext, Story.Load(Path.Combine(Directory.GetCurrentDirectory(), "Story.data")));
                return runtime.ProcessRequest();
            }
            else
            {
                // Otherwise we have no way of dealing with this intent
                Logger.Log("No intent handler found");
                return ResponseBuilder.Empty();
            }
        }

        #endregion
    }
}
