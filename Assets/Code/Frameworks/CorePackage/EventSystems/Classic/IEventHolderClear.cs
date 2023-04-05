﻿namespace CorePackage.EventSystems.Classic
{
    /// <summary>
    /// Used to call EventHolder.ClearListeners() without defining a generic type
    /// </summary>
    internal interface IEventContainerClear
    {
        void UnsubscribeAll();
    }
}
