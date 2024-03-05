using System.Collections;
using System.Collections.Generic;
using FirstGearGames.SmoothCameraShaker;
using UnityEngine;

public class ScreenShakeManager : MonoBehaviour
{
    public ScreenShake[] shake;

    public void ShakeScreen(string name)
    {
        foreach (ScreenShake s in shake)
        {
            if (s.name == name) { CameraShakerHandler.Shake(s.shake); }
        }
    }
}

[System.Serializable]
public struct ScreenShake
{
    public string name;
    public ShakeData shake;
}
