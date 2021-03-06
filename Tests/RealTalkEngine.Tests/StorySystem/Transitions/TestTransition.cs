﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using RealTalkEngine.StorySystem.Nodes;
using RealTalkEngine.StorySystem.Transitions;
using System;
using System.Collections.Generic;
using System.Text;
using RealTalkEngine.Tests.Mocks.StorySystem.Conditions;
using RealTalkEngine.StorySystem.Conditions;
using CelTestSharp;

namespace RealTalkEngine.Tests.StorySystem.Transitions
{
    [TestClass]
    public class TestTransition
    {
        #region Attribute Tests

        [TestMethod]
        public void Transition_HasSerializableAttribute()
        {
            AssertExt.HasCustomAttribute<SerializableAttribute>(typeof(Transition));
        }

        #endregion

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

        #region Create Condition Tests

        [TestMethod]
        public void CreateCondition_AddsNewlyCreatedInstance_ToTransition()
        {
            Transition transition = new Transition(new SpeechNode(), new SpeechNode());

            Assert.AreEqual(0, transition.ConditionCount);

            MockTransitionCondition condition = transition.CreateCondition<MockTransitionCondition>();

            Assert.IsNotNull(condition);
            Assert.AreEqual(1, transition.ConditionCount);
            Assert.AreSame(condition, transition.GetConditionAt(0));
        }

        [TestMethod]
        public void CreateCondition_SetsNewlyCreatedInstanceTransition_ToTransition()
        {
            Transition transition = new Transition(new SpeechNode(), new SpeechNode());
            MockTransitionCondition condition = transition.CreateCondition<MockTransitionCondition>();

            Assert.AreSame(transition, condition.Transition);
        }

        [TestMethod]
        public void CreateCondition_ReturnsNewlyCreatedInstanceTransition()
        {
            Transition transition = new Transition(new SpeechNode(), new SpeechNode());
            MockTransitionCondition condition = transition.CreateCondition<MockTransitionCondition>();

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
            MockTransitionCondition condition = transition.CreateCondition<MockTransitionCondition>();

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
            MockTransitionCondition condition = transition.CreateCondition<MockTransitionCondition>();
            condition.ConditionPasses_Result = true;

            Assert.IsTrue(condition.ConditionPasses());
            Assert.AreEqual(1, transition.ConditionCount);
            Assert.IsTrue(transition.ValidateConditions());
        }

        [TestMethod]
        public void ValidateConditions_OneConditionFails_ReturnsFalse()
        {
            Transition transition = new Transition(new SpeechNode(), new SpeechNode());
            MockTransitionCondition condition = transition.CreateCondition<MockTransitionCondition>();
            condition.ConditionPasses_Result = false;
            MockTransitionCondition condition2 = transition.CreateCondition<MockTransitionCondition>();
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
            MockTransitionCondition condition = transition.CreateCondition<MockTransitionCondition>();
            condition.ConditionPasses_Result = false;
            MockTransitionCondition condition2 = transition.CreateCondition<MockTransitionCondition>();
            condition2.ConditionPasses_Result = true;
            MockTransitionCondition condition3 = transition.CreateCondition<MockTransitionCondition>();
            condition3.ConditionPasses_Result = false;

            Assert.IsFalse(condition.ConditionPasses());
            Assert.IsTrue(condition2.ConditionPasses());
            Assert.IsFalse(condition3.ConditionPasses());
            Assert.AreEqual(3, transition.ConditionCount);
            Assert.IsFalse(transition.ValidateConditions());
        }

        #endregion

        #region IEnumerable Tests

        [TestMethod]
        public void IEnumerable_NoTransitionConditions_DoesNothing()
        {
            Transition transition = new Transition(new SpeechNode(), new SpeechNode());

            Assert.AreEqual(0, transition.ConditionCount);

            int counter = 0;
            foreach (TransitionCondition condition in transition)
            {
                ++counter;
            }

            Assert.AreEqual(0, counter);
        }

        [TestMethod]
        public void IEnumerable_WithTransitionConditions_IteratesOverNodes()
        {
            Transition transition = new Transition(new SpeechNode(), new SpeechNode());
            transition.CreateCondition<MockTransitionCondition>();
            transition.CreateCondition<MockTransitionCondition>();

            Assert.AreEqual(2, transition.ConditionCount);

            int counter = 0;
            foreach (TransitionCondition condition in transition)
            {
                ++counter;
            }

            Assert.AreEqual(2, counter);
        }

        #endregion
    }
}
