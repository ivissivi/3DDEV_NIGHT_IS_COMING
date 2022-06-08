using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    [SerializeField] private float mouseSensitivity;
    // Start is called before the first frame update

    private PlayerStats stats;

    private Transform parent;

    void Start()
    {
        stats = GetComponentInParent<PlayerStats>();
        parent = transform.parent;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        if(!stats.IsDead())
        {
            Rotate();
        }
        else if(Cursor.lockState == CursorLockMode.Locked)
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }

    void Rotate() 
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        parent.Rotate(Vector3.up, mouseX);
    }
}
