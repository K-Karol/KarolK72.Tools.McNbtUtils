# KarolK72.Tools.McNbtUtils

Set of projects that provides specific utilities regarding Minecraft data stored in NBT files.

Specifically, as of right now, it allows to generate a `/give` command for any inventory item including all the item's metadata, meaning you can generate `/give` commands that will give you armour/tools with all enchantments, and with modded items, it will bring all of the information across.

Example of a generated command:

```
/give @s graveyard:bone_dagger {RepairCost: 1, Enchantments: [{id: 'minecraft:sharpness', lvl: 9s}, {id: 'minecraft:unbreaking', lvl: 8s}, {id: 'minecraft:sweeping', lvl: 8s}, {id: 'apotheosis:scavenger', lvl: 3s}, {id: 'minecraft:looting', lvl: 5s}, {id: 'apotheosis:life_mending', lvl: 3s}, {id: 'apotheosis:capturing', lvl: 7s}, {id: 'minecraft:bane_of_arthropods', lvl: 10s}, {id: 'minecolonies:raider_damage_enchant', lvl: 6s}], affix_data: {uuids: [[I;-1682570598,-747680856,-1593705177,1737491421]], sockets: 3, affixes: {'apotheosis:sword/attribute/violent': 0.85534626f, 'irons_spellbooks:sword/attribute/spell_power': 0.35211515f, 'apotheosis:sword/attribute/lacerating': 0.7510442f, 'apotheosis:sword/mob_effect/sophisticated': 0.5523306f, 'apotheosis:sword/special/thunderstruck': 0.8374884f, 'apotheotic_additions:sword/attribute/spell_resist': 0.8195803f, 'apotheosis:durable': 0.37f}, rarity: 'apotheosis:mythic', name: '{"color":"#ED7014","translate":"misc.apotheosis.affix_name.three","with":[{"translate":"affix.apotheosis:sword/attribute/lacerating"},"",{"translate":"affix.apotheosis:sword/special/thunderstruck.suffix"}]}', gems: [{id: 'apotheosis:gem', tag: {uuids: [[I;-894052760,238634655,-1848950860,1552501556]], gem_power: 1f, affix_data: {rarity: 'apotheosis:ancient'}, gem: 'apotheosis:core/ballast'}, Count: 1}, {id: 'apotheosis:gem', tag: {affix_data: {rarity: 'apotheosis:ancient'}, gem: 'apotheosis:overworld/earth', gem_power: 1f}, Count: 1}, {id: 'apotheosis:gem', tag: {affix_data: {rarity: 'apotheosis:ancient'}, gem: 'apotheosis:the_end/endersurge', gem_power: 1f}, Count: 1}]}, Damage: 0, gems_count: 3} 1
```

It includes all apotheosis details incl. gems and other stats.

## Why

Had a specific use case where some items were lost due to a server issue and we would lose too much progress due to the backup frequency, therefore I created a simple program that would extract all the NBT details of the lost items and generate the necessary `/give` commands to replace the items that were lost.

Given how useful this was, I've extracted the logic and command generation and created this repository which includes a library with the command generator along with a Avalonia UI application that will allow to load an NBT file to browse the file tree and for any valid NBT tags that represent items, it will try to generate the `/give` command.

## Contents

* `KarolK72.Tools.McNbtUtils` - Library project that contains the logic for generating the give command.
* `KarolK72.Tools.McNbtUtils.App` - AvaloniaUI application that provides a simple way to load an NBT file with a tree view of the NBT file structure that will output the `/give` command where applicable.

There is a scaffolded `KarolK72.Tools.McNbtUtils.Cli` project that is intended to contain a `CLI` application that will allow to generate the `/give` commands through the command line, but it is empty for now.

## Project state

The UI app isn't finished, just provides basic functionality (button for browsing files is not implemented, but you can paste in the file path).

Unless there is any interest in polishing the project or providing more functionality, not much extra time will be spent on the project.