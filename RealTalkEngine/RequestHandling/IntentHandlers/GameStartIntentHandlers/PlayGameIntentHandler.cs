using Alexa.NET;
using Alexa.NET.Request;
using Alexa.NET.Response;
using Amazon.Lambda.Core;
using RealTalkEngine.StorySystem;
using System.IO;

namespace RealTalkEngine.RequestHandling.RequestHandlers
{
    public class PlayGameIntentHandler : IntentHandler
    {
        #region Properties and Fields

        /// <summary>
        /// The name of the intent this handler will process.
        /// </summary>
        public override string IntentName { get { return Intents.PlayGameIntentName; } }

        #endregion

        #region Intent Handler Abstract Implementations
        
        /// <summary>
        /// The behaviour when we want to start the game.
        /// Begins the story from the beginning.
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