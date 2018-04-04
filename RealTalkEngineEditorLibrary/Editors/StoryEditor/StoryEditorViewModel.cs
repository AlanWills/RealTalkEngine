using BindingsKernel;
using CelesteEngineEditor.Editors;
using NodeNetwork.ViewModels;
using RealTalkEngineEditorLibrary.StorySystem.Interfaces;
using System;
using System.Windows;

namespace RealTalkEngineEditorLibrary.Editors
{
    public class StoryEditorViewModel : EditorViewModel
    {
        #region Properties and Fields

        /// <summary>
        /// The network GUI on display within the story editor.
        /// </summary>
        public NetworkViewModel Network { get; } = new NetworkViewModel();

        #endregion

        #region Node Creation Functions

        /// <summary>
        /// Create a node of the inputted type and add it to the graph.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public NodeViewModel CreateNode(Type type, string name, Point position)
        {
            if (!typeof(NodeViewModel).IsAssignableFrom(type) || 
                !typeof(INodeViewModel).IsAssignableFrom(type) || 
                type.IsAbstract)
            {
                CelDebug.Fail();
                return null;
            }

            NodeViewModel node = Activator.CreateInstance(type) as NodeViewModel;
            (node as INodeViewModel).Name = name;
            node.Position = position;
            node.Changed.Subscribe(e =>
            {
                if (e.PropertyName == nameof(node.IsSelected))
                {
                    Editor.OpenEditorForObject(node);
                }
            });
            Network.Nodes.Add(node);

            return node;
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
    }
}
