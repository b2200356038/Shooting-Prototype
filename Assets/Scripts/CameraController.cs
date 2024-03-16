using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Transform playerTransform;
    [SerializeField] float TurnSpeed;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void LateUpdate()
    {
        transform.position = playerTransform.position;
    }
    public void AddYawInput(float yawInput)
    {
        transform.Rotate(Vector3.up, yawInput*Time.deltaTime*TurnSpeed);
    }
}
