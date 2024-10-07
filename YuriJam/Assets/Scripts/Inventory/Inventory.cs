using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    // Fields
    private Dictionary<Resources, int> inventory;
    public List<Resource> startingInventory;

    // Event for notifying on changes in resource amounts
    public EventHandler<OnResourceAmountUpdatedEventArgs> OnResourceAmountUpdated;
    public class OnResourceAmountUpdatedEventArgs : EventArgs
    {
        public Resources type;
        public int newAmount;
    }

    // Properties
    public static Inventory Instance
    { 
        get; 
        private set; 
    }

    // Methods
    public void Awake()
    {
        // Singleton setup for global access
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        // Initialize inventory keys
        inventory = new Dictionary<Resources, int>();
        foreach (Resources r in Enum.GetValues(typeof(Resources)))
            inventory[r] = 0;

        // Add starting inventory
        AddMultiple(startingInventory);
    }

    // Look up amount of a resource
    public int GetAmount(Resources type)
    {
        return inventory[type];
    }

    // Adds a certain amount of a selected resource
    public void AddAmount(Resources type, int amount)
    {
        inventory[type] += amount;
        OnResourceAmountUpdated?.Invoke(this, new OnResourceAmountUpdatedEventArgs { type = type, newAmount = inventory[type] });
    }

    public void AddAmount(Resource resource)
    {
        AddAmount(resource.type, resource.amount);
    }

    // Adds to multiple resources
    public void AddMultiple(List<Resource> resources)
    {
        foreach (Resource r in resources)
            AddAmount(r.type, r.amount);
    }

    // Removes a certain amount of a selected resource
    // Returns false if there is not enough to remove (would set resource amount below 0)
    public bool RemoveAmount(Resources type, int amount)
    {
        // Cannot remove more than held (cannot go below 0)
        if (inventory[type] < amount) return false;

        inventory[type] -= amount;
        OnResourceAmountUpdated?.Invoke(this, new OnResourceAmountUpdatedEventArgs { type = type, newAmount = inventory[type] });
        return true;
    }

    public bool RemoveAmount(Resource resource)
    {
        return RemoveAmount(resource.type, resource.amount);
    }

    // Removes from multiple resources
    // Returns false if unable to remove enough of a resource
    public bool RemoveMultiple(List<Resource> resources)
    {
        for (int i = 0; i < resources.Count; i++)
        {
            // If insufficient amount of a resource, undo previous removals
            if (!RemoveAmount(resources[i]))
            {
                // Skip current as it remains unchanged
                while (i-- > 0)
                    AddAmount(resources[i]);

                return false;
            }
        }

        // All removals were successful
        return true;
    }
}
