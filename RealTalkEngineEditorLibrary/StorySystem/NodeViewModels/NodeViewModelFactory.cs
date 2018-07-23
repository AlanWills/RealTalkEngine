using BindingsKernel;
using CelesteEngineEditor.Extensibility;
using NodeNetwork.ViewModels;
using RealTalkEngine.StorySystem.Nodes;
using RealTalkEngineEditorLibrary.StorySystem.Attributes;
using RealTalkEngineEditorLibrary.StorySystem.Interfaces;
using RealTalkEngineEditorLibrary.StorySystem.NodeViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;

namespace RealTalkEngineEditorLibrary.StorySystem.NodeViewModels
{
    public static class NodeViewModelFactory
    {
        #region Properties and Fields

        private static List<Type> NodeViewModelsImpl { get; set; } = new List<Type>();

        private static ReadOnlyCollection<Type> nodeViewModels;
        /// <summary>
        /// All of the available node view models loaded into the application.
        /// Call LoadNodeViewModels to refresh this collection.
        /// </summary>
        public static ReadOnlyCollection<Type> NodeViewModels
        {
            get
            {
                if (nodeViewModels == null)
                {
                    nodeViewModels = new ReadOnlyCollection<Type>(NodeViewModelsImpl);
                    LoadNodeViewModels();
                }

                return nodeViewModels;
            }
        }

        /// <summary>
        /// A dictionary of node types with a factory function for creating them.
        /// </summary>
        private static Dictionary<Type, Func<SpeechNode, NodeViewModel>> NodeViewModelLookup { get; } = new Dictionary<Type, Func<SpeechNode, NodeViewModel>>();

        #endregion

        /// <summary>
        /// Iterate through all DLLs in same location as the editor and load all non-abstract classes derived from NodeViewModel.
        /// </summary>
        public static void LoadNodeViewModels()
        {
            NodeViewModelsImpl.Clear();
            NodeViewModelLookup.Clear();

            // Go through each custom loaded type
            foreach (Type type in ExtensibilityUtils.Types)
            {
                if (!type.IsAbstract && type.IsSubclassOf(typeof(NodeViewModel)))
                {
                    NodeViewModelsImpl.Add(type);
                }
            }

            foreach (Type viewModel in NodeViewModelsImpl)
            {
                NodeViewModelAttribute viewModelAttribute = viewModel.GetCustomAttribute<NodeViewModelAttribute>();
                if (!NodeViewModelLookup.ContainsKey(viewModelAttribute.NodeType))
                {
                    NodeViewModelLookup.Add(viewModelAttribute.NodeType, (SpeechNode node) =>
                    {
                        return Activator.CreateInstance(viewModel, node) as NodeViewModel;
                    });
                }
                else
                {
                    CelDebug.Fail("Duplicate view model type for node type " + viewModelAttribute.NodeType.Name);
                    continue;
                }
            }
        }

        #region Factory Functions

        /// <summary>
        /// Use the inputted node to create a view model from all the registered node view models.
        /// Returns null if no view model exists for the inputted node type.
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public static NodeViewModel CreateViewModel(SpeechNode node)
        {
            if (NodeViewModelLookup.ContainsKey(node.GetType()))
            {
                return NodeViewModelLookup[node.GetType()](node) as NodeViewModel;
            }

            CelDebug.Fail("No node view model for inputted node type " + node.Name);
            return null;
        }

        /// <summary>
        /// Create a node and a wrapper node view model.
        /// Returns null if no view model exists for the inputted node type name.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="node"></param>
        /// <returns></returns>
        public static NodeViewModel CreateViewModel()
        {
            SpeechNode node = new SpeechNode();
            return node != null ? CreateViewModel(node) : null;
        }

        /// <summary>
        /// Use the inputted node to create a view model from all the registered node view models.
        /// Returns null if no view model exists for the inputted node type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="node"></param>
        /// <returns></returns>
        public static T CreateViewModel<T>(SpeechNode node) where T : NodeViewModel, INodeViewModel
        {
            return CreateViewModel(node) as T;
        }
        
        #endregion
    }
}