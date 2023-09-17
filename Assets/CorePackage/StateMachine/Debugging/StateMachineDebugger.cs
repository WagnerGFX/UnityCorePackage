#if UNITY_EDITOR

using System;
using System.Text;
using UnityEngine;

namespace CorePackage.StateMachine.Debugging
{
    /// <summary>
    /// Class specialized in debugging the state transitions, should only be used while in editor mode.
    /// </summary>
    [Serializable]
    internal class StateMachineDebugger
    {
        [SerializeField]
        [Tooltip("Issues a debug log when a state transition is triggered")]
        internal bool _debugTransitions = false;

        [SerializeField]
        [Tooltip("List all conditions evaluated, the result is read: ConditionName == BooleanResult [PassedTest]")]
        internal bool _appendConditionsInfo = true;

        [SerializeField]
        [Tooltip("List all actions activated by the new State")]
        internal bool _appendActionsInfo = true;

        [SerializeField]
        [Tooltip("The current State name [Readonly]")]
        internal string _currentState;

        private StateMachine _stateMachine;
        private StringBuilder _logBuilder;
        private string _targetState = string.Empty;

        private const string CHECK_MARK = "\u2714";
        private const string UNCHECK_MARK = "\u2718";
        private const string THICK_ARROW = "\u279C";
        private const string SHARP_ARROW = "\u27A4";

        /// <summary>
        /// Must be called together with <c>StateMachine.Awake()</c>
        /// </summary>
        internal void Awake(StateMachine stateMachine)
        {
            _stateMachine = stateMachine;
            _logBuilder = new StringBuilder();

            _currentState = stateMachine._currentState._originSO.name;
        }

        internal void TransitionEvaluationBegin(string targetState)
        {
            _targetState = targetState;

            if (!_debugTransitions)
            { return; }

            _logBuilder
                .Clear()
                .Append(_stateMachine.name)
                .AppendLine(" state changed")
                .Append(_currentState)
                .Append("  ")
                .Append(SHARP_ARROW)
                .Append("  ")
                .AppendLine(_targetState);

            if (_appendConditionsInfo)
            {
                _logBuilder
                    .AppendLine()
                    .AppendLine("Transition Conditions:");
            }
        }

        internal void TransitionConditionResult(string conditionName, bool result, bool isMet)
        {
            if (!_debugTransitions || _logBuilder.Length == 0 || !_appendConditionsInfo)
            { return; }

            _logBuilder
                .Append("    ")
                .Append(THICK_ARROW)
                .Append(' ')
                .Append(conditionName)
                .Append(" == ")
                .Append(result)
                .Append(" [")
                .Append(isMet ? CHECK_MARK : UNCHECK_MARK)
                .AppendLine("]");
        }

        internal void TransitionEvaluationEnd(bool passed, StateAction[] actions)
        {
            if (passed)
            { _currentState = _targetState; }

            if (!_debugTransitions || _logBuilder.Length == 0)
            { return; }

            if (passed)
            {
                LogActions(actions);
                PrintDebugLog();
            }

            _logBuilder.Clear();
        }

        private void LogActions(StateAction[] actions)
        {
            if (!_appendActionsInfo)
            { return; }

            _logBuilder
                .AppendLine()
                .AppendLine("State Actions:");

            foreach (StateAction action in actions)
            {
                _logBuilder
                    .Append("    ")
                    .Append(THICK_ARROW)
                    .Append(' ')
                    .AppendLine(action._originSO.name);
            }
        }

        private void PrintDebugLog()
        {
            _logBuilder
                .AppendLine()
                .Append("--------------------------------");

            Debug.Log(_logBuilder.ToString());
        }
    }
}

#endif
