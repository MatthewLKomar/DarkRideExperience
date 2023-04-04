using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBoxManager : MonoBehaviour
{
    public GameObject[] boxes;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BoxSomething(int counter)
    {
        // do something with the boxes 
        //for (int i = 0; i < boxes.Length; i += 1)
        //{
        //    print(boxes[i].name);
        //}

        if (counter < boxes.Length)
        {
            print(boxes[counter].name);
        }

    }
}
