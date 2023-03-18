using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{

    public BoxCollider2D foodArea;
    // Start is called before the first frame update

    private void RandomPosition(){
        Bounds bound = this.foodArea.bounds;

        float x = Random.Range(bound.min.x ,bound.max.x);
        float y = Random.Range(bound.min.y ,bound.max.y);

        this.transform.position = new Vector3(Mathf.Round(x),Mathf.Round(y),0.0f);
    }

    void Start()
    {
        RandomPosition();
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D other) {

       if (other.tag == "Player"){
        RandomPosition();
       } 
    }
}
