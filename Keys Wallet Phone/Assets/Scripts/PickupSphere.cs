using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupSphere : MonoBehaviour
{
    public Transform playerTransform;
    public float castRadius; 
    public float maxDistance;
    public LayerMask layerMask;
    public GameObject canvas;

    private RaycastHit hit;
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if(Physics.SphereCast(Camera.main.transform.position, castRadius,Camera.main.transform.forward, out hit, maxDistance, layerMask, QueryTriggerInteraction.UseGlobal))
        {
            canvas.SetActive(true);
            print("Press F to pick up " + hit.transform.gameObject.name);
            if(Input.GetKeyDown(KeyCode.F)) hit.transform.gameObject.GetComponent<PickableObject>().Pickup();
        }
        if(!Physics.SphereCast(Camera.main.transform.position, castRadius,Camera.main.transform.forward, out hit, maxDistance, layerMask, QueryTriggerInteraction.UseGlobal))
        {
            canvas.SetActive(false);
        }

    }
}
