using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class InteractiveShadow : MonoBehaviour
{
    [SerializeField] private Transform shadowTransform;

    [SerializeField] private Transform lightTransform;
    private LightType lightType;

    [SerializeField] private LayerMask targetLayerMask;
    [SerializeField] private Vector3 extrusionDirection = Vector3.zero;

    private Vector3[] objectVerticies;
    private Mesh shadowColliderMesh;
    private MeshCollider shadowCollider;
    private void Awake()
    {
        InitializeShadowCollider();
        lightType = lightTransform.GetComponent<Light>().type;
        objectVerticies  = transform.GetComponent<MeshFilter>().mesh.vertices.Distinct().ToArray();
        shadowColliderMesh = new Mesh();
    }
    private void Update()
    {
        shadowTransform.position = transform.position;
    }
    private void FixedUpdate()
    {
        shadowColliderMesh.vertices = ComputeShadowColliderMeshVertices();
        shadowCollider.sharedMesh = shadowColliderMesh;
    }
    private void InitializeShadowCollider()
    {
        GameObject shadowGameObject = shadowTransform.gameObject;
        shadowGameObject.hideFlags = HideFlags.HideInHierarchy;
        shadowCollider = shadowGameObject.AddComponent<MeshCollider>();
        shadowCollider.convex = true;
        shadowCollider.isTrigger = true;
    }
    private Vector3[] ComputeShadowColliderMeshVertices()
    {
        Vector3[] points = new Vector3[2 * objectVerticies.Length]; 
        Vector3 raycastDirection = lightTransform.forward;

        int n = objectVerticies.Length;

        for(int i = 0;i<n;i++)
        {
            Vector3 point = transform.TransformPoint(objectVerticies[i]);
            if(lightType != LightType.Directional)
            {
                raycastDirection = point - lightTransform.position;
            }
            points[i] = ComputeIntersectionPoint(point, raycastDirection);
            points[n+i] = ComputeExtrusionPoint(point, points[i]);
        }
        return points;
        
    }
    private Vector3 ComputeIntersectionPoint(  Vector3 fromPosition  , Vector3 direction ) 
    { 
        RaycastHit hit;

        if(Physics.Raycast(fromPosition, direction, out hit , Mathf.Infinity , targetLayerMask)) 
        {
            return hit.point - transform.position;
        }
        return fromPosition + 100 * direction - transform.position;
    }
    private Vector3 ComputeExtrusionPoint ( Vector3 objectVertexPosition , Vector3 shadowPointPosition ) 
    {
        if(extrusionDirection.sqrMagnitude == 0)
        {
            return objectVertexPosition - transform.position;
        }
        return shadowPointPosition + extrusionDirection;
    }
}

