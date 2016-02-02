# uBuild
Unity's "Missing" Build Pipeline

#Features#
- Build & Deploy to Steam
- Build & Deploy to Mac App Store
- CLI Building
- Save/Load JSON based config

[lot more is getting converted over, but it takes time]

The idea behind this system is to augment the amount of code needed to create custom build pipelines.
A given build can be made up of different parts:


## Modifiers ##
These are things that you would like changed in a paticular build settings, so modifications to the PlayerSettings bundle version for example.

## Routines ##
These are things that are more about crafting certain file structures, or doing some modifications outside of Unity





## Order of Operation ##
- Modifiers.PreProcessor
- Routines.PreProcessor
- BuildPlayer
- Routines.PostProcessor
- Modifiers.PostProcessor

