using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EpicWalk_BotState : Bot_StateMachine
{
    public override void EnterState(EpicBot_Controller bot)
    {
        bot.spaceIsPressed = false;
        bot.gooseAnimator.SetBool("Runnig", true);
        bot.changeBotMoveTypeCooldown = Random.Range(10, 20);
    }

    public override void UpdateState(EpicBot_Controller bot)
    {
        if (bot.spaceIsPressed)
            Jump(bot);
        else
            bot.path_Handle.AdvancedMove();

        if (bot.isOnWater)
            bot.gooseAnimator.SetBool("Swim", true);
        else
            bot.gooseAnimator.SetBool("Swim", false);

        ChangeState(bot);
    }

    public void Jump(EpicBot_Controller bot)
    {
        bot.gooseAnimator.SetTrigger("Pular");
        bot.audioSource.PlayOneShot(bot.audioClipJump, 0.8f);
        bot.path_Handle.TurnAgentOff();
        bot.transform.position += new Vector3(0,0.3f,0);
    }

    public void PlayerDash(EpicBot_Controller bot)
    {
        bot.botRB.AddForce(bot.botRB.transform.forward * 400);
    }

    public void ChangeState(EpicBot_Controller bot)
    {
        if (!bot.onGoundInstance.isOnGround)
        {
            bot.gooseAnimator.SetBool("Swim", false);
            bot.path_Handle.TurnAgentOff();
            bot.ChangeState(bot.air_BotState);
        }

        if (bot.IsRagdollEffect())
        {
            bot.gooseAnimator.SetBool("Swim", false);
            bot.path_Handle.TurnAgentOff();
            bot.ChangeState(bot.ragDoll_BotState);
        }

        if (bot.changeBotMoveTypeCooldown < 0)
        {
            bot.gooseAnimator.SetBool("Swim", false);
            bot.path_Handle.TurnAgentOff();
            bot.ChangeState(bot.dumbWalk_BotState);
        }
        else bot.changeBotMoveTypeCooldown -= Time.deltaTime;

        if (bot.botRespawnScrp.IsDead)
        {
            bot.gooseAnimator.SetBool("Swim", false);
            bot.vidaParaoTitanic--;
            bot.botRespawnScrp.IsDead = false;
            bot.ChangeState(bot.respawning_BotState);
        }
    }

    public void PlayStepFX(EpicBot_Controller bot)
    {
        if ((bot.botRB.velocity.x * bot.botRB.velocity.z) != 0)
        {
            int randomNum = Random.Range(0, bot.audioClipSteps.Length - 1);
            bot.audioSource.PlayOneShot(bot.audioClipSteps[randomNum], 0.4f - (0.3f * randomNum));
            bot.stepSoundDelay = 16;
        }
    }
}
