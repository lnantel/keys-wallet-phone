using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropSystem : MonoBehaviour
{
    public float collisionCounter;
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
    
        print("Collision multiplier" +collisionMultiplier);
        print("Number of collision" + collisionCounter);
    }

    private void OnCollisionEnter(Collision other) 
    {
        if(other.gameObject.layer == 10)
        {
            collisionCounter += .1f;
            InstantiateObject(DropObjet());
        } 
    }

    public int DropObjet()
    {        
        if(collisionCounter*collisionMultiplier * Random.Range(0.5f,1.5f) > 2) {
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
            print("Function to drop object called");
        }
    }
}




/*Systeme collision perte objet

float numberCollision = A chaque fois qu'un joeur entre en collision avec un objet → numberCollision += .01

float speedModifier (
    if(sprinting)
    speedModifier = 1.5f;

    if(!sprinting)
    speedModifier = 1f;
)

Update (
    CheckSpeed(other.sprinting);

)



OnCollisionEnter(Collider collider)
{
    if()
}

public void CheckSpeed(bool status)
{
    if(!status) speedModifier = 1f; 
    else if(status) speedModifier = 1.5f;
}


public int DropObjet()
{
    if(numberCollision*speedModifier*Mathf.Random.RandomRange(0.5f,1.5f) <= 2) {
        return null;
        break;
    }

    if(numberCollision*speedModifier*Mathf.Random.RandomRange(0.5f,1.5f) > 2) {
        return 1;
        break;
    }  
}

public void InstantiateObject(int value)
{
    if(!value)
    {
        print("No object dropped");
        break;
    }
    if(value = 1)
    {
        
    }
}
*/