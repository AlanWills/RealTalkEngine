using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace TestTwinary
{
    public class JsonStoryResources
    {
        #region Directories

        /// <summary>
        /// The full file path to the directory containing all the test resources.
        /// </summary>
        public static string ResourcesDirectory
        {
            get { return Path.Combine(Directory.GetCurrentDirectory(), "..", "..", "..", "Resources"); }
        }

        /// <summary>
        /// The full file path to the directory containing all the story resources.
        /// </summary>
        public static string StoriesDirectory
        {
            get { return Path.Combine(ResourcesDirectory, "Stories"); }
        }

        /// <summary>
        /// The full file path to the directory containing all the json story resources.
        /// </summary>
        public static string JsonStoriesDirectory
        {
            get { return Path.Combine(StoriesDirectory, "Json"); }
        }

        #endregion

        #region Files

        /// <summary>
        /// The full file path to the InvalidStory.json file.
        /// </summary>
        public static string InvalidStory
        {
            get { return Path.Combine(JsonStoriesDirectory, "InvalidStory.json"); }
        }

        /// <summary>
        /// The full file path to the EmptyStory.json file.
        /// </summary>
        public static string EmptyStory
        {
            get { return Path.Combine(JsonStoriesDirectory, "EmptyStory.json"); }
        }

        /// <summary>
        /// The full file path to the SingleNodeStory.json file.
        /// </summary>
        public static string SingleNodeStory
        {
            get { return Path.Combine(JsonStoriesDirectory, "SingleNodeStory.json"); }
        }

        /// <summary>
        /// The full file path to the SingleLinkStory.json file.
        /// </summary>
        public static string SingleLinkStory
        {
            get { return Path.Combine(JsonStoriesDirectory, "SingleLinkStory.json"); }
        }

        #endregion
    }
}
