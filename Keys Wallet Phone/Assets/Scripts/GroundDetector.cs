using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundDetector : MonoBehaviour {
    private PlayerStateManager psm;
    private bool touchingGround;
    private Vector3 groundNormal = Vector3.zero;
    private float slopeAngle;
    private int colliders = 0;
    private Vector3 contact = Vector3.zero;

    private void Start() {
        psm = GetComponentInParent<PlayerStateManager>();
    }

    void FixedUpdate() {
        if (touchingGround)
            psm.BecomeGrounded(groundNormal);
        else if (!touchingGround && psm.Grounded)
            psm.BecomeAirborne();

    }

    private void OnTriggerEnter(Collider other) {
        colliders++;
    }

    private void OnTriggerStay(Collider other) {
        CheckSphereExtra(other, GetComponent<SphereCollider>(), out Vector3 newContact, out Vector3 newNormal);
        if ((newContact - transform.position).magnitude < (contact - transform.position).magnitude) {
            slopeAngle = Mathf.Rad2Deg * Mathf.Acos(Mathf.Clamp01(Vector3.Dot(newNormal, Vector3.up)));
            if (slopeAngle < 60.0f) {
                touchingGround = true;
                contact = newContact;
                groundNormal = newNormal;
                psm.BecomeGrounded(groundNormal);
            }
            else if ((contact - transform.position).magnitude > GetComponent<SphereCollider>().radius) {
                contact = Vector3.zero;
                touchingGround = false;
            }
        }
    }

    private void OnTriggerExit(Collider other) {
        colliders--;
        if (colliders == 0) {
            touchingGround = false;
            contact = Vector3.zero;
        }
    }

    //CheckSphereExtra trouve le point de contact le plus proche sur un Collider générique (pas nécessairement convexe) ainsi que la normale de la surface
    //merci stackoverflow
    private bool CheckSphereExtra(Collider target_collider, SphereCollider sphere_collider, out Vector3 closest_point, out Vector3 surface_normal) {
        closest_point = Vector3.zero;
        surface_normal = Vector3.zero;
        float surface_penetration_depth = 0;

        Vector3 sphere_pos = sphere_collider.transform.position;
        if (Physics.ComputePenetration(target_collider, target_collider.transform.position, target_collider.transform.rotation, sphere_collider, sphere_pos, Quaternion.identity, out surface_normal, out surface_penetration_depth)) {
            closest_point = sphere_pos + (surface_normal * (sphere_collider.radius - surface_penetration_depth));

            surface_normal = -surface_normal;

            return true;
        }

        return false;
    }
}
