using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceLabel : MonoBehaviour
{
    public Resources targetResource;
    private TMPro.TMP_Text label;

    // Start is called before the first frame update
    void Start()
    {
        label = GetComponent<TMPro.TMP_Text>();
        Inventory.Instance.OnResourceAmountUpdated += UpdateLabel;
        UpdateLabel(this, new OnResourceAmountUpdatedEventArgs { type = targetResource, newAmount = Inventory.Instance.GetAmount(targetResource) });
    }

    public void UpdateLabel(object o, OnResourceAmountUpdatedEventArgs e)
    {
        if (e.type == targetResource)
            label.text = e.type.ToString() + ": " + e.newAmount;
    }
}
