# anino_exam

#Unity Version : 2020.3.18f1;

**System Setup**
-the main classes would be the SlotMachineMain.cs, SlotMachineReel.cs and SymbolDetector.cs.
-the game has three buttons, the knob that starts and stops the roll, and the plus and minus that adjust the bet.
-theres not alot of prefabs on my project just the five different reels.

**List of data sources**
-SymbolPayouts.asset(Assets/Scripts) - its a scriptable object that you can edit the payouts of each symbols via inspector;
-PayoutLines.asset(Assets/Scripts/PayoutLines) - payoutlines is also a scriptable object that you can edit via inspector or create a new one and slot it in the SlotMachineMain.cs payoutlines.

**Additional Notes**
-Its quite easy to add more in terms of adding symbols, adding paylines and editing the payouts.
-I don't think that my system is quite flexible since the reels is just an image that has box colliders and Symbol.cs(Set symbol enum and the Reel order) on each symbol. In order to add more you need to make a new image and add colliders and set Symbol.cs, but Im quite proud of the structure, if you wanna add a new symbol, you just add on the enum and SymbolPayouts.asset and payoutlines.
