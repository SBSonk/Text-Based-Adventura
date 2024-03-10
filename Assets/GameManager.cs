using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject[] walkPassScene, walkFailScene;
    bool overslept = false;

    public void SetOverslept(bool val) => overslept = val;
    
    public bool IsOverslept() => overslept;

    public void Sleep()
    {
        if (overslept)
        {
            foreach (var o in walkFailScene)
            {
                o.SetActive(true);
            }
        }
        else
        {
            foreach (var o in walkPassScene)
            {
                o.SetActive(true);
            }
        }
    }
}
