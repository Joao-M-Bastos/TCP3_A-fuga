using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Path_Handle : MonoBehaviour
{
    private Vector3 finishObjPos;

    public NavMeshAgent botAgent;
    public Smart_Bot_Move smart_Bot_Move;

    // Start is called before the first frame update
    void Start()
    {
        finishObjPos = GameObject.FindGameObjectWithTag("Finish").transform.position;
        this.smart_Bot_Move = this.GetComponentInChildren<Smart_Bot_Move>();
        this.botAgent = this.GetComponentInChildren<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AdvancedMove()
    {
        if (!botAgent.isActiveAndEnabled)
        {
            botAgent.gameObject.SetActive(true);
            botAgent.gameObject.transform.position = smart_Bot_Move.transform.position;
        }
        smart_Bot_Move.botRB.useGravity = false;
        botAgent.isStopped = false;
        botAgent.SetDestination(finishObjPos);
        smart_Bot_Move.transform.position = botAgent.gameObject.transform.position;
        smart_Bot_Move.transform.forward = botAgent.gameObject.transform.forward;
    }

    public void TurnAgentOff()
    {
        if (botAgent.isActiveAndEnabled)
        {
            smart_Bot_Move.botRB.useGravity = true;
            botAgent.isStopped = true;
            botAgent.gameObject.SetActive(false);
        }
    }
}
