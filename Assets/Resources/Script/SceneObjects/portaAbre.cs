using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class portaAbre : MonoBehaviour
{
    private bool opening;
    private bool openedBefore;

    public float rotSpeed;

    // Start is called before the first frame update
    void Start()
    {
        opening = false;
        openedBefore = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (opening == true)
            transform.Rotate(0f, rotSpeed * Time.deltaTime, 0f, Space.World);

        if (opening == true && this.transform.eulerAngles.y >= 135)
            opening = false;
    }

    void OnCollisionStay(Collision col)
    {
        if (col.gameObject.tag == "Player" && openedBefore == false)
        {
            opening = true;
            openedBefore = true;
        }
    }
}
