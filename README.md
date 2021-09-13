# anino_exam

#Unity Version : 2020.3.18f1.


**System Setup**

-the main classes would be the SlotMachineMain.cs, SlotMachineReel.cs and SymbolDetector.cs.
-the game has three buttons, the knob that starts and stops the roll, and the plus and minus that adjust the bet.
-theres not alot of prefabs on my project just the five different reels.


**List of data sources**

-SymbolPayouts.asset(Assets/Scripts) - its a scriptable object that you can edit the payouts of each symbols via inspector.

-PayoutLines.asset(Assets/Scripts/PayoutLines) - payoutlines is also a scriptable object that you can edit via inspector or create a new one and slot it in the SlotMachineMain.cs payoutlines.


**Additional Notes**

-Its quite easy to add more in terms of adding symbols, adding paylines and editing the payouts.

-The one thing Im not to sure about flexibility is the Reels since the reels is just an image that has box colliders and Symbol.cs(Set symbol enum and the Reel order) on each symbol, In order to add more you need to make a new image and add colliders and set Symbol.cs, but overall Im quite proud of the structure, if you wanna add a new symbol, you just add on the enum and SymbolPayouts.asset and payoutlines.

-The Player pulls the slot machine knob and it stops the first reel then that reel will send an event that it stopped, the second reel will receive that event and stops as well same process until the last one, which it will send an event that all 5 reels has stopped then the three symbol detector scans and checks the symbols, which it sorts and stores it to themselves after that the slot machine will get those data and checks the payout lines and tally up the symbols which in turn checks if the player has won the line, after checking out the payout lines it will now reads the tallied up symbols and checks the SymbolPayouts to calculate the total money that the player has won, it also shows up the payout lines at the end.

-I would make the reel more flexible and add more symbols. I also want to add particles and design because right now its looks like a boring slot machine compared to others and sounds as well.
