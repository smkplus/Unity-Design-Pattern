using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;


public class FlashLightController : MonoBehaviour
{
    public FlashLight flashLight;

    private ILightCommand _request;

    public List<ILightCommand> commands = new List<ILightCommand>();
    private int _currentCommandNum;
    
    private static readonly Dictionary<string, Color> Colors = new Dictionary<string, Color>()
    {
        // Built-in Colors
            
        {"red",Color.red},
        {"yellow",Color.yellow},
        {"green",Color.green},
        {"blue",Color.blue},
        {"cyan",Color.cyan},
        {"magenta",Color.magenta},
    };


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
                var lightColor = GetColorName(commands[i].LightColor);
                string colorName = $"<color={lightColor}> {lightColor} </color>";
            if (i == _currentCommandNum - 1)
                label += $">  <b> {colorName} </b> \n";
            else
            {
                label += $"   {colorName}  \n";
            }
        }

        GUI.Label(new Rect(0, 0, 400, 800), label);
        GUI.Label(new Rect(Screen.width/2f - 200, Screen.height/2f + 200, 400, 800), "Space = Turn On/Off - Left Arrow = Undo - Right Arrow = Redo");
    }

    public void Undo()
    {
        if (_currentCommandNum-1 > 0)
        {
            _currentCommandNum--;
            ILightCommand command = commands[_currentCommandNum-1];
            command.Undo();
        }
    }


    public string GetColorName(Color color)
    {
        return Colors.FirstOrDefault(x => x.Value == color).Key;
    }

    public void Redo()
    {
        if (_currentCommandNum < commands.Count)
        {
            ILightCommand command = commands[_currentCommandNum];
            _currentCommandNum++;
            command.Execute();
        }
    }

    
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            var remote = new RemoteControl1();
            
            var turnOn = new Light(flashLight);
            _request = turnOn;

            _request.LightColor = Colors.ElementAt(Random.Range(0,5)).Value;

            
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

public interface ILightCommand
{
    Color LightColor { get; set; }
    void Execute();
    void Undo();
    void Redo();
}

//Command

class Light : ILightCommand
{
    private FlashLight _flashLight;

    public Light(FlashLight flashLight)
    {
        _flashLight = flashLight;
    }

    public Color LightColor { get; set; }

    public void Execute()
    {
        _flashLight.SetColor(LightColor);
    }

    public void Undo()
    {
        Execute();
    }

    public void Redo()
    {
        Execute();
    }
}

// Invoker
public class RemoteControl1
{
    public void Execute(ILightCommand command)
    {
        command.Execute();
    }
}
