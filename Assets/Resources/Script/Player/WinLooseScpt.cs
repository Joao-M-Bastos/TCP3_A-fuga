using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinLooseScpt : MonoBehaviour
{
    public bool winLoose;

    private void OnLevelWasLoaded(int level)
    {
        HasWinLoose = false;
    }

    public bool HasWinLoose
    {
        set { winLoose = value; }
        get { return winLoose; }
    }
}
