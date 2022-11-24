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
        if (!bot.botRedDoll.IsRagDoll)
        {
            bot.ChangeState(bot.epicWalk_BotState);
        }
    }
}
