using BindingsKernel;
using Newtonsoft.Json;
using RealTalkEngine.StorySystem.Transitions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.Serialization;
using System.Text;
using Twinary.StorySystem.Nodes;
using Twinary.StorySystem.Transitions;

namespace RealTalkEngine.StorySystem.Nodes
{
    [Serializable]
    public class SpeechNode : IEnumerable<Transition>
    {
        #region Serialized Properties
        
        /// <summary>
        /// The display name of this node.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The zero-based index of this node within the story.
        /// </summary>
        public int NodeIndex { get; set; }
        
        /// <summary>
        /// The textual content of this node.
        /// </summary>
        public string Text { get; set; } = "";

        /// <summary>
        /// The tags that have been applied to this node.
        /// </summary>
        public List<string> Tags { get; private set; } = new List<string>();

        /// <summary>
        /// The transitions between this node and other nodes within the story.
        /// Used at runtime to determine the next nodes when moving through the story.
        /// </summary>
        private List<Transition> Transitions { get; set; } = new List<Transition>();

        /// <summary>
        /// The position of this node within the story graph.
        /// </summary>
        public Vector2 Position { get; set; }

        #endregion

        #region Properties
        
        /// <summary>
        /// The number of runtime transitions this node has.
        /// </summary>
        public int TransitionCount { get { return Transitions.Count; } }

        [NonSerialized]
        private Story m_parentStory;
        /// <summary>
        /// The parent story that this node is part of at runtime.
        /// </summary>
        public Story ParentStory
        {
            get { return m_parentStory; }
            set { m_parentStory = value; }
        }

        #endregion

        #region Constructors

        public SpeechNode()
        {
        }

        public SpeechNode(TwineSpeechNode twineSpeechNode)
        {
            Name = twineSpeechNode.Name;
            NodeIndex = twineSpeechNode.OneBasedIndex - 1;
            Text = twineSpeechNode.Text;
            Tags.AddRange(twineSpeechNode.Tags);
            Position = new Vector2(twineSpeechNode.Position.X, twineSpeechNode.Position.Y);
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

        #region IEnumerable Implementation

        public IEnumerator<Transition> GetEnumerator()
        {
            return Transitions.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return Transitions.GetEnumerator();
        }

        #endregion
    }
}
