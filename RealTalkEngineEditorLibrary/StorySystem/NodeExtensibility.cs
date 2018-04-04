using CelesteEngineEditor.Extensibility;
using NodeNetwork.ViewModels;
using RealTalkEngineEditorLibrary.StorySystem.Attributes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace RealTalkEngineEditorLibrary.StorySystem
{
    public static class NodeExtensibility
    {
        public static List<Type> LoadNodes()
        {
            List<Type> nodes = new List<Type>();

            Type nodeViewModel = typeof(NodeViewModel);
            foreach (FileInfo file in ExtensibilityUtils.AssemblyFiles)
            {
                Assembly assembly = Assembly.LoadFile(file.FullName);
                foreach (Type type in assembly.GetTypes())
                {
                    NodeAttribute nodeAttribute = type.GetCustomAttribute<NodeAttribute>();
                    if (nodeAttribute != null &&
                        nodeViewModel.IsAssignableFrom(type) &&
                        !type.IsAbstract)
                    {
                        nodes.Add(type);
                    }
                }
            }

            return nodes;
        }
    }
}
