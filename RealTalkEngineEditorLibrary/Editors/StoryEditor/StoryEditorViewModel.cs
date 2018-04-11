using BindingsKernel;
using CelesteEngineEditor.Editors;
using NodeNetwork.ViewModels;
using RealTalkEngine.StorySystem;
using RealTalkEngine.StorySystem.Nodes;
using RealTalkEngineEditorLibrary.StorySystem;
using RealTalkEngineEditorLibrary.StorySystem.Interfaces;
using RealTalkEngineEditorLibrary.StorySystem.NodeViewModels;
using System;
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
        /// The network GUI on display within the story editor.
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

            Story story = TargetObject as Story;
            foreach (BaseNode node in story.Nodes)
            {
                CreateNodeViewModel(node);
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
        public NodeViewModel CreateNode(Type type, string name, Point position)
        {
            if (!typeof(BaseNode).IsAssignableFrom(type) || type.IsAbstract)
            {
                CelDebug.Fail();
                return null;
            }

            BaseNode node = Story.CreateNode(type, name);
            NodeViewModel nodeViewModel = CreateNodeViewModel(node);
            nodeViewModel.Position = position;
            
            return nodeViewModel;
        }

        /// <summary>
        /// Create a node of the inputted type and add it to the graph.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name"></param>
        /// <returns></returns>
        public T CreateNode<T>(string name, Point position) where T : NodeViewModel, INodeViewModel
        {
            return CreateNode(typeof(T), name, position) as T;
        }

        #endregion

        #region View Model Utility Functions

        /// <summary>
        /// Creates and adds a view model for the inputted node and hooks up all relevant UI callbacks.
        /// </summary>
        /// <param name="node"></param>
        private NodeViewModel CreateNodeViewModel(BaseNode node)
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
