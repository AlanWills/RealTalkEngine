﻿using CelTestSharp;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RealTalkEngine.StorySystem;
using RealTalkEngine.StorySystem.Conditions;
using RealTalkEngine.StorySystem.Nodes;
using RealTalkEngine.StorySystem.Transitions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using Twinary.StorySystem;
using Twinary.StorySystem.Nodes;
using Twinary.StorySystem.Transitions;

namespace RealTalkEngine.Tests.StorySystem
{
    [TestClass]
    public class TestStory : UnitTest
    {
        #region Attribute Tests

        [TestMethod]
        public void Story_HasSerializableAttribute()
        {
            AssertExt.HasCustomAttribute<SerializableAttribute>(typeof(Story));
        }

        #endregion

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

        #region String

        [TestMethod]
        public void CreateNode_String_AddsNode_ToStory()
        {
            Story story = new Story();

            Assert.AreEqual(0, story.NodeCount);

            story.CreateNode("Test");

            Assert.AreEqual(1, story.NodeCount);
        }

        [TestMethod]
        public void CreateNode_String_SetsNodeName_ToInputtedValue()
        {
            Story story = new Story();

            Assert.AreEqual(0, story.NodeCount);

            SpeechNode node = story.CreateNode("Test");

            Assert.AreEqual("Test", node.Name);
        }

        [TestMethod]
        public void CreateNode_String_SetsNodeParentStory_ToStory()
        {
            Story story = new Story();
            SpeechNode speechNode = story.CreateNode("Test");

            Assert.AreSame(story, speechNode.ParentStory);
        }

        [TestMethod]
        public void CreateNode_String_ReturnsCreatedNode()
        {
            Story story = new Story();
            SpeechNode speechNode = story.CreateNode("Test");

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
            Assert.IsNull(story.CreateNode((TwineSpeechNode)null));
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
            SpeechNode speechNode = story.CreateNode("Test");

            Assert.AreEqual(1, story.NodeCount);
            Assert.AreSame(speechNode, story.GetNodeAt(0));
        }

        #endregion

        #region Find Node Tests

        #region String Overload

        [TestMethod]
        public void FindNode_InputtingNonExistentName_ReturnsNull()
        {
            Story story = new Story();

            Assert.AreEqual(0, story.NodeCount);
            Assert.IsNull(story.FindNode("WubbaLubba"));

            story.CreateNode("TestNode");

            Assert.AreEqual(1, story.NodeCount);
            Assert.IsNull(story.FindNode("WubbaLubba"));
        }

        [TestMethod]
        public void FindNode_InputtingExistentName_ReturnsCorrectNode()
        {
            Story story = new Story();
            SpeechNode speechNode = story.CreateNode("TestNode");

            Assert.AreEqual(1, story.NodeCount);
            Assert.AreSame(speechNode, story.FindNode("TestNode"));
        }

        #endregion

        #region Int Overload

        [TestMethod]
        public void FindNode_InputtingNonExistentNodeIndex_ReturnsNull()
        {
            Story story = new Story();

            Assert.AreEqual(0, story.NodeCount);
            Assert.IsNull(story.FindNode(1));

            SpeechNode speechNode = story.CreateNode("TestNode");
            speechNode.NodeIndex = 0;

            Assert.AreEqual(0, speechNode.NodeIndex);
            Assert.AreEqual(1, story.NodeCount);
            Assert.IsNull(story.FindNode(1));
        }

        [TestMethod]
        public void FindNode_InputtingExistentNodeIndex_ReturnsCorrectNode()
        {
            Story story = new Story();
            SpeechNode speechNode = story.CreateNode("TestNode");
            speechNode.NodeIndex = 1;

            Assert.AreEqual(1, speechNode.NodeIndex);
            Assert.AreEqual(1, story.NodeCount);
            Assert.AreSame(speechNode, story.FindNode(1));
        }

        #endregion

        #endregion

        #region Load Tests

        #region File Overload

        [TestMethod]
        public void InputtingNonExistentFile_ReturnsNull()
        {
            string filePath = "WubbaLubba";

            FileAssert.FileDoesNotExist(filePath);
            Assert.IsNull(Story.Load(filePath));
        }

