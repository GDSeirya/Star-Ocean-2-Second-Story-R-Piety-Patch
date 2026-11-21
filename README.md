# PietyCrit

A small gameplay mod for **Star Ocean: The Second Story R**, built using **BepInEx** and **Unity IL2CPP modding tools**.

In the vanilla game, each level-up of the **Piety** skill grants stat points to randomly chosen stats. This mod changes that so every Piety level-up grants **CRT** (critical rate) instead, while leaving the rest of the game's mechanics and systems untouched.

## Download

The latest releases of PietyCrit are available here:

**[PietyCrit Releases](https://github.com/GDSeirya/Star-Ocean-2-Second-Story-R-Piety-Patch/releases)**

## Features

Current features:

- Piety level-ups always grant **CRT** instead of random stats
- The number of points gained is the same as in the vanilla game
- No other skill is affected

## Requirements

### For Players

- A legitimate copy of **Star Ocean: The Second Story R**
- [BepInEx](https://github.com/BepInEx/BepInEx) IL2CPP installed
- Windows PC version of the game

### For Developers

- Visual Studio 2022 (or the `dotnet` CLI)
- .NET SDK
- BepInEx IL2CPP development environment
- IL2CPP interop assemblies generated from the game

## Installation

1. Install **BepInEx IL2CPP** into your Star Ocean: The Second Story R installation.
2. Download the latest **PietyCrit.dll** from the [Releases](https://github.com/GDSeirya/Star-Ocean-2-Second-Story-R-Piety-Patch/releases) page.
3. Copy the plugin DLL into:

```text
STAR OCEAN THE SECOND STORY R/
└── BepInEx/
    └── plugins/
        └── PietyCrit.dll
```

4. Launch the game.

BepInEx should automatically load the plugin.

> **Note:** If you are building the mod from source, see [Building From Source](#building-from-source).

## Building From Source

Clone the repository:

```bash
git clone https://github.com/GDSeirya/Star-Ocean-2-Second-Story-R-Piety-Patch.git
```

Open `PietyCrit.csproj` and set it to your game installation folder.

Build the project using Visual Studio (**Build → Build Solution**) or the command line:

```bash
dotnet build -c Release
```

The compiled DLL will be located in:

```text
bin/
└── Release/
    └── net8.0/
        └── PietyCrit.dll
```

Copy the resulting DLL into the BepInEx `plugins` folder to test the mod.

## Dependencies

### Runtime Dependencies

The following are required to run the mod:

- **Star Ocean: The Second Story R (PC version)**
  - A legitimate copy of the game is required.
- **BepInEx IL2CPP**
  - Required mod loader.
  - Provides plugin loading, IL2CPP interop, and Harmony patching.

## Development Dependencies

The following are required to build the mod from source.

### Required Software

- **Visual Studio 2022** (workload: `.NET desktop development`) or the `dotnet` CLI
- **.NET SDK**
- **Git**

### BepInEx Development Assemblies

The project references the `BepInEx.Unity.IL2CPP` NuGet package, along with the following assembly from your BepInEx installation:

```text
BepInEx/core/
└── Il2CppInterop.Runtime.dll
```

### Game IL2CPP Interop Assemblies

After installing BepInEx and launching the game once, the required IL2CPP interop assemblies will be generated:

```text
BepInEx/interop/
├── Il2Cppmscorlib.dll
├── UnityEngine.CoreModule.dll
└── Assembly-CSharp.dll
```

These files are generated from the game installation and are **not included in this repository**.

Developers must generate their own copies.

## Setting Up a Development Environment

1. Install BepInEx IL2CPP into the game directory.
2. Launch the game once to generate the IL2CPP interop assemblies.
3. Clone this repository.
4. Update your directories in `PietyCrit.csproj` to point to your game folder.
5. Open the project in Visual Studio 2022, or use the `dotnet` CLI.
6. Build the project.

## Modding Notes

This project targets the IL2CPP version of Unity used by **Star Ocean: The Second Story R**.

Because the game uses IL2CPP:

- Game assemblies are generated through BepInEx's IL2CPP interop tools.
- Decompiled assemblies are used for reference during development.
- Runtime patches and modifications are implemented through BepInEx and Harmony.

How the patch works:

- The Piety branch of `Game.CharacterParameter.SkillLevelupEffect` picks a random `CharacterStatus` for each point and calls `CharacterParameter.IncreaseBaseStatus(status, value)`.
- In the current game build, that is the only caller of `IncreaseBaseStatus`.
- The plugin applies a Harmony prefix to `IncreaseBaseStatus` that forces `status` to `CharacterStatus.CRITICAL`.
- `SkillLevelupEffect` itself is deliberately **not** patched. It takes a `ref GameManager.IncreaseResourcesParameter` struct containing reference-type fields, and patching it causes `NullReferenceException` errors from Il2CppInterop at startup.

A future game update could rename or inline `IncreaseBaseStatus`. If CRT stops changing, check the BepInEx log.

Generated game files and IL2CPP interop assemblies are intentionally not included in this repository.

## Contributing

Contributions, suggestions, bug reports, and testing reports are welcome.

Before submitting changes:

- Keep code organized and documented.
- Avoid committing generated files.
- Test changes in-game before submitting a pull request.
- Include relevant information when reporting bugs, such as logs, reproduction steps, and the game/mod version.

## License

This project is licensed under the **MIT License**.

See [LICENSE](LICENSE) for details.

## Disclaimer

This project is a fan-made modification and is not affiliated with, endorsed by, or sponsored by **Square Enix** or **Gemdrops**.

You must own a legitimate copy of **Star Ocean: The Second Story R** to use this mod.
