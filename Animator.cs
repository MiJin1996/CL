using UnityEngine;
using UnityEngine.InputSystem;

public class Animal : MonoBehaviour
{
    public LayerMask animalLayer;
    public AudioSource audioSource;
    public AudioClip crySound;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Touchscreen.current != null)
        {
            var touch = Touchscreen.current.primaryTouch;
            if (touch.press.wasPressedThisFrame)
            {
                Vector2 touchPos = touch.position.ReadValue();

                Ray ray = Camera.main.ScreenPointToRay(touchPos);              //ScreenPointToRay화면을 뚫고 해당 오브젝트에 닿는것
                if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, animalLayer))
                {
                    if (hit.transform.IsChildOf(transform))
                    {
                        if (crySound != null && !audioSource.isPlaying)
                        {
                            audioSource.PlayOneShot(crySound);
                        }
                    }

                }
            }
        }
    }
}
