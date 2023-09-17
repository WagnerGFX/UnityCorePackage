using System;
using UnityEngine;

namespace CorePackage.StateMachine
{
    [AttributeUsage(AttributeTargets.Field)]
    public class InitOnlyAttribute : PropertyAttribute { }
}
