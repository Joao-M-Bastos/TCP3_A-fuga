using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagDoll_BotState : Bot_StateMachine
{
    public override void EnterState(EpicBot_Controller bot)
    {
        bot.spaceIsPressed = false;
        bot.gooseAnimator.SetBool("Runnig", false);
    }

    public override void UpdateState(EpicBot_Controller bot)
    {
        ChangeState(bot);
    }

    public void ChangeState(EpicBot_Controller bot)
    {
        if (bot.botRespawnScrp.IsDead)
        {
            bot.botRedDoll.IsRagDoll = false;
            bot.vidaParaoTitanic--;
            bot.botRespawnScrp.IsDead = false;
            bot.botRedDoll.RagDollOff();
            bot.ChangeState(bot.respawning_BotState);
            return;
        }

        if (!bot.botRedDoll.IsRagDoll)
        {
            bot.botRedDoll.RagDollOff();
            bot.ChangeState(bot.epicWalk_BotState);
        }
    }
}
