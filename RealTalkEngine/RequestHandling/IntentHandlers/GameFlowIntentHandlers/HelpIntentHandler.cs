using Alexa.NET;
using Alexa.NET.Request;
using Alexa.NET.Response;
using Amazon.Lambda.Core;
using RealTalkEngine.StorySystem;
using System.IO;

namespace RealTalkEngine.RequestHandling.RequestHandlers
{
    public class HelpIntentHandler : IntentHandler
    {
        #region Properties and Fields

        /// <summary>
        /// The name of the intent this handler will process.
        /// </summary>
        public override string IntentName
        {
            get { return Intents.HelpIntentName; }
        }

        #endregion

        #region Intent Handler Abstract Implementations

        /// <summary>
        /// The behaviour when we want help from within the game.
        /// We imagine that we don't know what to do and so get the dispatcher to progress the story.
        /// </summary>
        /// <param name="intent"></param>
        /// <param name="session"></param>
        /// <param name="lambdaContext"></param>
        /// <returns></returns>
        public override SkillResponse HandleIntent(Intent intent)
        {
            StoryRuntime runtime = new StoryRuntime(RequestContext, Story.Load(Path.Combine(Directory.GetCurrentDirectory(), "Story.data")));
            return runtime.ProcessRequest();
        }

        #endregion
    }
}