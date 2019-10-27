# Unity-Design-Pattern
Unity Design Pattern



👮 Command
-------

Real world example
> A generic example would be you ordering food at a restaurant. You (i.e. `Client`) ask the waiter (i.e. `Invoker`) to bring some food (i.e. `Command`) and waiter simply forwards the request to Chef (i.e. `Receiver`) who has the knowledge of what and how to cook.
> Another example would be you (i.e. `Client`) switching on (i.e. `Command`) the television (i.e. `Receiver`) using a remote control (`Invoker`).

In plain words
> Allows you to encapsulate actions in objects. The key idea behind this pattern is to provide the means to decouple client from receiver.

Wikipedia says
> In object-oriented programming, the command pattern is a behavioral design pattern in which an object is used to encapsulate all information needed to perform an action or trigger an event at a later time. This information includes the method name, the object that owns the method and values for the method parameters.

**Programmatic Example**

First of all we have the receiver that has the implementation of every action that could be performed
```c#
// Receiver
using UnityEngine;

// Receiver
public class Bulb : MonoBehaviour
{
    public void TurnOn()
    {
        print("Bulb has been lit");
    }

    public void TurnOff()
    {
        print("Darkness!");
    }
}
```
then we have an interface that each of the commands are going to implement and then we have a set of commands

![image](https://user-images.githubusercontent.com/16706911/67629961-5bc50e80-f894-11e9-8ec4-912472437f37.png)

```c#
public Bulb bulb;

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
    public void Submit(ICommand command)
    {
        command.Execute();
    }
}
```
Then we have an `Invoker` with whom the client will interact to process any commands
```c#
// Invoker
class RemoteControl
{
    public void Submit(ICommand command)
    {
        command.Execute();
    }
}
```
Finally let's see how we can use it in our client
```c#
 var turnOn = new TurnOn(bulb);
 var turnOff = new TurnOff(bulb);

 var remote = new RemoteControl();
        
 remote.Submit(turnOn); // Bulb has been lit!
 remote.Submit(turnOff); // Darkness!
```

Command pattern can also be used to implement a transaction based system. Where you keep maintaining the history of commands as soon as you execute them. If the final command is successfully executed, all good otherwise just iterate through the history and keep executing the `undo` on all the executed commands.

### Real World Examples

[Simon Memory Game](http://www.kidsmathgamesonline.com/memory/simon.html)

![memory game](https://www.mentesliberadas.com/wp-content/uploads/2011/09/simon.jpg)

Photoshop History

![photoshop](https://i.ytimg.com/vi/5RNLIXgRb7A/hqdefault.jpg)

another way to Implement Command Pattern is using a Action.
Action is a Delegate. It is defined like this:

```public delegate void Action();```

You could create your own delegate types similarly to how you would create abstract methods; you write the signature but no implementation. You then create instances of these delegates by taking the reference of a method.


Delegate in C# is a reference type, which holds a reference to the function and invokes the function when called with an Invoke method. If one is coming from C++/C background, delegate is like a pointer to a function. 

