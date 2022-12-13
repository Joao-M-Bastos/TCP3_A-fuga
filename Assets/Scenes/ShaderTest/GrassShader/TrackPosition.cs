using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackPosition : MonoBehaviour
{
    private GameObject _tracker;
    private Material _grassMat;
    private bool _isStep = false;

    void Start()
    {
        _grassMat = GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        if (_tracker != null)
        {
            Vector3 trackerPos = _tracker.GetComponent<Transform>().position;
            _grassMat.SetVector("_TrackerPosition", trackerPos);

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 9 && !_isStep)
        {
            _tracker = other.gameObject;
            Debug.Log("ëntroiu");
            _isStep = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == _tracker)
        {
            _tracker = null;
            _isStep = false;
        }
    }
}
