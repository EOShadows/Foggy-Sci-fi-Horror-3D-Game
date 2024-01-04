# Foggy-Sci-fi-Horror-3D-Game
Short Summary - 3D game I am working on using Unity 3D.  Horror genre where you play as a researcher stranded on planet covered in fog/dust storm, defending their self against other astronauts gone violent.

You are a researcher on a rust colored planet/moon caught in a dangerous situation.  Something has come over the other researchers making them insane, and they are trying to kill you.  Using your survival knife and skills in knife throwing, your goal is to survive in this now hostile environment.  The fog/dust storm however makes it so that you can only see your enemies when they get up close.

### Currently Implemented Features

- Character controller made by me, includes
  - bobbing motion as the player moves which changes when the player is running
  - The ability to jump, allowing the player to still move midair (as players seem to prefer this option) but with variables in the editor to adjust how much they can move.
- Thrown knife will stick (as if stabbed) to walls, the ground, and enemies.
- Thrown knife's angle will change according to velocity.  If the player were to throw the knife up high, they will later (once it comes back down) find it stabbed down into the ground.
- When enemies are killed by the player,
  - Enemies will change into ragdolls
  - A blood particle burst effect will appear where the player's knife hits the enemy.
  - Blood will appear on the ground where the enemy was killed, put there with a decal projector such that it will show on any surface, even if it is uneven.
- Enemy ragdoll bodies will despawn at a certain distance (such that the fog/dust storm pretty much completely hides them) in order to both declutter the play space as well as keep the game running smoothly.
- In case the player loses track of their knife once thrown, a compass appears to show them the direction of the knife.
- Player health which is shown to the player through cracks shown on the screen (supposed to be the player's helment being cracked by enemies).
- Dense fog, carefully adjusted along with the colors of the sky and ground to create the right environement for the mood and to allow enemies to hide in the fog.

At the moment the 3D model assets are used from the free Poly Angel - Space Pack by Poly Angel on the Unity Asset Store.  I edited the astronaut model in Blender for my purposes, adding a smooth shader to remove the poly aspect of the model, and making my own changes to the texture with Blender's texture paint in order to make it fit my needs in the horror game.  Current plans are to keep it this way as the astronaut model after my edits looks nice in the game, but I may still decide later on to update it to something I completely model myself.  The buildings I will absolutely change later on, likely to something I model myself.

Additional 3D model asset at the moment is the player's knife, which is from the free M9 Knife asset by Urbanity on the Unity Asset Store.  Current plans will likely be to change this to something I model myself to more accurately match a survival knife an astronaut might have.
