using System;
using System.Collections.Generic;
using System.Text;
using Twinary.StorySystem.Nodes;
using Twinary.StorySystem.Transitions;

namespace Twinary.Tests.Mocks
{
    public class MockTwineSpeechNode : TwineSpeechNode
    {
        #region Public Interfaces to Protected Fields

        public List<TwineLink> TwineLinks_Public { get { return TwineLinks; } }

        #endregion
    }
}
