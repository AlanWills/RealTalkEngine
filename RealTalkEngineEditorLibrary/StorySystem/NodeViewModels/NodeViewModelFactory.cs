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
        private static Dictionary<Type, Func<BaseNode, NodeViewModel>> NodeViewModelLookup { get; } = new Dictionary<Type, Func<BaseNode, NodeViewModel>>();

        #endregion

        /// <summary>
        /// Iterate through all DLLs in same location as the editor and load all non-abstract classes derived from NodeViewModel.
        /// </summary>
        public static void LoadNodeViewModels()
        {
            NodeViewModelsImpl.Clear();
            NodeViewModelLookup.Clear();

            // Go through each DLL
            foreach (FileInfo assembly in ExtensibilityUtils.AssemblyFiles)
            {
                // Load the assembly
                Assembly loadedAssembly = Assembly.LoadFile(assembly.FullName);
                NodeViewModelsImpl.AddRange(loadedAssembly.GetTypes().Where(x => !x.IsAbstract && x.IsSubclassOf(typeof(NodeViewModel))));
            }

            foreach (Type viewModel in NodeViewModelsImpl)
            {
                NodeViewModelAttribute viewModelAttribute = viewModel.GetCustomAttribute<NodeViewModelAttribute>();
                if (!NodeViewModelLookup.ContainsKey(viewModelAttribute.NodeType))
                {
                    NodeViewModelLookup.Add(viewModelAttribute.NodeType, (BaseNode baseNode) =>
                    {
                        return Activator.CreateInstance(viewModel, baseNode) as NodeViewModel;
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
        public static NodeViewModel CreateViewModel(BaseNode node)
        {
            if (NodeViewModelLookup.ContainsKey(node.GetType()))
            {
                return NodeViewModelLookup[node.GetType()](node) as NodeViewModel;
            }

            CelDebug.Fail("No node view model for inputted node type " + node.Name);
            return null;
        }

        /// <summary>
        /// Use the inputted node type name to create a view model from all the registered node view models.
        /// Returns null if no view model exists for the inputted node type name.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="node"></param>
        /// <returns></returns>
        public static NodeViewModel CreateViewModel(string nodeTypeName)
        {
            BaseNode node = NodeFactory.CreateNode(nodeTypeName);
            return node != null ? CreateViewModel(node) : null;
        }

        /// <summary>
        /// Use the inputted node to create a view model from all the registered node view models.
        /// Returns null if no view model exists for the inputted node type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="node"></param>
        /// <returns></returns>
        public static T CreateViewModel<T>(BaseNode node) where T : NodeViewModel, INodeViewModel
        {
            return CreateViewModel(node) as T;
        }

        /// <summary>
        /// Use the inputted node type name to create a view model from all the registered node view models.
        /// Returns null if no view model exists for the inputted node type name.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="node"></param>
        /// <returns></returns>
        public static T CreateViewModel<T>(string nodeTypeName) where T : NodeViewModel, INodeViewModel
        {
            return CreateViewModel(nodeTypeName) as T;
        }

        /// <summary>
        /// Use the inputted node generic type to create a view model from all the registered node view models.
        /// Returns null if no view model exists for the inputted node type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="node"></param>
        /// <returns></returns>
        public static T CreateViewModel<T>() where T : BaseNode, new()
        {
            T node = new T();
            return CreateViewModel(node) as T;
        }

        #endregion
    }
}
