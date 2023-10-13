# Project Rock Hopper

A 2D Space Mining Game

**TBD:** "People don't buy what you do, they buy why you do it." -- Simon Sinek.  Need a compelling short story that explains why I should play this game.

## Platforms

**MVP Decision:** Starting with PC through Steam. (Windows only, or is Steam dev cross platform to Mac?)

*Futures:* Steam deck, iOS, Android 

## Money and Monetization

Things that can be bought for real money:
- Loot
- Supplies (food, seeds, bait, beer)
- All upgrades 

**TBD:** Need an amusing name for the standard unit of currency in the game. *Candidates:* Rallods, Noolies, ???

## Personalization

A user should be able to set and modify their:

1. name
1. gender (sensitively)
1. color of hair, eyes, and skin
1. shape of hair and eyes
1. height (e.g., short, typical, tall)
1. physique (e.g., endomorphic, mesomorphic, exomorphic)
1. color and type of clothing (per game loop, e.g., space suit, mining outfit, fishing clothes, farming clothes)
1. shape, color, and decorations of gaming craft per game loop (e.g., asteroid lander, fishing boat, mother ship)
1. ???

**MVP Proposed:** All of 1 through 6 because we want to major on having the user see themselves in this game. For 7 and above, these should be added as the respective game loop is added. As much as possible, the user should feel that all of this is *their* stuff.

May also help to personalize if the marauders and other enemies are from one or more alien races.

**TBD:** Need amusing enemy race names, like Neerdowells

## Notifications

To stimulate ongoing engagement, users should receive notifications about:
1. Upgrades being ready for a gaming craft or piece of equipment
1. Crops, eggs, etc. are ready
1. Fishing pond restocked
1. Special (more lucrative) missions or contracts
1. New game loop or features available
1. ???

Need to consider:
- Can we find out the costs involved in sending texts along the lines of how companies do 2FA?
- May need to collect an email address (Yikes). 
- May need to build a small mobile app to receive and display notifications, even for a PC gamer. 
- Worst case, user only receives their notifications in a list they can access whenever they rejoin the game. 

**MVP Decision:** TBD, may depend on research 

## Badges and Awards

Need to have badges for achieving all manner of new things for the first time.

The badges can help suggest how to play parts of the game.

Awards can be earned from the Mars Planetary Leader or similar for taking out marauders to make mining an asteroid safe, for bringing back crucial materials. There should be a celebratory panel to present or read out the award, and there should be loot!


## Game Loops

### Asteroid Landing/Take-Off 

Asteroid Lander has:
- main thruster to counteract gravity, different gravity per asteroid
- small left and right thrusters for horizontal movement
- hull: can be upgraded to various kinetic energy ratings
- shields: available as upgrade, and can be upgraded to various kinetic energy ratings
- guns: left, right, and top that can shoot projectiles or lasers that can destroy space debris and enemy marauders   

Aspects of the game play:
- Lander leaves mothership and is subjected to gravity.
- Occasional left and right moving space debris, greater frequency and speed variations for harder asteroids a user may visit as they advance in level or game play experience
- Must use all thrusters, or hull integrity and shields, or guns to deal with space debris
- Must land at acceptably slow speeds, vertically and horizontally
- Mining operation occurs (see next section)
- Take-off to get back to mothership, still avoiding space debris and enemy ships
- Must dock at an acceptable speeds, vertically and horizontally
- Must complete all aspects without running out of fuel or food

If the lander operation fails (hit by space debris or marauder fire, ran out of fuel or food, landing or docking too hard), the lander can fall to the planet and send out a distress call to the mother ship for a rescue. While waiting, the side guns could be pointed diagonally upward, and those along with the top gun could be used to defend the lander from marauders until the rescue ship arrives to tow the lander to the mothership (cutscene animation for towing).

When the lander docks with the mothership, there should be a beautiful looking and sounding presentation of all loot gained. It should look and sound like winning at a casino.

This is the hard, pulse-pounding game loop, so it may need 

