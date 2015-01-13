# Music Framework #

This is music framework for generating midi messages based on musical scales.

it supports arabic scales tuning.

the library supports reading and writing midi files.

it also has a built in sequencer.

the framework doesn't come with a synthesizer however you should provide the synthesizer yourself .. the MusicPlayer project uses the windows synthesizer.

This library is ported from an old code of me. The primary reason for creating this library was for learning music especially how can One generate arabic scales with qurater notes on the computer.

The library also can parse mml (melody macro languages) and product the corresponding midi messages for it.

## MML Language Reference ##
The MML language has a standard notation from C to B 

each note can be preceded by accedential then a square brackets contains the octave and the duration of the note.

Sample: "**C[6|1/4] B# B#/ G[3] E/**"  

### Notes Names ##
- `C`: Do
- `D`: Re
- `E`: Mi
- `F`: Fa
- `G`: Sol
- `A`: La
- `B`: Si

### Accedentials ###
- `#`: **Sharp** note.
- `/`: **Half Sharp** (essential for arabic music).
- `'`: **Half Half Sharp** (essential for indian music)
- `*`: **Picar** (returns the tone to its original frequency)
- `.`: **Half Half Flat** note.
- `!`: **Half Flat** ote.
- `~`: **Flat** note

### Note Duration ###
The duration of note is written inside square brackets 
**C[1/8]** express Do with 1/8 of the full Rund

### Note Octave ###
The note octave precedes the duration as of **C[5|1/4]** the octave here is the 5th octave. It is also valid to exclude the duration part to be **C[5]** only.



