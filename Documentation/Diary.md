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


Efter att vi skapat klasser med tillhörande properties så inledde vi med lite enkel logik i klassen Dice som vi sedan flyttade till Board.
Vi skapade Game klassen som håller koll på spel, dock så kan funktionaliteten kollidera med Board så det är något vi ska se efter.
I Square lade vi till så att man kan identifiera varje Square objekt med ett Id och lade till en bool om någon befinner sig i den samt en variabel som håller koll på hur många spelare är i den.
Vi redigerade LudoDbAccess för att ha metoder för att kunna spara och ladda information från databasen.
Sedan lade vi till en ny klass inom LudoGameEngine som bygger ett UserInterface för användaren, vilket vi inte hade tänkt från början.


1/4 <- Tredje Dagen.

Förmiddagen
Diskuterade över att skapa en loop som göra att man kan komma tillbaka till menyn för att sedan kunna skapa nytt spel / ladda spel
Påbörjat en meny som frågar över hur många spelare som ska spela. Efter man skrivit in antal så skapas X-antal spelare i en metod.
Efter spelaren skapats så får man skriva in Namn (Färg och id sätts automatiskt).

Eftermiddagen
Vi har precis installerat in EF i vårt program. Vi flyttade CreateGame() över till Game klassen ifrån UserInterface. 



2/4
Förmiddagen:

Fixat lite små grejor
- CreateGame() <- Fixat så att det står antal spelare istället för att skriva ut exakt hur många spelare som har blivit inlagda i listan.
Så istället för:
- "Added Player"
- "Added Player"

Står det nu:
Added: "2" players in the list. 

- Flyttat "brädan" till Game klassen, CurrentBoard()
- La till 2 tomma metoder med kommentarer i Board.cs
- Felhanterade CreateGame() där användaren inte kan skriva in ett nummer som namn. 

Eftermiddagen:

Efter mycket om och men så tror jag att man nu kan köra "Create new game" -> Vilket skapar ett spelId,
lägger till spelare och sedan lägger till deras pjäser.

Efter man skapat det så körs metoden SaveGame() vilket lägger till SpelBräde, Spelare och Pjäser i db.

3/4
Förmiddagen:
Vi gick igenom programmet för att se vad som behövde göras.
- Ändrade strukturen i databasen då vi stötte på problem med FK.
- När vi fick FK att fungera så skrev den korrekt i databasen och kopplade spelar id till positioner samt att varje spelare tillhör ett visst game id.
- Påbörjade metoden PlayGame som ska köra spelet.

Eftermiddagen:
- Skapat en metod för vilken som slumpar fram vem som startar spelet.
- Påbörjat en metod som frågar vilken piece som skall flyttas.
- Skapat en metod som tar ut en lista från databasen på vilka pjäser den spelaren äger (1 - 4).
- Slängde in några fler TODO's i task list.

Vi ska se över lite metoder om det går att hämta data från databasen istället för att skicka med en massa listor och dyl.


4/4
Förmiddagen:
- Vi har suttit försökt att testa att få ut det högsta värdet ifrån "boardId" i Xunit. 
- Gick inte att köra testet pga av en Console.ReadLine() som blockerade vårt testa att köras.
- Vi såg också att hanterat metoden felaktigt så att den spottade ut nåt vi inte ville ha. 
- Efter lite mek så insåg vi att databasen var smartare än sig själv då vi faktiskt kan hämta ut hela spelet ur den.
- Testet funkar nu.

Eftermiddagen:
Vi har fortsatt pysslat med tester. 
- Vi testade om RollDice om den gav ett värde mellan 1-6, det funkade.
- Vi testade highestId på player för att vi skulle sätta id på spelarna vi skapade.
- Lagt till en tabell i databasen för att se vilka pjäser som är aktiva.
- Vi påbörjade metoden CurrentBoard, den metoden ska ta in alla positoner från pjäser och sätta ut det på spelplanen.  

5/4
Förmiddagen:
 - Skapat en metod som tar in spel som inte är avslutad (IsCompleted = false) till en lista, Därifrån ska vi skapa ett val om vilket spel man skulle vilja ladda

6/4 
Förmiddagen
- Vi höll på med funktionen att ifall man slog en 6 på träningen skall kunna få möjligheten att flytta ut en pjäs utifrån nest.
- Vi har diskuterat om att flytta lite kod till olika metoder för att lättare kunna kalla på dom.

Eftermiddagen
- Vi skapade mapparna GameLogic (med klasserna: Dice, GameLoop, Move och UpdateGameBoard), Initialize (Med klasserna
    CreateGame, LoadGame och SaveGame) och UI (med klasserna: Square och UserInterface)
- Vi flyttade sedan logiken dit där den hör hemma.

7/4
Förmiddagen
- Vi har i  "UpdateGameBoard.cs" fixat en metod ("UpdatePlayerTurn") som ser till att vi byter tur på spelarna som spelar.  
