using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestRealTalkEngine.StorySystem.Nodes
{
    [TestClass]
    public class TestSpeechNode
    {
        #region Constructor Tests

        #region Default

        [TestMethod]
        public void Constructor_Default_SetsName_ToEmptyString()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void Constructor_Default_SetsText_ToEmptyString()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void Constructor_Default_SetsTags_ToEmptyList()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void Constructor_Default_SetsTransitions_ToEmptyList()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void Constructor_Default_SetsParentStory_ToNull()
        {
            Assert.Fail();
        }

        #endregion

        #region Twine Speech Node

        [TestMethod]
        public void Constructor_TwineSpeechNode_SetsName_ToNodeName()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void Constructor_TwineSpeechNode_SetsText_ToNodeText()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void Constructor_TwineSpeechNode_SetsTags_ToNodeTags()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void Constructor_TwineSpeechNode_SetsTransitions_ToEmptyList()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void Constructor_TwineSpeechNode_SetsParentStory_ToNull()
        {
            Assert.Fail();
        }

        #endregion

        #endregion

        #region Create And Add Transition Tests

        [TestMethod]
        public void CreateAndAddTransition_InputtingNullDestinationNode_DoesNothing()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void CreateAndAddTransition_CreatesNewTransition_WithCorrectValues()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void CreateAndAddTransition_ReturnsCreatedTransition()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void CreateAndAddTransition_AddsCreatedTransitionToNode()
        {
            Assert.Fail();
        }

        #endregion

        #region Get Transition At Tests

        [TestMethod]
        public void GetTransitionAt_InputtingOutOfBoundsIndex_ReturnsNull()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void GetTransitionAt_InputtingInBoundsIndex_ReturnsCorrectTransition()
        {
            Assert.Fail();
        }

        #endregion

        #region Get Next Node Tests

        [TestMethod]
        public void GetNextNode_NoTransitions_ReturnsNull()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void GetNextNode_NoValidTransitions_ReturnsNull()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void GetNextNode_OneValidTransition_ReturnsDestinationNode()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void GetNextNode_MultipleValidTransition_ReturnsFirstValidDestinationNode()
        {
            Assert.Fail();
        }

        #endregion
    }
}
