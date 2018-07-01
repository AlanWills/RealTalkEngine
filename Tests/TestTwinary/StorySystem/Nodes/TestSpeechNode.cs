using CelTestSharp;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using TestTwinary.Mocks;
using Twinary.StorySystem.Nodes;
using Twinary.StorySystem.Transitions;

namespace TestTwinary.StorySystem.Nodes
{
    [TestClass]
    public class TestSpeechNode
    {
        #region Constructor Tests

        [TestMethod]
        public void Constructor_Default_SetsName_ToEmptyString()
        {
            SpeechNode speechNode = new SpeechNode();

            Assert.AreEqual("", speechNode.Name);
        }

        [TestMethod]
        public void Constructor_SetsName_ToInputtedString()
        {
            SpeechNode speechNode = new SpeechNode("Test", 0);

            Assert.AreEqual("Test", speechNode.Name);
        }

        [TestMethod]
        public void Constructor_SetsText_ToEmptyString()
        {
            SpeechNode speechNode = new SpeechNode();

            Assert.AreEqual("", speechNode.Text);
        }

        [TestMethod]
        public void Constructor_SetsTags_ToEmptyList()
        {
            SpeechNode speechNode = new SpeechNode();

            AssertExt.IsEmpty(speechNode.Tags);
        }

        [TestMethod]
        public void Constructor_Default_SetsNodeIndex_ToZero()
        {
            SpeechNode speechNode = new SpeechNode();

            Assert.AreEqual(0, speechNode.NodeIndex);
        }

        [TestMethod]
        public void Constructor_SetsNodeIndex_ToInputtedIndex()
        {
            SpeechNode speechNode = new SpeechNode("Test", 2);

            Assert.AreEqual(2, speechNode.NodeIndex);
        }

        [TestMethod]
        public void Constructor_SetsTransitions_ToEmptyList()
        {
            SpeechNode speechNode = new SpeechNode();

            Assert.AreEqual(0, speechNode.TransitionCount);
        }

        #endregion

        #region Initialize Transitions

        [TestMethod]
        public void InitializeTransitions_InputtingNull_DoesNothing()
        {
            MockSpeechNode mockSpeechNode = new MockSpeechNode();
            mockSpeechNode.TwineLinks_Public.Add(new TwineLink("Test Link", "Text|TestNode"));

            Assert.AreEqual(0, mockSpeechNode.TransitionCount);

            mockSpeechNode.InitializeTransitions(null);

            Assert.AreEqual(0, mockSpeechNode.TransitionCount);
        }

        [TestMethod]
        public void InitializeTransitions_InputtingDictionaryWithNoMatchingNodeNames_DoesNothing()
        {
            MockSpeechNode mockSpeechNode = new MockSpeechNode();
            mockSpeechNode.TwineLinks_Public.Add(new TwineLink("Test Link", "Text|TestNode"));

            Assert.AreEqual(0, mockSpeechNode.TransitionCount);

            mockSpeechNode.InitializeTransitions(new Dictionary<string, SpeechNode>());

            Assert.AreEqual(0, mockSpeechNode.TransitionCount);
        }

        [TestMethod]
        public void InitializeTransitions_InputtingDictionaryWithMatchingNodeNames_AddsCorrectTransitions()
        {
            MockSpeechNode mockSpeechNode = new MockSpeechNode();
            mockSpeechNode.TwineLinks_Public.Add(new TwineLink("Test Link", "Text|TestNode"));

            Assert.AreEqual(0, mockSpeechNode.TransitionCount);

            SpeechNode speechNode = new SpeechNode("TestNode", 1);
            Dictionary<string, SpeechNode> lookup = new Dictionary<string, SpeechNode>()
            {
                { "TestNode", speechNode }
            };

            mockSpeechNode.InitializeTransitions(lookup);

            Assert.AreEqual(1, mockSpeechNode.TransitionCount);
            Assert.AreSame(mockSpeechNode, mockSpeechNode.GetTransitionAt(0).Source);
            Assert.AreSame(speechNode, mockSpeechNode.GetTransitionAt(0).Destination);
        }

        #endregion
    }
}
