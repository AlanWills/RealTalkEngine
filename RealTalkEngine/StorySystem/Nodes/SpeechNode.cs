using Newtonsoft.Json;
using RealTalkEngine.StorySystem.Transitions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.Serialization;
using System.Text;
using Twinary.StorySystem.Nodes;
using Twinary.StorySystem.Transitions;

namespace RealTalkEngine.StorySystem.Nodes
{
    [Serializable]
    public class SpeechNode
    {
        #region Serialized Properties and Fields

        /// <summary>
        /// The display name of this node.
        /// </summary>
        [DataMember(IsRequired = true)]
        public string Name { get; private set; } = "";
        
        /// <summary>
        /// The textual content of this node.
        /// </summary>
        [DataMember]
        public string Text { get; private set; } = "";

        /// <summary>
        /// The tags that have been applied to this node.
        /// </summary>
        [DataMember]
        public List<string> Tags { get; private set; } = new List<string>();

        /// <summary>
        /// The transitions between this node and other nodes within the story.
        /// Used at runtime to determine the next nodes when moving through the story.
        /// </summary>
        [DataMember]
        private List<Transition> Transitions { get; set; } = new List<Transition>();

        /// <summary>
        /// The parent story that this node is part of at runtime.
        /// </summary>
        public Story ParentStory { get; set; }

        #endregion

        #region Properties
        
        /// <summary>
        /// The number of runtime transitions this node has.
        /// </summary>
        public int TransitionCount { get { return Transitions.Count; } }

        #endregion

        #region Constructors

        public SpeechNode()
        {
        }

        public SpeechNode(TwineSpeechNode twineSpeechNode)
        {
            Name = twineSpeechNode.Name;
            Text = twineSpeechNode.Text;
            Tags.AddRange(twineSpeechNode.Tags);
        }

        #endregion
        
        #region Transition Functions

        /// <summary>
        /// Creates a new transition from this node to the inputted node and adds it to this node's transitions.
        /// </summary>
        /// <param name="destinationNode"></param>
        /// <returns></returns>
        public Transition CreateTransition(SpeechNode destinationNode)
        {
            if (destinationNode == null)
            {
                return null;
            }

            Transition transition = new Transition(this, destinationNode);
            Transitions.Add(transition);

            return transition;
        }

        /// <summary>
        /// Return the transition on this node at the inputted index.
        /// If the index is out of the range of the transitions, this function returns null.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public Transition GetTransitionAt(uint index)
        {
            return index < TransitionCount ? Transitions[(int)index] : null;
        }

        #endregion

        #region Utility Functions

        /// <summary>
        /// Iterate through all the transitions on this node and find the first which is valid.
        /// Then, returns the destination node for this first valid transition.
        /// </summary>
        /// <returns></returns>
        public SpeechNode GetNextNode()
        {
            foreach (Transition transition in Transitions)
            {
                if (transition.ValidateConditions())
                {
                    return transition.Destination;
                }
            }

            return null;
        }

        #endregion
    }
}
