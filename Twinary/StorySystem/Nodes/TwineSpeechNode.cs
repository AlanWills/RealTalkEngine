using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Numerics;
using Twinary.Deserialization;
using Twinary.StorySystem.Transitions;

namespace Twinary.StorySystem.Nodes
{
    [Serializable]
    public class TwineSpeechNode
    {
        #region Serialized Properties and Fields

        /// <summary>
        /// The display name of this node.
        /// </summary>
        [JsonProperty(PropertyName = "name", Required = Required.Always)]
        public string Name { get; set; } = "";

        /// <summary>
        /// The one based index of this node in the story.
        /// Do not use this field, use the NodeIndex property instead.
        /// </summary>
        [JsonProperty(PropertyName = "pid", Required = Required.Always)]
        public int OneBasedIndex { get; set; } = 1;

        /// <summary>
        /// The textual content of this node.
        /// </summary>
        [JsonProperty(PropertyName = "text")]
        public string Text { get; set; } = "";

        /// <summary>
        /// The tags that have been applied to this node.
        /// </summary>
        [JsonProperty(PropertyName = "tags")]
        public List<string> Tags { get; private set; } = new List<string>();

        /// <summary>
        /// The twine links from this node to other nodes.
        /// Not really used in the runtime, but more for an intermediate storage.
        /// </summary>
        [JsonProperty(PropertyName = "links")]
        public List<TwineLink> TwineLinks { get; private set; } = new List<TwineLink>();

        [JsonProperty(PropertyName = "position")]
        [JsonConverter(typeof(Vector2JsonDeserializer))]
        public Vector2 Position { get; set; }

        #endregion
    }
}