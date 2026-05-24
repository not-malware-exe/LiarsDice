# Liars Dice

The goal for my project is to get an online multiplayer game of Liars Dice working, which I have not achieved yet.
Currently my project is able to run a server and several clients that can connect to the server locally on my computer. The server will assign an id to the clients and keep a list of clients up to date for each client (clients can know when and what client joins/leaves the server). All clients can control the position of their own icon and all client can see each other's icons move.

## Instructions for Build and Use

Steps to build and/or run the software:

1. Open test_network.tscn in the Godot engine to view test_network scene.
2. Open the Debug tab > Costumize Run Instances.
3. Enable Multiple Instance and set instances to 2 or more and press "OK".
4. Press the "Run Current Scene" button to start the run instances.
5. Move the windows to where you can see all of them.

Instructions for using the software:

1. Press the server button on one window (and only one) to host a server on your local IPAddress and a randomly chosen port. A client will also be created on the window and connected tot he server.
2. Press the client buttons to create a client on the other windows that will connect to the server. 
3. Click on a window and click ASWD to move the icon that is created for each client. The icon should move on each window.
4. Close a client window to disconnect client (should take a couple of seconds for the server to disconnect non-existant client.)
5. Close the server window to close server. Any remaining clients will start pushing errors in the console and will no longer be able to update icon positions with eachother.
6. Close windows or press the "Stop Running Project" button in the Godot window to stop program instances.

## Development Environment

To recreate the development environment, you need the following software and/or libraries with the specified versions:

* Windows 11
* .Net 8
* VSCode 1.116
* Godot 4.6.stable.mono .Net

## Useful Websites to Learn More

I found these websites useful in developing this software:

* Google Gemini
* Youtube - IcyEngine - Godot Multiplayer Tutorial: Low-Level API (https://www.youtube.com/watch?v=8GfJw0E5MFE)
* Godot Engine 4.6 documentation in English - All classes - UPNP (https://docs.godotengine.org/en/stable/classes/class_upnp.html) (Will be useful later when I make the game able to run online)

## Future Work

The following items I plan to fix, improve, and/or add to this project in the future:

* I don't exactly have a plan, but I will try to get Liar's dice to work next sprint.
* Grey box UI
* Add cards
* Create 3D assets and animations
* Create UI art