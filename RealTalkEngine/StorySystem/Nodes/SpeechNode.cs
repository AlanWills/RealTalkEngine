using Alexa.NET.Response.Ssml;
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
        /// The SSML elements that will be used for this node.
        /// </summary>
        public Speech Speech { get; set; } = new Speech();

        #endregion
    }
}
