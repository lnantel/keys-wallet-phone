using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance;
    private List<PickableObject> _objectsInInventory = new List<PickableObject>();

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    private void OnEnable()
    {
        PickableObject.objecPickedUp += AddToInventory;
        DropSystem.objectDrop += DropRandomObject;
    }

    private void OnDisable()
    {
        PickableObject.objecPickedUp -= AddToInventory;
        DropSystem.objectDrop -= DropRandomObject;
    }

    private void AddToInventory(PickableObject obj)
    {
        _objectsInInventory.Add(obj);
        Debug.Log("Added : " + obj.objectName + " to inventory.");
    }

    private void RemoveFromInventory(PickableObject obj)
    {
        _objectsInInventory.Remove(obj);
        Debug.Log("Removed : " + obj.objectName + " from inventory.");
    }

    // Function that checks if an object is picked up in inventory (uses object name/string)
    public bool CheckForObject(string objectName)
    {
        var found = _objectsInInventory.Find(x => x.objectName == objectName);

        if (found != null)
        {
            return true;
        }
        return false;
    }

    private void DropRandomObject()
    {
        int objectCount = _objectsInInventory.Count;

        // Don't execute if 0 objects
        if (objectCount <= 0)
            return;

        int dropIndex = Random.Range(0, objectCount);

        PickableObject objToDrop = _objectsInInventory[dropIndex];
        RemoveFromInventory(objToDrop);

        Vector3 RandomPos = GetRandomPosition();
        objToDrop.Drop(RandomPos);
    }

    private Vector3 GetRandomPosition()
    {
        float x = Random.Range(-5f, 5f);
        float y = Random.Range(1f, 2f);
        float z = Random.Range(-5f, 5f);

        return new Vector3(x, y, z);
    }
}
