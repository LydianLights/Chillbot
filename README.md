*This project is currently in development -- it is not in a complete state*

# ChillBot

#### _A friendly, multipurpose Discord chat bot_

## Description

ChillBot is a simple Discord chat bot built for my own entertainment. ChillBot's features mostly consist of things I found amusing to implement, and there's really no strong direction to its development.

## Source Code

ChillBot targets .NET Core 2.0 (you can get the .NET Core SDK [here](https://www.microsoft.com/net/download/windows)). Additionally, ChillBot is built using the [Discord.NET](https://github.com/RogueException/Discord.Net) wrapper for the Discord API. I highly recommend reading the documentation there to understand the source code structure.

ChillBot uses a variety of APIs, currently including:
* [Discord (duh)](https://discordapp.com/developers/docs/intro)
* [Giphy](https://developers.giphy.com/)

In order to run a ChillBot of your own, you must go to the provided links and register applications with each API provider. Once that is done, ChillBot requires a configuration file called `bot-secrets.json`, which must be placed in the `./ChillBot/` directory. The file must have the following structure:

```json
{
  "Discord": {
    "ProductionToken": "{DISCORD PRODUCTION BOT TOKEN GOES HERE}",
    "DevelopmentToken": "{DISCORD DEVELOPMENT BOT TOKEN GOES HERE}"
  },
  "Giphy": {
    "ApiKey": "{GIPHY API KEY GOES HERE}"
  }
}
```

I actually use two Discord bots as seen here:
![two-bots](https://i.imgur.com/IToBEv5.png)
One is for production, the other for development (this way I can test stuff while still leaving the bot online). If you wish to use only one bot for both modes, you can just put the same token in for both the `ProductionToken` and the `DevelopmentToken`.

The Discord keys are required; the other keys are optional (but omitting them will prevent commands using those APIs from working).

_Copyright (c) 2018 **Rane Fields**_
