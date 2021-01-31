using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObject : MonoBehaviour
{   
    RaycastHit hit;
    public int castDist;
    public GameObject springConnection;
    public GameObject grabbedObject;
    public GameObject lookingAt;
    public LayerMask layerMask;

    private SpringJoint springJoint;
    private float originalAngularDrag;
    private SimpleMouseLook mouse;

    // Start is called before the first frame update
    void Start()
    {
        castDist = 2;
        springJoint = springConnection.GetComponent<SpringJoint>();
        mouse = GetComponent<SimpleMouseLook>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonUp(0) && grabbedObject != null) ReleaseObject();
        if(Input.GetMouseButtonDown(0) && grabbedObject == null) GrabObject();

        bool raycast = Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, castDist, layerMask, QueryTriggerInteraction.UseGlobal);
        if (raycast && grabbedObject == null && lookingAt == null)
        {
            lookingAt = hit.transform.gameObject;
            SetOutline(lookingAt, true, Color.blue);
        }
        if(!raycast && grabbedObject == null && lookingAt != null)
        {
            SetOutline(lookingAt, false, Color.blue);
            lookingAt = null;
        }
    }

    public void GrabObject()
    {
        if(lookingAt != null)
        {
            print("Object Grabbed");
            grabbedObject = lookingAt;

            mouse.mouseSensitivityMultiplier = 0.5f;
            Rigidbody grabbedObjectRigidbody = grabbedObject.GetComponent<Rigidbody>();
            originalAngularDrag = grabbedObjectRigidbody.angularDrag;
            grabbedObjectRigidbody.angularDrag = 100.0f;
            springJoint.connectedBody = grabbedObjectRigidbody;
            springJoint.connectedAnchor = grabbedObject.transform.InverseTransformPoint(hit.point);
            SetOutline(grabbedObject, true, Color.white);            
        }

        else print("No object grabbed");
    }

    public void ReleaseObject()
    {
        springJoint.connectedBody.angularDrag = originalAngularDrag;
        float velocity = Mathf.Clamp(springJoint.connectedBody.velocity.magnitude, 0.0f, 10.0f);
        springJoint.connectedBody.velocity = springJoint.connectedBody.velocity.normalized * velocity;
        springJoint.connectedBody = null;
        SetOutline(grabbedObject, true, Color.blue);
        grabbedObject = null;
        mouse.mouseSensitivityMultiplier = 1.0f;

        print("Object Released");
    }

    void SetOutline(GameObject obj, bool enabled, Color color, float width = 10.0f) {
        Outline outline = obj.GetComponent<Outline>();
        if (outline == null) {
            obj.AddComponent<Outline>();
            outline = obj.GetComponent<Outline>();
        }
        outline.enabled = enabled;
        outline.OutlineColor = color;
        outline.OutlineWidth = width;
    }
}
