# City of Mind RP

This is the code that will power our state of the art GTA V RP Servier.

We work with C# on the back- and frontend, the base is currently the FiveM Framework that's widely known.

The project has two sub components, one is the client and the other one is the server.

## Goal

Creating a RP Server system ontop of FiveM but keep it decoupled as much as possible.
Sadly this is not really possible on the client side as we are heavily relying on the intergration 
of the FiveM api to do our stuff.

On the server end, things are quite different there we managed to decouple it almost entirely so
that switching the server to different modding framework should be a work of a couple of days and not weeks
or months.


## Client

This project holds all the logic, huds, and menus that are visible on the client side.
It losely follows a MVC pattern, since we cannot really use Controllers due to the way
FiveM starts their C# scripts.

But we try to make the best of it in terms of performance, and filesize.


## Server

This project is the backbone of the whole RP Server, here the magic happens that makes City of Minds 
what it is. A tryuly unique experience.

The server also losely follows MVC, in that regards that each Controller is only responsible for a very specific
task. If something needs to be done with Money, then either the ATMController or the BankController will take care of it.

If it's regarding a Character then the CharacterController takes over.

The server part is using EnityFramework to talk to a PostgresSQL database, this ensures easy to read and very maintainable database 
queries. 



