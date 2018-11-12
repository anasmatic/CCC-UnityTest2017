# UnityProjectSample 
 <sub>first commit 2017<sub>
# Game: Snake
[game apk link](https://www.dropbox.com/s/hapyveh063y21gy/CCCTest0.2.apk?dl=0)
##### Overview:
This is a basic game prototype for mobile, uses swipes to control movement.
While building this game for the test, I decided to go to "code show off" direction, instead of making a fun game.

##### game hierarchy: 

 * Main
 	* Game
 		* Snake Layer
        * Fruits Layer
        * Walls Layer
 	* Menu
 		* Play Button
 		* Status Text 

and here is a quick preview of game structure and components

#### Dependency Injection:
as I should use MVC for UI interaction, but game UI was so simple that Adding MVC would complex things up ! , I chose to introduce the soul of MVC buy applying Dependency Injection along with Command pattern to communicate between the 3 Major Classes : ( MainManger , Game , Menu )

#### Snake as graph :
The snake (hero of this game) consist of parts connected together, a traditional approaches is to create the parts and add them to array, then loop it every time we need to move/remove/find part.
but it would be better if we treated our snake as data structure , in particular Linked list, not one of C# excessive performance ones, but let's create one with our functions
Every body part has reference to next and previous parts, and thus we can iterate over the snake and move it with no for loops or arrays.

#### InputHandler Command Pattern :
The best way to handle inputs in games is to use Command pattern, this can allow adding more inputs easily and handling different states of inputs with state machine.

#### Fruits Factory :
It may look strange that a Factory would produce 1 fruit every 5 seconds ! , but if we think about adding blue fruit , green fruit, and even black fruit , the factory will be a great help and we will appreciate its existence.

#### Fruits pool :
one of the most essential techniques in games development is to use pools to avoid creating and destroying object, because the creation and destruction of objects is expensive in matters of performance.

#### Singletons :
Some developers think Singleton is the pure evil of the patterns world ! , but actually it is a great help when we want to communicate with tight coupled classes, like Map.

#### Observers Of the Observable :
Important pattern , Used to notify the snake that it eat a fruit and should grow !
we can do that without keeping a reference of the player in every enemy by using the Observer pattern.

#### UnityTest ! :
While I was working on Map class, I found a good reason to use TDD, as the Map class is responsible of helping the FruitsFactory to create a Fruit that won't overlaps snake body.
Also test unit was set to test pool size count.


