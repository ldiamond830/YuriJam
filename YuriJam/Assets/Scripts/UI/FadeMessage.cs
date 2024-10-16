using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Drawing;

public class FadeMessage : MonoBehaviour
{
    // Fields
    private TMP_Text text;
    private float fadeTime = 0;

    public string message;
    public float fadeLength;
    public UnityEngine.Color textColor;
    public float textSize;


    // Start is called before the first frame update
    void Start()
    {
        text = gameObject.AddComponent<TextMeshPro>();

        text.isOverlay = true;
        text.text = message;
        text.color = textColor;
        text.alignment = TextAlignmentOptions.Center;
        text.fontSize = textSize;
    }

    // Update is called once per frame
    void Update()
    {
        if (fadeTime >= fadeLength)
            Destroy(gameObject);

        transform.position += Vector3.up * Time.deltaTime;
        text.alpha = 1 - (fadeTime / fadeLength);
        fadeTime += Time.deltaTime;
    }
}
