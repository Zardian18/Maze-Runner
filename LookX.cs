using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookX : MonoBehaviour
{
    [SerializeField]
    private float _sens = 1f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float _mouseX = Input.GetAxis("Mouse X");

        transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y + (_mouseX * _sens), transform.localEulerAngles.z);

    }
}
