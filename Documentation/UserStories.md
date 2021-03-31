User Stories

As a < player >, want to < register > players so that they can< play>.
As a < player >, I want < load up previous game > so that < patrik (player)>will lose.
As a < player in game >, I want < get an alternative > so that i <can choose to roll dice or show board>.
As a < player >, I want < roll the dice > so that < move forward to win >.
As a < player>, I want < “push” other pieces> so that i < reset my opponent's piece >.
As a < player>, I want all my< pieces > in center of <board> so that i < win >.
As a < player >, I want < press escape > so that a< menu > shows.
As a < player >, I want < press save > so that i < save the game and quit >.
As a < player >, I want < press quit > so that i < quit without save the game >.


User Case

As a < player >, want to < register > players so that they can< play>.
- Application ask how many players (2-4), Input
- Player enter names
- System creates boardId
- System set name / colour
- System set position for pieces
- System randomize wich player to start

As a < player >, I want < load up previous game > so that < patrik (player)>will lose.
- System get a list of “active” games
- Player choose a game from list
- System run chosen game
- Player plays on

As a < player in game >, I want < get an alternative > so that i <can choose to roll dice or show board>.
- Player choose 1. roll dice or 2. show current board

As a < player >, I want < show current board > so that i < get an overview >.
- System prints the current map with all positions

As a < player >, I want < roll the dice > so that < pieces can move >.
- System get method from class Dice that randomize a number between 1-6
- If rolled 1-5, player can move a piece that is outside of nest.
- If rolled 6, player can pick a piece from nest to board or move piece on board then rolls again.

As a < player>, I want all my< pieces > in center of <board> so that i < win >.
- Player needs to get all the 4 pieces in the center of the board to win the game. 
- When player enters the final track, the player needs to roll the dice exact amount the get in goal for a point.

As a < player>, I want < “push” other pieces> so that i < reset my opponent's piece >.
- If last move ends up upon an opponent’s piece, that piece resets to nest
- // Extra - some squares should have stars which prevent players to “push” other pieces.

As a < player >, I want < press escape > so that a< menu > shows.
- Player gets to choose (Y/N) Quit


/*
As a < player >, I want < press save & quit > so that i < save the game and quit >.
- Player has the option to save the current game. 

As a < player >, I want < press quit > so that i < quit without save the game >.
- Player has the option to quit the game without save 
*/

