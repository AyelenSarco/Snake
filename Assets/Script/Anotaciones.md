
 ## Snake:

  - Scale: 1,1,1 
  - Order in layer: 1

 ### Components:
- Setting the Rigidbody to "is Kinematic" allows us to have more precise control over the movement of the snake through script, without any unwanted physics simulation.
- Box Collider 2D: 
    - isTrigger --> true.
    - size of the collider: x = 0,72  y = 0,75 in order that when the snake eats food and grows up de colliders do not touch each other.
- Script Snake