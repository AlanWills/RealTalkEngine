using CelTestSharp;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using Twinary.Tests.Mocks;
using Twinary.StorySystem.Nodes;
using Twinary.StorySystem.Transitions;

namespace Twinary.Tests.StorySystem.Nodes
{
    [TestClass]
    public class TestTwineSpeechNode
    {
        #region Constructor Tests

        [TestMethod]
        public void Constructor_SetsName_ToEmptyString()
        {
            TwineSpeechNode speechNode = new TwineSpeechNode();

            Assert.AreEqual("", speechNode.Name);
        }
        
        [TestMethod]
        public void Constructor_SetsText_ToEmptyString()
        {
            TwineSpeechNode speechNode = new TwineSpeechNode();

            Assert.AreEqual("", speechNode.Text);
        }

        [TestMethod]
        public void Constructor_SetsOneBasedIndex_ToOne()
        {
            TwineSpeechNode speechNode = new TwineSpeechNode();

            Assert.AreEqual(1, speechNode.OneBasedIndex);
        }

        [TestMethod]
        public void Constructor_SetsTags_ToEmptyList()
        {
            TwineSpeechNode speechNode = new TwineSpeechNode();

            AssertExt.IsEmpty(speechNode.Tags);
        }
        
        [TestMethod]
        public void Constructor_SetsTwineLinks_ToEmptyList()
        {
            TwineSpeechNode speechNode = new TwineSpeechNode();

            AssertExt.IsEmpty(speechNode.TwineLinks);
        }

        #endregion
    }
}
