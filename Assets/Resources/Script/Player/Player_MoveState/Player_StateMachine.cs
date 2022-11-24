using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Player_StateMachine
{
    abstract public void EnterState(Player_Controller player);
    abstract public void UpdateState(Player_Controller player);
}
