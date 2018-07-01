using System;
using System.Collections.Generic;
using System.Text;
using Twinary.StorySystem.Nodes;
using Twinary.StorySystem.Transitions;

namespace TestTwinary.Mocks
{
    public class MockSpeechNode : SpeechNode
    {
        #region Public Interfaces to Protected Fields

        public List<TwineLink> TwineLinks_Public { get { return TwineLinks; } }

        #endregion
    }
}
