# Gateless Gate Sharp

![Screen Shot](https://i.imgur.com/s7UWtVh.png)

Gateless Gate Sharp is the first open-source OpenCL dual ETH/XMR/PASC/LBC/FTC miner for Windows operating systems. It focuses on performance, stability, and ease of use.
Unlike [the original Gateless Gate](https://github.com/zawawawa/gatelessgate), this miner aims at stability with a much simpler design and the managed .NET Framework.

* [Gateless_Gate_Sharp_1.2.2_alpha_Setup.msi](https://github.com/zawawawa/GatelessGateSharp/releases/download/v1.2.2-alpha/Gateless_Gate_Sharp_1.2.2_alpha_Setup.msi) (Windows x64 Installer; highly recommended)
* [Gateless_Gate_Sharp_1.2.2_alpha.7z](https://github.com/zawawawa/GatelessGateSharp/releases/download/v1.2.2-alpha/Gateless_Gate_Sharp_1.2.2_alpha.7z)
* [Gateless_Gate_Sharp_1.2.2_alpha.zip](https://github.com/zawawawa/GatelessGateSharp/releases/download/v1.2.2-alpha/Gateless_Gate_Sharp_1.2.2_alpha.zip)

* [Gateless_Gate_Sharp_1.1.21_beta_Setup.msi](https://github.com/zawawawa/GatelessGateSharp/releases/download/v1.1.21-beta/Gateless_Gate_Sharp_1.1.21_beta_Setup.msi) (Windows x64 Installer; highly recommended)
* [Gateless_Gate_Sharp_1.1.21_beta.7z](https://github.com/zawawawa/GatelessGateSharp/releases/download/v1.1.21-beta/Gateless_Gate_Sharp_1.1.21_beta.7z)
* [Gateless_Gate_Sharp_1.1.21_beta.zip](https://github.com/zawawawa/GatelessGateSharp/releases/download/v1.1.21-beta/Gateless_Gate_Sharp_1.1.21_beta.zip)

(Previous releases are found [here](https://github.com/zawawawa/GatelessGateSharp/releases).)

Currently, the miner supports Ethash/daggerhashimoto, CryptoNight, Pascal, Lbry, Lyra2REv2, NeoScrypt, and the following major anonymous pools by default: NiceHash, ethermine.org, ethpool.org, DwarfPool, Nanopool, mineXMR.com, and zpool. Support for custom pools has also been added. All you have to do to mine is to download and run the installer, launch the miner, enter your wallet address(es), and click the Start button.

## Prerequisites

The minimum requirements for the miner are as follows:

* Graphics card(s) with the AMD GCN architecture and/or the NVIDIA Maxwell and Pascal architectures.
* 64-bit Windows operating system.

Please note that the current focus of the project is on AMD and this program has been tested mostly against [AMD Radeon Software Adrenalin Edition 17.12.2](http://support.amd.com/en-us/kb-articles/Pages/Radeon-Software-Adrenalin-Edition-17.12.2-Release-Notes.aspx). Other drivers may or may not work. For the best performance, please **turn off AMD CrossFire, if applicable, and choose Compute for GPU Workload in Radeon Settings.**

![Screen Shot](https://i.imgur.com/TNIBhCa.png)

## About the DEVFEE

This miner has a built-in 1% DEVFEE. I must emphasize that **I absolutely need the DEVFEE in order to continue this project, and I will not provide any support for those who run binaries I did not sign.** This project requires a full-time attention and testing is quite expensive. The DEVFEE is non-negotiable. If you don't like it, please go elsewhere.

## About BIOS Mods

Gateless Gate Sharp does not officialy support BIOS mods for other miners such as Claymore's as there are so many modded BIOS'es out there with outrageous parameters, which indicates that people who made them had no idea about what they were doing. You can of course try them at your own risk, but please don't complain to me if they don't work with GGS.
