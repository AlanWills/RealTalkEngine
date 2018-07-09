using Microsoft.VisualStudio.TestTools.UnitTesting;
using RealTalkEngine.StorySystem.Nodes;
using RealTalkEngine.StorySystem.Transitions;
using System;
using System.Collections.Generic;
using System.Text;
using TestRealTalkEngine.Mocks.StorySystem.Conditions;

namespace TestRealTalkEngine.StorySystem.Transitions
{
    [TestClass]
    public class TestTransition
    {
        #region Constructor Tests

        [TestMethod]
        public void Constructor_SetsSourceNode_ToInputtedValue()
        {
            SpeechNode speechNode = new SpeechNode();
            Transition transition = new Transition(speechNode, new SpeechNode());

            Assert.AreSame(speechNode, transition.Source);
        }

        [TestMethod]
        public void Constructor_SetsDestinationNode_ToInputtedValue()
        {
            SpeechNode destinationNode = new SpeechNode();
            Transition transition = new Transition(new SpeechNode(), destinationNode);

            Assert.AreSame(destinationNode, transition.Destination);
        }

        [TestMethod]
        public void Constructor_SetsConditions_ToEmptyList()
        {
            Transition transition = new Transition(new SpeechNode(), new SpeechNode());

            Assert.AreEqual(0, transition.ConditionCount);
        }

        #endregion

        #region Add Condition Tests

        [TestMethod]
        public void AddCondition_InputtingNull_DoesNotAddToTransition_ReturnsNull()
        {
            Transition transition = new Transition(new SpeechNode(), new SpeechNode());

            Assert.AreEqual(0, transition.ConditionCount);
            Assert.IsNull(transition.AddCondition(null));
            Assert.AreEqual(0, transition.ConditionCount);
        }

        [TestMethod]
        public void AddCondition_AddsInputtedCondition_ToTransition()
        {
            Transition transition = new Transition(new SpeechNode(), new SpeechNode());

            Assert.AreEqual(0, transition.ConditionCount);

            MockTransitionCondition condition = new MockTransitionCondition();
            transition.AddCondition(condition);

            Assert.AreEqual(1, transition.ConditionCount);
            Assert.AreSame(condition, transition.GetConditionAt(0));
        }

        [TestMethod]
        public void AddCondition_SetsInputtedConditionTransition_ToTransition()
        {
            Transition transition = new Transition(new SpeechNode(), new SpeechNode());
            MockTransitionCondition condition = new MockTransitionCondition();

            Assert.IsNull(condition.Transition);

            transition.AddCondition(condition);

            Assert.AreSame(transition, condition.Transition);
        }

        [TestMethod]
        public void AddCondition_ReturnsInputtedCondition()
        {
            Transition transition = new Transition(new SpeechNode(), new SpeechNode());
            MockTransitionCondition condition = new MockTransitionCondition();

            Assert.AreSame(condition, transition.AddCondition(condition));
        }

        #endregion

        #region Create And Add Condition Tests

        [TestMethod]
        public void CreateAndAddCondition_AddsNewlyCreatedInstance_ToTransition()
        {
            Transition transition = new Transition(new SpeechNode(), new SpeechNode());

            Assert.AreEqual(0, transition.ConditionCount);

            MockTransitionCondition condition = transition.CreateAndAddCondition<MockTransitionCondition>();

            Assert.IsNotNull(condition);
            Assert.AreEqual(1, transition.ConditionCount);
            Assert.AreSame(condition, transition.GetConditionAt(0));
        }

        [TestMethod]
        public void CreateAndAddCondition_SetsNewlyCreatedInstanceTransition_ToTransition()
        {
            Transition transition = new Transition(new SpeechNode(), new SpeechNode());
            MockTransitionCondition condition = transition.CreateAndAddCondition<MockTransitionCondition>();

            Assert.AreSame(transition, condition.Transition);
        }

        [TestMethod]
        public void CreateAndAddCondition_ReturnsNewlyCreatedInstanceTransition()
        {
            Transition transition = new Transition(new SpeechNode(), new SpeechNode());
            MockTransitionCondition condition = transition.CreateAndAddCondition<MockTransitionCondition>();

            Assert.AreSame(condition, transition.GetConditionAt(0));
        }

        #endregion

        #region Get Condition At Tests

        [TestMethod]
        public void GetConditionAt_InputtingInvalidIndex_ReturnsNull()
        {
            Transition transition = new Transition(new SpeechNode(), new SpeechNode());

            Assert.AreEqual(0, transition.ConditionCount);
            Assert.IsNull(transition.GetConditionAt(1));
        }

        [TestMethod]
        public void GetConditionAt_InputtingValidIndex_ReturnsCorrectCondition()
        {
            Transition transition = new Transition(new SpeechNode(), new SpeechNode());
            MockTransitionCondition condition = transition.CreateAndAddCondition<MockTransitionCondition>();

            Assert.AreEqual(1, transition.ConditionCount);
            Assert.AreSame(condition, transition.GetConditionAt(0));
        }

        #endregion

        #region Validate Conditions

        [TestMethod]
        public void ValidateConditions_NoConditions_ReturnsTrue()
        {
            Transition transition = new Transition(new SpeechNode(), new SpeechNode());

            Assert.AreEqual(0, transition.ConditionCount);
            Assert.IsTrue(transition.ValidateConditions());
        }

        [TestMethod]
        public void ValidateConditions_AllConditionsPass_ReturnsTrue()
        {
            Transition transition = new Transition(new SpeechNode(), new SpeechNode());
            MockTransitionCondition condition = transition.CreateAndAddCondition<MockTransitionCondition>();
            condition.ConditionPasses_Result = true;

            Assert.IsTrue(condition.ConditionPasses());
            Assert.AreEqual(1, transition.ConditionCount);
            Assert.IsTrue(transition.ValidateConditions());
        }

        [TestMethod]
        public void ValidateConditions_OneConditionFails_ReturnsFalse()
        {
            Transition transition = new Transition(new SpeechNode(), new SpeechNode());
            MockTransitionCondition condition = transition.CreateAndAddCondition<MockTransitionCondition>();
            condition.ConditionPasses_Result = false;
            MockTransitionCondition condition2 = transition.CreateAndAddCondition<MockTransitionCondition>();
            condition2.ConditionPasses_Result = true;

            Assert.IsFalse(condition.ConditionPasses());
            Assert.IsTrue(condition2.ConditionPasses());
            Assert.AreEqual(2, transition.ConditionCount);
            Assert.IsFalse(transition.ValidateConditions());
        }

        [TestMethod]
        public void ValidateConditions_MultipleConditionsFail_ReturnsFalse()
        {
            Transition transition = new Transition(new SpeechNode(), new SpeechNode());
            MockTransitionCondition condition = transition.CreateAndAddCondition<MockTransitionCondition>();
            condition.ConditionPasses_Result = false;
            MockTransitionCondition condition2 = transition.CreateAndAddCondition<MockTransitionCondition>();
            condition2.ConditionPasses_Result = true;
            MockTransitionCondition condition3 = transition.CreateAndAddCondition<MockTransitionCondition>();
            condition3.ConditionPasses_Result = false;

            Assert.IsFalse(condition.ConditionPasses());
            Assert.IsTrue(condition2.ConditionPasses());
            Assert.IsFalse(condition3.ConditionPasses());
            Assert.AreEqual(3, transition.ConditionCount);
            Assert.IsFalse(transition.ValidateConditions());
        }

        #endregion
    }
}
