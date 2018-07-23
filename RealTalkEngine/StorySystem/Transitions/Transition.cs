using RealTalkEngine.StorySystem.Conditions;
using RealTalkEngine.StorySystem.Nodes;
using System;
using System.Collections;
using System.Collections.Generic;

namespace RealTalkEngine.StorySystem.Transitions
{
    [Serializable]
    public class Transition : IEnumerable<TransitionCondition>
    {
        #region Serialized Properties

        /// <summary>
        /// The source node that this transition is connected from.
        /// </summary>
        public SpeechNode Source { get; private set; }

        /// <summary>
        /// The destination node that this transition is connected to.
        /// </summary>
        public SpeechNode Destination { get; private set; }

        /// <summary>
        /// All of the conditions that must be satisfied for the transition to be valid at runtime.
        /// </summary>
        private List<TransitionCondition> Conditions { get; set; } = new List<TransitionCondition>();

        #endregion

        #region Properties and Fields

        /// <summary>
        /// Returns the number of conditions attached to this transition.
        /// </summary>
        public int ConditionCount { get { return Conditions.Count; } }

        #endregion

        public Transition(SpeechNode source, SpeechNode destination)
        {
            Source = source;
            Destination = destination;
        }

        #region Condition Functions

        /// <summary>
        /// Adds the inputted condition to this transition.
        /// </summary>
        /// <param name="transitionCondition"></param>
        /// <returns></returns>
        public TransitionCondition AddCondition(TransitionCondition transitionCondition)
        {
            if (transitionCondition != null)
            {
                transitionCondition.Transition = this;
                Conditions.Add(transitionCondition);
            }

            return transitionCondition;
        }

        /// <summary>
        /// Creates a condition of the inputted type and adds the created condition to this transition.
        /// </summary>
        /// <param name="transitionCondition"></param>
        /// <returns></returns>
        public T CreateCondition<T>() where T : TransitionCondition, new()
        {
            return AddCondition(new T()) as T;
        }

        /// <summary>
        /// Returns the condition at the inputted index, or null if the index was invalid.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public TransitionCondition GetConditionAt(uint index)
        {
            return 0 <= index && index < Conditions.Count ? Conditions[(int)index] : null;
        }

        #endregion

        #region Validation Functions

        /// <summary>
        /// Check that each condition on this transition passes.
        /// </summary>
        /// <returns></returns>
        public bool ValidateConditions()
        {
            foreach (TransitionCondition condition in Conditions)
            {
                if (!condition.ConditionPasses())
                {
                    return false;
                }
            }

            // All of the conditions have passed so this transition is valid.
            return true;
        }

        #endregion

        #region IEnumerable Implementation

        public IEnumerator<TransitionCondition> GetEnumerator()
        {
            return Conditions.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return Conditions.GetEnumerator();
        }

        #endregion
    }
}