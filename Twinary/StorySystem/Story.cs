using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization;
using System.Text;
using Twinary.StorySystem.Nodes;

namespace Twinary.StorySystem
{
    [Serializable, DataContract]
    public class Story
    {
        #region Serialized Properties

        /// <summary>
        /// The name of this story.
        /// </summary>
        [JsonProperty(PropertyName = "name", Required = Required.Always)]
        public string Name { get; private set; }

        /// <summary>
        /// The 1-based index of the node index which is the starting node for this story.
        /// </summary>
        [JsonProperty(PropertyName = "startnode", Required = Required.Always)]
        public int OneBasedStartNodeIndex { get; private set; }

        /// <summary>
        /// The software responsible for creating this story.
        /// </summary>
        [JsonProperty(PropertyName = "creator")]
        public string Creator { get; private set; }

        /// <summary>
        /// The version of the software responsible for creating this story.
        /// </summary>
        [JsonProperty(PropertyName = "creator-version")]
        public string CreatorVersion { get; private set; }

        [JsonProperty(PropertyName = "ifid")]
        public Guid IfID { get; private set; }

        /// <summary>
        /// The nodes contained within this story.
        /// </summary>
        [JsonProperty(PropertyName = "passages")]
        public List<SpeechNode> Nodes { get; private set; } = new List<SpeechNode>();

        #endregion

        #region Properties and Fields
        
        /// <summary>
        /// A dictionary of node name to node instance which allows us to quickly access nodes from their names.
        /// </summary>
        private Dictionary<string, SpeechNode> NodeLookup { get; set; } = new Dictionary<string, SpeechNode>();

        #endregion

        /// <summary>
        /// Ensure people use the public Load function for loading stories.
        /// </summary>
        private Story()
        {
        }

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
                Story story = JsonConvert.DeserializeObject<Story>(File.ReadAllText(filePath));
                story?.Initialize();

                return story;
            }
            catch { return null; }
        }

        #endregion

        #region Initialization

        /// <summary>
        /// Sets up this story.  Initializes data structures and transitions within the story.
        /// </summary>
        private void Initialize()
        {
            InitializeNodeLookup();
        }

        /// <summary>
        /// Initialize our NodeLookup to provide quick access to node instances within this story.
        /// </summary>
        private void InitializeNodeLookup()
        {
            foreach (SpeechNode node in Nodes)
            {
                Debug.Assert(!NodeLookup.ContainsKey(node.Name));
                if (!NodeLookup.ContainsKey(node.Name))
                {
                    NodeLookup.Add(node.Name, node);
                }
            }
        }
        
        #endregion
    }
}