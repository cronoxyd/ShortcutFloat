using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShortcutFloat.Common.Runtime.Interop
{
    public enum HookType : int
    {
        /// <summary>
        /// Installs a hook procedure that monitors messages before the system sends them to the destination window procedure. For more information, see the <see href="https://docs.microsoft.com/previous-versions/windows/desktop/legacy/ms644975(v=vs.85)">CallWndProc</see> hook procedure.
        /// </summary>
        WH_CALLWNDPROC = 4,

        /// <summary>
        /// Installs a hook procedure that monitors messages after they have been processed by the destination window procedure. For more information, see the <see href="https://docs.microsoft.com/windows/desktop/api/winuser/nc-winuser-hookproc">CallWndRetProc</see> hook procedure.
        /// </summary>
        WH_CALLWNDPROCRET = 12,

        /// <summary>
        /// Installs a hook procedure that receives notifications useful to a CBT application. For more information, see the <see href="https://docs.microsoft.com/previous-versions/windows/desktop/legacy/ms644977(v=vs.85)">CBTProc</see> hook procedure.
        /// </summary>
        WH_CBT = 5,

        /// <summary>
        /// Installs a hook procedure useful for debugging other hook procedures. For more information, see the <see href="https://docs.microsoft.com/previous-versions/windows/desktop/legacy/ms644978(v=vs.85)">DebugProc</see> hook procedure.
        /// </summary>
        WH_DEBUG = 9,

        /// <summary>
        /// Installs a hook procedure that will be called when the application's foreground thread is about to become idle. This hook is useful for performing low priority tasks during idle time. For more information, see the <see href="https://docs.microsoft.com/previous-versions/windows/desktop/legacy/ms644980(v=vs.85)">ForegroundIdleProc</see> hook procedure.
        /// </summary>
        WH_FOREGROUNDIDLE = 11,

        /// <summary>
        /// Installs a hook procedure that monitors messages posted to a message queue. For more information, see the <see href="https://docs.microsoft.com/previous-versions/windows/desktop/legacy/ms644981(v=vs.85)">GetMsgProc</see> hook procedure.
        /// </summary>
        WH_GETMESSAGE = 3,

        /// <summary>
        /// Installs a hook procedure that posts messages previously recorded by a <see href="https://docs.microsoft.com/windows/desktop/winmsg/about-hooks">WH_JOURNALRECORD</see> hook procedure. For more information, see the <see href="https://docs.microsoft.com/previous-versions/windows/desktop/legacy/ms644982(v=vs.85)">JournalPlaybackProc</see> hook procedure.
        /// </summary>
        WH_JOURNALPLAYBACK = 1,

        /// <summary>
        /// Installs a hook procedure that records input messages posted to the system message queue. This hook is useful for recording macros. For more information, see the <see href="https://docs.microsoft.com/previous-versions/windows/desktop/legacy/ms644983(v=vs.85)">JournalRecordProc</see> hook procedure.
        /// </summary>
        WH_JOURNALRECORD = 0,

        /// <summary>
        /// Installs a hook procedure that monitors keystroke messages. For more information, see the <see href="https://docs.microsoft.com/previous-versions/windows/desktop/legacy/ms644984(v=vs.85)">KeyboardProc</see> hook procedure.
        /// </summary>
        WH_KEYBOARD = 2,

        /// <summary>
        /// Installs a hook procedure that monitors low-level keyboard input events. For more information, see the <see href="https://docs.microsoft.com/previous-versions/windows/desktop/legacy/ms644985(v=vs.85)">LowLevelKeyboardProc</see> hook procedure.
        /// </summary>
        WH_KEYBOARD_LL = 13,

        /// <summary>
        /// Installs a hook procedure that monitors mouse messages. For more information, see the <see href="https://docs.microsoft.com/previous-versions/windows/desktop/legacy/ms644988(v=vs.85)">MouseProc</see> hook procedure.
        /// </summary>
        WH_MOUSE = 7,

        /// <summary>
        /// Installs a hook procedure that monitors low-level mouse input events. For more information, see the <see href="https://docs.microsoft.com/previous-versions/windows/desktop/legacy/ms644986(v=vs.85)">LowLevelMouseProc</see> hook procedure.
        /// </summary>
        WH_MOUSE_LL = 14,

        /// <summary>
        /// Installs a hook procedure that monitors messages generated as a result of an input event in a dialog box, message box, menu, or scroll bar. For more information, see the <see href="https://docs.microsoft.com/previous-versions/windows/desktop/legacy/ms644987(v=vs.85)">MessageProc</see> hook procedure.
        /// </summary>
        WH_MSGFILTER = -1,

        /// <summary>
        /// Installs a hook procedure that receives notifications useful to shell applications. For more information, see the <see href="https://docs.microsoft.com/previous-versions/windows/desktop/legacy/ms644991(v=vs.85)">ShellProc</see> hook procedure.
        /// </summary>
        WH_SHELL = 10,

        /// <summary>
        /// Installs a hook procedure that monitors messages generated as a result of an input event in a dialog box, message box, menu, or scroll bar. The hook procedure monitors these messages for all applications in the same desktop as the calling thread. For more information, see the <see href="https://docs.microsoft.com/previous-versions/windows/desktop/legacy/ms644992(v=vs.85)">SysMsgProc</see> hook procedure.
        /// </summary>
        WH_SYSMSGFILTER = 6
    }
}
