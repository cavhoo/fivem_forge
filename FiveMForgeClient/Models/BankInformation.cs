namespace FiveMForgeClient.Models
{
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
}