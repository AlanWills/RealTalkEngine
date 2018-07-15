using CelTestSharp;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RealTalkEngine.RequestHandling;
using RealTalkEngine.StorySystem;
using RealTalkEngine.StorySystem.Nodes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace RealTalkEngine.Tests.RequestHandling
{
    [TestClass]
    public class TestStoryRuntime
    {
        #region Constructor Tests

        [TestMethod]
        public void Constructor_SetsRequestContext_ToInputtedValue()
        {
            RequestContext requestContext = new RequestContext(null, null, null);
            StoryRuntime storyRuntime = new StoryRuntime(requestContext);

            Assert.AreSame(requestContext, storyRuntime.RequestContext);
        }

        [TestMethod]
        public void Constructor_InputtingStory_SetsStoryToInputtedValue()
        {
            RequestContext requestContext = new RequestContext(null, null, null);
            Story story = new Story();
            StoryRuntime storyRuntime = new StoryRuntime(requestContext, story);

            Assert.AreSame(story, storyRuntime.Story);
        }

        [TestMethod]
        public void Constructor_InputtingFilePath_LoadsStoryFromInputtedFilePath()
        {
            string path = Path.Combine(Resources.TempDir, "Test.data");
            Story story = new Story();
            story.Save(path);

            FileAssert.FileExists(path);

            RequestContext requestContext = new RequestContext(null, null, null);
            StoryRuntime storyRuntime = new StoryRuntime(requestContext, path);

            Assert.IsNotNull(storyRuntime.Story);
        }

        #endregion

        #region Try Set Current Node Tests

        [TestMethod]
        public void TrySetCurrentNode_InputtingNonExistentNodeIndex_DoesNotChangeCurrentNode()
        {
            RequestContext requestContext = new RequestContext(null, null, null);
            Story story = new Story();
            SpeechNode speechNode = story.CreateNode("Test");
            speechNode.NodeIndex = 1;

            StoryRuntime storyRuntime = new StoryRuntime(requestContext, story);
            storyRuntime.TrySetCurrentNode(1);

            Assert.AreSame(speechNode, storyRuntime.CurrentNode);
            Assert.IsNull(story.FindNode(100));

            storyRuntime.TrySetCurrentNode(100);

            Assert.AreSame(speechNode, storyRuntime.CurrentNode);
        }

        [TestMethod]
        public void TrySetCurrentNode_InputtingExistentNodeIndex_SetsCurrentNodeCorrectly()
        {
            RequestContext requestContext = new RequestContext(null, null, null);
            Story story = new Story();
            SpeechNode speechNode = story.CreateNode("Test");
            speechNode.NodeIndex = 1;
            SpeechNode speechNode2 = story.CreateNode("Test2");
            speechNode2.NodeIndex = 100;

            StoryRuntime storyRuntime = new StoryRuntime(requestContext, story);
            storyRuntime.TrySetCurrentNode(1);

            Assert.AreSame(speechNode, storyRuntime.CurrentNode);
            Assert.AreSame(speechNode2, story.FindNode(100));

            storyRuntime.TrySetCurrentNode(100);

            Assert.AreSame(speechNode2, storyRuntime.CurrentNode);
        }

        [TestMethod]
        public void TrySetCurrentNode_InputtingNonExistentNodeName_DoesNotChangeCurrentNode()
        {
            RequestContext requestContext = new RequestContext(null, null, null);
            Story story = new Story();
            SpeechNode speechNode = story.CreateNode("Test");
            speechNode.NodeIndex = 1;

            StoryRuntime storyRuntime = new StoryRuntime(requestContext, story);
            storyRuntime.TrySetCurrentNode(1);

            Assert.AreSame(speechNode, storyRuntime.CurrentNode);
            Assert.IsNull(story.FindNode("WubbaLubbaDubDub"));

            storyRuntime.TrySetCurrentNode("WubbaLubbaDubDub");

            Assert.AreSame(speechNode, storyRuntime.CurrentNode);
        }

        [TestMethod]
        public void TrySetCurrentNode_InputtingExistentNodeName_SetsCurrentNodeCorrectly()
        {
            RequestContext requestContext = new RequestContext(null, null, null);
            Story story = new Story();
            SpeechNode speechNode = story.CreateNode("Test");
            speechNode.NodeIndex = 1;
            SpeechNode speechNode2 = story.CreateNode("Test2");

            StoryRuntime storyRuntime = new StoryRuntime(requestContext, story);
            storyRuntime.TrySetCurrentNode(1);

            Assert.AreSame(speechNode, storyRuntime.CurrentNode);
            Assert.AreSame(speechNode2, story.FindNode("Test2"));

            storyRuntime.TrySetCurrentNode("Test2");

            Assert.AreSame(speechNode2, storyRuntime.CurrentNode);
        }

        #endregion

        #region Process Request Tests

        [TestMethod]
        public void ProcessRequest_CreatesTellResponse_UsingCurrentNodeText()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void ProcessRequest_CreatesSessionAttributesIfNoneExist()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void ProcessRequest_UsesSessionAttributesIfTheyExist()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void ProcessRequest_CurrentNodeKeyDoesntExistInSessionAttributes_AddsNextNodeName_ToSessionAttributes()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void ProcessRequest_CurrentNodeKeyDoesExistInSessionAttributes_SetsNextNodeName()
        {
            Assert.Fail();
        }

        #endregion
    }
}
