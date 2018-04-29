using Alexa.NET.Response.Ssml;
using BindingsKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealTalkEngine.StorySystem.Nodes
{
    public class SpeechNode : BaseNode
    {
        #region Properties and Fields

        /// <summary>
        /// The dialog that will be spoken for this node.
        /// </summary>
        [Serialize]
        public Speech Speech { get; set; } = new Speech();

        #endregion
    }
}