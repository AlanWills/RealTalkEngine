using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealTalkEngineEditorLibrary.StorySystem.Attributes
{
    public class NodeViewModelAttribute : Attribute
    {
        #region Properties and Fields

        /// <summary>
        /// The node type that the viewmodel this is attached to will be used to represent.
        /// </summary>
        public Type NodeType { get; }

        /// <summary>
        /// The name that will appear in UI designed to allow users to create a node of this type.
        /// </summary>
        public string MenuName { get; }

        #endregion

        public NodeViewModelAttribute(Type nodeType, string menuName)
        {
            NodeType = nodeType;
            MenuName = menuName;
        }
    }
}
