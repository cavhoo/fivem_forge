namespace FiveMForge.Models
{
    public interface IBankInformation
    {
        int SpriteId { get; set; }
        string Name { get; set; }
        float X { get; set; }
        float Y { get; set; }
        float Z { get; set; }
        bool IsActive { get; set; }
        bool IsAdminOnly { get; set; }
    }
    public struct BankInformation
    {
        public BankInformation(string name, int spriteId, float x, float y, float z, bool isActive, bool isAdminOnly)
        {
            Name = name;
            SpriteId = spriteId;
            X = x;
            Y = y;
            Z = z;
            IsActive = isActive;
            IsAdminOnly = isAdminOnly;
        }

        public int SpriteId { get; set; }
        public string Name { get; set; }
        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }
        public bool IsActive { get; set; }
        public bool IsAdminOnly { get; set; }

        public override string ToString()
        {
            return $"{Name},{IsActive}, {IsAdminOnly}, '{X}:{Y}:{Z}'";
        }
    }
    
    public static class BankLocations
    {
        public static BankInformation[] Locations =
        {
            new("Bank", 108, 150.266f, 1040.203f, 29.374f, true, false),
            new("Bank", 108, -1212.980f, -330.841f, 37.787f, true, false),
            new("Bank", 108, -2962.582f, 482.627f, 15.703f, true, false),
            new("Bank", 108, -112.202f, 6469.295f, 31.626f, true, false),
            new("Bank", 108, 314.187f, -278.621f, 54.170f, true, false),
            new("Bank", 108, -351.534f, -49.529f, 49.042f, true, false),
            new("Pacific Bank", 106, 241.727f, 220.706f, 106.286f, true, true),
            new("Bank", 108, 1175.0643310547f, 2706.6435546875f, 38.094036102295f, true, false)
        };
    }
}