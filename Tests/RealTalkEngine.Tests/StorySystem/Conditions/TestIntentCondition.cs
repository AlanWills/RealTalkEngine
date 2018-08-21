using Alexa.NET.Request;
using Alexa.NET.Request.Type;
using CelTestSharp;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RealTalkEngine.RequestHandling;
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
        #region Attribute Tests

        [TestMethod]
        public void IntentCondition_HasSerializableAttribute()
        {
            AssertExt.HasCustomAttribute<SerializableAttribute>(typeof(IntentCondition));
        }

        #endregion

        #region Condition Passes Tests

        [TestMethod]
        public void ConditionPasses_WithCurrentRuntimeSetToDifferentIntent_ReturnsFalse()
        {
            SkillRequest request = new SkillRequest();
            request.Request = new IntentRequest() { Intent = new Intent() { Name = "Test" } };
            RequestContext requestContext = new RequestContext(request, null, null);

            StoryRuntime runtime = new StoryRuntime(requestContext);
            Story story = new Story();
            story.Runtime = runtime;
            SpeechNode speechNode = story.CreateNode("TestNode");
            Transition transition = speechNode.CreateTransition(new SpeechNode());
            IntentCondition intentCondition = transition.CreateCondition<IntentCondition>();
            intentCondition.IntentName = "WubbaLubbaDubDub";

            Assert.IsFalse(intentCondition.ConditionPasses());
        }

        [TestMethod]
        public void ConditionPasses_WithCurrentRuntimeSetToSameIntent_ReturnsTrue()
        {
            SkillRequest request = new SkillRequest();
            request.Request = new IntentRequest() { Intent = new Intent() { Name = "Test" } };
            RequestContext requestContext = new RequestContext(request, null, null);

            StoryRuntime runtime = new StoryRuntime(requestContext);
            Story story = new Story();
            story.Runtime = runtime;
            SpeechNode speechNode = story.CreateNode("TestNode");
            Transition transition = speechNode.CreateTransition(new SpeechNode());
            IntentCondition intentCondition = transition.CreateCondition<IntentCondition>();
            intentCondition.IntentName = "Test";

            Assert.IsTrue(intentCondition.ConditionPasses());
        }

        #endregion
    }
}
