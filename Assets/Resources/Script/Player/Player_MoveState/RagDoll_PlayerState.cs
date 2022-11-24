using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagDoll_PlayerState : Player_StateMachine
{
    public override void EnterState(Player_Controller player)
    {
        player.gooseAnimator.SetBool("Runnig", false);

    }

    public override void UpdateState(Player_Controller player)
    {
        ChangeState(player);
    }

    public void ChangeState(Player_Controller player)
    {
        if (!player.playerRedDoll.IsRagDoll)
        {
            player.ChangeState(player.walk_PlayerState);
        }
    }
}
