/*
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GazeToolBar
{
    public class GazeToolbar
    {
        //The Action handler handles the sidebar button clicks
        private ActionHandler actionHandler;

        //Keyboard hook listens for key presses
        private KeyboardHook keyboardHook;

        //Shortcut key worker handles the pressing of any shortcut keys
        private ShortcutKeyWorker shortcutKeyWorker;

        //The icon the the system tray
        private TrayMenu trayMenu;

        //The statemanager controls most of the program
        private StateManager_new manager;

        public GazeToolbar()
        {
            actionHandler = new ActionHandler();
            keyboardHook = new KeyboardHook();
            //manager = new StateManager_new();

            //TODO: move FKeyMapDictionary to  ShortcutKeyWorker class and see if eyeXHost is needed
            //shortcutKeyWorker = new ShortcutKeyWorker(keyboardHook, FKeyMapDictionary, eyeXHost);

            //Start monitoring key presses.
            keyboardHook.HookKeyboard();

            //The menu that appears in the system tray
            trayMenu = new TrayMenu();
            trayMenu.menuExit.Click += new EventHandler(MenuCloseClick);
        }

        public void MenuCloseClick(Object s, EventArgs e)
        {
            Close();
        }

        public void Close()
        {
            Application.Exit();
        }
    }
}
*/