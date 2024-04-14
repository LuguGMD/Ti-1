using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [Header("Movement")]
    Transform pivot;

    float horizontalInput;
    float verticalInput;

    public float speed;
    public float floatSpeed;

    public float floatHeight;
    public float xBoundary;
    public float zBoundary;

    float xOffset;
    float zOffset;
    float yOffset;

    // Start is called before the first frame update
    void Start()
    {
        //Getting the camera controller position
        pivot = CameraController.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        //Getting player's input
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        //Moving the player based on the pivot point
        xOffset += horizontalInput * speed * Time.deltaTime;
        zOffset += verticalInput * speed * Time.deltaTime;

        //Making the player float
        yOffset = Mathf.Cos(Time.fixedTime * floatSpeed) * floatHeight + floatHeight;
    }

    private void LateUpdate()
    {
        //Clamping player position
        xOffset = Mathf.Clamp(xOffset, -xBoundary, xBoundary);
        zOffset = Mathf.Clamp(zOffset, -zBoundary, zBoundary);

        //Getting the offset in all directions into one Vector3
        Vector3 offset = pivot.right * xOffset + pivot.forward * zOffset + pivot.up * yOffset;

        //Updating the player position
        transform.position = pivot.position + offset;

    }
}
