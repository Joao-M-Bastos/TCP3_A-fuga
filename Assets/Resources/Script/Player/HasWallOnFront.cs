using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HasWallOnFront
{
    public bool HasWall(Transform objTransform)
    {
        if (Physics.Raycast(objTransform.position + (objTransform.forward * 0.25f), objTransform.forward, 0.25f))
            return true;
        return false;
    }
}
