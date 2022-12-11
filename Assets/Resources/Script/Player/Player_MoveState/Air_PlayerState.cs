using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Air_PlayerState : Player_StateMachine
{
    public override void EnterState(Player_Controller player)
    {

    }

    public override void UpdateState(Player_Controller player)
    {
        if (Input.GetKeyDown(KeyCode.Space) && player.airJumpCount > 0)
            Jump(player);


        Fly(player);

        ChangeState(player);
    }

    public void ChangeState(Player_Controller player)
    {
        if (player.onGoundInstance.isOnGround)
        {
            player.gooseAnimator.SetTrigger("Levantar");
            player.gooseAnimator.SetBool("WingsOpen", false);
            player.ChangeState(player.walk_PlayerState);
        }

        if (player.IsRagdollEffect())
        {
            player.gooseAnimator.SetTrigger("Levantar");
            player.gooseAnimator.SetBool("WingsOpen", false);
            player.ChangeState(player.ragDoll_PlayerState);
        }

        if (player.playerRespawnScrp.IsDead)
        {
            player.gooseAnimator.SetBool("WingsOpen", false);
            player.playerRespawnScrp.IsDead = false;
            player.ChangeState(player.spawning_PlayerState);
        }
    }

    public void Jump(Player_Controller player)
    {
        player.audioSource.PlayOneShot(player.audioClipJump, 0.8f);
        player.playerRB.velocity = new Vector3(player.playerRB.velocity.x, player.playerJumpForce / 1.6f, player.playerRB.velocity.z);
        player.airJumpCount--;
    }

    public void Fly(Player_Controller player)
    {
        player.isWingsOpen = this.CanOpenWings(player);
        player.gooseAnimator.SetBool("WingsOpen", player.isWingsOpen);

        player.playerRB.AddForce(0, 1.5f * player.gravityValue, 0);

        if (player.isWingsOpen)
        {
            player.playerRB.velocity = new Vector3(player.playerRB.velocity.x, -player.playerPlaneValue, player.playerRB.velocity.z);
        }
    }

    public bool CanOpenWings(Player_Controller player)
    {
        if (Input.GetKey(KeyCode.Space) && player.playerRB.velocity.y < -player.playerPlaneValue + 0.01f) return true;
        return false;
    }
}
