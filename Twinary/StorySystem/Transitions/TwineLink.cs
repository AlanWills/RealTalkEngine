using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using Twinary.StorySystem.Nodes;

namespace Twinary.StorySystem.Transitions
{
    [Serializable]
    public class TwineLink
    {
        #region Serialized Properties

        /// <summary>
        /// The display name of this transition.
        /// </summary>
        [JsonProperty(PropertyName = "name", Required = Required.Always), DataMember(Name = "name", IsRequired = true)]
        public string Name { get; private set; } = "";

        /// <summary>
        /// The link which will be of the form [Link Text in Source|Destination Node Name]
        /// </summary>
        [JsonProperty(Required = Required.Always), DataMember(IsRequired = true)]
        private string Link { get; set; } = "";

        #endregion
        
        #region Properties

        /// <summary>
        /// The link text for this transition.
        /// </summary>
        public string LinkText { get { return (Link.IndexOf('|') >= 0) ? Link.Split('|')[0] : ""; } }

        /// <summary>
        /// The display name of the destination node for this transition.
        /// </summary>
        public string DestinationName { get { return (Link.IndexOf('|') >= 0) ? Link.Split('|')[1] : ""; } }

        #endregion

        #region Constructors

        public TwineLink()
        {
        }

        public TwineLink(string name, string link)
        {
            Name = name;
            Link = link;
        }

        #endregion
    }
}
