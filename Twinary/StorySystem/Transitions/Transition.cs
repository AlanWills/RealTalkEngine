using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Twinary.StorySystem.Transitions
{
    [Serializable]
    public class Transition
    {
        #region Serialized Properties

        /// <summary>
        /// The display name of this transition.
        /// </summary>
        [JsonProperty(PropertyName = "name", Required = Required.Always), DataMember(Name = "name", IsRequired = true)]
        public string Name { get; private set; }

        /// <summary>
        /// The link which will be of the form [Link Text in Source|Destination Node Name]
        /// </summary>
        [JsonProperty(Required = Required.Always), DataMember(IsRequired = true)]
        private string link;

        #endregion
        
        #region Properties

        /// <summary>
        /// The display name of the destination node for this transition.
        /// </summary>
        public string DestinationName { get { return link.Split('|')[1]; } }

        #endregion
    }
}