        [TestMethod]
        public void InputtingExistentInvalidFile_ReturnsNull()
        {
            string filePath = Path.Combine(Resources.TempDir, "Test.txt");
            File.WriteAllText(filePath, "WubbaLubba");

            FileAssert.FileExists(filePath);
            Assert.IsNull(Story.Load(filePath));
        }

        [TestMethod]
        public void InputtingExistentValidFile_ReturnsStory()
        {
            Story story = new Story();
            story.Name = "Test";

            string filePath = Path.Combine(Resources.TempDir, "Test.txt");
            SaveStoryBinary(story, filePath);

            FileAssert.FileExists(filePath);
            Story loadedStory = Story.Load(filePath);

            Assert.IsNotNull(story);
            Assert.AreEqual("Test", loadedStory.Name);
            Assert.AreEqual(0, loadedStory.NodeCount);
        }

        [TestMethod]
        public void InputtingExistentValidFile_SetsUpNodesCorrectly()
        {
            Story story = new Story();
            SpeechNode speechNode = story.CreateNode("TestNode");
            speechNode.Text = "Test Text";
            speechNode.Tags.Add("Tag1");

            string filePath = Path.Combine(Resources.TempDir, "Test.txt");
            SaveStoryBinary(story, filePath);

            FileAssert.FileExists(filePath);
            Story loadedStory = Story.Load(filePath);

            Assert.IsNotNull(loadedStory);
            Assert.AreEqual(1, loadedStory.NodeCount);

            SpeechNode loadedNode = loadedStory.GetNodeAt(0);

            Assert.IsNotNull(loadedNode);
            Assert.AreEqual("TestNode", loadedNode.Name);
            Assert.AreEqual("Test Text", loadedNode.Text);
            Assert.AreEqual(1, loadedNode.Tags.Count);
            Assert.AreEqual("Tag1", loadedNode.Tags[0]);
            Assert.AreEqual(0, loadedNode.TransitionCount);
            Assert.AreSame(loadedStory, loadedNode.ParentStory);
        }

        [TestMethod]
        public void InputtingExistentValidFile_SetsUpTransitionsCorrectly()
        {
            Story story = new Story();
            SpeechNode speechNode = story.CreateNode("Node1");
            SpeechNode destinationNode = story.CreateNode("Node2");
            Transition transition = speechNode.CreateTransition(destinationNode);
            IntentCondition condition = transition.CreateCondition<IntentCondition>();
            condition.IntentName = "TestIntent";

            string filePath = Path.Combine(Resources.TempDir, "Test.txt");
            SaveStoryBinary(story, filePath);

            FileAssert.FileExists(filePath);
            Story loadedStory = Story.Load(filePath);

            Assert.IsNotNull(loadedStory);
            Assert.AreEqual(2, loadedStory.NodeCount);

            SpeechNode loadedNode = loadedStory.GetNodeAt(0);
            SpeechNode loadedDestinationNode = loadedStory.GetNodeAt(1);

            Assert.IsNotNull(loadedNode);
            Assert.IsNotNull(loadedDestinationNode);
            Assert.AreEqual(1, loadedNode.TransitionCount);

            Transition loadedTransition = loadedNode.GetTransitionAt(0);

            Assert.AreSame(loadedNode, loadedTransition.Source);
            Assert.AreSame(loadedDestinationNode, loadedTransition.Destination);
            Assert.AreEqual(1, loadedTransition.ConditionCount);

            TransitionCondition loadedCondition = loadedTransition.GetConditionAt(0);

            Assert.IsInstanceOfType(loadedCondition, typeof(IntentCondition));
            Assert.AreEqual("TestIntent", (loadedCondition as IntentCondition).IntentName);
            Assert.AreSame(loadedTransition, loadedCondition.Transition);
        }
        
        #endregion

        #region Twine Story Overload

        [TestMethod]
        public void InputtingNullTwineStory_ReturnsNull()
        {
            Assert.IsNull(Story.Load((TwineStory)null));
        }

        [TestMethod]
        public void InputtingNonNullTwineStory_ReturnsStory()
        {
            TwineStory twineStory = new TwineStory();
            twineStory.Name = "Test";
            Story story = Story.Load(twineStory);

            Assert.IsNotNull(story);
            Assert.AreEqual("Test", story.Name);
        }

