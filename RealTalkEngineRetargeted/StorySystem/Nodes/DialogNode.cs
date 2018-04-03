using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealTalkEngine.StorySystem.Nodes
{
    public class DialogNode : BaseNode
    {
        #region Properties and Fields

        /// <summary>
        /// The dialog that will be spoken for this node.
        /// </summary>
        public string Dialog { get; set; } = "";

        #endregion
    }
}
