using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Air_BotState : Bot_StateMachine
{
    public override void EnterState(EpicBot_Controller bot)
    {
        bot.spaceIsPressed = false;
    }

    public override void UpdateState(EpicBot_Controller bot)
    {
        if (bot.airJumpCount > 0 && bot.botRB.velocity.y < 0)
            Jump(bot);

        Fly(bot);

        ChangeState(bot);
    }

    public void ChangeState(EpicBot_Controller bot)
    {
        if (bot.onGoundInstance.isOnGround || bot.faseManager.isFaseTitanic)
        {
            bot.gooseAnimator.SetBool("WingsOpen", false);
            bot.gooseAnimator.SetTrigger("Levantar");


            if (bot.path_Handle.OnNavMesh(false))
                bot.ChangeState(bot.epicWalk_BotState);
            else bot.ChangeState(bot.dumbWalk_BotState);
        }

        if (bot.botRespawnScrp.IsDead)
        {
            bot.vidaParaoTitanic--;
            bot.botRespawnScrp.IsDead = false;
            bot.ChangeState(bot.respawning_BotState);
        }

        if (bot.IsRagdollEffect())
            bot.ChangeState(bot.ragDoll_BotState);
    }

    public void Jump(EpicBot_Controller bot)
    {
        bot.audioSource.PlayOneShot(bot.audioClipJump, 0.8f);
        bot.botRB.velocity = new Vector3(bot.botRB.velocity.x, bot.botJumpForce / 1.6f, bot.botRB.velocity.z);
        bot.airJumpCount--;
    }

    public void Fly(EpicBot_Controller bot)
    {
        bot.isWingsOpen = this.CanOpenWings(bot);
        bot.gooseAnimator.SetBool("WingsOpen", bot.isWingsOpen);

        if (bot.isWingsOpen)
        {
            Vector3 moveDir = bot.transform.forward * bot.botSpeed;

            moveDir.y = -bot.botPlaneValue;

            bot.botRB.velocity = moveDir;
        }
    }

    public bool CanOpenWings(EpicBot_Controller bot)
    {
        if (bot.botRB.velocity.y < -bot.botPlaneValue + 0.01f) return true;
        return false;
    }
}
