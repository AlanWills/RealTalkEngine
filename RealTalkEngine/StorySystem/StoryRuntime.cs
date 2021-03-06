﻿using Alexa.NET;
using Alexa.NET.Request;
using Alexa.NET.Response;
using Alexa.NET.Response.Ssml;
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
        /// The key we use to obtain the name of the current node.
        /// </summary>
        public const string CurrentNodeKey = "CurrentNode";

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

        public StoryRuntime(RequestContext requestContext, Story story)
        {
            RequestContext = requestContext;
            Story = story;
        }

        public StoryRuntime(RequestContext requestContext, string storyFilePath)
        {
            RequestContext = requestContext;
            Story = Story.Load(storyFilePath);
        }

        #region Current Node Functions

        /// <summary>
        /// Attempts to set the current node to the node with the inputted NodeIndex in the story.
        /// If no such node exists, this function will not change the value of the current node.
        /// </summary>
        /// <param name="nodeName"></param>
        public void TrySetCurrentNode(int nodeIndex)
        {
            CurrentNode = Story.FindNode(nodeIndex) ?? CurrentNode;
        }

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

        #region Runtime Progression Functions

        /// <summary>
        /// Obtain the text from current node in the story.
        /// Then create a response using the current node and return it.
        /// Finally, transition to the next node in the story.
        /// </summary>
        /// <returns></returns>
        public SkillResponse ProcessRequest()
        {
            Speech speech = new Speech();
            speech.Elements.Add(new Sentence(CurrentNode.Text));

            SkillResponse response = ResponseBuilder.Tell(speech);
            Dictionary<string, object> sessionAttributes = RequestContext.Session.Attributes ?? new Dictionary<string, object>();

            SpeechNode nextNode = CurrentNode.GetNextNode();
            string nextNodeName = nextNode != null ? nextNode.Name : "";
            response.Response.ShouldEndSession = nextNode == null;

            if (!sessionAttributes.ContainsKey(CurrentNodeKey))
            {
                // Add the name of the next node to update our progression through the story
                sessionAttributes.Add(CurrentNodeKey, nextNodeName);
            }
            else
            {
                // Update the name of the next node to update our progression through the story
                sessionAttributes[CurrentNodeKey] = nextNodeName;
            }

            response.SessionAttributes = sessionAttributes;

            return response;
        }

        #endregion
    }
}