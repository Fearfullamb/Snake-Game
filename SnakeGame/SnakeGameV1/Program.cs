using System.Threading; // Needed for the game speed delay

bool isRunning = true;

int snakeX = 5;
int snakeY = 5;
int lives = 3;
int score = 0;
Random rand = new();
int foodX = rand.Next(0, 10);
int foodY = rand.Next(0, 10);

// Direction variables (starts moving Right automatically)
int moveX = 1;
int moveY = 0;

while (isRunning)
{
    // 1. Check if a key was pressed (non-blocking)
    if (Console.KeyAvailable)
    {
        ConsoleKeyInfo keyInfo = Console.ReadKey(true);

        if (keyInfo.Key == ConsoleKey.Q)
        {
            isRunning = false;
        }
        else if (keyInfo.Key == ConsoleKey.W)
        {
            moveX = 0;
            moveY = -1; // Move Up
        }
        else if (keyInfo.Key == ConsoleKey.S)
        {
            moveX = 0;
            moveY = 1;  // Move Down
        }
        else if (keyInfo.Key == ConsoleKey.A)
        {
            moveX = -1;
            moveY = 0;  // Move Left
        }
        else if (keyInfo.Key == ConsoleKey.D)
        {
            moveX = 1;
            moveY = 0;  // Move Right
        }
    }

    // 2. Calculate the next position based on current direction
    int nextX = snakeX + moveX;
    int nextY = snakeY + moveY;

    // Check if the next step is inside the grid walls (0 to 9)
    if (nextX >= 0 && nextX < 10 && nextY >= 0 && nextY < 10)
    {
        snakeX = nextX;
        snakeY = nextY;
    }
    else
    {
        // Hit a wall! Lose a life and stop moving until a new key is pressed
        lives--;
        moveX = 0;
        moveY = 0;
    }

    // Check for Game Over
    if (lives <= 0)
    {
        Console.Clear();
        Console.WriteLine("=== GAME OVER ===");
        Console.WriteLine("You crashed into the walls 3 times!");
        break;
    }

    // 3. Draw the game board
Console.Clear();
    Console.WriteLine("=== SNAKE GAME ===");
    Console.WriteLine($"Lives Left: {lives}  |  Score: {score}");
    Console.WriteLine("Controls: W, A, S, D | Q to exit\n");

    // Check if snake is eating the food right before we draw
    if (snakeX == foodX && snakeY == foodY)
    {
        score++;
        foodX = rand.Next(10);
        foodY = rand.Next(10);
    }
    
    // Outer loop for rows (this creates 'y'!)
    for (int y = 0; y < 10; y++)
    {
        // Inner loop for columns (this creates 'x'!)
        for (int x = 0; x < 10; x++)
        {
            if (x == snakeX && y == snakeY)
            {
                Console.Write("O ");
            }
            else if (x == foodX && y == foodY)
            {
                Console.Write("X ");
            }
            else
            {
                Console.Write(". ");
            }
        }
        Console.WriteLine();
    }
    
    // 4. Pause for 300 milliseconds
    Thread.Sleep(300);
}

Console.WriteLine("System shutting down...");