using System;
using System.Collections.Generic;
using System.Text;
using Alexa.NET.Request.Type;
using RealTalkEngine.RequestHandling;
using RealTalkEngine.StorySystem.Transitions;

namespace RealTalkEngine.StorySystem.Conditions
{
    [Serializable]
    public class IntentCondition : TransitionCondition
    {
        #region Serialized Properties

        /// <summary>
        /// The intent name that needs to be matched for this condition to pass.
        /// </summary>
        public string IntentName { get; set; }

        #endregion
        
        #region Condition Overrides

        /// <summary>
        /// Returns true if the current intent has the same name as the intent set on this condition.
        /// </summary>
        /// <returns></returns>
        public override bool ConditionPasses()
        {
            RequestContext context = Transition.Source.ParentStory.Runtime.RequestContext;
            IntentRequest request = context.Request.Request as IntentRequest;

            return request.Intent.Name == IntentName;
        }

        #endregion
    }
}
