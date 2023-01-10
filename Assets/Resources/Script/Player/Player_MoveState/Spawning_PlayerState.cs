 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawning_PlayerState : Player_StateMachine
{
    float countdown;

    public override void EnterState(Player_Controller player)
    {
        player.gooseAnimator.SetBool("Runnig", false);
        player.playerRB.isKinematic = true;

        this.countdown = 1.5f;
    }

    public override void UpdateState(Player_Controller player)
    {
        countdown += -Time.deltaTime;
        ChangeState(player);
    }

    public void ChangeState(Player_Controller player)
    {
        if (countdown<=0)
        {
            player.playerRB.isKinematic = false;
            player.ChangeState(player.air_PlayerState);
        }
    }
}
