using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Narraration : MonoBehaviour
{
    // Start is called before the first frame update
    private AudioSource audioSource;
    public AudioClip clip;
    public float toLoad;

    IEnumerator timer()
    {
        yield return new WaitForSeconds(toLoad);
        SceneManager.LoadScene("scenes/Map1");
    }
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.PlayOneShot(clip);
        StartCoroutine(timer());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
