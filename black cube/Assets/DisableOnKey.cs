using UnityEngine;

public class DisableOnKey : MonoBehaviour
{
    public GameObject description; // Ссылка на объект с описанием

    void OnEnable()
    {
        if (description != null)
            description.SetActive(true);
    }

    void OnDisable()
    {
        if (description != null)
            description.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            gameObject.SetActive(false);
        }
    }
}