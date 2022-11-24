using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Bot_StateMachine
{
    abstract public void EnterState(EpicBot_Controller bot);

    abstract public void UpdateState(EpicBot_Controller bot);
}
