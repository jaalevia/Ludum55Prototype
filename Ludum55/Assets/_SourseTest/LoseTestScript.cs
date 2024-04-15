using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoseTestScript : MonoBehaviour
{
    public int LosePoints;
    public int WinPoints;
    [SerializeField] SpawnSystemRENAME _win;
    void Start()
    {
        LosePoints = 0;
    }

    void Update()
    {
        if (LosePoints >= 3)
        {
            Debug.Log("You Lose");
        }
        /*if (_win.PointToWin == WinPoints)
        {
            Debug.Log("You Win");
        }*/
    }

}
