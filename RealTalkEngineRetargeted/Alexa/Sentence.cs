using BindingsKernel.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace RealTalkEngine.Alexa
{
    public class Sentence : ISsml, ICustomSerialization, ICustomDeserialization
    {
        #region Properties and Fields

        /// <summary>
        /// The text that will be spoken as part of this sentence.
        /// </summary>
        public string Text { get; set; }

        #endregion

        public Sentence()
        {
        }

        public Sentence(string text)
        {
            Text = text;
        }

        #region Custom Serialization and Deserialization

        public void Serialize(string name, XmlWriter writer)
        {
            writer.WriteAttributeString("text", Text);
        }

        public void Deserialize(string name, XmlReader reader)
        {
            Text = reader.GetAttribute(name);
        }

        #endregion
    }
}
