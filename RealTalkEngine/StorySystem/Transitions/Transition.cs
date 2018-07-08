using RealTalkEngine.StorySystem.Conditions;
using RealTalkEngine.StorySystem.Nodes;
using System;
using System.Collections.Generic;

namespace RealTalkEngine.StorySystem.Transitions
{
    [Serializable]
    public class Transition
    {
        #region Properties and Fields

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
            Conditions.Add(transitionCondition);
            return transitionCondition;
        }

        /// <summary>
        /// Creates a condition of the inputted type and adds the created condition to this transition.
        /// </summary>
        /// <param name="transitionCondition"></param>
        /// <returns></returns>
        public T CreateAndCondition<T>() where T : TransitionCondition, new()
        {
            T condition = new T();
            condition.Transition = this;

            return AddCondition(condition) as T;
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
    }
}