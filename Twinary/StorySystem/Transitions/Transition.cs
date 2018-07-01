using System;
using System.Collections.Generic;
using System.Text;
using Twinary.StorySystem.Nodes;

namespace Twinary.StorySystem.Transitions
{
    [Serializable]
    public class Transition
    {
        #region Properties and Fields

        /// <summary>
        /// The source node that this transitino is connected from.
        /// </summary>
        public SpeechNode Source { get; private set; }

        /// <summary>
        /// The destination node that this transition is connected to.
        /// </summary>
        public SpeechNode Destination { get; private set; }

        #endregion

        public Transition(SpeechNode source, SpeechNode destination)
        {
            Source = source;
            Destination = destination;
        }
    }
}