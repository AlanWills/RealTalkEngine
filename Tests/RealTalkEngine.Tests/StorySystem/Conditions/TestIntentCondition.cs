using Alexa.NET.Request;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RealTalkEngine.StorySystem;
using RealTalkEngine.StorySystem.Conditions;
using RealTalkEngine.StorySystem.Nodes;
using RealTalkEngine.StorySystem.Transitions;
using System;
using System.Collections.Generic;
using System.Text;

namespace RealTalkEngine.Tests.StorySystem.Conditions
{
    [TestClass]
    public class TestIntentCondition
    {
        #region Condition Passes Tests

        [TestMethod]
        public void ConditionPasses_WithCurrentRuntimeSetToDifferentIntent_ReturnsFalse()
        {
            StoryRuntime runtime = new StoryRuntime(new Intent() { Name = "Test" }, null);
            Story story = new Story();
            story.Runtime = runtime;
            SpeechNode speechNode = story.CreateNode();
            Transition transition = speechNode.CreateTransition(new SpeechNode());
            IntentCondition intentCondition = transition.CreateCondition<IntentCondition>();
            intentCondition.IntentName = "WubbaLubbaDubDub";

            Assert.IsFalse(intentCondition.ConditionPasses());
        }

        [TestMethod]
        public void ConditionPasses_WithCurrentRuntimeSetToSameIntent_ReturnsTrue()
        {
            StoryRuntime runtime = new StoryRuntime(new Intent() { Name = "Test" }, null);
            Story story = new Story();
            story.Runtime = runtime;
            SpeechNode speechNode = story.CreateNode();
            Transition transition = speechNode.CreateTransition(new SpeechNode());
            IntentCondition intentCondition = transition.CreateCondition<IntentCondition>();
            intentCondition.IntentName = "Test";

            Assert.IsTrue(intentCondition.ConditionPasses());
        }

        #endregion
    }
}
