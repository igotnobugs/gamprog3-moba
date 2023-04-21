using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugCommandBase
{
    //private string _commandId;
    private string _commandDescription;
    private string _commandFormat;

    //public string commandId { get { return _commandId; } }
    public string commandDescription { get { return _commandDescription; } }
    public string commandFormat { get { return _commandFormat; } }

    public DebugCommandBase(string description, string format)
    {
        //_commandId = id;
        _commandDescription = description;
        _commandFormat = format;
    }
}

public class DebugCommand : DebugCommandBase
{
    private Action _command;

    public DebugCommand(string description, string format, Action command)
        : base(description, format)
    {
        _command = command;
    }

    public void Invoke()
    {
        _command.Invoke();
    }
}

public class DebugCommand<T1> : DebugCommandBase
{
    private Action<T1> _command;

    public DebugCommand(string description, string format, Action<T1> command) 
        : base (description, format)
    {
        _command = command;
    }

    public void Invoke(T1 value)
    {
        _command.Invoke(value);
    }
}

