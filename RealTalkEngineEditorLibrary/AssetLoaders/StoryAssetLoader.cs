using CelesteEngineEditor.AssetLoaders;
using CelesteEngineEditor.Assets;
using RealTalkEngine.StorySystem;
using RealTalkEngineEditorLibrary.Assets;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealTalkEngineEditorLibrary.AssetLoaders
{
    public class StoryAssetLoader : AssetLoader
    {
        #region Properties and Fields

        /// <summary>
        /// The file extension for loading stories for the real talk engine in binary format.
        /// </summary>
        public const string FileExtension = ".bst";

        /// <summary>
        /// The file extension for loading stories for the real talk engine in binary format.
        /// </summary>
        public override string AssetFileExtension { get { return FileExtension; } }

        #endregion

        #region Load Implementation

        /// <summary>
        /// Attempts to load the story from the inputted file.
        /// Will return null if this process was unsuccessful.
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public override IAsset Load(FileInfo file)
        {
            Story story = Story.Load(file.FullName);
            return story != null ? new StoryAsset(file, story) : null;
        }

        #endregion
    }
}
