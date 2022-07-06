using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopyTransform : MonoBehaviour
{
    [SerializeField] Transform target;

    private void Start()
    {
        target.transform.position = transform.position;
        target.transform.rotation = transform.rotation;
    }

    private void Update()
    {
        transform.position = target.position;
        transform.rotation = target.rotation;
    }
}
