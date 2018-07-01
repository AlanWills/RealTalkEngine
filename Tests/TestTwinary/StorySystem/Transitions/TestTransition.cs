using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using Twinary.StorySystem.Nodes;
using Twinary.StorySystem.Transitions;

namespace TestTwinary.StorySystem.Transitions
{
    [TestClass]
    public class TestTransition
    {
        #region Constructor Tests

        [TestMethod]
        public void Constructor_SetsSourceNode_ToInputtedValue()
        {
            SpeechNode speechNode = new SpeechNode();
            Transition transition = new Transition(speechNode, new SpeechNode());

            Assert.AreSame(speechNode, transition.Source);
        }

        [TestMethod]
        public void Constructor_SetsDestinationNode_ToInputtedValue()
        {
            SpeechNode destinationNode = new SpeechNode();
            Transition transition = new Transition(new SpeechNode(), destinationNode);

            Assert.AreSame(destinationNode, transition.Destination);
        }

        #endregion
    }
}
