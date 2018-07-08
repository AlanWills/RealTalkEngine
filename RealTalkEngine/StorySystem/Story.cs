using Newtonsoft.Json;
using RealTalkEngine.StorySystem.Nodes;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using Twinary.StorySystem;
using Twinary.StorySystem.Transitions;

namespace RealTalkEngine.StorySystem
{
    [Serializable, DataContract]
    public class Story
    {
        #region Serialized Properties

        /// <summary>
        /// The name of this story.
        /// </summary>
        [DataMember(IsRequired = true)]
        public string Name { get; private set; }

        /// <summary>
        /// The 0-based index of the node index which is the starting node for this story.
        /// </summary>
        [DataMember(IsRequired = true)]
        private int StartNodeIndex { get; set; }

        /// <summary>
        /// The nodes contained within this story.
        /// </summary>
        private List<SpeechNode> Nodes { get; set; } = new List<SpeechNode>();

        #endregion

        #region Properties and Fields

        /// <summary>
        /// The starting node for this story.
        /// </summary>
        public SpeechNode StartNode { get { return (0 <= StartNodeIndex && StartNodeIndex < Nodes.Count) ? Nodes[StartNodeIndex] : null; } }

        /// <summary>
        /// The current runtime that this story is being processed by.
        /// </summary>
        public StoryRuntime Runtime { get; set; }

        /// <summary>
        /// A dictionary of node name to node instance which allows us to quickly access nodes from their names.
        /// </summary>
        private Dictionary<string, SpeechNode> m_nodeLookup = new Dictionary<string, SpeechNode>();

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

            using (FileStream file = new FileStream(filePath, FileMode.Open))
            {
                try
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    Story story = formatter.Deserialize(file) as Story;
                    story?.InitializeNodeLookup();

                    return story;
                }
                catch { return null; }
            }
        }

        /// <summary>
        /// Attempt to load a story from the inputted twinary story.
        /// Returns null if there was a problem converting the story.
        /// </summary>
        /// <returns></returns>
        public static Story Load(Twinary.StorySystem.Story twineStory)
        {
            Story story = new Story();
            story.Name = twineStory.Name;
            story.StartNodeIndex = twineStory.OneBasedStartNodeIndex;
            
            foreach (Twinary.StorySystem.Nodes.SpeechNode twineSpeechNode in twineStory.Nodes)
            {
                story.CreateAndAddNode(twineSpeechNode);
            }

            story.InitializeNodeLookup();
            story.InitializeNodeTransitions(twineStory);

            return null;
        }

        #endregion

        #region Initialization
        
        /// <summary>
        /// Initialize our NodeLookup to provide quick access to node instances within this story.
        /// </summary>
        private void InitializeNodeLookup()
        {
            foreach (SpeechNode node in Nodes)
            {
                Debug.Assert(!m_nodeLookup.ContainsKey(node.Name));
                if (!m_nodeLookup.ContainsKey(node.Name))
                {
                    m_nodeLookup.Add(node.Name, node);
                }
            }
        }

        /// <summary>
        /// Set up all the transitions between all the nodes in the story.
        /// The node lookup must be initialized before calling this function.
        /// </summary>
        private void InitializeNodeTransitions(Twinary.StorySystem.Story twineStory)
        {
            Debug.Assert(Nodes.Count == m_nodeLookup.Count);

            // For each twine node in the story
            foreach (Twinary.StorySystem.Nodes.SpeechNode twineNode in twineStory.Nodes)
            {
                // Attempt to find the corresponding node in this story
                SpeechNode speechNode, destinationNode;
                if (m_nodeLookup.TryGetValue(twineNode.Name, out speechNode))
                {
                    // If we have found it, we go through each twine link in the original node
                    foreach (TwineLink twineLink in twineNode.TwineLinks)
                    {
                        // And try and find the corresponding destination node in this story that it was pointing to
                        if (m_nodeLookup.TryGetValue(twineLink.DestinationName, out destinationNode))
                        {
                            // If we find it, we create a transition in this story
                            speechNode.CreateAndAddTransition(destinationNode);
                        }
                    }
                }
            }
        }

        #endregion

        #region Node Functions

        /// <summary>
        /// Creates a new node in this story using the inputted twine speech node format.
        /// Will add the newly created node to this story.
        /// </summary>
        /// <param name="twineSpeechNode"></param>
        /// <returns></returns>
        public SpeechNode CreateAndAddNode(Twinary.StorySystem.Nodes.SpeechNode twineSpeechNode)
        {
            SpeechNode speechNode = new SpeechNode(twineSpeechNode);
            speechNode.ParentStory = this;
            Nodes.Add(speechNode);

            return speechNode;
        }

        #endregion
    }
}