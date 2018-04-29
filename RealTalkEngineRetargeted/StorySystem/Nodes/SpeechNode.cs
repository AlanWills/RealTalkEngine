using BindingsKernel;
using RealTalkEngine.Alexa;

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