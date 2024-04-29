using System.Data.SqlTypes;
using System.IO.Pipes;
using System.Linq.Expressions;
using System.Net.Security;
using System.Reflection.Metadata.Ecma335;

namespace Bacud_MidtermGame
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // PLAYER -> DONE IN ORDER FOR VARIABLES TO BE ACCESSED BY EVERY SINGLE FUNCTION PRESENT IN THE CODE (GLOBAL VARIABLE)
            int healthPoints = 100;
            int attackPoints = 0;
            string playerName = "";
            int maxhealthPoints = 100;

            // SYSTEM -> DONE IN ORDER FOR VARIABLES TO BE ACCESSED BY EVERY SINGLE FUNCTION PRESENT IN THE CODE (GLOBAL VARIABLE)

            bool hasHealth = true;
            bool matchProgress = true;
            bool playerTurn = true;
            bool actionOngoing = true;
            bool flee = false;
            int levelCounter = 1;
            int turnCounter = 1;
            int enemyChoice = 0;
            Random rnd = new Random();

            // ENEMY INFORMAITON -> DONE IN ORDER FOR VARIABLES TO BE ACCESSED BY EVERY SINGLE FUNCTION PRESENT IN THE CODE (GLOBAL VARIABLE)

            int enemyATK = 0;
            int enemyHP = 0;
            List<string> enemyPool = new List<string> { "Storm", "Janice", "Alice", "Raiden", "Bryan", "Mist", "Shadow", "Sun", "Gabe", "Xavier", "Vaughn", "Louis", "Minor", "Nicholas"};

            // STARTING PROMPT

            while (true) // CHECKS IF USERNAME IS INSERTED PROPERLY
            {
                Console.Clear();
                Console.WriteLine("= = = = = = = = = = = = = = = = = = = = = = ");
                Console.WriteLine("        G A M B L E R ' S  R P G");
                Console.WriteLine("= = = = = = = = = = = = = = = = = = = = = = \n");

                Console.Write("Welcome to the Gambler's RPG!\n");
                Console.Write("Input Character Name: ");

                playerName = Console.ReadLine();

                if (playerName.Length > 0)
                {
                    break;
                }

                else
                {
                    Console.Clear();
                    Console.WriteLine("= = = = = = = = = = = = = = = = = = = = = = ");
                    Console.WriteLine("        G A M B L E R ' S  R P G");
                    Console.WriteLine("= = = = = = = = = = = = = = = = = = = = = = \n");


                    Console.WriteLine("Error: Please input a valid username");
                    Console.WriteLine("Input any key to continue...");
                    Console.ReadKey();
                }
            }

            // GAME DESCRIPTION PROMPT

            Console.Clear();
            Console.WriteLine("= = = = = = = = = = = = = = = = = = = = = = ");
            Console.WriteLine("        G A M B L E R ' S  R P G");
            Console.WriteLine("= = = = = = = = = = = = = = = = = = = = = = \n");

            Console.Write("Character Info:\n");

            Console.Write($"Name: {playerName}\n");
            Console.Write($"HP : {healthPoints}\n\n");

            Console.Write("Mechanics:\n");
            Console.Write("* All additional stats, like ATK, will be randomized per turn\n");
            Console.Write("* All actions are affected by chance, with the following effects:\n\n");
            Console.Write("\t* Attack may miss, hit, or have a critical strike on the enemy\n");
            Console.Write("\t* Heal may miss and not work. If it does, Heal regains a random amount of HP\n");
            Console.Write("\t* Flee may miss and not work. If it does, skip to the next opponent\n\n");
            Console.Write("* Enemies and their stats will be randomly generatedn\n");
            Console.Write("* Enemy actions also have the same effect as User Actions\n");
            Console.Write("* Users must input any key after a move has been done, regardless of user or enemy action\n\n");
            Console.Write("Input any key to continue and start the game...");
            Console.ReadKey();

            // GAME START

            while (hasHealth) // WHILE LOOP IF USER STILL HAS 100 HP
            {
                if (levelCounter >= 4) // TENTATIVE, BUT ACCORDING TO INSTRUCTIONS THERE ARE ONLY 3 LEVELS
                {
                    break;
                }

                // LEVEL INFORMAITON

                Console.Clear();
                Console.WriteLine("= = = = = = = = = = = = = = = = = = = = = = ");
                Console.WriteLine("        G A M B L E R ' S  R P G");
                Console.WriteLine("= = = = = = = = = = = = = = = = = = = = = = \n");

                // RANDOMLY GENERATES THE STATS

                string levelEnemy = enemyPool[(rnd.Next(0, enemyPool.Count))];
                attackPoints = rnd.Next(1, 21);
                enemyATK = rnd.Next(1, 21);
                enemyHP = rnd.Next(50, 101);
                int maxenemyHP = enemyHP; // -> DONE FOR FUTURE USE

                // STAT DISPLAY

                Console.Write($"Level {levelCounter}: {playerName} vs {levelEnemy} \n\n");

                Console.Write("Level Information:\n\n");

                Console.Write("Character Info:\n");
                Console.Write($"Name: {playerName}\n");
                Console.Write($"HP: {healthPoints}\n");
                Console.Write($"ATK: {attackPoints} \n\n");

                Console.Write("Enemy Info:\n");
                Console.Write($"Enemy: {levelEnemy}\n");
                Console.Write($"HP: {enemyHP} \n");
                Console.Write($"ATK: {enemyATK}\n\n");

                Console.Write("Input any key to start the match...");
                Console.ReadKey();

                while (matchProgress) // LEVEL IN PROGRESS LOOP -> DONE SO THAT SWAPPING OF TURNS IS DONE SMOOTHLY
                {
                    while(playerTurn) // PLAYER TURN LOOP -> DONE FOR MENUS
                    {
                        try
                        {
                            Console.Clear();
                            Console.WriteLine("= = = = = = = = = = = = = = = = = = = = = = ");
                            Console.WriteLine("        G A M B L E R ' S  R P G");
                            Console.WriteLine("= = = = = = = = = = = = = = = = = = = = = = \n");

                            Console.Write($"Turn {turnCounter}:\n\n");
                            Console.Write("Player's Turn:\n");
                            Console.Write($"{playerName} HP: {healthPoints} ATK: {attackPoints} || {levelEnemy} HP: {enemyHP} ATK: {enemyATK}\n\n");
                            Console.Write("Choose a move: [1] Attack [2] Heal [3] Flee\n\n");

                            actionOngoing = true;
                            int moveChoice = int.Parse(Console.ReadLine());
                            
                            switch (moveChoice) // CHOICE ACTION MOVE BLOCK
                            {
                                case 1: // ATTACK ACTION
                                    while (actionOngoing)
                                    {
                                        try
                                        {
                                            Console.Clear();
                                            Console.WriteLine("= = = = = = = = = = = = = = = = = = = = = = ");
                                            Console.WriteLine("        G A M B L E R ' S  R P G");
                                            Console.WriteLine("= = = = = = = = = = = = = = = = = = = = = = \n");

                                            Console.Write($"Turn {turnCounter}:\n\n");
                                            Console.Write("Player's Turn:\n");
                                            Console.Write($"{playerName} HP: {healthPoints} ATK: {attackPoints} || {levelEnemy} HP: {enemyHP} ATK: {enemyATK}\n\n");

                                            Console.Write("Do you wish to Attack?\n");
                                            Console.Write("[1] Yes [2] No\n\n");

                                            int userConfirmation = int.Parse(Console.ReadLine()); // MAKES SURE IF USER WANTS TO DO THE ACTION

                                            if (userConfirmation == 1)
                                            {
                                                int diceRoller = rnd.Next(1, 6); // CHANCE ROLLER -> 1/5 CHANCE FOR ATK TO MISS, 1/5 CHANCE FOR ATK TO CRIT, 3/5 CHANCE FOR NORMAL ATK

                                                switch (diceRoller) 
                                                {
                                                    case 1: // MISS ATK ACTION

                                                        Console.WriteLine("\nAttack has missed...");
                                                        actionOngoing = false;
                                                        Console.ReadKey();
                                                        break;

                                                    case 2:
                                                    case 3: // NORMAL ATK ACTION
                                                    case 4:

                                                        enemyHP -= attackPoints;
                                                        Console.WriteLine($"\nAttack Successful! Enemy has lost {attackPoints} HP");
                                                        actionOngoing = false;
                                                        Console.ReadKey();
                                                        break;

                                                    case 5: // CRIT ACTION
                                                        enemyHP -= (attackPoints * 2); 
                                                        Console.WriteLine($"\nCritical Strike! Enemy has lost {attackPoints * 2} HP");
                                                        actionOngoing = false;
                                                        Console.ReadKey();
                                                        break;

                                                }
                                            }

                                            else if (userConfirmation == 2) // IF USER ISN'T SURE, GOES BACK TO INITIAL CHOICE / MOVE SELECTION MENU
                                            {
                                                break;
                                            }

                                            else // ERROR MESSAGE
                                            {
                                                Console.Write("\nAn error has occured, please try again\n");
                                                Console.Write("Input any key to continue...\n");
                                                Console.ReadKey();
                                            }
                                        }
                                        
                                        catch // ERRPR MESSAGE
                                        {
                                            Console.Write("\nAn error has occured, please try again\n");
                                            Console.Write("Input any key to continue...\n");
                                            Console.ReadKey();
                                        }
                                    }

                                    break;

                                case 2: // HEAL ACTION

                                    while (actionOngoing)
                                    {
                                        try
                                        {
                                            Console.Clear();
                                            Console.WriteLine("= = = = = = = = = = = = = = = = = = = = = = ");
                                            Console.WriteLine("        G A M B L E R ' S  R P G");
                                            Console.WriteLine("= = = = = = = = = = = = = = = = = = = = = = \n");

                                            Console.Write($"Turn {turnCounter}:\n\n");
                                            Console.Write("Player's Turn:\n");
                                            Console.Write($"{playerName} HP: {healthPoints} ATK: {attackPoints} || {levelEnemy} HP: {enemyHP} ATK: {enemyATK}\n\n");

                                            Console.Write("Do you wish to Heal?\n");
                                            Console.Write("[1] Yes [2] No\n\n");

                                            int userConfirmation = int.Parse(Console.ReadLine()); // MAKES SURE IF USER WANTS TO DO THE ACTION
                                            if (userConfirmation == 1)
                                            {
                                                if (healthPoints == maxhealthPoints)
                                                {
                                                    Console.WriteLine("There is no need to heal, you have max HP");
                                                    Console.ReadKey();
                                                    break;
                                                }

                                                else
                                                {
                                                    int diceRoller = rnd.Next(1, 5); // CHANCE ROLLER (GETTING A HEAL IS 50/50)

                                                    switch (diceRoller)
                                                    {
                                                        // 50% CHANCE OF FAILING
                                                        case 1:
                                                        case 2:
                                                            Console.WriteLine("\nHeal has failed...");
                                                            actionOngoing = false;
                                                            Console.ReadKey();
                                                            break;

                                                        // 50% CHANCE OF SUCCEEDING
                                                        case 3:
                                                        case 4:

                                                            int healPlaceholder = rnd.Next(1, maxhealthPoints - healthPoints); // MAKES SURE THAT HEAL DOES NOT GO OVER THE INITIAL MAX HP VALUE

                                                            healthPoints += healPlaceholder;
                                                            Console.WriteLine($"\nHeal Successful! You have healed {healPlaceholder} HP");
                                                            actionOngoing = false;
                                                            Console.ReadKey();
                                                            break;

                                                    }
                                                }
                                             
                                            }

                                            else if (userConfirmation == 2) // IF USER ISN'T SURE, GOES BACK TO INITIAL CHOICE / MOVE SELECTION MENU
                                            {
                                                break;
                                            }

                                            else if (userConfirmation != 1 || userConfirmation != 2) // ERROR MESSAGE
                                            {
                                                Console.Write("\nAn error has occured, please try again\n");
                                                Console.Write("Input any key to continue...\n");
                                                Console.ReadKey();
                                            }
                                        }

                                        catch // ERROR MESSAGE
                                        {
                                            Console.Write("\nAn error has occured, please try again\n");
                                            Console.Write("Input any key to continue...\n");
                                            Console.ReadKey();
                                        }
                                    }
                                    break;


                                case 3: // FLEE ACTION
                                    while (actionOngoing)
                                    {
                                        try
                                        {
                                            Console.Clear();
                                            Console.WriteLine("= = = = = = = = = = = = = = = = = = = = = = ");
                                            Console.WriteLine("        G A M B L E R ' S  R P G");
                                            Console.WriteLine("= = = = = = = = = = = = = = = = = = = = = = \n");

                                            Console.Write($"Turn {turnCounter}:\n\n");
                                            Console.Write("Player's Turn:\n");
                                            Console.Write($"{playerName} HP: {healthPoints} ATK: {attackPoints} || {levelEnemy} HP: {enemyHP} ATK: {enemyATK}\n\n");

                                            Console.Write("Do you wish to Flee?\n");
                                            Console.Write("[1] Yes [2] No\n\n");

                                            int userConfirmation = int.Parse(Console.ReadLine()); // MAKES SURE THAT USER WANTS TO DO THE ACTION

                                            if (userConfirmation == 1)
                                            {
                                                int diceRoller = rnd.Next(1, 5); // CHANCE ROLLER (FLEEING A BATTLE IS 25% SUCCESSFUL) -> DONE FOR BALANCE

                                                switch (diceRoller) 
                                                {
                                                    case 1:
                                                    case 2:
                                                    case 3:
                                                        Console.WriteLine("\nFlee has failed..."); // IF IT ROLLS 1-3, FLEE ACTION WILL FAIL AND SKIP THE PLAYER'S TURN
                                                        actionOngoing = false;
                                                        Console.ReadKey();
                                                        break;

                                                    case 4:
                                                        Console.WriteLine($"\nFlee Successful! You have ran away from {levelEnemy}"); // IF IT ROLLS 4, FLEE BECOMES SUCCESSFUL AND SKIPS TO NEXT LEVEL
                                                        flee = true;
                                                        actionOngoing = false;
                                                        Console.ReadKey();
                                                        break;
                                                    }
                                            }

                                            else if (userConfirmation == 2) // IF USER ISN'T SURE, GOES BACK TO INITIAL CHOICE / MOVE SELECTION MENU
                                            {
                                                break;
                                            }

                                            else if (userConfirmation != 1 || userConfirmation != 2) // IF USER INPUT IS NOT 1 OR 2, ERROR MESSAGE
                                            {
                                                Console.Write("\nAn error has occured, please try again\n");
                                                Console.Write("Input any key to continue...\n");
                                                Console.ReadKey();
                                            }

                                        }
                                        catch // ERROR MESSAGE
                                        {
                                            Console.Write("\nAn error has occured, please try again\n");
                                            Console.Write("Input any key to continue...\n");
                                            Console.ReadKey();
                                        }
                                    }
                                    break;
                            }  

                            if (moveChoice != 1 && moveChoice != 2 && moveChoice != 3) // ERROR MESSAGE IF INPUT IS NOT ONE OF THE CHOICES
                            {
                                Console.Write("\nAn error has occured, please try again\n");
                                Console.Write("Input any key to continue...\n");
                                Console.ReadKey();
                            }
                        }
                        catch // ERROR MESSAGE
                        {
                            Console.WriteLine("\nAn error has occured. Please try again");
                            Console.WriteLine("Input any key to continue...");
                            Console.ReadKey();
                        }

                        if (!actionOngoing) // ENDS PLAYER TURN LOOP
                        {
                            playerTurn = false;
                        }

                    } // END OF PLAYER TURN WHILE LOOP

                    if (flee) // CHECKS IF FLEE IS SUCCESSFUL AND IF IT IS, SKIPS TO NEXT LEVEL. OTHERWISE, DOES NOTHING
                    {
                        levelCounter++;
                        break;
                    }

                    if (enemyHP <= 0) // CHECKS IF ENEMY IS DEAD AFTER PLAYER ACTION. IF NOT, DOES NOTHING
                    {
                        Console.WriteLine($"\nLevel Cleared! Enemy '{levelEnemy}' has died");
                        Console.WriteLine($"Input any key to proceed...");
                        Console.ReadKey();
                        levelCounter++;
                        break;
                    }

                    Console.Write("\nEnemy's Turn:\n"); // ENEMY TURN
                    Console.Write($"{playerName} HP: {healthPoints} ATK: {attackPoints} || {levelEnemy} HP: {enemyHP} ATK: {enemyATK}\n\n");

                    while (true) // CHOICE RANDOMIZER
                    {
                        enemyChoice = rnd.Next(1, 3); 

                        if (enemyChoice == 2) // IF ENEMY STILL AT MAX HP, THERE IS A 0% CHANCE OF HEAL TAKING PLACE. ELSE, DOES NOTHING AND PROCEEDS
                        {
                            if (enemyHP != maxenemyHP) 
                            {
                                break;
                            }

                            else
                            {
                                continue;
                            }
                        }
                        break;
                    }
                    
                    switch (enemyChoice) // ENEMY ACTIONS
                    {
                        case 1:
                            int diceRoller = rnd.Next(1, 6); // CHANCE ROLLER -> ENEMY ATTACK. SAME RULES APPLY HERE

                            switch (diceRoller)
                            {
                                case 1: // ATK MISS ACTION
                                    Console.WriteLine("Enemy Attack has missed...");
                                    Console.ReadKey();
                                    break;

                                case 2:
                                case 3: // NORMAL ATK ACTION
                                case 4:

                                    healthPoints -= enemyATK;
                                    Console.WriteLine($"Enemy Attack Successful! You have lost {enemyATK} HP");
                                    Console.ReadKey();
                                    break;

                                case 5: // ATK CRIT ACTION

                                    healthPoints -= (enemyATK * 2);
                                    Console.WriteLine($"Critical Strike! You has lost {enemyATK * 2} HP");
                                    Console.ReadKey();
                                    break;

                            }
                            break;

                        case 2:

                            diceRoller = rnd.Next(1, 5); // CHANCE ROLLER -> ENEMY HEAL. 50/50 FOR THE SAKE OF BALANCE. SAME RULES APPLY

                            switch (diceRoller)
                            {
                                case 1: // 50% NOT HEALING
                                case 2:
                                    Console.WriteLine("\nEnemy Heal has failed...");
                                    actionOngoing = false;
                                    Console.ReadKey();
                                    break;

                                case 3: // 50% HEALING 
                                case 4:

                                    int healPlaceholder = rnd.Next(1, maxenemyHP - enemyHP); // HEAL MUST NOT LET ENEMY HEAL OVER THEIR INITIAL MAX HP. 

                                    enemyHP += healPlaceholder;
                                    Console.WriteLine($"\nEnemy Heal Successful! Enemy has healed {healPlaceholder} HP ");
                                    actionOngoing = false;
                                    Console.ReadKey();
                                    break;
                            }
                            break;
                    }

                    if (healthPoints <= 0) // CHECKS IF PLAYER DIES AFTER ENEMY ACTION
                    {
                        matchProgress = false; 
                    }

                    else // ELSE, RETURN TO PLAYER'S ACTION
                    {
                        playerTurn = true;
                        turnCounter++;
                    }

                } // END OF LEVEL / MATCH WHILE LOOP

                if (healthPoints <= 0) // IF HP IS EQUAL OR BELOW 0, TRIGGERS END OF GAME
                {
                    hasHealth = false;
                }

                else // RESETS IMPORTANT VARIABLES IN ORDER TO INITIATE PREVIOUS LOOPS
                {
                    matchProgress = true;
                    flee = false;
                    playerTurn = true;
                    turnCounter = 1;
                    continue;
                }

            } // END OF GAME LOOP


            // SYSTEM END PROGRAM

            Console.Clear();
            Console.WriteLine("= = = = = = = = = = = = = = = = = = = = = = ");
            Console.WriteLine("        G A M B L E R ' S  R P G");
            Console.WriteLine("= = = = = = = = = = = = = = = = = = = = = = \n");

            if (!hasHealth) // IF GAME HAS ENDED DUE TO LOSS OF HEALTH
            {
                Console.Write("GAME OVER!\n");
                Console.Write("You have died #SkillIssue \n");
            }

            else // IF GAME HAS BEEN BEATEN LEGITIMATELY
            {
                Console.Write("GAME OVER!\n");
                Console.Write("You have triumphantly conquered 3 levels and cleared Gambler's RPG \n");
            }
        }
    }
}