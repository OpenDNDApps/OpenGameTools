
# Basic scene flow

[Doc link 😄](https://github.com/VGDevs/VGDevs-UnityTools/blob/master/Documentation/Examples%20-%2001%20-%20Basic%20Scene%20Flow.md)

## What?

This is a basic or initial way to handle scenes in your project, an example on how to start the app or game so everything is organized and loaded correctly.

&nbsp;

## Why?

You may want to let your players know the app is still running while it is loading.
This also makes sure you load your systems only once (those that require to be unique of course), since you will NOT come back to these scenes again.

Examples of these might be: Audio/Music systems, User authentication, Event systems, Analytics, etc.

If you think you don't need this I still invite you to have it like this in your game, you never know that you might need it in the future :) just turn off all visuals and you'll never notice.

&nbsp;

## How?

The [AppInitialization](https://github.com/VGDevs/VGDevs-UnityTools/blob/master/Scripts/Core/SceneManagement/AppInitialization.cs) script handles the scene loading using the [SceneManager](https://docs.unity3d.com/ScriptReference/SceneManagement.SceneManager.LoadSceneAsync.html) provided by Unity.

![AppInitialization Config](https://user-images.githubusercontent.com/1507317/106372469-add5c900-634e-11eb-9b56-94447988d400.png "AppInitialization Config")

Remember to update the "Requirement Scene Name" and the "First Game Scene Name" variables on the script. 
You may also want to add those scenes to the Build Settings.

**Init Scene:**
> This is the first scene that should be loaded, it should be kept super lightweight, it should ONLY care about loading the next required stuff while presenting the player with something so they know the app is actually doing something and it is not frozen. like a ⌛

**Requirements Scene:**
> this is the second scene, this scene should have everything the app/game should NEED to run, but only the things that SHOULD BE loaded only once. Like managers, systems, etc.

**Any other Scene:**
> At last, your game as you see fit, what ever is needed for your app/game to run. 

```
+--------------+
|  Init Scene  | 
+------+-------+    
        |
        |   
        v
+--------------+
| Requirements | 
|    Scene     |     
+------+-------+
        |
        |  
        v
+--------------+        +-------------+
|  First Game  +------> |  Any Other  |
| Scene (menu?)| <------+  Game Scene |
+--------------+        +-------------+
```
