namespace Day6
{
    internal static class Part1
    {
        public static void Run()
        {
            using FileStream stream = File.OpenRead("input.txt");
            const int MarkerLength = 14;
            var shiftRegister = new char?[MarkerLength];
            int streamIndex = 0;
            
            while(true)
            {
                var characterByte = stream.ReadByte();
                if (characterByte == -1) break;
                var character = (char)characterByte;
                if(streamIndex > MarkerLength)
                {
                    if (shiftRegister.Distinct().Count() == MarkerLength)
                    {
                        Console.WriteLine(streamIndex);
                        break;
                    }
                }
                Array.Copy(shiftRegister, 1, shiftRegister, 0, shiftRegister.Length - 1);
                shiftRegister[MarkerLength-1] = character;
                streamIndex++;
            }
        }
    }
}
