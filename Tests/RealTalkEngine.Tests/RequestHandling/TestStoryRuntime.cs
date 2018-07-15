using Alexa.NET.Request;
using Alexa.NET.Request.Type;
using Alexa.NET.Response;
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
            Story story = new Story();
            SpeechNode speechNode = story.CreateNode("Test");
            speechNode.Text = "TestText";
            SkillRequest request = new SkillRequest();
            request.Request = new IntentRequest();
            request.Session = new Session();
            RequestContext requestContext = new RequestContext(request, null, null);
            StoryRuntime storyRuntime = new StoryRuntime(requestContext, story);
            storyRuntime.TrySetCurrentNode(0);

            SkillResponse response = storyRuntime.ProcessRequest();

            Assert.IsNotNull(response);
            Assert.AreEqual("SSML", response.Response.OutputSpeech.Type);
            Assert.AreEqual("<speak><s>TestText</s></speak>", (response.Response.OutputSpeech as SsmlOutputSpeech).Ssml);
            AssertExt.IsEmpty(response.Response.Directives);
            Assert.IsNull(response.Response.Reprompt);
        }

        [TestMethod]
        public void ProcessRequest_CreatesSessionAttributesIfNoneExist()
        {
            Story story = new Story();
            SpeechNode speechNode = story.CreateNode("Test");
            SkillRequest request = new SkillRequest();
            request.Request = new IntentRequest();
            request.Session = new Session();
            request.Session.Attributes = null;

            RequestContext requestContext = new RequestContext(request, null, null);
            StoryRuntime storyRuntime = new StoryRuntime(requestContext, story);
            storyRuntime.TrySetCurrentNode(0);

            Assert.IsNull(request.Session.Attributes);

            SkillResponse response = storyRuntime.ProcessRequest();

            Assert.IsNotNull(response.SessionAttributes);
            AssertExt.IsNotEmpty(response.SessionAttributes);
        }

        [TestMethod]
        public void ProcessRequest_UsesSessionAttributesIfTheyExist()
        {
            Story story = new Story();
            SpeechNode speechNode = story.CreateNode("Test");
            SkillRequest request = new SkillRequest();
            request.Request = new IntentRequest();
            request.Session = new Session();
            request.Session.Attributes = new Dictionary<string, object>();

            RequestContext requestContext = new RequestContext(request, null, null);
            StoryRuntime storyRuntime = new StoryRuntime(requestContext, story);
            storyRuntime.TrySetCurrentNode(0);

            Assert.IsNotNull(request.Session.Attributes);

            SkillResponse response = storyRuntime.ProcessRequest();

            Assert.IsNotNull(response.SessionAttributes);
            Assert.AreSame(request.Session.Attributes, response.SessionAttributes);
        }

        [TestMethod]
        public void ProcessRequest_CurrentNodeKeyDoesntExistInSessionAttributes_AddsNextNodeName_ToSessionAttributes()
        {
            Story story = new Story();
            SpeechNode speechNode = story.CreateNode("Test");
            SpeechNode speechNode2 = story.CreateNode("Test2");
            speechNode.CreateTransition(speechNode2);

            Assert.IsNotNull(speechNode.GetNextNode());

            SkillRequest request = new SkillRequest();
            request.Request = new IntentRequest();
            request.Session = new Session();
            request.Session.Attributes = new Dictionary<string, object>();

            RequestContext requestContext = new RequestContext(request, null, null);
            StoryRuntime storyRuntime = new StoryRuntime(requestContext, story);
            storyRuntime.TrySetCurrentNode(0);

            Assert.IsFalse(request.Session.Attributes.ContainsKey(StoryRuntime.CurrentNodeKey));

            SkillResponse response = storyRuntime.ProcessRequest();

            Assert.IsNotNull(response.SessionAttributes);
            Assert.AreSame(request.Session.Attributes, response.SessionAttributes);
            Assert.IsTrue(response.SessionAttributes.ContainsKey(StoryRuntime.CurrentNodeKey));
            Assert.AreEqual("Test2", response.SessionAttributes[StoryRuntime.CurrentNodeKey]);
        }

        [TestMethod]
        public void ProcessRequest_CurrentNodeKeyDoesExistInSessionAttributes_SetsNextNodeName()
        {
            Story story = new Story();
            SpeechNode speechNode = story.CreateNode("Test");
            SpeechNode speechNode2 = story.CreateNode("Test2");
            speechNode.CreateTransition(speechNode2);

            Assert.IsNotNull(speechNode.GetNextNode());

            SkillRequest request = new SkillRequest();
            request.Request = new IntentRequest();
            request.Session = new Session();
            request.Session.Attributes = new Dictionary<string, object>() { { StoryRuntime.CurrentNodeKey, "Test" } };

            RequestContext requestContext = new RequestContext(request, null, null);
            StoryRuntime storyRuntime = new StoryRuntime(requestContext, story);
            storyRuntime.TrySetCurrentNode(0);

            Assert.IsTrue(request.Session.Attributes.ContainsKey(StoryRuntime.CurrentNodeKey));
            Assert.AreEqual("Test", request.Session.Attributes[StoryRuntime.CurrentNodeKey]);

            SkillResponse response = storyRuntime.ProcessRequest();

            Assert.IsNotNull(response.SessionAttributes);
            Assert.AreSame(request.Session.Attributes, response.SessionAttributes);
            Assert.IsTrue(response.SessionAttributes.ContainsKey(StoryRuntime.CurrentNodeKey));
            Assert.AreEqual("Test2", response.SessionAttributes[StoryRuntime.CurrentNodeKey]);
        }

        #endregion
    }
}
