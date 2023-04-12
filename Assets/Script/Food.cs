using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{

    public BoxCollider2D foodArea;
    [SerializeField] GameManager manager;

    void Start()
    {
        SpawnFood();
    }

    private void SpawnFood(){
            Bounds bound = this.foodArea.bounds;
            Vector2 foodPosition;
            do {
                float x = Random.Range(bound.min.x ,bound.max.x);
                float y = Random.Range(bound.min.y ,bound.max.y);
                foodPosition = new Vector2(Mathf.Round(x),Mathf.Round(y));
            } while (manager.FoodCollidesWithSegment(foodPosition));
            
            this.transform.position = foodPosition;
        }
    
    


    private void OnTriggerEnter2D(Collider2D other) {

       if (other.tag == "Player"){
        SpawnFood();
       } 
    }
}
