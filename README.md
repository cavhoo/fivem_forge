#City of Mind RP

This is a server framework written in C# with the help of the FiveM Multiplayer Framework for GTA5/RDR2.

It's supposed to be a turnkey solution for running your own server, with everthing bundled in a bunch of DLL files,
that you drop into your server and fire it up.

The solution contained in this repository is consisting of two projects:

* Client
- Holds all interaction and UI elements used on the City of Minds RP server

* Core
- Holds all the server logic, which handles login, sessions, characters, money etc.



Most of the stuff is running server side, and therefore makes the client leaner.
This will also make it easier to synchronize between clients because everything is
coming from a single source of truth.
