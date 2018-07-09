using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using Twinary.StorySystem.Transitions;

namespace Twinary.Tests.StorySystem.Transitions
{
    [TestClass]
    public class TestTwineLink
    {
        #region Constructor Tests

        [TestMethod]
        public void Constructor_Default_SetsName_ToEmptyString()
        {
            TwineLink twineLink = new TwineLink();

            Assert.AreEqual("", twineLink.Name);
        }

        [TestMethod]
        public void Constructor_SetsName_ToInputtedValue()
        {
            TwineLink twineLink = new TwineLink("Name", "Link");

            Assert.AreEqual("Name", twineLink.Name);
        }

        [TestMethod]
        public void Constructor_Default_SetsLinkText_ToEmptyString()
        {
            TwineLink twineLink = new TwineLink();

            Assert.AreEqual("", twineLink.LinkText);
        }

        [TestMethod]
        public void Constructor_SetsLinkText_ToCorrectValue()
        {
            TwineLink twineLink = new TwineLink("Name", "Text|Destination");

            Assert.AreEqual("Text", twineLink.LinkText);
        }

        [TestMethod]
        public void Constructor_Default_SetsDestinationName_ToEmptyString()
        {
            TwineLink twineLink = new TwineLink();

            Assert.AreEqual("", twineLink.DestinationName);
        }

        [TestMethod]
        public void Constructor_SetsDestinationName_ToCorrectValue()
        {
            TwineLink twineLink = new TwineLink("Name", "Text|Destination");

            Assert.AreEqual("Destination", twineLink.DestinationName);
        }

        #endregion
    }
}
