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
  * Piece.cs (Håller positioner)
    * Id
    * Position
    * Player Player (fk)
  * Player.cs ()
  * Square.cs (Fastnade lite på vad vi menar med square och hur vi ska använda den. Förslag hamnade på att varje spelare har en lista på "sin" spelplan och vi läser ut den via       regex med index)
  * Board ()
    * Id
    * Piece Piece (fk)  
  * Program.cs 

LudoGameEngine () 
  * Program.cs



