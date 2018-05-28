# Gateless Gate Sharp

Gateless Gate Sharp is an user-friendly yet extremely powerful open-source multi-algorithm miner for Windows operating systems.
It focuses on performance, stability, and ease of use, featuring a one-of-a-kind ability to modify memory timings on the fly
and a powerful built-in optimizer. Unlike [the original Gateless Gate](https://github.com/zawawawa/gatelessgate), this miner
aims at stability with a much simpler design and the managed .NET Framework.

* [Gateless_Gate_Sharp_1.3.8_alpha_Setup.msi](https://github.com/zawawawa/GatelessGateSharp/releases/download/v1.3.8-alpha/GatelessGateSharpInstaller.exe) (Windows x64 Installer)

(Previous releases are found [here](https://github.com/zawawawa/GatelessGateSharp/releases).)

All you have to do to mine is to download and run the installer, launch the miner, enter your wallet address(es), and click the Start button.
Please carefully read **[Prerequisites](#prerequisites)** before installation. The **[online manual](https://github.com/zawawawa/GatelessGateSharp/blob/v1.3/Documentation/TOC.md)**, which is still work in progress, might be helpful, too.

![Screen Shot](https://i.imgur.com/gsiVgfP.png)

Currently, the miner supports the following algorithms:

* Ethash/Pascal dual-mining
* Ethash
* CryptoNight
* CryptoNight-Light
* CryptoNight-Heavy
* CryptoNightV7
* X16R
* X16S
* NeoScrypt
* Pascal
* Lbry
* LyraREv2

The following major pools are natively supported as default pools:

* NiceHash
* ethermine.org
* ethpool.org
* DwarfPool
* Nanopool
* Mining Pool Hub
* zpool
* mineXMR.com

In addition to the default pools, support for custom pools is also available. 

## <a name="prerequisites"></a>Prerequisites

The minimum requirements for the miner are as follows:

* Graphics card(s) with the AMD GCN architecture and/or the NVIDIA Maxwell and Pascal architectures.
* Windows 8.1 64-bit or later (Windows 10 is strongly recommended).
* [AMD Radeon Software Adrenalin Edition 18.5.1](http://support.amd.com/en-us/kb-articles/Pages/Radeon-Software-Adrenalin-Edition-18.5.1-Release-Notes.aspx) for AMD cards.

**Note: Please DO NOT USE UTILITIES FOR OVERCLOCKING SUCH AS MSI AFTERBURNER with Gateless Gate Sharp.**

The current focus of the project is on AMD and this program has been tested mostly against [AMD Radeon Software Adrenalin Edition 18.5.1](http://support.amd.com/en-us/kb-articles/Pages/Radeon-Software-Adrenalin-Edition-18.5.1-Release-Notes.aspx). Other drivers may or may not work.*

## About the DEVFEE

This miner has a built-in 1% DEVFEE. Moreover, shares submitted during benchmarking/optimization will go to the developer. I must emphasize that **I absolutely need the DEVFEE in order to continue this project, and I will not provide any support for those who run binaries I did not sign.** This project requires a full-time attention and testing is quite expensive. The DEVFEE is non-negotiable. If you don't like it, please go elsewhere.

## About BIOS Mods

Gateless Gate Sharp does not officialy support BIOS mods for other miners such as Claymore's as there are so many modded BIOS'es out there with outrageous parameters, which indicates that people who made them had no idea about what they were doing. You can of course try them at your own risk, but please don't complain to me if they don't work with GGS.
