# Asteroids Clone
This is a retro Asteroids Clone that I built to learn C# in a fun way.  
I used the awesome SFML graphics library with it's C# bindings.

[Here's a quick video of it in action:](https://www.youtube.com/watch?v=karFnycGjbY)

## Key Ideas

### Abstract Class
The main abstract class within my application was the Entity class. An Entity represents 
essentially any object that would be floating on the screen. That means Asteroids, Projectiles,
Ships. Making all these objects inherit from Entity made it really easy to write DRY code, since 
they share so many properties and methods.

### Entity Containers
I added and removed entities from the screen by using dictionaries with a unique entity ID as a key
and the entity object itself as a value. This made adding and removing entities a O(1) operation. It
also made removing items very safe since I didn't have to worry about removing an item twice or something.

### Collisions
I avoid most of computationally expensive collision checks by only using circle-circle and 
triangle vertices vs circle collision checks. The main collision check I am avoiding is
triangle edge vs circle collision checks, this requires lots of linear algebra calculations.
I can discard circle within triangle collision checks since the player ship will never be larger
than the smallest asteroid.

