using Alexa.NET.Request;
using RealTalkEngine.RequestHandling;
using RealTalkEngine.StorySystem.Nodes;
using System;
using System.Collections.Generic;
using System.Text;

namespace RealTalkEngine.StorySystem
{
    public class StoryRuntime
    {
        #region Properties and Fields
        
        /// <summary>
        /// The context of the current request this runtime needs.
        /// </summary>
        public RequestContext RequestContext { get; private set; }

        /// <summary>
        /// The current story this runtime is processing.
        /// </summary>
        public Story Story { get; set; }

        /// <summary>
        /// The current node we are on in the runtime.
        /// </summary>
        public SpeechNode CurrentNode { get; private set; }

        #endregion

        public StoryRuntime(RequestContext requestContext)
        {
            RequestContext = requestContext;
        }

        #region Current Node Functions

        /// <summary>
        /// Attempts to set the current node to the node with the inputted name in the story.
        /// If no such node exists, this function will not change the value of the current node.
        /// </summary>
        /// <param name="nodeName"></param>
        public void TrySetCurrentNode(string nodeName)
        {
            CurrentNode = Story.FindNode(nodeName) ?? CurrentNode;
        }

        #endregion
    }
}