        [TestMethod]
        public void InputtingNonNullTwineStory_WithNonExistentStartNode_SetsStartNode_ToNull()
        {
            TwineStory twineStory = new TwineStory();
            TwineSpeechNode twineSpeechNode = new TwineSpeechNode();
            twineSpeechNode.Name = "TestName";
            twineStory.Nodes.Add(twineSpeechNode);
            twineStory.OneBasedStartNodeIndex = 100;

            Assert.IsNull(twineStory.Nodes.Find(x => x.OneBasedIndex == 100));

            Story story = Story.Load(twineStory);

            Assert.IsNotNull(story);
            Assert.IsNull(story.StartNode);
        }

        [TestMethod]
        public void InputtingNonNullTwineStory_WithExistentStartNode_SetsUpStartNodeCorrectly()
        {
            TwineStory twineStory = new TwineStory();
            TwineSpeechNode twineSpeechNode = new TwineSpeechNode();
            twineSpeechNode.Name = "TestName";
            twineSpeechNode.OneBasedIndex = 100;
            twineStory.Nodes.Add(twineSpeechNode);
            twineStory.OneBasedStartNodeIndex = 100;

            Assert.AreSame(twineSpeechNode, twineStory.Nodes.Find(x => x.OneBasedIndex == 100));

            Story story = Story.Load(twineStory);

            Assert.IsNotNull(story);
            Assert.IsNotNull(story.StartNode);
            Assert.AreSame(story.StartNode, story.GetNodeAt(0));
        }

        [TestMethod]
        public void InputtingNonNullTwineStory_SetsUpNodesCorrectly()
        {
            TwineStory twineStory = new TwineStory();
            TwineSpeechNode twineSpeechNode = new TwineSpeechNode();
            twineSpeechNode.Name = "TestName";
            twineStory.Nodes.Add(twineSpeechNode);
            Story story = Story.Load(twineStory);

            Assert.IsNotNull(story);
            Assert.AreEqual("TestName", twineSpeechNode.Name);
        }

        [TestMethod]
        public void InputtingNonNullTwineStory_SetsUpTransitionsCorrectly()
        {
            TwineStory twineStory = new TwineStory();
            TwineSpeechNode twineSpeechNode = new TwineSpeechNode();
            twineSpeechNode.Name = "TestName";
            twineStory.Nodes.Add(twineSpeechNode);
            TwineSpeechNode twineSpeechNode2 = new TwineSpeechNode();
            twineSpeechNode2.Name = "TestName2";
            twineStory.Nodes.Add(twineSpeechNode2);

            TwineLink twineLink = new TwineLink("Link", "Text|TestName2");
            twineSpeechNode.TwineLinks.Add(twineLink);

            Story story = Story.Load(twineStory);

            Assert.IsNotNull(story);
            Assert.AreEqual(2, story.NodeCount);

            SpeechNode speechNode = story.GetNodeAt(0);

            Assert.AreEqual(1, speechNode.TransitionCount);

            Transition transition = speechNode.GetTransitionAt(0);

            Assert.AreSame(speechNode, transition.Source);
            Assert.AreSame(story.GetNodeAt(1), transition.Destination);
        }

        #endregion

        #endregion

        #region Save Tests

        [TestMethod]
        public void Save_Overwrite_FileDoesNotExist_SavesFileCorrectly_ReturnsTrue()
        {
            Story story = new Story();
            SpeechNode speechNode = story.CreateNode("Test");
            SpeechNode speechNode2 = story.CreateNode("Test2");
            speechNode.CreateTransition(speechNode2);

            string filePath = Path.Combine(Resources.TempDir, "Test.data");

            FileAssert.FileDoesNotExist(filePath);
            Assert.IsTrue(story.Save(filePath, true));
            FileAssert.FileExists(filePath);

            Story loadedStory = Story.Load(filePath);

            Assert.IsNotNull(loadedStory);
            Assert.AreEqual(2, loadedStory.NodeCount);
            Assert.IsNotNull(loadedStory.FindNode("Test"));
            Assert.IsNotNull(loadedStory.FindNode("Test2"));
            Assert.AreEqual(1, loadedStory.FindNode("Test").TransitionCount);
            Assert.AreSame(loadedStory.FindNode("Test"), loadedStory.FindNode("Test").GetTransitionAt(0).Source);
            Assert.AreSame(loadedStory.FindNode("Test2"), loadedStory.FindNode("Test").GetTransitionAt(0).Destination);
        }

