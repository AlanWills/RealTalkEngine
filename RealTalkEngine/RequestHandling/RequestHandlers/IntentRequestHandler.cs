using System.Collections.Generic;
using Alexa.NET.Request;
using Alexa.NET.Response;
using Amazon.Lambda.Core;
using Alexa.NET.Request.Type;
using Alexa.NET;
using RealTalkEngine.StorySystem;

namespace RealTalkEngine.RequestHandling.RequestHandlers
{
    public class IntentRequestHandler : SkillRequestHandler
    {
        #region Properties and Fields

        /// <summary>
        /// A list of custom intent handlers we will check first when processing an intent.
        /// </summary>
        private static List<IntentHandler> CustomIntentHandlers { get; set; } = new List<IntentHandler>()
        {
            new PlayGameIntentHandler(),
            new StartFromNodeIntentHandler(),
            new HelpIntentHandler(),
            new StopIntentHandler(),
            new StartOverIntentHandler()
        };

        /// <summary>
        /// All of the currently supported intents.
        /// </summary>
        private static List<string> SupportedIntents { get; set; } = new List<string>()
        {
            Intents.YesIntentName,
            Intents.NoIntentName,
            Intents.TellMeWhatsHappenedIntent,
            Intents.WithThemNowIntent,
            Intents.HowManyWeeksPregnantIntent,
            Intents.HowOldIsMotherIntent,
            Intents.WhereAreYouIntent,
            Intents.IsBabyVisibleIntent,
            Intents.StayOnTheLineIntent,
            Intents.BabyWillBeSlipperyIntent,
            Intents.SupportBabyIntent,
            Intents.CheckOkIntent,
            Intents.IsBabyCryingOrBreathingIntent,
            Intents.IsAnythingObviouslyWrongIntent,
            Intents.RubBabysBackInstructionIntent,
            Intents.IsBoyOrGirlIntent,
            Intents.CongratulationsIntent,
            Intents.AreParamedicsWithYouIntent,
            Intents.WellDoneIntent,
        };

        #endregion

        #region Skill Request Handler Implementations

        /// <summary>
        /// Return true if the inputted request is an IntentRequest and it's Intent is not null.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public override bool IsHandlerForRequest(SkillRequest request)
        {
            return request.Request is IntentRequest &&
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

            IntentHandler handler = CustomIntentHandlers.Find(x => x.IsHandlerForIntent(intentRequest.Intent));
            if (handler != null)
            {
                // Custom handler
                return handler.HandleIntent(intentRequest.Intent);
            }
            else if (SupportedIntents.Contains(intentRequest.Intent.Name))
            {
                // Otherwise we have a story intent
                return ResponseBuilder.Empty();
                //return Story.CreateResponse(intentRequest.Intent, request.Session, lambdaContext);
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
