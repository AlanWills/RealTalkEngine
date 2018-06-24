using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Text;

namespace Twinary.StorySystem
{
    [Serializable, DataContract]
    public class Story
    {
        #region Serialized Properties

        /// <summary>
        /// The name of this story.
        /// </summary>
        [JsonProperty(PropertyName = "name", Required = Required.Always), DataMember(Name = "name", IsRequired = true)]
        public string Name { get; private set; }

        /// <summary>
        /// The 1-based index of the node index which is the starting node for this story.
        /// </summary>
        [JsonProperty(PropertyName = "startnode", Required = Required.Always), DataMember(Name = "startnode", IsRequired = true)]
        public int StartNode { get; private set; }

        /// <summary>
        /// The software responsible for creating this story.
        /// </summary>
        [JsonProperty(PropertyName = "creator"), DataMember(Name = "creator")]
        public string Creator { get; private set; }

        /// <summary>
        /// The version of the software responsible for creating this story.
        /// </summary>
        [JsonProperty(PropertyName = "creator-version"), DataMember(Name = "creator-version")]
        public string CreatorVersion { get; private set; }

        [JsonProperty(PropertyName = "ifid"), DataMember(Name = "ifid")]
        public Guid IfID { get; private set; }

        /// <summary>
        /// The nodes contained within this story.
        /// </summary>
        [JsonProperty(PropertyName = "passages"), DataMember(Name = "passages")]
        public List<SpeechNode> Nodes { get; private set; } = new List<SpeechNode>();

        #endregion

        #region Loading

        /// <summary>
        /// Attempt to load a story from the inputted file.
        /// Returns null if the inputted story does not exist or there was a problem deserializing the story.
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static Story Load(string filePath)
        {
            if (!File.Exists(filePath))
            {
                return null;
            }

            try
            {
                return JsonConvert.DeserializeObject<Story>(File.ReadAllText(filePath));
            }
            catch { return null; }
        }

        #endregion
    }
}
