using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EpicWalk_BotState : Bot_StateMachine
{
    public override void EnterState(EpicBot_Controller bot)
    {
        bot.spaceIsPressed = false;
        bot.gooseAnimator.SetBool("Runnig", true);
        bot.changeBotMoveTypeCooldown = Random.Range(5, 10);
    }

    public override void UpdateState(EpicBot_Controller bot)
    {
        if (bot.spaceIsPressed)
            Jump(bot);
        else
            bot.path_Handle.AdvancedMove();

        ChangeState(bot);
    }

    public void Jump(EpicBot_Controller bot)
    {
        bot.gooseAnimator.SetTrigger("Pular");
        bot.audioSource.PlayOneShot(bot.audioClipJump, 0.8f);
        bot.path_Handle.TurnAgentOff();
        bot.transform.position += new Vector3(0,0.2f,0);
    }

    public void PlayerDash(EpicBot_Controller bot)
    {
        bot.botRB.AddForce(bot.botRB.transform.forward * 400);
    }

    public void ChangeState(EpicBot_Controller bot)
    {
        if (!bot.onGoundInstance.isOnGround)
        {
            bot.path_Handle.TurnAgentOff();
            bot.ChangeState(bot.air_BotState);
        }

        if (bot.IsRagdollEffect())
        {
            bot.path_Handle.TurnAgentOff();
            bot.ChangeState(bot.ragDoll_BotState);
        }

        if (bot.changeBotMoveTypeCooldown < 0)
        {
            bot.path_Handle.TurnAgentOff();
            bot.ChangeState(bot.dumbWalk_BotState);
        }
        else bot.changeBotMoveTypeCooldown -= Time.deltaTime;
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
