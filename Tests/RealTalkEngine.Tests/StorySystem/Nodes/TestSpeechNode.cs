﻿using CelTestSharp;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RealTalkEngine.StorySystem.Nodes;
using RealTalkEngine.StorySystem.Transitions;
using System;
using System.Collections.Generic;
using System.Text;
using RealTalkEngine.Tests.Mocks.StorySystem.Conditions;
using Twinary.StorySystem.Transitions;
using Twinary.StorySystem.Nodes;

namespace RealTalkEngine.Tests.StorySystem.Nodes
{
    [TestClass]
    public class TestSpeechNode
    {
        #region Attribute Tests

        [TestMethod]
        public void SpeechNode_HasSerializableAttribute()
        {
            AssertExt.HasCustomAttribute<SerializableAttribute>(typeof(SpeechNode));
        }

        #endregion

        #region Constructor Tests

        #region Default

        [TestMethod]
        public void Constructor_Default_SetsName_ToEmptyString()
        {
            SpeechNode speechNode = new SpeechNode();

            Assert.AreEqual("", speechNode.Name);
        }

        [TestMethod]
        public void Constructor_Default_SetsNodeIndex_ToZero()
        {
            SpeechNode speechNode = new SpeechNode();

            Assert.AreEqual(0, speechNode.NodeIndex);
        }

        [TestMethod]
        public void Constructor_Default_SetsText_ToEmptyString()
        {
            SpeechNode speechNode = new SpeechNode();

            Assert.AreEqual("", speechNode.Text);
        }

        [TestMethod]
        public void Constructor_Default_SetsTags_ToEmptyList()
        {
            SpeechNode speechNode = new SpeechNode();

            AssertExt.IsEmpty(speechNode.Tags);
        }

        [TestMethod]
        public void Constructor_Default_SetsTransitions_ToEmptyList()
        {
            SpeechNode speechNode = new SpeechNode();

            Assert.AreEqual(0, speechNode.TransitionCount);
        }

        [TestMethod]
        public void Constructor_Default_SetsParentStory_ToNull()
        {
            SpeechNode speechNode = new SpeechNode();

            Assert.IsNull(speechNode.ParentStory);
        }

        #endregion

        #region Twine Speech Node

        [TestMethod]
        public void Constructor_TwineSpeechNode_SetsName_ToNodeName()
        {
            TwineSpeechNode twineSpeechNode = new TwineSpeechNode();
            twineSpeechNode.Name = "Test";
            SpeechNode speechNode = new SpeechNode(twineSpeechNode);

            Assert.AreEqual("Test", speechNode.Name);
        }

        [TestMethod]
        public void Constructor_TwineSpeechNode_SetsNodeIndex_ToCorrectValue()
        {
            TwineSpeechNode twineSpeechNode = new TwineSpeechNode();
            twineSpeechNode.OneBasedIndex = 13;
            SpeechNode speechNode = new SpeechNode(twineSpeechNode);

            Assert.AreEqual(12, speechNode.NodeIndex);
        }

        [TestMethod]
        public void Constructor_TwineSpeechNode_SetsText_ToNodeText()
        {
            TwineSpeechNode twineSpeechNode = new TwineSpeechNode();
            twineSpeechNode.Text = "Text";
            SpeechNode speechNode = new SpeechNode(twineSpeechNode);

            Assert.AreEqual("Text", speechNode.Text);
        }

        [TestMethod]
        public void Constructor_TwineSpeechNode_SetsTags_ToNodeTags()
        {
            TwineSpeechNode twineSpeechNode = new TwineSpeechNode();
            twineSpeechNode.Tags.Add("Tag1");
            twineSpeechNode.Tags.Add("Tag2");
            SpeechNode speechNode = new SpeechNode(twineSpeechNode);

            Assert.AreEqual(2, speechNode.Tags.Count);
            Assert.AreEqual("Tag1", speechNode.Tags[0]);
            Assert.AreEqual("Tag2", speechNode.Tags[1]);
        }

        [TestMethod]
        public void Constructor_TwineSpeechNode_SetsTransitions_ToEmptyList()
        {
            TwineSpeechNode twineSpeechNode = new TwineSpeechNode();
            twineSpeechNode.TwineLinks.Add(new TwineLink());
            SpeechNode speechNode = new SpeechNode(twineSpeechNode);

            Assert.AreEqual(1, twineSpeechNode.TwineLinks.Count);
            Assert.AreEqual(0, speechNode.TransitionCount);
        }

        [TestMethod]
        public void Constructor_TwineSpeechNode_SetsParentStory_ToNull()
        {
            TwineSpeechNode twineSpeechNode = new TwineSpeechNode();
            SpeechNode speechNode = new SpeechNode(twineSpeechNode);

            Assert.IsNull(speechNode.ParentStory);
        }

        #endregion

        #endregion

        #region Create Transition Tests

        [TestMethod]
        public void CreateTransition_InputtingNullDestinationNode_ReturnsNull()
        {
            SpeechNode speechNode = new SpeechNode();

            Assert.AreEqual(0, speechNode.TransitionCount);
            Assert.IsNull(speechNode.CreateTransition(null));
            Assert.AreEqual(0, speechNode.TransitionCount);
        }

