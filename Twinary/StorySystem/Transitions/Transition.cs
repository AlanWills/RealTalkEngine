using System;
using System.Collections.Generic;
using System.Text;
using Twinary.StorySystem.Conditions;
using Twinary.StorySystem.Nodes;

namespace Twinary.StorySystem.Transitions
{
    [Serializable]
    public class Transition
    {
        #region Properties and Fields

        /// <summary>
        /// The source node that this transition is connected from.
        /// </summary>
        public SpeechNode Source { get; private set; }

        /// <summary>
        /// The destination node that this transition is connected to.
        /// </summary>
        public SpeechNode Destination { get; private set; }

        /// <summary>
        /// All of the conditions that must be satisfied for the transition to be valid at runtime.
        /// </summary>
        private List<TransitionCondition> Conditions { get; set; } = new List<TransitionCondition>();

        #endregion

        public Transition(SpeechNode source, SpeechNode destination)
        {
            Source = source;
            Destination = destination;
        }
    }
}