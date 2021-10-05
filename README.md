<img src="https://github.com/cronoxyd/ShortcutFloat/blob/master/Doc/Images/ShortcutFloatIcon.png?raw=true" align="right" style="width: 128px; height: 128px;">

# Shortcut Float
## About
Shortcut Float is a utility program that displays a pinned (TopMost) window over applications that contains buttons which trigger user-defined shortcuts (such as `Ctrl` + `C` and `Ctrl` + `V`). The aim is to make working with traditional Windows apps more accessible for touchscreen or stylus users.

## Installing Shortcut Float
### Requirements
* Windows 10 (although this is a soft requirement, Shortcut Float will probably still work on older editions of Windows but that is untested)
* .NET 5+ Runtime (the installer should handle this though)

### Installation
Download the latest release from the _Releases_ page, extract the `*.zip` and run `setup.exe`.

## Usage
### Getting started
When you first start Shortcut Float, all that will happen is that the icon of the application appears in the info tray. You will then have to create either a default configuration (which will be displayed regardless of which application is currently active) or an window title or process name specific configuration.

To open the settings, double-click the icon in the info tray or right-click the icon and then select _Settings_.

### Creating and managing configurations
Either click the _Edit default configuration_ button in the _General_ section or click _New_ in in the _Configurations_ section.

#### Defining the target for a configuration (not applicable for the default configuration)
Configurations target windows with specific titles and/or processes with specific names. You have to specify at least one to make the configuration work.

Both fields support regular expressions and the expression has to be valid in order to save the configuration.

> [!IMPORTANT]
> Do not include the extension of the process as seen in the Task Manager (mostly `*.exe`).

Here are some examples:

<ol type="a">
    <li>        
        <p>
            <b>Targeting Visual Studio Code</b><br>
            Window title: <code>Visual Studio Code$</code><br>
            Process name: <span aria-hidden="true" style="color: gray;">(empty)</span>
        </p>
    </li>
    <li>        
        <p>
            <b>Targeting all apps of the Affinity range</b><br>
            Window title: <code>^Affinity (Photo|Designer|Publisher)</code><br>
            Process name: <span aria-hidden="true" style="color: gray;">(empty)</span>
        </p>
    </li>
    <li>        
        <p>
            <b>Targeting Windows Explorer</b><br>
            Window title: <span aria-hidden="true" style="color: gray;">(empty)</span><br>
            Process name: <code>^explorer$</code>
        </p>
    </li>
</ol>

#### Configuring the window positioning
The options for the positioning of the float window in the settings window are the global settings and will be used if a specific configuration's settings are not set (= empty dropdown or checkbox with square).

By default, the float window will simply appear and not react to any changes of the targeted window (position or size).

You can enable the _Sticky float window_ option so that the window will always follow the position of the targeted window. The positioning of the float window can further be configured to be either:
* Absolute, which means that the float window will always maintain a fixed offset from the top-left corner of the targeted window or
* Relative, which means that the float window will always maintain a fixed offset from the center of the targeted window (and therefore also react to changes in the size).

#### Defining shortcuts
The shortcut definitions will then actually show up in the float window with the name you configured. Use the actions in the _Shortcut definitions_ sections to add, edit, remove and move the definitions. A definition can have multiple actions, such as:
* A Keystroke action which consists of a modifier key (`Ctrl`, `Alt`, `Shift` or `Win`) and a action key (all other keys), both optional,
* A Textblock action which simply sends all characters to the targeted window.
The Name you can assign a shortcut definition will be the text of the button in the float window. The name of each action is purely for your reference and will not be used otherwise.

### Day-to-day
* Once you've defined either a custom or the default configuration you can start using Shortcut Float. Simply switch to an application for which you have created a configuration and watch the float window appear. You can then move the float window to whatever position you like and depending on your configuration the window will either stay exactly there or follow the window if you move or resize it.
* There is also logic to prevent the float window from going off-screen should you move a window in such a way that it would be off-screen.
* For now, there is no _Start with Windows_ option so you're gonna have to place a shortcut in the _Autostart_ folder or place an appropriate entry in the Registry.

## Roadmap / open item list
* Implement a proper light/dark theme
* Implement (optional) icons instead of text for the buttons
* Improve UX regarding user input validation
* Concept more window positioning behaviours (maybe one that snaps to the window's edge closest to the cursor)
* Investigate deeper interop (such as reacting to a targeted window being in a specific state like the user having selected a brush tool in a image editor)
* Investigate the feasibility of re-implementing the UI in UWP
* Implement more options for showing the float window (such as only being "armed" when the computer is in tablet mode or only showing the window when a button is held on the stylus)