        [TestMethod]
        public void CreateTransition_CreatesNewTransition_WithCorrectValues()
        {
            SpeechNode speechNode = new SpeechNode();
            SpeechNode destinationNode = new SpeechNode();

            Assert.AreEqual(0, speechNode.TransitionCount);

            speechNode.CreateTransition(destinationNode);

            Assert.AreEqual(1, speechNode.TransitionCount);
            Assert.AreSame(speechNode, speechNode.GetTransitionAt(0).Source);
            Assert.AreSame(destinationNode, speechNode.GetTransitionAt(0).Destination);
        }

        [TestMethod]
        public void CreateTransition_ReturnsCreatedTransition()
        {
            SpeechNode speechNode = new SpeechNode();
            SpeechNode destinationNode = new SpeechNode();
            Transition transition = speechNode.CreateTransition(destinationNode);

            Assert.AreSame(transition, speechNode.GetTransitionAt(0));
        }
        
        #endregion

        #region Get Transition At Tests

        [TestMethod]
        public void GetTransitionAt_InputtingOutOfBoundsIndex_ReturnsNull()
        {
            SpeechNode speechNode = new SpeechNode();

            Assert.AreEqual(0, speechNode.TransitionCount);
            Assert.IsNull(speechNode.GetTransitionAt(1));
        }

        [TestMethod]
        public void GetTransitionAt_InputtingInBoundsIndex_ReturnsCorrectTransition()
        {
            SpeechNode speechNode = new SpeechNode();
            SpeechNode destinationNode = new SpeechNode();
            Transition transition = speechNode.CreateTransition(destinationNode);

            Assert.AreSame(transition, speechNode.GetTransitionAt(0));
        }

        #endregion

        #region Get Next Node Tests

        [TestMethod]
        public void GetNextNode_NoTransitions_ReturnsNull()
        {
            SpeechNode speechNode = new SpeechNode();

            Assert.AreEqual(0, speechNode.TransitionCount);
            Assert.IsNull(speechNode.GetNextNode());
        }

        [TestMethod]
        public void GetNextNode_NoValidTransitions_ReturnsNull()
        {
            SpeechNode speechNode = new SpeechNode();
            SpeechNode destinationNode = new SpeechNode();
            Transition transition = speechNode.CreateTransition(destinationNode);
            transition.CreateCondition<MockTransitionCondition>();

            Assert.AreEqual(1, speechNode.TransitionCount);
            Assert.IsFalse(transition.ValidateConditions());
            Assert.IsNull(speechNode.GetNextNode());
        }

        [TestMethod]
        public void GetNextNode_OneValidTransition_ReturnsDestinationNode()
        {
            SpeechNode speechNode = new SpeechNode();
            SpeechNode destinationNode = new SpeechNode();
            Transition transition = speechNode.CreateTransition(destinationNode);
            MockTransitionCondition condition = transition.CreateCondition<MockTransitionCondition>();
            condition.ConditionPasses_Result = true;

            Assert.AreEqual(1, speechNode.TransitionCount);
            Assert.IsTrue(transition.ValidateConditions());
            Assert.AreSame(destinationNode, speechNode.GetNextNode());
        }

        [TestMethod]
        public void GetNextNode_MultipleValidTransition_ReturnsFirstValidDestinationNode()
        {
            SpeechNode speechNode = new SpeechNode();

            SpeechNode destinationNode = new SpeechNode();
            Transition transition = speechNode.CreateTransition(destinationNode);
            MockTransitionCondition condition = transition.CreateCondition<MockTransitionCondition>();
            condition.ConditionPasses_Result = true;

            SpeechNode secondDestinationNode = new SpeechNode();
            Transition secondTransition = speechNode.CreateTransition(secondDestinationNode);
            MockTransitionCondition secondCondition = secondTransition.CreateCondition<MockTransitionCondition>();
            secondCondition.ConditionPasses_Result = true;

            Assert.AreEqual(2, speechNode.TransitionCount);
            Assert.IsTrue(transition.ValidateConditions());
            Assert.IsTrue(secondTransition.ValidateConditions());
            Assert.AreSame(destinationNode, speechNode.GetNextNode());
        }

        #endregion

        #region IEnumerable Tests

        [TestMethod]
        public void IEnumerable_WithNoTransitions_DoesNothing()
        {
            SpeechNode speechNode = new SpeechNode();

            Assert.AreEqual(0, speechNode.TransitionCount);

            int counter = 0;
            foreach (Transition transition in speechNode)
            {
                ++counter;
            }

            Assert.AreEqual(0, counter);
        }

        [TestMethod]
        public void IEnumerable_WithTransitions_IteratesOverTransitions()
        {
            SpeechNode speechNode = new SpeechNode();
            speechNode.CreateTransition(new SpeechNode());
            speechNode.CreateTransition(new SpeechNode());

            Assert.AreEqual(2, speechNode.TransitionCount);

            int counter = 0;
            foreach (Transition transition in speechNode)
            {
                ++counter;
            }

            Assert.AreEqual(2, counter);
        }

        #endregion
    }
}
