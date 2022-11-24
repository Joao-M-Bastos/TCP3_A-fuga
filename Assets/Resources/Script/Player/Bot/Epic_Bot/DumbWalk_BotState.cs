using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DumbWalk_BotState : Bot_StateMachine
{
    public float randx, randy;

    private float changeDirectionDelay;

    public override void EnterState(EpicBot_Controller bot)
    {
        bot.spaceIsPressed = false;
        bot.path_Handle.TurnAgentOff();
        bot.changeBotMoveTypeCooldown = Random.Range(1, 2);
    }

    public override void UpdateState(EpicBot_Controller bot)
    {
        Move(bot);

        ChangeState(bot);
    }

    public void Move(EpicBot_Controller bot)
    {
        Vector3 direction;

        if (changeDirectionDelay < 0)
        {
            randx = Mathf.Floor(Random.Range(-1, 2));
            randy = Mathf.Floor(Random.Range(-1, 2));
            changeDirectionDelay = 1;
        }
        else
            changeDirectionDelay -= Time.deltaTime;

        direction = new Vector3(randx, 0, randy).normalized;

        Walk(bot, direction);
    }

    public void Walk(EpicBot_Controller bot, Vector3 direction)
    {
        //Favor chamar sons de passos.... eventualmente

        float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;

        float angle = Mathf.SmoothDampAngle(bot.transform.eulerAngles.y, targetAngle, ref bot.turnSmoothVelocity, bot.turnSmoothTime) ;

        Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward * bot.botSpeed;

        bot.transform.rotation = Quaternion.Euler(0f, angle, 0f);

        moveDir.y = bot.botRB.velocity.y;

        bot.botRB.velocity = moveDir;

        if(Mathf.Abs(randy) + Mathf.Abs(randx) == 0)
            bot.gooseAnimator.SetBool("Runnig", false);
        else
            bot.gooseAnimator.SetBool("Runnig", true);
    }

    public void PlayerDash(EpicBot_Controller bot)
    {
        bot.botRB.AddForce(bot.botRB.transform.forward * 400);
    }

    public void ChangeState(EpicBot_Controller bot)
    {

        if (bot.IsRagdollEffect())
            bot.ChangeState(bot.ragDoll_BotState);

        if (bot.changeBotMoveTypeCooldown < 0)
            bot.ChangeState(bot.epicWalk_BotState);
        else bot.changeBotMoveTypeCooldown -= Time.deltaTime;
    }

    public void Friction(EpicBot_Controller bot)
    {
        Vector3 botActualSpeed = bot.botRB.velocity;

        if (Mathf.Abs(botActualSpeed.x) > 5)
            botActualSpeed.x -= Mathf.Sign(botActualSpeed.x) * bot.frictionValue * Time.deltaTime;
        else
            botActualSpeed.x = 0;

        if (Mathf.Abs(botActualSpeed.z) > 5)
            botActualSpeed.z -= Mathf.Sign(botActualSpeed.z) * bot.frictionValue * Time.deltaTime;
        else
            botActualSpeed.z = 0;

        bot.botRB.velocity = botActualSpeed;
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
