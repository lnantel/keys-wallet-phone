using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        SceneManager.sceneLoaded += ResetOnLoad;
    }

    private void OnDisable()
    {
        PickableObject.objecPickedUp -= AddToInventory;
        DropSystem.objectDrop -= DropRandomObject;
        SceneManager.sceneLoaded -= ResetOnLoad;
    }

    private void ResetOnLoad(Scene scene, LoadSceneMode loadSceneMode)
    {
        _objectsInInventory.Clear();
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
            Debug.Log("Found : " + objectName);
            return true;
        }
        Debug.Log("Could not find : " + objectName);
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
        objToDrop.Drop();
        RemoveFromInventory(objToDrop);

        //Vector3 RandomPos = GetRandomPosition();
        //objToDrop.Drop(RandomPos);
    }

    // TODO : Add bounds
    private Vector3 GetRandomPosition()
    {
        Vector3 playerPos = GameManager.instance.CurrentPlayer.transform.position;
        float x = playerPos.x + Random.Range(-2f, 2f);
        float y = playerPos.y + 1f;
        float z = playerPos.z + Random.Range(-2f, 2f);

        return new Vector3(x, y, z);
    }
}
