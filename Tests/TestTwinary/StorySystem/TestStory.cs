using CelTestSharp;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Twinary.StorySystem;

namespace TestTwinary.StorySystem
{
    [TestClass]
    public class TestStory
    {
        #region Load Tests

        [TestMethod]
        public void Story_Load_InputtingNonExistentFilePath_ReturnsNull()
        {
            string path = "WubbaLubbaDubDub";

            Assert.IsFalse(File.Exists(path));
            Assert.IsNull(Story.Load(path));
        }

        [TestMethod]
        public void Story_Load_InputtingInvalidStoryFilePath_ReturnsNull()
        {
            Assert.IsTrue(File.Exists(JsonStoryResources.InvalidStory));
            Assert.IsNull(Story.Load(JsonStoryResources.InvalidStory));
        }

        [TestMethod]
        public void Story_Load_EmptyStory_DeserializesCorrectly()
        {
            /*
            {
              "name": "EmptyStory",
              "startnode": "1",
              "creator": "Twine",
              "creator-version": "2.2.1",
              "ifid": "AB8A0633-93B4-4221-8F93-CD9E268C3211"
            }
            */
            Story story = Story.Load(JsonStoryResources.EmptyStory);

            Assert.IsNotNull(story);
            Assert.AreEqual("EmptyStory", story.Name);
            Assert.AreEqual(1, story.StartNode);
            Assert.AreEqual("Twine", story.Creator);
            Assert.AreEqual("2.2.1", story.CreatorVersion);
            Assert.AreEqual(new Guid("AB8A0633-93B4-4221-8F93-CD9E268C3211"), story.IfID);
            Assert.IsNotNull(story.Nodes);
            AssertExt.IsEmpty(story.Nodes);
        }

        [TestMethod]
        public void Story_Load_StoryWithSingleNode_DeserializesCorrectly()
        {
            /*
            {
              "passages": [
                {
                  "text": "Single Node Text",
                  "name": "Single Node",
                  "pid": "1",
                  "position": {
                    "x": "412",
                    "y": "187.5"
                  },
                  "tags": [
                    "Node"
                  ]
                }
              ],
              "name": "SingleNodeStory",
              "startnode": "1",
              "creator": "Twine",
              "creator-version": "2.2.1",
              "ifid": "E496B74E-C387-4E7F-B66E-9FB8927FE229"
            }
            */

            Story story = Story.Load(JsonStoryResources.SingleNodeStory);

            Assert.IsNotNull(story);
            Assert.AreEqual("SingleNodeStory", story.Name);
            Assert.AreEqual(1, story.StartNode);
            Assert.AreEqual("Twine", story.Creator);
            Assert.AreEqual("2.2.1", story.CreatorVersion);
            Assert.AreEqual(new Guid("E496B74E-C387-4E7F-B66E-9FB8927FE229"), story.IfID);
            Assert.IsNotNull(story.Nodes);
            Assert.AreEqual(1, story.Nodes.Count);

            SpeechNode speechNode = story.Nodes[0];

            Assert.IsNotNull(speechNode);
            Assert.AreEqual("Single Node", speechNode.Name);
            Assert.AreEqual("Single Node Text", speechNode.Text);
            Assert.AreEqual(0, speechNode.NodeIndex);
            Assert.IsNotNull(speechNode.Tags);
            Assert.AreEqual(1, speechNode.Tags.Count);
            Assert.AreEqual("Node", speechNode.Tags[0]);
        }

        #endregion
    }
}
