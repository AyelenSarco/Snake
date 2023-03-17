using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{

    /* in order to the Snake moves automatticly in a direction, i'm goig to need two variables: Speed and direction.
        So when de player press a Key change de direction.
    */
    [SerializeField] private float speed = 2f;
    public float speedMultiplier = 1f;
    private float nextUpdate;
    private Vector2 direction = Vector2.left; //default snake direction

  
    private void Update() {
      if (Input.GetKeyDown(KeyCode.A) && direction != Vector2.right){
         direction = Vector2.left; 
      } 
      else if (Input.GetKeyDown(KeyCode.D) && direction != Vector2.left){
         direction = Vector2.right;
      } 
      else if (Input.GetKeyDown(KeyCode.W) && direction != Vector2.down){
         direction = Vector2.up;
      } 
      else if (Input.GetKeyDown(KeyCode.S) && direction != Vector2.up){
         direction = Vector2.down;
      }   


      
    }
    

    private void FixedUpdate() {
        
      // Investigate why doesn't work !!!
      // Vector3 newPosition = transform.position + (Vector3)(direction * speed * Time.fixedDeltaTime);
      // Debug.Log(transform.position + " + " +  Mathf.Ceil(newPosition.x) + "," + Mathf.Ceil(newPosition.y) + "," + 0f);
      // transform.position = new Vector3(Mathf.Ceil(newPosition.x), Mathf.Ceil(newPosition.y), 0f);
      

      // Can I manage the speed of the snake with this code??
      // this.transform.position = new Vector3(
      //    Mathf.Round(this.transform.position.x) + direction.x  ,
      //    Mathf.Round(this.transform.position.y) + direction.y  ,
      //    0
      // );
      // ----------------------------------------------------------------------------

      // this.transform.position += new Vector3(
      //    direction.x * speed * Time.fixedDeltaTime,
      //    direction.y * speed * Time.fixedDeltaTime,
      //    0
      // );

      // if (Time.time < nextUpdate) {
      //   return;
      // }

      // Set each segment's position to be the same as the one it follows. We
      // must do this in reverse order so the position is set to the previous
      // position, otherwise they will all be stacked on top of each other.
      // for (int i = segments.Count - 1; i > 0; i--) {
      //    segments[i].position = segments[i - 1].position;
      // }

      // Move the snake in the direction it is facing
      // Round the values to ensure it aligns to the grid
      float x = Mathf.Round(transform.position.x) + direction.x;
       float y = Mathf.Round(transform.position.y) + direction.y;

      transform.position = new Vector2(x, y);
      //nextUpdate = Time.time + (1f / (speed * speedMultiplier));
    }
     
        
   
    
    
}


/*
      - Time.fixedDeltaTime is a float representing the amount of time (in seconds) since the last FixedUpdate call. 
         It is used to ensure that the Snake moves at a consistent speed regardless of the frame rate of the game.

      - (direction * speed * Time.fixedDeltaTime) multiplies the direction vector by the speed variable and Time.fixedDeltaTime 
         to get a vector that represents the amount of movement that should be applied to the Snake in this fixed update.
*/