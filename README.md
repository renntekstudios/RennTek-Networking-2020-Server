[RennTek-Networking-2020](https://github.com/renntekstudios/RennTek-Networking-2020-Server.git)
=================

A Networking system build server/client side of original renntek studios networking library. Complete revamp to 2020


Author
-------

**RennTek Studios Limited**

+ [Website](http://renntekstudios.co.uk)
+ [Twitter](https://twitter.com/renntekstudios)
+ [Youtube](https://www.youtube.com/channel/UCM5gszt2uUex0XnEDpIivpg)
+ [Facebook](https://www.facebook.com/renntekstudios)
+ [Instagram](https://www.instagram.com/renntekstudioslimited/)
+ [Github](https://github.com/renntekstudios)

![Console Sample](Screenshots/console_sample.PNG)

## Master server

### Requirements

- NodeJS v13 or newer

A master server can track a number of servers and check if they are available.
Simply run `node master-server --help` from the project to see the usage.

When a client connects, the master responds with a JSON list of objects as { host: 'server1', port: 'server1port' }


