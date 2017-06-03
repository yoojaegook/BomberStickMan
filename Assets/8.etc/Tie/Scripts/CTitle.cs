using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class CTitle : MonoBehaviour {

    public bool first;

    void Start()
    {
        if(first)
        {
            SceneManager.LoadScene("Title");
        }    
    }
	public void OnGameScene()
    {
        StartCoroutine(NextScene());
    }
    IEnumerator NextScene()
    {
        FadeFilter.instance.FadeOut(Color.black,0.6f);
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("Shoot");
    }
}
