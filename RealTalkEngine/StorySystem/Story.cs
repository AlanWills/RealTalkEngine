using Newtonsoft.Json;
using RealTalkEngine.StorySystem.Conditions;
using RealTalkEngine.StorySystem.Nodes;
using RealTalkEngine.StorySystem.Transitions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using Twinary.StorySystem;
using Twinary.StorySystem.Nodes;
using Twinary.StorySystem.Transitions;

namespace RealTalkEngine.StorySystem
{
    [Serializable]
    public class Story
    {
        #region Serialized Properties

        /// <summary>
        /// The name of this story.
        /// </summary>
        public string Name { get; set; } = "";

        /// <summary>
        /// The name of the node which is the start of our story.
        /// </summary>
        private string StartNodeName { get; set; } = "";

        /// <summary>
        /// The nodes contained within this story.
        /// </summary>
        private List<SpeechNode> Nodes { get; set; } = new List<SpeechNode>();

        #endregion

        #region Properties and Fields

        /// <summary>
        /// The number of nodes in this story.
        /// </summary>
        public int NodeCount { get { return Nodes.Count; } }

        /// <summary>
        /// The starting node for this story.
        /// Will return null if the starting node could not be found.
        /// </summary>
        public SpeechNode StartNode { get { return m_nodeLookup.ContainsKey(StartNodeName) ? m_nodeLookup[StartNodeName] : null; } }

        [NonSerialized]
        private StoryRuntime m_runtime;
        /// <summary>
        /// The current runtime that this story is being processed by.
        /// </summary>
        public StoryRuntime Runtime
        {
            get { return m_runtime; }
            set { m_runtime = value; }
        }

        /// <summary>
        /// A dictionary of node name to node instance which allows us to quickly access nodes from their names.
        /// </summary>
        [NonSerialized]
        private Dictionary<string, SpeechNode> m_nodeLookup = new Dictionary<string, SpeechNode>();

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

            using (FileStream file = new FileStream(filePath, FileMode.Open))
            {
                try
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    return formatter.Deserialize(file) as Story;
                }
                catch { return null; }
            }
        }

        /// <summary>
        /// Attempt to load a story from the inputted twinary story.
        /// Returns null if there was a problem converting the story.
        /// </summary>
        /// <returns></returns>
        public static Story Load(TwineStory twineStory)
        {
            if (twineStory == null)
            {
                return null;
            }

            Story story = new Story();
            story.Name = twineStory.Name;

            TwineSpeechNode firstNode = twineStory.Nodes.Find(x => x.OneBasedIndex == twineStory.OneBasedStartNodeIndex);
            story.StartNodeName = firstNode != null ? firstNode.Name : "";
            
            foreach (TwineSpeechNode twineSpeechNode in twineStory.Nodes)
            {
                story.CreateNode(twineSpeechNode);
            }

            // Only need to initialize transitions here - story.CreateNode will initialize the lookup
            story.InitializeNodeTransitions(twineStory);

            return story;
        }

        #endregion

        #region Saving

        public void Save(string filePath, bool overwrite = false)
        {
            // Will clear or create the file
            File.WriteAllText(filePath, "");

            using (FileStream file = new FileStream(filePath, FileMode.Open))
            {
                try
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    formatter.Serialize(file, this);
                }
                catch { }
            }
        }

        #endregion

        #region Deserialization

        [OnDeserialized]
        private void OnDeserialized(StreamingContext context)
        {
            if (m_nodeLookup == null)
            {
                m_nodeLookup = new Dictionary<string, SpeechNode>();
            }

            InitializeNodes();
            InitializeNodeLookup();
            InitializeNodeTransitions();
        }

        #endregion

        #region Initialization

        /// <summary>
        /// When binary deserializing, some node runtime references get lost so we fix them up here.
        /// </summary>
        private void InitializeNodes()
        {
            foreach (SpeechNode node in Nodes)
            {
                node.ParentStory = this;
            }
        }

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
        /// Set up all the transitions' references in this story.
        /// If we have deserialized from binary, certain runtime references will be missing.
        /// </summary>
        private void InitializeNodeTransitions()
        {
            // For every node
            foreach (SpeechNode node in Nodes)
            {
                // And every transition that node has
                foreach (Transition transition in node)
                {
                    // Fix up the reference to the transition in every condition
                    foreach (TransitionCondition condition in transition)
                    {
                        condition.Transition = transition;
                    }
                }
            }
        }

        /// <summary>
        /// Set up all the transitions between all the nodes in the story.
        /// The node lookup must be initialized before calling this function.
        /// </summary>
        private void InitializeNodeTransitions(TwineStory twineStory)
        {
            Debug.Assert(Nodes.Count == m_nodeLookup.Count);

            // For each twine node in the story
            foreach (TwineSpeechNode twineNode in twineStory.Nodes)
            {
                // Attempt to find the corresponding node in this story
                if (m_nodeLookup.TryGetValue(twineNode.Name, out SpeechNode speechNode))
                {
                    // If we have found it, we go through each twine link in the original node
                    foreach (TwineLink twineLink in twineNode.TwineLinks)
                    {
                        // And try and find the corresponding destination node in this story that it was pointing to
                        if (m_nodeLookup.TryGetValue(twineLink.DestinationName, out SpeechNode destinationNode))
                        {
                            // If we find it, we create a transition in this story
                            speechNode.CreateTransition(destinationNode);
                        }
                    }
                }
            }
        }

        #endregion

        #region Node Functions

        /// <summary>
        /// Adds the inputted node into this story.
        /// </summary>
        /// <param name="speechNode"></param>
        /// <returns></returns>
        public SpeechNode AddNode(SpeechNode speechNode)
        {
            if (speechNode != null)
            {
                speechNode.ParentStory = this;

                // Keep our lookup and node list up to date
                m_nodeLookup.Add(speechNode.Name, speechNode);
                Nodes.Add(speechNode);
            }

            return speechNode;
        }

        /// <summary>
        /// Creates a new SpeechNode and adds it to this story.
        /// </summary>
        /// <param name="twineSpeechNode"></param>
        /// <returns></returns>
        public SpeechNode CreateNode(string nodeName)
        {
            SpeechNode speechNode = new SpeechNode
            {
                Name = nodeName
            };

            return AddNode(speechNode);
        }

        /// <summary>
        /// Creates a new node in this story using the inputted twine speech node format.
        /// Will add the newly created node to this story.
        /// </summary>
        /// <param name="twineSpeechNode"></param>
        /// <returns></returns>
        public SpeechNode CreateNode(TwineSpeechNode twineSpeechNode)
        {
            return twineSpeechNode != null ? AddNode(new SpeechNode(twineSpeechNode)) : null;
        }

        /// <summary>
        /// If the inputted index is valid, returns the node at the corresponding index in the nodes list.
        /// Otherwise returns null.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public SpeechNode GetNodeAt(uint index)
        {
            return 0 <= index && index < Nodes.Count ? Nodes[(int)index] : null;
        }

        /// <summary>
        /// Attempts to find a node with a name which matches the inputted name.
        /// Will return null if no such node could be found.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public SpeechNode FindNode(string name)
        {
            return m_nodeLookup.ContainsKey(name) ? m_nodeLookup[name] : null;
        }

        /// <summary>
        /// Attempts to find a node with the same NodeIndex as the inputted value.
        /// Will return null if no such node could be found.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public SpeechNode FindNode(int nodeIndex)
        {
            return Nodes.Find(x => x.NodeIndex == nodeIndex);
        }

        #endregion
    }
}