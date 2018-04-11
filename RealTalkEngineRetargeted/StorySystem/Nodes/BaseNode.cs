using BindingsKernel;
using System.Xml.Serialization;

namespace RealTalkEngine.StorySystem.Nodes
{
    public abstract class BaseNode : ScriptableObject
    {
        #region Properties and Fields

        [Serialize, XmlAttribute("position")]
        public Vector2 Position { get; set; } = new Vector2();

        #endregion
    }
}
