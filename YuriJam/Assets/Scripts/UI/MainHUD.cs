using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainHUD : MonoBehaviour
{
    public static void CreateFadeMessage(Vector3 position, string message, float length, Color color, float size = 30)
    {
        FadeMessage msg = new GameObject("FadeMessage", typeof(FadeMessage)).GetComponent<FadeMessage>();
        msg.transform.position = position;
        msg.message = message;
        msg.fadeLength = length;
        msg.textColor = color;
        msg.textSize = size;
    }
}
