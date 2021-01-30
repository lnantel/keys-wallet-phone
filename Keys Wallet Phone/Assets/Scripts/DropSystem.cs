using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropSystem : MonoBehaviour
{
    public static Action objectDrop;

    public float collisionCounter;
    //Nessite une connection avec le player controller
    public bool isSprinting;
    public float collisionMultiplier; 

    // Start is called before the first frame update
    void Start()
    {
        collisionCounter = 0f;
        collisionMultiplier = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.LeftShift)) collisionMultiplier = 1.5f;
        if(Input.GetKeyUp(KeyCode.LeftShift)) collisionMultiplier = 1f;
    
        //print("Collision multiplier" +collisionMultiplier);
        //print("Number of collision" + collisionCounter);
    }

    private void OnCollisionEnter(Collision other) 
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("CollidableObjects"))
        {
            collisionCounter += .1f;
            InstantiateObject(DropObjet());
            Debug.Log("COLLISION");
        }
    }

    public int DropObjet()
    {        
        if(collisionCounter*collisionMultiplier * UnityEngine.Random.Range(0.5f,1.5f) > 2) {
            collisionCounter =0f;
            return 1;
        }  
        return 0;
    }

    public void InstantiateObject(int value)
    {
        if(value == 0)
        {
            print("No object dropped");
        }
        if(value == 1)
        {
            //Nécessite l'ajout d'une fonction ou Action pour faire Spawn les objets
            print("Function to drop object called");

            if (objectDrop != null)
                objectDrop();
        }
    }
}
