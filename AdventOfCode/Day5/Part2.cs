namespace Day5
{
    internal static class Part2
    {
        public static void Run()
        {
            List<Stack<char>> stacks = new();
            List<List<char>> stacksTopToBottom = new();

            void Move(int number, int from, int to)
            {
                var moveQueue = new List<char>();
                for (int i = 0; i < number; i++)
                {
                    moveQueue.Add(stacks[from].Pop());
                }

                moveQueue.Reverse();
                foreach (var c in moveQueue)
                {
                    stacks[to].Push(c);
                }
            }

            bool parsingStack = true;
            foreach (var readLine in File.ReadLines("input.txt"))
            {
                if (!readLine.Any())
                {
                    foreach (var stackList in stacksTopToBottom)
                    {
                        var newStack = new Stack<char>();
                        stacks.Add(newStack);
                        stackList.Reverse();
                        foreach (var c in stackList)
                        {
                            newStack.Push(c);
                        }
                    }
                    parsingStack = false;
                }
                else
                {
                    if (parsingStack)
                    {
                        for (int i = 1; i < readLine.Length; i += 4)
                        {
                            if (char.IsNumber(readLine[i])) continue;

                            var index = (i - 1) / 4;
                            if (stacksTopToBottom.Count <= index) stacksTopToBottom.Add(new List<char>());
                            if (readLine[i] == ' ') continue;
                            var stackInputList = stacksTopToBottom[index];
                            stackInputList.Add(readLine[i]);
                        }
                    }
                    else
                    {
                        var parts = readLine.Split(' ');
                        var number = int.Parse(parts[1]);
                        var from = int.Parse(parts[3]) - 1;
                        var to = int.Parse(parts[5]) - 1;
                        Move(number, from, to);
                    }
                }
            }

            foreach (var stack in stacks)
            {
                Console.Write(stack.Pop());
            }
        }
    }
}
