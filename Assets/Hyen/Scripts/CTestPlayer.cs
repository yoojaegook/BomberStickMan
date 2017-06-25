using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CTestPlayer : MonoBehaviour {
    Rigidbody2D _rigidbody2D;
    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }
    // Use this for initialization
    void Start () {
        //_rigidbody2D.velocity = Vector2.right * 100;
        StartCoroutine(WaitStart());

    }

    IEnumerator WaitStart()
    {
        yield return new WaitForSeconds(3f);
        _rigidbody2D.velocity = Vector2.right * 100;
        Debug.Log("출발");
    }
}


