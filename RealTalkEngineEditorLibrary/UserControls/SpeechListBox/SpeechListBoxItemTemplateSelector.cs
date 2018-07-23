using Alexa.NET.Response.Ssml;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace RealTalkEngine.UserControls
{
    public class SpeechListBoxItemTemplateSelector : DataTemplateSelector
    {
        #region Properties and Fields

        private static List<SpeechDataTemplate> templates;
        /// <summary>
        /// All of the available speech data templates that will allow us to customise the UI based on the type of the propery.
        /// </summary>
        private static List<SpeechDataTemplate> Templates
        {
            get
            {
                if (templates == null)
                {
                    LoadSpeechDataTemplates(Application.Current.Resources, true);
                }

                return templates;
            }
        }

        /// <summary>
        /// The fallback data template to use if we cannot find a matching custom template.
        /// </summary>
        public DataTemplate FallbackDataTemplate { get; set; }

        #endregion

        #region Initialization Functions

        /// <summary>
        /// Attempt to find all speech data templates in the inputted dictionary's resources and merged dictionaries.
        /// This is a recursive function, so all property data templates will loaded from all sub dictionaries.
        /// The second flag determines whether we clear our existing cache of Templates.  Usually when you call this you want this to be true.
        /// </summary>
        /// <param name="dictionary"></param>
        public static void LoadSpeechDataTemplates(ResourceDictionary dictionary, bool clearExisting = true)
        {
            templates = templates ?? new List<SpeechDataTemplate>();

            if (clearExisting)
            {
                templates.Clear();
            }

            foreach (DictionaryEntry entry in dictionary)
            {
                if (entry.Value is SpeechDataTemplate)
                {
                    templates.Add(entry.Value as SpeechDataTemplate);
                }
            }

            foreach (ResourceDictionary childDictionary in dictionary.MergedDictionaries)
            {
                // Don't clear our existing templates for recursive calls
                LoadSpeechDataTemplates(childDictionary, false);
            }

        }
        #endregion

        #region Overrides

        /// <summary>
        /// Attempt to find a corresponding matching speech data template for the inputted ssml element.
        /// If not, we use the fallback data template.
        /// </summary>
        /// <param name="item"></param>
        /// <param name="container"></param>
        /// <returns></returns>
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (item is ISsml)
            {
                ISsml speech = item as ISsml;
                if (Templates.Exists(x => speech.GetType() == (x.DataType as Type)))
                {
                    return Templates.Find(x => speech.GetType() == (x.DataType as Type));
                }

                return FallbackDataTemplate ?? base.SelectTemplate(item, container);
            }
            else
            {
                return base.SelectTemplate(item, container);
            }
        }

        #endregion
    }
}
