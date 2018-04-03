using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealTalkEngineEditorLibrary.StorySystem.Attributes
{
    public class NodeAttribute : Attribute
    {
        #region Properties and Fields

        public string MenuName { get; }

        #endregion

        public NodeAttribute(string menuName)
        {
            MenuName = menuName;
        }
    }
}
