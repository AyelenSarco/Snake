using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{
   [SerializeField] GameManager manager;
    private Vector2 direction = Vector2.left; //default snake direction
    private int initialSize = 4;
    public Transform segment;
    private List<Transform> segments = new List<Transform>();

    private bool inputDisabled;
    // for handle the velocity :
    [SerializeField] private float speed = 10f;   // for more velocity increase it's value. Values between 5 to 20/5 (25 level GOOD)
    private float speedMultiplier = 1f;
    private float nextUpdate;



   private void Start() {
      ResetState();
      inputDisabled = false;
      
   }
  
    private void Update() {
      if (!inputDisabled){
         if ((Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) && direction != Vector2Int.right)
         {
               
            direction = Vector2Int.left;
            inputDisabled = true;
         }
         else if ((Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) && direction != Vector2Int.left)
         {

            direction = Vector2Int.right;
            inputDisabled = true;               
         }
         else if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) && direction != Vector2Int.down)
         {
               
            direction = Vector2Int.up;
            inputDisabled = true;
         }
         else if ((Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) && direction != Vector2Int.up)
         {

            direction = Vector2Int.down;
            inputDisabled = true;   
         }

         
         Invoke("EnableInput", 0.02f);
    }
       
   }
    
   private void EnableInput(){
      this.inputDisabled = false;
   }

    private void FixedUpdate() {
        

      //Wait until the next update before proceeding
      
      // Debug.Log("Time.Time: " + Time.time);
      // Debug.Log("nextUpdate: " + nextUpdate);
      Debug.Log("Snake head position: " + transform.position.x + "," + transform.position.y );
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
         manager.AddScore();
       } else if ( other.tag == "Obstacle"){
         ResetState();
         manager.OnLose();
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

   // move to manager
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

      this.speed = 10;

    }


    public bool IsCollidingWithFood(Vector2 position) {
        foreach (Transform segment in segments) {
            if (segment.position == (Vector3)position) {
               return true;
            }
        }
        return false;
    }

   public void increaseSpeed() {
      if (this.speed < 30f){
         this.speed += 1f;
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