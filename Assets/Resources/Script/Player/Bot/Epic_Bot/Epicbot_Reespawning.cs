using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Epicbot_Reespawning : Bot_StateMachine
{
    float countdown;

    public override void EnterState(EpicBot_Controller bot)
    {
        bot.gooseAnimator.SetBool("Runnig", false);
        bot.botRB.isKinematic = true;
        bot.transform.rotation = Quaternion.identity;

        this.countdown = 1.5f;
    }

    public override void UpdateState(EpicBot_Controller bot)
    {
        countdown += -Time.deltaTime;
        ChangeState(bot);
    }

    public void ChangeState(EpicBot_Controller bot)
    {
        if (countdown <= 0)
        {
            bot.botRB.isKinematic = false;
            if(bot.faseManager.isFaseTitanic)
                bot.ChangeState(bot.dumbWalk_BotState);
            else
                bot.ChangeState(bot.air_BotState);
        }
    }
}
