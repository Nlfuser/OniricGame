using UnityEngine;

public class RotateSprite : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private float initialRotation;
    private bool isRotating;
    private bool isCompleted;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        isCompleted = false;

        // Randomly rotate the sprite between -30 and -15 or between 15 and 30 degrees
        initialRotation = Random.Range(0, 2) == 0 ? Random.Range(-30f, -15f) : Random.Range(15f, 30f);
        transform.rotation = Quaternion.Euler(0f, 0f, initialRotation);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isCompleted)
        {
            isRotating = true;
        }

        if (isRotating)
        {
            // Get mouse movement to adjust rotation
            float rotationAmount = Input.GetAxis("Mouse X") * 5f;
            transform.Rotate(Vector3.forward, rotationAmount);
        }

        if (Input.GetMouseButtonUp(0))
        {
            // Stop rotation adjustment on mouse release
            isRotating = false;

            // Check if the rotation is within the desired range
            if (Mathf.Abs(transform.rotation.eulerAngles.z) <= 3f || Mathf.Abs(transform.rotation.eulerAngles.z - 360f) <= 3f)
            {
                isCompleted = true;
            }
            else
            {
                // Reset isCompleted if the rotation is not within the desired range
                isCompleted = false;
            }
        }
    }
}