        [TestMethod]
        public void Save_Overwrite_FileExists_OverwritesFileCorrectly_ReturnsTrue()
        {
            Story story = new Story();
            SpeechNode speechNode = story.CreateNode("Test");
            SpeechNode speechNode2 = story.CreateNode("Test2");
            speechNode.CreateTransition(speechNode2);

            string filePath = Path.Combine(Resources.TempDir, "Test.data");
            File.WriteAllText(filePath, "Test");

            FileAssert.FileExists(filePath);
            Assert.IsTrue(story.Save(filePath, true));
            FileAssert.FileExists(filePath);

            Story loadedStory = Story.Load(filePath);

            Assert.IsNotNull(loadedStory);
            Assert.AreEqual(2, loadedStory.NodeCount);
            Assert.IsNotNull(loadedStory.FindNode("Test"));
            Assert.IsNotNull(loadedStory.FindNode("Test2"));
            Assert.AreEqual(1, loadedStory.FindNode("Test").TransitionCount);
            Assert.AreSame(loadedStory.FindNode("Test"), loadedStory.FindNode("Test").GetTransitionAt(0).Source);
            Assert.AreSame(loadedStory.FindNode("Test2"), loadedStory.FindNode("Test").GetTransitionAt(0).Destination);
        }

        [TestMethod]
        public void Save_DontOverwrite_FileDoesNotExist_SavesFileCorrectly_ReturnsTrue()
        {
            Story story = new Story();
            SpeechNode speechNode = story.CreateNode("Test");
            SpeechNode speechNode2 = story.CreateNode("Test2");
            speechNode.CreateTransition(speechNode2);

            string filePath = Path.Combine(Resources.TempDir, "Test.data");

            FileAssert.FileDoesNotExist(filePath);
            Assert.IsTrue(story.Save(filePath, false));
            FileAssert.FileExists(filePath);

            Story loadedStory = Story.Load(filePath);

            Assert.IsNotNull(loadedStory);
            Assert.AreEqual(2, loadedStory.NodeCount);
            Assert.IsNotNull(loadedStory.FindNode("Test"));
            Assert.IsNotNull(loadedStory.FindNode("Test2"));
            Assert.AreEqual(1, loadedStory.FindNode("Test").TransitionCount);
            Assert.AreSame(loadedStory.FindNode("Test"), loadedStory.FindNode("Test").GetTransitionAt(0).Source);
            Assert.AreSame(loadedStory.FindNode("Test2"), loadedStory.FindNode("Test").GetTransitionAt(0).Destination);
        }

        [TestMethod]
        public void Save_DontOverwrite_FileExists_DoesNothing_ReturnsFalse()
        {
            Story story = new Story();
            SpeechNode speechNode = story.CreateNode("Test");
            SpeechNode speechNode2 = story.CreateNode("Test2");
            speechNode.CreateTransition(speechNode2);

            string filePath = Path.Combine(Resources.TempDir, "Test.data");
            File.WriteAllText(filePath, "Test");

            FileAssert.FileExists(filePath);
            Assert.IsFalse(story.Save(filePath, false));
            FileAssert.FileExists(filePath);
            Assert.AreEqual("Test", File.ReadAllText(filePath));
        }

        #endregion

        #region Utility Functions

        /// <summary>
        /// Saves the inputted story to the inputted file in binary format.
        /// </summary>
        /// <param name="story"></param>
        /// <param name="filePath"></param>
        public void SaveStoryBinary(Story story, string filePath)
        {
            using (FileStream file = new FileStream(filePath, FileMode.OpenOrCreate))
            {
                try
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    formatter.Serialize(file, story);
                }
                catch (Exception e) { Assert.Fail(e.Message); }
            }
        }

        #endregion
    }
}