using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CorePackage.EventSystems.Classic
{
    /// <summary>
    /// Use when a sender is required.
    /// </summary>
    public interface IEventArgsWithSender : IEventArgs
    {
        object Sender { get; }
    }
}
