using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{

    /* in order to the Snake moves automatticly in a direction, i'm goig to need two variables: Speed and direction.
        So when de player press a Key change de direction.
    */
     
    private Vector2 direction = Vector2.left; //default snake direction
    private int initialSize = 4;

    // for handle the Snake's segments
    public Transform segment;
    private List<Transform> segments = new List<Transform>();


    // for handle the velocity :
    private float speed = 15f;   // for more velocity increase it's value. Values between 5 to 20/5 (25 level GOOD)
    private float speedMultiplier = 1f;
    private float nextUpdate;



   private void Start() {
      ResetState();
      
   }
  
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
        
      // Investigate why doesn't work the speed of the object if I change de value of speed variable !!!
      // Debug.Log("direction: " + direction);
      // Debug.Log("transform.position: " + transform.position);

      // Vector3 newPosition = transform.position + (Vector3)(direction * speed * (Time.fixedDeltaTime * speed));
      // Debug.Log("newPosition: " + newPosition);
      
      // if (direction.x < 0) {
      //    newPosition.x = Mathf.Floor(newPosition.x);
      // } else if (direction.x > 0) {
      //    newPosition.x = Mathf.Ceil(newPosition.x);
      // }
      // if (direction.y < 0) {
      //    newPosition.y = Mathf.Floor(newPosition.y);
      // } else if (direction.y > 0) {
      //    newPosition.y = Mathf.Ceil(newPosition.y);
      // }
      // Debug.Log("newPosition arounded: " + newPosition);
      // transform.position = newPosition;
      //-------------------------------------------------------------------------------------------------------------

      // Debug.Log("direction: " + direction);
      // Debug.Log("transform.position: " + transform.position);
      // Debug.Log("Mathf.Round(transform.position.x) + (direction.x * speed * Time.fixedDeltaTime) --> " + Mathf.Round(transform.position.x) +" + " + (direction.x * speed * Time.fixedDeltaTime) + " = " + (Mathf.Round(transform.position.x) + (direction.x * speed * Time.fixedDeltaTime)));
      // Debug.Log("Mathf.Round(transform.position.y) + (direction.y * speed * Time.fixedDeltaTime) --> " + Mathf.Round(transform.position.y) +" + " + (direction.y * speed * Time.fixedDeltaTime)+ " = " + (Mathf.Round(transform.position.y) + (direction.y * speed * Time.fixedDeltaTime)));

      // this.transform.position = new Vector3(
      //    Mathf.Round(Mathf.Round(transform.position.x) + (direction.x * speed * Time.fixedDeltaTime)),
      //    Mathf.Round(Mathf.Round(transform.position.y) + (direction.y * speed * Time.fixedDeltaTime)),
      //    0
      // );

      //Wait until the next update before proceeding
      
      Debug.Log("Time.Time: " + Time.time);
      Debug.Log("nextUpdate: " + nextUpdate);

        if (Time.time < nextUpdate) {
            return;
        }

      // Set each segment's position to be the same as the one it follows. We
      // must do this in reverse order before we move the head.
        for (int i = segments.Count - 1; i > 0; i--) {
            segments[i].position = segments[i - 1].position;
        }

        // Move the snake in the direction it is facing
        
        float x = transform.position.x + direction.x;
        float y = transform.position.y + direction.y;

        // Round the values to ensure it aligns to the grid. The rounding depends on the current direction of the snake.

         if (direction.x < 0) {
            x = Mathf.Floor(x);
         } else if (direction.x > 0) {
            x = Mathf.Ceil(x);
         }
         if (direction.y < 0) {
            y = Mathf.Floor(y);
         } else if (direction.y > 0) {
            y = Mathf.Ceil(y);
         }
         
        transform.position = new Vector2(x, y);
        nextUpdate = Time.time + (1f / (speed * speedMultiplier));
    } 
    

    private void Grow(){
         Transform newSegment = Instantiate(this.segment);
         newSegment.position = segments[segments.Count - 1].position;

         this.segments.Add(newSegment);
    }
    private void OnTriggerEnter2D(Collider2D other) {

       if (other.tag == "Food"){
         Grow();
       } else if ( other.tag == "Obstacle"){
         ResetState();
       } else if (other.tag == "Wall") {
            if (direction == Vector2.down) {
               this.transform.position = new Vector2(this.transform.position.x,
                                                      - (other.transform.position.y + 1));
            } else if ( direction == Vector2.up){
               this.transform.position = new Vector2(this.transform.position.x,
                                                      - (other.transform.position.y - 1));
            } else if (direction == Vector2.left) {
               this.transform.position = new Vector2(- (this.transform.position.x + 1 ),
                                                      this.transform.position.y);
            } else if (direction == Vector2.right) {
               this.transform.position = new Vector2(- (this.transform.position.x - 1 ),
                                                      this.transform.position.y);
            }
       }
    }

    private void ResetState(){
      this.direction = Vector2.left;
      this.transform.position = Vector2.zero;

      // destroy all the element of the snake except the head
      for (int i = 1; i < segments.Count; i++) {
         Destroy(segments[i].gameObject);
      }

      // Removes all elements from the snake
      this.segments.Clear();

      // Add the head
      this.segments.Add(this.transform);

      // enlarge the snake to the initial size
      for (int i = 0; i < initialSize; i++){
         Grow();
      }


    }
}


/*
      - Time.fixedDeltaTime is a float representing the amount of time (in seconds) since the last FixedUpdate call. 
         It is used to ensure that the Snake moves at a consistent speed regardless of the frame rate of the game.

      - (direction * speed * Time.fixedDeltaTime) multiplies the direction vector by the speed variable and Time.fixedDeltaTime 
         to get a vector that represents the amount of movement that should be applied to the Snake in this fixed update.


      TO DO:
       - funcion que verifiqeu cantidad de alimento comido para modificar velocidad
       - funcion para crecer cuando come comida
       - colisiones
       - resetear start
*/