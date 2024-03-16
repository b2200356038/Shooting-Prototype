using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimComponent : MonoBehaviour
{
    [SerializeField] Transform muzzle;
    [SerializeField] private float aimRange;
    [SerializeField] LayerMask aimLayerMask;
    public GameObject GetAimTarget()
    {
        RaycastHit hit;
        if (Physics.Raycast(muzzle.position, GetAimDirection(), out hit, aimRange, aimLayerMask))
        {
            return hit.collider.gameObject;
        }
        return null;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawRay(muzzle.position, GetAimDirection() * aimRange);
    }
    Vector3 GetAimDirection()
    {
       Vector3 muzzleDirection = muzzle.forward;
       return new Vector3(muzzleDirection.x, 0, muzzleDirection.z).normalized;
    }
}
