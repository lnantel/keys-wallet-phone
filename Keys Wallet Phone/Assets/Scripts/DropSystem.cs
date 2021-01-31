using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropSystem : MonoBehaviour
{
    public static Action objectDrop;
    public static Action playerCollided;

    public float collisionCounter;
    //Nessite une connection avec le player controller

    private SimplePlayerController _Player;

    public float collisionMultiplier;

    private void OnEnable()
    {
        GameManager.playerLoaded += AssignPlayer;
        Doormat.playerOnDoormat += ResetCollisionCounter;
    }

    private void OnDisable()
    {
        GameManager.playerLoaded -= AssignPlayer;
        Doormat.playerOnDoormat -= ResetCollisionCounter;
    }

    // Start is called before the first frame update
    void Start()
    {
        collisionCounter = 0f;
        collisionMultiplier = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        if (_Player != null)
        {
            collisionMultiplier = _Player.IsSprinting ? 1.5f : 1f;
        }
        //print("Collision multiplier" +collisionMultiplier);
        //print("Number of collision" + collisionCounter);
    }

    private void AssignPlayer(SimplePlayerController player)
    {
        _Player = player;
    }

    private void ResetCollisionCounter()
    {
        collisionCounter = 0f;
    }

    private void OnCollisionEnter(Collision other) 
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("CollidableObjects"))
        {
            if (playerCollided != null)
                playerCollided();

            collisionCounter += (.1f * collisionMultiplier);
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
