using Newtonsoft.Json;
using System;
using System.Collections.Generic;
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
        public string Name { get; private set; }

        /// <summary>
        /// The one based index of this node in the story.
        /// Do not use this field, use the NodeIndex property instead.
        /// </summary>
        [JsonProperty(PropertyName = "pid", Required = Required.Always), DataMember(Name = "pid", IsRequired = true)]
        private int one_based_index;

        /// <summary>
        /// The textual content of this node.
        /// </summary>
        [JsonProperty(PropertyName = "text"), DataMember(Name = "text")]
        public string Text { get; private set; }

        /// <summary>
        /// The tags that have been applied to this node.
        /// </summary>
        [JsonProperty(PropertyName = "tags"), DataMember(Name = "tags")]
        public List<string> Tags { get; private set; } = new List<string>();

        /// <summary>
        /// The transitions from this node to other nodes.
        /// </summary>
        [JsonProperty(PropertyName = "links"), DataMember(Name = "links")]
        public List<Transition> Transitions { get; private set; } = new List<Transition>();

        #endregion

        #region Properties

        /// <summary>
        /// The zero based index of this node within the story.
        /// </summary>
        public int NodeIndex { get { return one_based_index - 1; } }

        #endregion
    }
}
