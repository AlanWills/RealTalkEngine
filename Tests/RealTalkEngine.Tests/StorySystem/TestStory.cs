using Microsoft.VisualStudio.TestTools.UnitTesting;
using RealTalkEngine.StorySystem;
using RealTalkEngine.StorySystem.Nodes;
using System;
using System.Collections.Generic;
using System.Text;
using Twinary.StorySystem.Nodes;

namespace RealTalkEngine.Tests.StorySystem
{
    [TestClass]
    public class TestStory
    {
        #region Constructor Tests

        [TestMethod]
        public void Constructor_SetsName_ToEmptyString()
        {
            Story story = new Story();

            Assert.AreEqual("", story.Name);
        }

        [TestMethod]
        public void Constructor_SetsNodeCount_ToZero()
        {
            Story story = new Story();

            Assert.AreEqual(0, story.NodeCount);
        }

        [TestMethod]
        public void Constructor_SetsStartNode_ToNull()
        {
            Story story = new Story();

            Assert.IsNull(story.StartNode);
        }

        [TestMethod]
        public void Constructor_SetsRuntime_ToNull()
        {
            Story story = new Story();

            Assert.IsNull(story.Runtime);
        }

        #endregion

        #region Add Node Tests

        [TestMethod]
        public void AddNode_InputtingNull_DoesNotAddNode_ReturnsNull()
        {
            Story story = new Story();

            Assert.AreEqual(0, story.NodeCount);
            Assert.IsNull(story.AddNode(null));
            Assert.AreEqual(0, story.NodeCount);
        }

        [TestMethod]
        public void AddNode_AddsNode_ToStory()
        {
            Story story = new Story();

            Assert.AreEqual(0, story.NodeCount);

            story.AddNode(new SpeechNode());

            Assert.AreEqual(1, story.NodeCount);
        }

        [TestMethod]
        public void AddNode_SetsNodeParentStory_ToStory()
        {
            Story story = new Story();
            SpeechNode speechNode = new SpeechNode();

            Assert.IsNull(speechNode.ParentStory);

            story.AddNode(speechNode);

            Assert.AreSame(story, speechNode.ParentStory);
        }

        [TestMethod]
        public void AddNode_ReturnsInputtedNode()
        {
            Story story = new Story();
            SpeechNode speechNode = new SpeechNode();
            
            Assert.AreSame(speechNode, story.AddNode(speechNode));
        }

        #endregion

        #region Create Node Tests

        #region Default

        [TestMethod]
        public void CreateNode_Default_AddsNode_ToStory()
        {
            Story story = new Story();

            Assert.AreEqual(0, story.NodeCount);

            story.CreateNode();

            Assert.AreEqual(1, story.NodeCount);
        }

        [TestMethod]
        public void CreateNode_Default_SetsNodeParentStory_ToStory()
        {
            Story story = new Story();
            SpeechNode speechNode = story.CreateNode();

            Assert.AreSame(story, speechNode.ParentStory);
        }

        [TestMethod]
        public void CreateNode_ReturnsCreatedNode()
        {
            Story story = new Story();
            SpeechNode speechNode = story.CreateNode();

            Assert.AreEqual(1, story.NodeCount);
            Assert.AreSame(speechNode, story.GetNodeAt(0));
        }

        #endregion

        #region Twine Speech Node Overload

        [TestMethod]
        public void CreateNode_TwineSpeechNode_InputtingNull_DoesNotCreateNode_ReturnsNull()
        {
            Story story = new Story();

            Assert.AreEqual(0, story.NodeCount);
            Assert.IsNull(story.CreateNode(null));
            Assert.AreEqual(0, story.NodeCount);
        }

        [TestMethod]
        public void CreateNode_TwineSpeechNode_CreatesNodeWithCorrectValues()
        {
            TwineSpeechNode twineSpeechNode = new TwineSpeechNode();
            twineSpeechNode.Name = "Test";
            twineSpeechNode.Text = "Text Here";
            twineSpeechNode.Tags.Add("Tag1");
            Story story = new Story();

            Assert.AreEqual(0, story.NodeCount);

            SpeechNode speechNode = story.CreateNode(twineSpeechNode);

            Assert.IsNotNull(speechNode);
            Assert.AreEqual("Test", speechNode.Name);
            Assert.AreEqual("Text Here", speechNode.Text);
            Assert.AreEqual(1, speechNode.Tags.Count);
            Assert.AreEqual("Tag1", speechNode.Tags[0]);
        }

        [TestMethod]
        public void CreateNode_TwineSpeechNode_AddsNodeToStory()
        {
            TwineSpeechNode twineSpeechNode = new TwineSpeechNode();
            Story story = new Story();

            Assert.AreEqual(0, story.NodeCount);

            story.CreateNode(twineSpeechNode);

            Assert.AreEqual(1, story.NodeCount);
        }

        [TestMethod]
        public void CreateNode_TwineSpeechNode_ReturnsCreatedNode()
        {
            TwineSpeechNode twineSpeechNode = new TwineSpeechNode();
            Story story = new Story();
            SpeechNode speechNode = story.CreateNode(twineSpeechNode);

            Assert.AreSame(speechNode, story.GetNodeAt(0));
        }

        #endregion

        #endregion

        #region Get Node At Tests

        [TestMethod]
        public void GetNodeAt_InputtingInvalidIndex_ReturnsNull()
        {
            Story story = new Story();

            Assert.AreEqual(0, story.NodeCount);
            Assert.IsNull(story.GetNodeAt(1));
        }

        [TestMethod]
        public void GetNodeAt_InputtingValidIndex_ReturnsCorrectNode()
        {
            Story story = new Story();
            SpeechNode speechNode = story.CreateNode();

            Assert.AreEqual(1, story.NodeCount);
            Assert.AreSame(speechNode, story.GetNodeAt(0));
        }

        #endregion
    }
}
