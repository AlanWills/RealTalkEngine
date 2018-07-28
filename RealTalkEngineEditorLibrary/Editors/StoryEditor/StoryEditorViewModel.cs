using BindingsKernel;
using CelesteEngineEditor.Editors;
using NodeNetwork.ViewModels;
using RealTalkEngine.StorySystem;
using RealTalkEngine.StorySystem.Nodes;
using RealTalkEngine.StorySystem.Transitions;
using RealTalkEngineEditorLibrary.StorySystem;
using RealTalkEngineEditorLibrary.StorySystem.Interfaces;
using RealTalkEngineEditorLibrary.StorySystem.NodeViewModels;
using System;
using System.Collections.Generic;
using System.Windows;

namespace RealTalkEngineEditorLibrary.Editors
{
    public class StoryEditorViewModel : EditorViewModel
    {
        #region Properties and Fields

        /// <summary>
        /// The story we are currently editing.
        /// </summary>
        public Story Story { get { return TargetObject as Story; } }

        /// <summary>
        /// The node map for this story.
        /// </summary>
        public NetworkViewModel Network { get; } = new NetworkViewModel();

        #endregion

        #region Editor View Model Overrides

        /// <summary>
        /// Create node UIs for all the nodes in the story when we set it for the Story Editor.
        /// </summary>
        protected override void OnTargetObjectChanged()
        {
            base.OnTargetObjectChanged();

            Network.Nodes.Clear();

            Dictionary<SpeechNode, NodeViewModel> nodeLookup = new Dictionary<SpeechNode, NodeViewModel>();

            Story story = TargetObject as Story;
            for (int i = 0; i < story.NodeCount; ++i)
            {
                SpeechNode node = story.GetNodeAt((uint)i);
                NodeViewModel nodeViewModel = CreateNodeViewModel(node);
                nodeLookup.Add(node, nodeViewModel);
            }

            for (int node_index = 0; node_index < story.NodeCount; ++node_index)
            {
                SpeechNode node = story.GetNodeAt((uint)node_index);
                NodeViewModel nodeViewModel = nodeLookup[node];

                foreach (Transition transition in node)
                {
                    Network.Connections.Add(new ConnectionViewModel(Network, nodeLookup[transition.Destination].Inputs[0], nodeViewModel.Outputs[0]));
                }
            }
        }

        #endregion

        #region Node Creation Functions

        /// <summary>
        /// Create a node of the inputted type and add it to the story.
        /// Will then create an appropriate GUI for the node in the story editor.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public NodeViewModel CreateNode(string name, Point position)
        {
            SpeechNode node = Story.CreateNode(name);
            NodeViewModel nodeViewModel = CreateNodeViewModel(node);
            nodeViewModel.Position = position;
            
            return nodeViewModel;
        }
        
        #endregion

        #region View Model Utility Functions

        /// <summary>
        /// Creates and adds a view model for the inputted node and hooks up all relevant UI callbacks.
        /// </summary>
        /// <param name="node"></param>
        private NodeViewModel CreateNodeViewModel(SpeechNode node)
        {
            NodeViewModel nodeViewModel = NodeViewModelFactory.CreateViewModel(node);
            nodeViewModel.Changed.Subscribe(e =>
            {
                if (e.PropertyName == nameof(nodeViewModel.IsSelected))
                {
                    Editor.OpenEditorForObject(node);
                }
            });
            Network.Nodes.Add(nodeViewModel);

            return nodeViewModel;
        }

        #endregion
    }
}
