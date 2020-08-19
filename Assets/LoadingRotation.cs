using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingRotation : MonoBehaviour
{

    [SerializeField]
    GameObject _loadingImage;
    [SerializeField]
    float speed = 1f;

    IEnumerator Start()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.001f);
            _loadingImage.transform.Rotate(Vector3.forward * speed);
        }
        
    }

    
}
