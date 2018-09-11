using CelesteEngineEditor.AssetLoaders;
using CelesteEngineEditor.Assets;
using RealTalkEngineEditorLibrary.Assets;
using System.IO;
using Twinary.StorySystem;

namespace RealTalkEngineEditorLibrary.AssetLoaders
{
    public class TwineStoryAssetLoader : AssetLoader
    {
        #region Properties and Fields

        /// <summary>
        /// The file extension for compatible twine stories that we can load into the project.
        /// </summary>
        public const string FileExtension = ".twjs";

        /// <summary>
        /// The file extension for compatible twine stories that we can load into the project.
        /// </summary>
        public override string AssetFileExtension { get { return FileExtension; } }

        #endregion

        #region Load Functions

        /// <summary>
        /// Attempts to load the inputted file as a twine story project asset.
        /// Will return null if this process was a failure.
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public override IAsset Load(FileInfo file)
        {
            return null;
            //TwineStory twineStory = TwineStory.Load(file.FullName);
            //return twineStory != null ? new TwineStoryAsset(file, twineStory) : null;
        }

        #endregion
    }
}
