# Brief Description
A little game I'm building to my portifolio. The game is being built using Modular customizable systems. Right now I have:

    - A movement system controlled by an event and use reflection to pass data from the object to a special function called behaviour that defines -as the name says - the behaviour of a step on the pipeline.

    - A procedural gun system also oriented by behaviours. This system allow the user to build several guns based on a scriptableObjects. The bullets spawning are controlled by some special functions that are mapped using an enum.