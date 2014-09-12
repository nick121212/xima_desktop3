//Copyright (c) Microsoft Corporation.  All rights reserved.

using System;
using System.Windows;

namespace Microsoft.WindowsAPICodePack.Taskbar
{
    /// <summary>
    /// Event args for the TabbedThumbnailClosing event. The application should set
    /// the Cancel property to true if the thumbnail should not be removed.
    /// </summary>
    public class TabbedThumbnailClosingEventArgs : TabbedThumbnailEventArgs
    {
        /// <summary>
        /// Creates a Event Args for a TabbedThumbnailClosing event.
        /// </summary>
        /// <param name="windowHandle">Window handle for the control/window related to the event</param>
        /// <param name="preview">TabbedThumbnail related to this event</param>
        public TabbedThumbnailClosingEventArgs(IntPtr windowHandle, TabbedThumbnail preview)
            : base(windowHandle, preview)
        {
        }

        /// <summary>
        /// Creates a Event Args for a TabbedThumbnailClosing event.
        /// </summary>
        /// <param name="windowsControl">WPF Control (UIElement) related to the event</param>
        /// <param name="preview">TabbedThumbnail related to this event</param>
        public TabbedThumbnailClosingEventArgs(UIElement windowsControl, TabbedThumbnail preview)
            : base(windowsControl, preview)
        {
        }

        /// <summary>
        /// Gets or sets a value indicating whether the thumbnail should not be removed.
        /// </summary>
        public bool Cancel
        {
            get;
            set;
        }
    }
}