**MVP Proposal:** Some version of this game loop needs to be in the first version. TBD: Are there bits that can/should be cut back? 

### Asteroid Mining

While building the Asteroid Landing/Take-off game loop, this could start out as just a cutscene animation that shows some digging and then a celebratory loot page appears to show what you got.

Later, this could turn into a game that allows the player to navigate on the surface and to dig into the asteroid to deeper levels to find better and better loot, possible old mines, possible enemies. 

Things the player can get:
- lower value ores (nickel, iron, etc.)
- middle value ores (copper, titanium, etc.)
- higher value ores (silver, gold, platinum, etc.)
- gems (sapphires, rubies, emeralds, diamonds, etc.)
- fuel-related materials (e.g., heavy hydrogen)

What the player can do with them:
- sell them for loot (e.g., copper, silver, gold, sapphires, rubies, emeralds, etc.)
- upgrade the lander's hull (e.g., nickel, iron, titanium)
- upgrade mining tools (e.g., iron, diamonds)
- refine fuel (e.g., heavy hydrogen)

**MVP Proposal:** Some version of this game loop needs to be in the first version. TBD: Are there bits that can/should be cut back? 

### Farming

Build up a farm full of plants and animals that produce produce (lol), which can add to food stores or be sold for loot.

Things that can be produced on the farm:
- flowers (tulips, daisies, petunias (there could be a Hitchhikers shoutout here))
- fruits (e.g., apples, oranges, bananas, prunes (there could be some humour opportunities here))
- vegetables (e.g., corn, peas, carrots)
- grains (e.g., wheat, oats)
- Chicken eggs
- Cow's milk
- Goat cheese
- ???

*Deliberately* staying away from producing meat from chickens, cows, or pigs.

There should be calming music playing while the player is at the farm.

Things that can be bought or upgraded:
- a hoe
- a shovel
- a watering can
- seeds
- chickens
- cows
- goats
- fertilizer
- ???

Animal manure, eggshells, and plant waste can also be converted to fertilizer.

A frog may occasionally visit the farm and dance and sing favorites like "Hello, My Baby". The frog could also tell bad dad jokes like "I double-parked my car, you know what happened? It was toad."

*Deliberately* not adding a frog catching component due to danger of someone wanting to add frog legs to the menu. No animals are harmed in the making of this game.  Well, except...

### Fishing

Catching fish and other seafood can add to food stores and fertilizer. May also sell for loot.

Various types of seafood that couuld be caught:
- Bottom feeders (e.g., catfish) 
- Top feeders (e.g., trout, flounder)
- Shellfish (e.g., shrimp, crabs, lobster)

Lower and higher size, difficulty, and value as food or loot.

Things that the player can buy or upgrade:
- Boat
- Motor
- Fuel
- Fishing rod
- Sinkers
- Lures
- Scoop net
- Cast net (for shrimp)
- Crab traps
- Lobster traps (a comment about being caught on outside of the trap needs to be made)
- Beer, consumption of which may temporarily improve the player's fishing skills

Fish skeletons and seafood shells could be used to help make fertizer for the farming game.

May add a deep sea phase to trawl for herring while avoiding sharks
 
### Space Hunts, or Traveling To/From Asteroids and Space Stations 

- This is where the mothership comes in. 
- Long range scanners. 
- Detect the best asteroids to mine
- Detect and engage in 2D battles with roving marauders and motherships of the Neerdowell race

### Space Stations 

Different fuels, supplies, vendors, mining loot purchasers

Subgame minis to add over time:
1. Casino gambling (blackjack, poker, slots, roulette, etc.)
1. Arcade (pinball, mini asteroid lander (self-reference), space invader type, fly swat, bug stomp, etc.)
1. ???

## Guilds

Need cooperative guilds that can share resources (food, fuels, tools, weapons, upgrades, loot). 

**TBD:** Need to think about and research whether this feature would reduce game revenue

**TBD:** Need a way to invite and accept other players to be in a guild.

Guild members can join common missions where they can help each other to survive mining expeditions or space hunting, and everyone who goes on the mission gets a share of the loot.

