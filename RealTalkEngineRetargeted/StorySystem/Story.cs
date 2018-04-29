using BindingsKernel;
using BindingsKernel.Serialization;
using RealTalkEngine.StorySystem.Nodes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace RealTalkEngine.StorySystem
{
    public class Story : ScriptableObject, ICustomSerialization, ICustomDeserialization
    {
        #region Properties and Fields

        private List<BaseNode> NodesImpl { get; set; } = new List<BaseNode>();

        private ReadOnlyCollection<BaseNode> nodes;
        public ReadOnlyCollection<BaseNode> Nodes
        {
            get { return nodes ?? (nodes = new ReadOnlyCollection<BaseNode>(NodesImpl)); }
        }

        #endregion

        #region Serialization and Deserialization

        public void Serialize(string name, XmlWriter writer)
        {
            writer.WriteStartElement("Nodes");

            foreach (BaseNode baseNode in Nodes)
            {
                baseNode.WriteXml(writer);
            }

            writer.WriteEndElement();
        }

        public void Deserialize(string name, XmlReader reader)
        {
            reader.Read();
            reader.Read();

            CelDebug.Assert(reader.LocalName == "Nodes");
            reader.ReadStartElement();

            // Now, we go through all the child nodes and deserialize them
            while (reader.IsStartElement())
            {
                // Cache this value here as the ReadElementContentAsString method will advance the reader onwards
                string elementName = reader.LocalName;
                BaseNode node = NodeFactory.CreateNode(elementName);
                if (node != null)
                {
                    node.ReadXml(reader, new Dictionary<Guid, System.Reflection.PropertyInfo>());
                    NodesImpl.Add(node);
                }
            }

            reader.ReadEndElement();
        }

        #endregion

        #region Node Management Functions

        /// <summary>
        /// Create a node of the inputted type and add it to this story.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public BaseNode CreateNode(Type type, string name)
        {
            BaseNode baseNode = Activator.CreateInstance(type) as BaseNode;
            baseNode.Name = name;
            NodesImpl.Add(baseNode);

            return baseNode;
        }

        #endregion
    }
}
