using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Epic_Path_Handle : MonoBehaviour
{
    private Vector3 finishObjPos;

    public NavMeshAgent botAgent;
    public EpicBot_Controller epic_Bot_Controller;

    // Start is called before the first frame update
    void Start()
    {
        finishObjPos = GameObject.FindGameObjectWithTag("Finish").transform.position;
        this.epic_Bot_Controller = this.GetComponentInChildren<EpicBot_Controller>();
        this.botAgent = this.GetComponentInChildren<NavMeshAgent>();
        botAgent.gameObject.SetActive(false);
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
            botAgent.gameObject.transform.position = epic_Bot_Controller.transform.position;
        }
        epic_Bot_Controller.botRB.useGravity = false;
        botAgent.isStopped = false;
        botAgent.SetDestination(finishObjPos);

        epic_Bot_Controller.transform.position = botAgent.gameObject.transform.position;
        epic_Bot_Controller.transform.forward = botAgent.gameObject.transform.forward;
    }

    public void TurnAgentOff()
    {
        if (botAgent.isActiveAndEnabled)
        {
            epic_Bot_Controller.botRB.useGravity = true;
            botAgent.isStopped = true;
            botAgent.gameObject.SetActive(false);
        }
    }

    public bool OnNavMesh(bool isActive)
    {
        if (isActive)
            return botAgent.isOnNavMesh;

        botAgent.gameObject.SetActive(true);
        botAgent.gameObject.transform.position = epic_Bot_Controller.transform.position;
        isActive = botAgent.isOnNavMesh;
        botAgent.gameObject.SetActive(false);
        return isActive;
    }
}
