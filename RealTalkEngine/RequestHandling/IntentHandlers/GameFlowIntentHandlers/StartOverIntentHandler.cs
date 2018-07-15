using Alexa.NET;
using Alexa.NET.Request;
using Alexa.NET.Response;
using Amazon.Lambda.Core;
using RealTalkEngine.StorySystem;
using System.IO;

namespace RealTalkEngine.RequestHandling.RequestHandlers
{
    public class StartOverIntentHandler : IntentHandler
    {
        #region Properties and Fields

        /// <summary>
        /// The name of the intent we are handling.
        /// </summary>
        public override string IntentName
        {
            get { return Intents.StartOverIntentName; }
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
            StoryRuntime storyRuntime = new StoryRuntime(RequestContext, Story.Load(Path.Combine(Directory.GetCurrentDirectory(), "Story.data")));
            return storyRuntime.ProcessRequest();
        }

        #endregion
    }
}
