using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class buttonVR : MonoBehaviour
{
    public UnityEvent onPress;
    public UnityEvent onRelease;
    public bool isButtonToggle;
    bool isPressed;
    bool wasReleased;
    GameObject button;
    GameObject presser;
#pragma warning disable CS0108 // Deprecated warning
    Rigidbody rigidbody;
#pragma warning restore CS0108 // Deprecated warning
    Vector3 buttonUp;

    // Start is called before the first frame update
    void Start()
    {
        button = gameObject.transform.GetChild(0).gameObject;
        isPressed = false;
        wasReleased = true;

        rigidbody = button.GetComponent<Rigidbody>();
        buttonUp = button.transform.up;
        rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
        if (Mathf.Round(buttonUp.x * 1000) == 0)
            rigidbody.constraints |= RigidbodyConstraints.FreezePositionX;
        if (Mathf.Round(buttonUp.y * 1000) == 0)
            rigidbody.constraints |= RigidbodyConstraints.FreezePositionY;
        if (Mathf.Round(buttonUp.z * 1000) == 0)
            rigidbody.constraints |= RigidbodyConstraints.FreezePositionZ;
    }

    private void Update()
    {
        if (button.transform.localPosition.y < 2.5f)
            button.transform.localPosition = new Vector3(0, 2.5f, 0);
        if (button.transform.localPosition.y > 4f)
            button.transform.localPosition = new Vector3(0, 4f, 0);

        button.transform.localPosition = new Vector3(0, button.transform.localPosition.y, 0);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!wasReleased)
            return;

        if (isButtonToggle)
        {
            if (isPressed)
            {
                onRelease.Invoke();
                isPressed = false;
                return;
            }
            presser = other.gameObject;
            onPress.Invoke();
            isPressed = true;
            return;
        }
        if (!isPressed)
        {
            presser = other.gameObject;
            onPress.Invoke();
            isPressed = true;
            return;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!isButtonToggle && other.gameObject == presser)
        {
            onRelease.Invoke();
            isPressed = false;
        }
        wasReleased = true;
    }

    public bool getIsPressed()
    {
        return isPressed;
    }
}
