using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DebugControl : MonoBehaviour
{
    [SerializeField] 
    private bool showConsole = false;

    private bool showHelp = false;

    private string input;

    public static Dictionary<string, object> commandDict = new Dictionary<string, object>();

    private void Awake()
    {
        #region General
        AddCommand("help", new DebugCommand
            ("Show all available commands", "help", () =>
            {
                showHelp = true;
            }));

        AddCommand("sgtm", new DebugCommand<float>
            ("Set the game time scale to X.", "sgtm <scale>", (x) =>
            {
                Time.timeScale = x;
            }));
        #endregion
    }

    public static void AddCommand(string commandName, object newCommand)
    {
        if (commandDict.ContainsKey(commandName))
        {
            Debug.Log("Cannot add command " + commandName + ". It already exist.");
        } else
        {
            commandDict.Add(commandName, newCommand);
        }
    }

    #region Toggling Debug
    public void OnToggleDebug() 
    {
        showConsole = !showConsole;
        
        if (showConsole)
        {
            showHelp = false;
            input = "";
        }     
    }

    public void OnSubmit()
    {
        if (showConsole)
        {
            if (HandleInput())
            {
                input = "";
            } else
            {
                Debug.Log(input + " is an unknown command");
            }      
        }
    }
    #endregion

    private bool HandleInput()
    {
        string[] properties = input.Split(' ');

        object command;
        if (commandDict.TryGetValue(properties[0], out command))
        {
            if (command as DebugCommand != null)
            {
                (command as DebugCommand).Invoke();

            } else if (command as DebugCommand<float> != null)
            {
                (command as DebugCommand<float>).Invoke(float.Parse(properties[1]));
            }
            return true;
        }

        return false;
    }

    #region GUI
    Vector2 scroll;

    private void OnGUI()
    {
        if (!showConsole) return;

        float y = 0.0f + Screen.height * 0.1f;

        if (showHelp)
        {
            GUI.Box(new Rect(0, y, Screen.width, 100), "");
            Rect viewport = new Rect(0, 0, Screen.width - 30, 20 * commandDict.Count);
            scroll = GUI.BeginScrollView(new Rect(0, y + 5f, Screen.width, 90), scroll, viewport);

            /*for (int i = 0; i < commandDict.Count; i++)
            {
                commandDict.el
                DebugCommandBase command = commandDict.ElementAt. as DebugCommandBase;
                string label = $"{command.commandFormat} - {command.commandDescription}";
                Rect labelRect = new Rect(5, 20 * i, viewport.width - 100, 20);
                GUI.Label(labelRect, label);
            }*/

            int commandCount = 0;
            foreach(var item in commandDict)
            {
                DebugCommandBase command = item.Value as DebugCommandBase;
                string label = $"{command.commandFormat} - {command.commandDescription}";
                Rect labelRect = new Rect(5, 20 * commandCount, viewport.width - 100, 20);
                GUI.Label(labelRect, label);

                commandCount++;
            }

            GUI.EndScrollView();
            y += 100;
        }


        GUI.Box(new Rect(0, y, Screen.width, 30), "");
        GUI.backgroundColor = new Color(0, 0, 0, 0);

        GUI.SetNextControlName("InputTextField");
        input = GUI.TextField(new Rect(10.0f, y + 5.0f, Screen.width - 20.0f, 20.0f), input);
        GUI.FocusControl("InputTextField");
    }
    #endregion
}
