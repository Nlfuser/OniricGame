using UnityEngine;

public class RotateSprite : MonoBehaviour
{
    [HideInInspector] public bool isCompleted;
    private float _initialRotation;

    private void Start()
    {
        isCompleted = false;

        // Randomly rotate the sprite between -75 and -20 or between 20 and 75 degrees
        _initialRotation = Random.Range(0, 2) == 0 ? Random.Range(-75f, -20f) : Random.Range(20f, 75f);
        transform.rotation = Quaternion.Euler(0f, 0f, _initialRotation);
    }

    private void Update()
    {
        if (Input.GetMouseButtonUp(0) && !isCompleted)
        {
            // Check if the rotation is within the desired range
            if (Mathf.Abs(transform.rotation.eulerAngles.z) <= 2f || Mathf.Abs(transform.rotation.eulerAngles.z - 360f) <= 2f)
            {
                isCompleted = true;
                transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                ItemCounter.instance.UpdateObjective();
            }
            else // Reset isCompleted if the rotation is not within the desired range
                isCompleted = false;
        }
    }

    private void OnMouseDrag()
    {
        if (!isCompleted)
        {
            // Get mouse movement to adjust rotation
            var rotationAmount = Input.GetAxis("Mouse X") * 5f;
            transform.Rotate(Vector3.forward, rotationAmount);
        }
    }
}
