using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObject : MonoBehaviour
{   
    RaycastHit hit;
    public int castDist;
    public GameObject springConnection;
    public GameObject grabbedObject;
    public bool releaseTimer;
    public bool hasSpringJoint;
    
    // Start is called before the first frame update
    void Start()
    {
        castDist = 5;
        releaseTimer = true;
        hasSpringJoint = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0) && grabbedObject != null ) ReleaseObject();

        if(Input.GetMouseButtonDown(0) && grabbedObject == null && releaseTimer) GrabObject();
    }

    public void GrabObject()
    {
        if(Physics.Raycast(transform.position, transform.forward, out hit, castDist) && hit.transform.gameObject.layer == 10)
        {
            print("Object Grabbed");
        
            if(hit.transform.gameObject.GetComponent<SpringJoint>() == null) {
                hasSpringJoint = false;
                print("Doesn't have joint");
            } else hasSpringJoint = true;

            switch (hasSpringJoint)
            {
            
                case false :
                    hit.transform.gameObject.AddComponent<SpringJoint>().connectedBody = springConnection.GetComponent<Rigidbody>();
                    hit.transform.gameObject.AddComponent<Outline>().OutlineColor = Color.white;
                    hit.transform.gameObject.GetComponent<Outline>().OutlineWidth = 10;
                    grabbedObject = hit.transform.gameObject;
                    break;

                case true :
                    hit.transform.gameObject.GetComponent<SpringJoint>().connectedBody = springConnection.GetComponent<Rigidbody>();
                    hit.transform.gameObject.AddComponent<Outline>().OutlineColor = Color.white;
                    hit.transform.gameObject.GetComponent<Outline>().OutlineWidth = 10;
                    grabbedObject = hit.transform.gameObject;
                    break;
            }
            
        }

        else print("No object grabbed");
    }

    public void ReleaseObject()
    {

        //Destroy(grabbedObject.GetComponent<SpringJoint>().connectedBody);
        grabbedObject.GetComponent<SpringJoint>().connectedBody = null;
        Destroy(hit.transform.gameObject.GetComponent<Outline>());
        grabbedObject = null;
        hasSpringJoint = true;
        print("Object Released");
        StartCoroutine(WaitAfterRelease());
    }

    IEnumerator WaitAfterRelease()
    {
        releaseTimer = false;
        yield return new WaitForSeconds(2);
        releaseTimer = true;

        
    }

}
