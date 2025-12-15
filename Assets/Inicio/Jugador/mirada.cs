using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class mirada : MonoBehaviour
{
    public GameObject Robert;
    public float speed = 100f;
    private float girox = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    
    void Update()
    {
        float x = Input.GetAxis("Mouse X") * speed * Time.deltaTime;
        float y = Input.GetAxis("Mouse Y") * speed * Time.deltaTime;

        girox -= y;
        girox = Mathf.Clamp(girox, -90f, 90f);

        transform.localRotation = Quaternion.Euler(girox, 0 ,0);
        Robert.transform.Rotate(Vector3.up * x);

    }
}
