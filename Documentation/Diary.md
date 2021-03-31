# Documentation

30/3 <- Första dagen.

Vi har kommit igång med att skriva CRC-kort med vilka klasser vi kan behöva ha. Dessutom har vi studerat hur spelet "Ludo" går till. 

31/3 <- Andra dagen.

Vi har fortsatt med "User Stories" och börjat med "User Case".

Utifrån ovanstående kom vi fram till nedanstående som tilltänkt början.

LudoBoard ()
* DataAccess (Katalog som håller config och skick till och från databas)
  * LudoDbAccess.cs ()
  * LudoDbContext.cs ()
*  DataModels (Katalog som har datamodeller för DataAccess)
  * Color.cs (Håller de fyra färgerna (Enum))
  * Dice.cs (Har bara en metod för att slå tärning.)
  * Nest.cs (Kanske inte behövs om man sätter en position på varje Piece)
    * Id
    * Piece Pieces (fk)
    * 
  * Piece.cs (Håller positioner)
    * Id
    * Position
    * Player Player (fk)
  * Player.cs ()
    * Id
    * Name
    * Color Color (fk)
    * IsWinner (Lades till i efterhand för att kunna se vinnare. Man ska då tänka på allt!!!)
  * Square.cs (Fastnade lite på vad vi menar med square och hur vi ska använda den. Förslag hamnade på att varje spelare har en lista på "sin" spelplan och vi läser ut den via       regex med index)
    *  Id
    *  IsOccupied
  * Board (Genererar en ny board. Sätts vid create new game.)
    * Id
    * Piece Piece (fk) 
  * Program.cs 
  * Game
    * Id
    * Piece Piece (fk)
    * DateTime LastPlayedDate (Lades till i eferhand efter en del kodning då vi diskuterade friskt kring vad som skulle visas från databasen. Tanke att vi använder till laddning och spårning) 
    * DateTime CompletedDate (Samma som ovan)
    * Bool IsCompleted (En checker för att se om spelet är färdigspelat. Denna lades även till i efterhand.)

LudoGameEngine () 
  * Program.cs


Efter att vi skapat klasser med tillhörande properties så inledde vi med lite enkel logik i klassen Dice.


