using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyDamageTextPopUp : MonoBehaviour
{
    public Transform camera;
    // Start is called before the first frame update
    void Start()
    {
        camera = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        Destroy(gameObject, 3f);
    }

    private void LateUpdate()
    {
        transform.rotation = camera.transform.rotation;
    }
}
