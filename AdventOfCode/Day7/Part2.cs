
namespace Day7;

internal static class Part2
{
    private class Node
    {
        public bool IsDirectory;
        public string Name;
        public int Size;
        public readonly Dictionary<string, Node> Children = new();
        public Node parent;
    }

    private class FileSystem
    {
        private readonly Node root;
        private Node current;

        public FileSystem(string rootDirectory)
        {
            this.root = new Node()
            {
                IsDirectory = true,
                Name = rootDirectory
            };
            this.current = this.root;
        }

        public void ChangeDir(string name)
        {
            if (name == "..")
            {
                this.current = this.current.parent;
                return;
            }
            if (!this.current.Children.TryGetValue(name, out var next)) return;

            if (next.IsDirectory)
            {
                this.current = next;
            }
        }

        public void AddDirectory(string directoryName)
        {
            var node = new Node()
            {
                IsDirectory = true,
                Name = directoryName,
                parent = this.current
            };

            this.current.Children[directoryName] = node;
        }

        public void AddFile(string size, string fileName)
        {
            var node = new Node()
            {
                Name = fileName,
                parent = this.root,
                Size = int.Parse(size)
            };

            this.current.Children[fileName] = node;
        }

        public int GetSize()
        {
            var nodes = DirectoriesBelowLimit(this.root);
            return nodes.Sum(GetDirectorySize);
        }

        private List<Node> DirectoriesBelowLimit(Node directory)
        {
            List<Node> nodes = new List<Node>();
            if (directory.IsDirectory && GetDirectorySize(directory) < 100000)
            {
                nodes.Add(directory);
            }

            foreach (var directoryChild in directory.Children.Values)
            {
                if (directoryChild.IsDirectory)
                {
                    nodes.AddRange(DirectoriesBelowLimit(directoryChild));
                }
            }

            return nodes;
        }

        private static int GetDirectorySize(Node directory)
        {
            int size = 0;
            foreach (var childrenValue in directory.Children.Values)
            {
                if (childrenValue.IsDirectory)
                {
                    size += GetDirectorySize(childrenValue);
                }
                else
                {
                    size += childrenValue.Size;
                }
            }

            return size;
        }

        private const int MaxSize =    70000000;
        private const int NeededSize = 30000000;

        public void GetDirectoryToDelete()
        {
            int currentSize = GetDirectorySize(this.root);
            int remainingNeeded = MaxSize - currentSize;
            int smallest = int.MaxValue;
            Node? smallestDirectory = SmallestDirectoryAbove(this.root, NeededSize - remainingNeeded, ref smallest);
            Console.WriteLine(GetDirectorySize(smallestDirectory!));
        }

        private Node? SmallestDirectoryAbove(Node directory, int size, ref int smallest)
        {
            foreach (var directoryChild in directory.Children.Values)
            {
                if (!directoryChild.IsDirectory) continue;

                var smallerNode = this.SmallestDirectoryAbove(directoryChild, size, ref smallest);
                if (smallerNode != null)
                {
                    directory = smallerNode;
                }
            }

            int directorySize = GetDirectorySize(directory);
            if (directorySize < size || directorySize > smallest) return null;
            smallest = directorySize;
            return directory;
        }
    }

    private enum Command
    {
        None,
        ChangeDir,
        List
    }

    private static Command ParseCommand(string command)
    {
        return command switch
        {
            "cd" => Command.ChangeDir,
            "ls" => Command.List,
            _ => Command.None
        };
    }

    public static void Run()
    {
        FileSystem s = new FileSystem("/");
        Command currentCommand = Command.None;

        foreach (var readLine in File.ReadLines("input.txt"))
        {
            if (readLine.StartsWith("$"))
            {
                var parts = readLine.Split(' ');
                currentCommand = ParseCommand(parts[1]);
                if (currentCommand == Command.ChangeDir)
                {
                    s.ChangeDir(parts[2]);
                }

                continue;
            }

            if (currentCommand == Command.List)
            {
                var parts = readLine.Split(' ');
                if (parts[0] == "dir")
                {
                    s.AddDirectory(parts[1]);
                }
                else
                {
                    s.AddFile(parts[0], parts[1]);
                }
            }
        }
        s.GetDirectoryToDelete();
    }
}