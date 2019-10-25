using System;
using System.Collections.Generic;
using UnityEngine;


public class BulbController : MonoBehaviour
{
    public Bulb bulb;

    private ICommand _request;

    public List<ICommand> commands = new List<ICommand>();
    private int _currentCommandNum;

    //Shows what's going on in the command list
    void OnGUI()
    {
        string label = "   start";
        if (_currentCommandNum == 0)
        {
            label = $"> {label}";
        }

        label += "\n";

        for (int i = 0; i < commands.Count; i++)
        {
            if (i == _currentCommandNum - 1) 
                label += $">  {commands[i]}  \n";
            else
                label += $"   {commands[i]}   \n";
        }

        GUI.Label(new Rect(0, 0, 400, 800), label);
        GUI.Label(new Rect(Screen.width/2f - 200, Screen.height/2f + 200, 400, 800), "Space = Turn On/Off - Left Arrow = Undo - Right Arrow = Redo");
    }

    public void Undo()
    {
        if (_currentCommandNum > 0)
        {
            _currentCommandNum--;
            ICommand command = commands[_currentCommandNum];
            command.Undo();
        }
    }

    public void Redo()
    {
        if (_currentCommandNum < commands.Count)
        {
            ICommand command = commands[_currentCommandNum];
            _currentCommandNum++;
            command.Execute();
        }
    }
    

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            var remote = new RemoteControl();
            var turnOn = new TurnOn(bulb);
            var turnOff = new TurnOff(bulb);
            
            bulb.toggle = !bulb.toggle;
            _request = bulb.toggle ? (ICommand)  turnOn : turnOff;
            
            remote.Execute(_request);
            commands.Add(_request);


            _currentCommandNum = commands.Count;
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Undo();
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            Redo();
        }
    }
}

public interface ICommand
{
    void Execute();
    void Undo();
    void Redo();
}

//Command

class TurnOn : ICommand
{
    private Bulb _bulb;

    public TurnOn(Bulb bulb)
    {
        _bulb = bulb;
    }

    public void Execute()
    {
        _bulb.TurnOn();
    }

    public void Undo()
    {
        _bulb.TurnOff();
    }

    public void Redo()
    {
        Execute();
    }
}

//Command

class TurnOff : ICommand
{
    private Bulb _bulb;

    public TurnOff(Bulb bulb)
    {
        _bulb = bulb;
    }

    public void Execute()
    {
        _bulb.TurnOff();
    }

    public void Undo()
    {
        _bulb.TurnOn();
    }

    public void Redo()
    {
        Execute();
    }
}

// Invoker
public class RemoteControl
{
    public void Execute(ICommand command)
    {
        command.Execute();
    }
}