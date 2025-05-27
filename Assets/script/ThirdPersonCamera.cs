using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    public Transform target;          // Target yang diikuti kamera (biasanya player)
    public Vector3 offset = new Vector3(0, 3, -6); // Posisi offset dari target
    public float mouseSensitivity = 5f;

    public float minY = -35f;         // Batas rotasi vertikal atas
    public float maxY = 60f;          // Batas rotasi vertikal bawah

    private float currentX = 0f;
    private float currentY = 0f;

    void Start()
    {
        LockCursor();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            UnlockCursor();
        }
        else if (Input.GetMouseButtonDown(0)) // Klik kiri untuk kembali menyembunyikan cursor
        {
            LockCursor();
        }
    }

    void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }


    void LateUpdate()
    {
        if (target == null) return;

        // Input mouse
        currentX += Input.GetAxis("Mouse X") * mouseSensitivity;
        currentY -= Input.GetAxis("Mouse Y") * mouseSensitivity;
        currentY = Mathf.Clamp(currentY, minY, maxY);

        // Buat rotasi berdasarkan input
        Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);
        Vector3 desiredPosition = target.position + rotation * offset;

        // Update posisi dan arah kamera
        transform.position = desiredPosition;
        transform.LookAt(target.position);
    }
}
