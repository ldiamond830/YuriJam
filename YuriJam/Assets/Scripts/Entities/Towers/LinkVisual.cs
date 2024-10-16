using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinkVisual : MonoBehaviour
{
    // Fields
    private Tower tower;
    private Color baseColor;
    private SpriteRenderer linkSprite;
    private bool currLink = false;

    // Start is called before the first frame update
    void Start()
    {
        tower = GetComponent<Tower>();
        linkSprite = transform.GetChild(0).GetChild(0).GetComponent<SpriteRenderer>();
        
        // Default to color of parent tower
        baseColor = tower.transform.GetChild(0).GetComponent<SpriteRenderer>().color;
        linkSprite.color = baseColor;
    }

    // Update is called once per frame
    void Update()
    {
        if (tower.linked && !currLink)      // Activate link visual
        {
            currLink = true;

            // Use color of linked tower
            // If linked tower is same type, use white to stand out
            linkSprite.color = tower.linked.transform.GetChild(0).GetComponent<SpriteRenderer>().color;
            if (linkSprite.color == baseColor) linkSprite.color = Color.white;

            // Rotate tower sprite to show link direction
            float zRot = Vector3.SignedAngle(Vector3.right, tower.linked.transform.position - transform.position, Vector3.forward);
            transform.GetChild(0).transform.eulerAngles = Vector3.forward * zRot;
        }
        else if (!tower.linked && currLink) // Revert to default visual
        {
            currLink = false;

            // Revert to color of parent tower
            linkSprite.color = tower.transform.GetChild(0).GetComponent<SpriteRenderer>().color;
        }
    }
}
