using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.Serialization;
using System.Text;
using Twinary.StorySystem.Transitions;

namespace Twinary.StorySystem.Nodes
{
    [Serializable]
    public class SpeechNode
    {
        #region Serialized Properties and Fields

        /// <summary>
        /// The display name of this node.
        /// </summary>
        [JsonProperty(PropertyName = "name", Required = Required.Always), DataMember(Name = "name", IsRequired = true)]
        public string Name { get; private set; } = "";

        /// <summary>
        /// The one based index of this node in the story.
        /// Do not use this field, use the NodeIndex property instead.
        /// </summary>
        [JsonProperty(PropertyName = "pid", Required = Required.Always), DataMember(Name = "pid", IsRequired = true)]
        private int OneBasedIndex = 1;

        /// <summary>
        /// The textual content of this node.
        /// </summary>
        [JsonProperty(PropertyName = "text"), DataMember(Name = "text")]
        public string Text { get; private set; } = "";

        /// <summary>
        /// The tags that have been applied to this node.
        /// </summary>
        [JsonProperty(PropertyName = "tags"), DataMember(Name = "tags")]
        public List<string> Tags { get; private set; } = new List<string>();

        /// <summary>
        /// The twine links from this node to other nodes.
        /// Not really used in the runtime, but more for an intermediate storage.
        /// </summary>
        [JsonProperty(PropertyName = "links"), DataMember(Name = "links")]
        protected List<TwineLink> TwineLinks { get; private set; } = new List<TwineLink>();

        #endregion

        #region Properties

        /// <summary>
        /// The zero based index of this node within the story.
        /// </summary>
        public int NodeIndex { get { return OneBasedIndex - 1; } }

        /// <summary>
        /// The transitions between this node and other nodes within the story.
        /// Used at runtime to determine the next nodes when moving through the story.
        /// </summary>
        private List<Transition> Transitions { get; set; } = new List<Transition>();
        
        /// <summary>
        /// The number of runtime transitions this node has.
        /// </summary>
        public int TransitionCount { get { return Transitions.Count; } }

        #endregion

        #region Initialization

        /// <summary>
        /// Create transitions from this node to the other nodes in the story using the inputted lookup.
        /// </summary>
        /// <param name="nodeLookup"></param>
        public void InitializeTransitions(Dictionary<string, SpeechNode> nodeLookup)
        {
            if (nodeLookup != null)
            {
                foreach (TwineLink link in TwineLinks)
                {
                    // Lazily evaluated so cached
                    string destination = link.DestinationName;
                    if (nodeLookup.ContainsKey(destination))
                    {
                        // Add a new runtime transition for this twine link
                        Transitions.Add(new Transition(this, nodeLookup[destination]));
                    }
                }
            }
        }

        #endregion

        #region Constructors

        public SpeechNode()
        {
        }

        public SpeechNode(string name, int zeroBasedIndex)
        {
            Name = name;
            OneBasedIndex = zeroBasedIndex + 1;
        }

        #endregion

        #region Transition Functions

        public Transition GetTransitionAt(uint index)
        {
            return index < TransitionCount ? Transitions[(int)index] : null;
        }

        #endregion
    }
}
