using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Automation;
using System.Windows.Automation.Text;
using System.Windows.Forms;

namespace Windows_Text_Reader
{
    class TextSelectionReader
    {
        //Array of methods to use to try and get the selected text
        private Func<string>[] _selectionmethods;

        /// <summary>
        /// Initializes a new instance of the <see cref="TextSelectionReader"/> class.
        /// </summary>
        public TextSelectionReader()
        {
            _selectionmethods = new Func<string>[] {
                () => this.GetTextViaHtml(),
                () => this.GetTextFromAutomationElement(),
                () => this.GetTextFromWin32Api(),
                () => this.GetTextViaClipboard(),
            };

        }

        private string GetTextViaHtml()
        {
            Process[] plist = Process.GetProcesses();
            foreach (Process p in plist)
            {
                if (p.ProcessName == "notepad")
                {
                    AutomationElement ae = AutomationElement.FromHandle(p.MainWindowHandle);

                    AutomationElement npEdit = ae.FindFirst(TreeScope.Descendants, new PropertyCondition(AutomationElement.ClassNameProperty, "Edit"));

                    TextPattern tp = npEdit.GetCurrentPattern(TextPattern.Pattern) as TextPattern;
                    TextPatternRange[] trs;

                    if (tp.SupportedTextSelection == SupportedTextSelection.None)
                    {
                        return null;
                    }
                    else
                    {
                        trs = tp.GetSelection();
                        String Text = trs[0].GetText(-1);
                        return Text;
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// Tries to get the currently selected text from the active control.
        /// </summary>
        /// <returns>
        ///     Returns the currently selected text from the active control or null when all methods
        ///     of retrieving the currently selected text from the active control failed.
        /// </returns>
        public string TryGetSelectedTextFromActiveControl()
        {
            try
            {
                foreach (var action in _selectionmethods)
                {
                    var result = action.Invoke();
                    if (result != null)
                        return result;
                }
            }
            catch { }
            return null;
        }

        /// <summary>
        /// Tries to get the currently selected text from the active control and applies a filter to it.
        /// </summary>
        /// <typeparam name="T">The type of objects returned in an IEnumerable.</typeparam>
        /// <param name="filter">The filter to apply to the text retrieved before returning the results.</param>
        /// <returns>
        ///     Returns the filtered results from the currently selected text from the active control or an
        ///     empty IEnumerable of T when all methods of retrieving the currently selected text from the
        ///     active control failed.
        /// </returns>
        /// <remarks>
        ///     The result of the first filter that returns at least one result of T will be returned, other
        ///     methods will not be used after this. When a filter returns an empty IEnumerable (even though
        ///     the method did actually retrieve selected text from the active control) the next method
        ///     will be tried.
        /// </remarks>
        public IEnumerable<T> TryGetSelectedTextFromActiveControl<T>(Func<string, IEnumerable<T>> filter)
        {
            try
            {
                foreach (var action in _selectionmethods)
                {
                    var result = filter.Invoke(action.Invoke());
                    if (result.Any())
                        return result;
                }
            }
            catch { }

            return Enumerable.Empty<T>();
        }

        /// <summary>
        /// Uses UIAutomation to try and retrieve selected text from the active control.
        /// </summary>
        /// <returns>
        /// Returns the selected text from the active control or null when UIAutomation
        /// fails to retrieve the text.
        /// </returns>
        private string GetTextFromAutomationElement()
        {
            AutomationElement element = AutomationElement.FocusedElement;
            if (element == null)    // no element
                return null;

            object pattern;

            // the "Text" pattern is supported by some applications (including Notepad)and returns the current selection for example
            if (element.TryGetCurrentPattern(TextPattern.Pattern, out pattern))
                return string.Join(Environment.NewLine, ((TextPattern)pattern).GetSelection().Select(r => r.GetText(-1)));

            // the "Value" pattern is supported by many application
            if (element.TryGetCurrentPattern(ValuePattern.Pattern, out pattern))
                return ((ValuePattern)pattern).Current.Value;

            //Failed :(
            return null;
        }

        /// <summary>
        /// Uses P/Invokes to try and retrieve selected text from the active control.
        /// </summary>
        /// <returns>
        /// Returns the selected text from the active control or null when the API calls
        /// fail to retrieve the text.
        /// </returns>
        private string GetTextFromWin32Api()
        {
            //Get active window's control hWnd
            int activeWinPtr = GetForegroundWindow().ToInt32();
            int activeThreadId = 0;
            int processId;
            activeThreadId = GetWindowThreadProcessId(activeWinPtr, out processId);
            int currentThreadId = GetCurrentThreadId();
            if (activeThreadId != currentThreadId)
                AttachThreadInput(activeThreadId, currentThreadId, true);
            IntPtr activeCtrlId = GetFocus();

            //Get total text length
            int textlength = (int)SendMessage(activeCtrlId, WM_GETTEXTLENGTH, IntPtr.Zero, IntPtr.Zero) + 1;

            //Have any text at all?
            if (textlength > 0)
            {
                //Get selection
                int selstart;
                int selend;
                SendMessage(activeCtrlId, EM_GETSEL, out selstart, out selend);

                StringBuilder sb = new StringBuilder(textlength);
                SendMessage(activeCtrlId, WM_GETTEXT, (IntPtr)textlength, sb);

                //Slice out selection
                string value = sb.ToString();
                sb.Clear();
                if ((value.Length > 0) && (selend - selstart > 0) && (selstart < value.Length) && (selend < value.Length))
                    return value.Substring(selstart, selend - selstart);
            }

            //Failed :(
            return null;
        }

        /// <summary>
        /// Uses the clipboard to try and retrieve selected text from the active control.
        /// </summary>
        /// <returns>
        /// Returns the selected text from the active control or null when the Clipboard method
        /// fails to retrieve the text.
        /// </returns>
        private string GetTextViaClipboard()
        {
            //TODO: Possibly try to "backup" the clipboard? DO read http://stackoverflow.com/a/2579846 first though!

            //CTRL+C needs to be sent from a Single Threaded Apartmentstate thread...
            if (Thread.CurrentThread.GetApartmentState() != ApartmentState.STA)
            {
                var thread = new Thread(() =>
                {
                    SendKeys.SendWait("^c");
                    SendKeys.Flush();
                });
                thread.SetApartmentState(ApartmentState.STA);
                thread.Start();
                thread.Join();
            }
            else
            {
                SendKeys.SendWait("^c");
                SendKeys.Flush();
            }

            //Get clipboard text
            string result = Clipboard.GetText();
            result = string.IsNullOrWhiteSpace(result) ? null : result;
            //If anything was on the clipboard, clear it.
            if (result != null)
                Clipboard.Clear();

            //TODO: Possibly try to "restore" the clipboard?

            return result;
        }

        #region Win32 API methods
        #region SendMessage overloads
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern bool SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, StringBuilder lParam);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr wparam, IntPtr lparam);

        [DllImport("user32.dll")]
        private static extern int SendMessage(IntPtr hWnd, uint Msg, out int wParam, out int lParam);
        #endregion

        private const uint WM_GETTEXTLENGTH = 0x000E;
        private const uint WM_GETTEXT = 0x000D;
        private const uint EM_GETSEL = 0xB0;

        [DllImport("user32.dll", ExactSpelling = true)]
        private static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll", ExactSpelling = true)]
        private static extern IntPtr GetFocus();

        [DllImport("user32.dll", SetLastError = true)]
        private static extern int GetWindowThreadProcessId(int handle, out int processId);

        [DllImport("user32", SetLastError = true, ExactSpelling = true)]
        private static extern int AttachThreadInput(int idAttach, int idAttachTo, bool fAttach);

        [DllImport("kernel32.dll")]
        private static extern int GetCurrentThreadId();
        #endregion
    }
}
