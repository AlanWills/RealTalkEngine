using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Twinary.Deserialization
{
    public static class JsonDeserializationExtensions
    {
        /// <summary>
        /// Got this function from this answer.
        /// Don't really know what it does, but don't really care.
        /// https://stackoverflow.com/questions/41510242/custom-deserializer-only-for-some-fields-with-json-net
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public static JToken RemoveFromLowestPossibleParent(this JToken node)
        {
            if (node == null)
            {
                return null;
            }

            JToken contained = node.AncestorsAndSelf().Where(t => t.Parent is JContainer && t.Parent.Type != JTokenType.Property).FirstOrDefault();
            if (contained != null)
            {
                contained.Remove();
            }

            // Also detach the node from its immediate containing property -- Remove() does not do this even though it seems like it should
            if (node.Parent is JProperty)
            {
                ((JProperty)node.Parent).Value = null;
            }

            return node;
        }
    }
}