﻿using Alexa.NET;
using Alexa.NET.Request;
using Alexa.NET.Response;
using Amazon.Lambda.Core;
using RealTalkEngine.StorySystem;
using System.IO;

namespace RealTalkEngine.RequestHandling.RequestHandlers
{
    public class StartFromNodeIntentHandler : IntentHandler
    {
        #region Properties and Fields
        
        /// <summary>
        /// The name of the slot for the node index.
        /// </summary>
        public const string NodeIndexSlotName = "NodeIndex";

        /// <summary>
        /// The AWS intent name that this handler will process.
        /// </summary>
        public override string IntentName { get { return Intents.StartFromNodeIntentName; } }

        #endregion

        #region Intent Handler Abstract Implementations

        public override SkillResponse HandleIntent(Intent intent)
        {
            long nodeIndex = long.Parse(intent.Slots["NodeIndex"].Value);
            StoryRuntime storyRuntime = new StoryRuntime(RequestContext, Story.Load(Path.Combine(Directory.GetCurrentDirectory(), "Story.data")));
            storyRuntime.TrySetCurrentNode((int)nodeIndex);
            
            return storyRuntime.ProcessRequest();
        }

        #endregion
    }
}
