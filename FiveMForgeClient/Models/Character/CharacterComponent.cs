using System.Security.Policy;

namespace FiveMForgeClient.Models.Character
{
    public struct CharacterComponent
    {
        public string Label { get; set; }
        public string Name { get; set; }
        public decimal Value { get; set; }
        public decimal Min { get; set; }
        public float ZoomOffset { get; set; }
        public float CamOffset { get; set; }
        public string Texture { get; set; }
        
        public int ComponentId { get; set; }

        public CharacterComponent(string label, string name, decimal value, decimal min, float zoomOffset,
            float camOffset)
        {
            Label = label;
            Name = name;
            Value = value;
            Min = min;
            ZoomOffset = zoomOffset;
            CamOffset = camOffset;
            Texture = null;
            ComponentId = -1;
        }
        public CharacterComponent(string label, string name, decimal value, decimal min, float zoomOffset,
            float camOffset, string texture) : this(label, name, value, min, zoomOffset, camOffset)
        {
            Texture = texture;
        }

        public CharacterComponent(string label, string name, decimal value, decimal min, float zoomOffset,
            float camOffset, int componentId) : this(label, name, value, min, zoomOffset, camOffset)
        {
            ComponentId = componentId;
        }
        
    }
}