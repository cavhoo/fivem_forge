extern alias CFX;
using System;
using System.Collections.Generic;
using CFX::CitizenFX.Core;
using FiveMForgeClient.Services.Language;

namespace FiveMForgeClient.Models.Character
{
  public static class CharacterComponents
  {
    public static readonly string[] InheritanceMoms =
    {
      "Hannah", "Aubrey", "Jasmine", "Gisele", "Amelia", "Isabella", "Zoe", "Ava", "Camila", "Violet", "Sophia",
      "Evelyn", "Nicole", "Ashley", "Gracie", "Brianna", "Natalie", "Olivia", "Elizabeth", "Charlotte", "Emma"
    };

    public static readonly string[] InheritanceDads = 
    {
      "Benjamin", "Daniel", "Joshua", "Noah", "Andrew", "Juan", "Alex", "Isaac", "Evan", "Ethan", "Vincent", "Angel",
      "Diego", "Adrian", "Gabriel", "Michael", "Santiago", "Kevin", "Louis", "Samuel", "Anthony", " Claude", "Niko"
    };

    public static readonly string[] MaleHairStyles = 
    {
      "Close Shave", "Buzzcut", "Faux Hawk", "Hipster", "Side Parting", "Shorter Cut", "Biker", "Ponytail", "Cornrows",
      "Slicked", "Short Brushed", "Spikey",
      "Caesar", "Chopped", "Dreads", "Long Hair", "Shaggy Curls", "Surfer Dude", "Short Side Part",
      "High Slicked Sides", "Long Slicked", "Hipster Youth", "Mullet", "Nightvision"
    };

    public static readonly string[] FemaleHairStyles = 
    {
      "Close Shave", "Short", "Layered Bob", "Pigtails", "Ponytail", "Braided Mohawk", "Braids", "Bob", "Faux Hawk",
      "French Twist", "Long Bob", "Loose Tied",
      "Pixie", "Shaved Bangs", "Top Knot", "Wavy Bob", "Pin Up Girl", "Messy Bun", "Unknown", "Tight Bun",
      "Twisted Bob", "Big Bangs", "Braided Top Knot", "Mullet", "Nightvision"
    };

    public static readonly string[] EyebrowStyles = 
    {
      "None", "Balanced", "Fashion", "Cleopatra", "Quizzical", "Femme", "Seductive", "Pinched", "Chola", "Triomphe",
      "Carefree", "Curvaceous", "Rodent",
      "Double Tram", "Thin", "Penciled", "Mother Plucker", "Straight and Narrow", "Natural", "Fuzzy", "Unkempt",
      "Caterpillar", "Regular", "Mediterranean", "Groomed", "Bushels",
      "Feathered", "Prickly", "Monobrow", "Winged", "Triple Tram", "Arched Tram", "Cutouts", "Fade Away", "Solo Tram"
    };

    public static readonly string[] BeardStyles = 
    {
      "Clean Shaven", "Light Stubble", "Balbo", "Circle Beard", "Goatee", "Chin", "Chin Fuzz", "Pencil Chin Strap",
      "Scruffy", "Musketeer", "Mustache",
      "Trimmed Beard", "Stubble", "Thin Circle Beard", "Horseshoe", "Pencil and Chops", "Chin Strap Beard",
      "Balbo and Sideburns", "Mutton Chops", "Scruffy Beard", "Curly",
      "Curly and Deep Stranger", "Handlebar", "Faustic", "Otto and Patch", "Otto and Full Stranger", "Light Franz",
      "The Hampstead", "The Ambrose", "Lincoln Curtain"
    };

    public static readonly string[] SkinImperfections = 
    {
      "None", "Measles", "Pimples", "Spots", "Break Out", "Blackheads", "Build Up", "Pustules", "Zits", "Full Acne",
      "Acne", "Cheek Rash", "Face Rash",
      "Picker", "Puberty", "Eyesore", "Chin Rash", "Two Face", "T Zone", "Greasy", "Marked", "Acne Scarring",
      "Full Acne Scarring", "Cold Sores", "Impetigo"
    };

    public static readonly string[] AgingSigns = 
    {
      "None", "Crow's Feet", "First Signs", "Middle Aged", "Worry Lines", "Depression", "Distinguished", "Aged",
      "Weathered", "Wrinkled", "Sagging", "Tough Life",
      "Vintage", "Retired", "Junkie", "Geriatric"
    };

    public static readonly string[] SkinComplexion = 
    {
      "None", "Rosy Cheeks", "Stubble Rash", "Hot Flush", "Sunburn", "Bruised", "Alchoholic", "Patchy", "Totem",
      "Blood Vessels", "Damaged", "Pale", "Ghostly"
    };

    public static readonly string[] MolesAndFreckles = 
    {
      "None", "Cherub", "All Over", "Irregular", "Dot Dash", "Over the Bridge", "Baby Doll", "Pixie", "Sun Kissed",
      "Beauty Marks", "Line Up", "Modelesque",
      "Occasional", "Speckled", "Rain Drops", "Double Dip", "One Sided", "Pairs", "Growth"
    };

    public static readonly string[] SunSkinEffects = 
    {
      "None", "Uneven", "Sandpaper", "Patchy", "Rough", "Leathery", "Textured", "Coarse", "Rugged", "Creased",
      "Cracked", "Gritty"
    };

    public static readonly string[] EyeColor = 
    {
      "Green", "Emerald", "Light Blue", "Ocean Blue", "Light Brown", "Dark Brown", "Hazel", "Dark Gray", "Light Gray",
      "Pink", "Yellow", "Purple", "Blackout",
      "Shades of Gray", "Tequila Sunrise", "Atomic", "Warp", "ECola", "Space Ranger", "Ying Yang", "Bullseye", "Lizard",
      "Dragon", "Extra Terrestrial", "Goat", "Smiley", "Possessed",
      "Demon", "Infected", "Alien", "Undead", "Zombie"
    };

    public static readonly string[] MakeUp = 
    {
      "None", "Smoky Black", "Bronze", "Soft Gray", "Retro Glam", "Natural Look", "Cat Eyes", "Chola", "Vamp",
      "Vinewood Glamour", "Bubblegum", "Aqua Dream",
      "Pin up", "Purple Passion", "Smoky Cat Eye", "Smoldering Ruby", "Pop Princess",
      "Kiss My Axe", "Panda Pussy", "The Bat", "Skull in Scarlet", "Serpentine", "The Veldt", "Unknown 1", "Unknown 2",
      "Unknown 3", "Unknown 4", "Tribal Lines", "Tribal Swirls",
      "Tribal Orange", "Tribal Red", "Trapped in A Box", "Clowning",
      "Guyliner", "Unknown 5", "Blood Tears", "Heavy Metal", "Sorrow", "Prince of Darkness", "Rocker", "Goth", "Punk",
      "Devastated"
    };

    public static readonly string[] LipStick = 
    {
      "None", "Color Matte", "Color Gloss", "Lined Matte", "Lined Gloss", "Heavy Lined Matte", "Heavy Lined Gloss",
      "Lined Nude Matte", "Liner Nude Gloss",
      "Smudged", "Geisha"
    };

    public static readonly string[] ChestHair = 
    {
      "None", "Color Matte", "Color Gloss", "Lined Matte", "Lined Gloss", "Heavy Lined Matte", "Heavy Lined Gloss",
      "Lined Nude Matte", "Liner Nude Gloss",
      "Smudged", "Geisha"
    };

    public static readonly string[] Blush = 
    {
      "None", "Full", "Angled", "Round", "Horizontal", "High", "Sweetheart", "Eighties"
    };

    public static readonly string[] ClothingCategories = 
    {
      "Masks", "Unused (hair)", "Gloves", "Pants", "Bags & Parachutes", "Shoes", "Necklace and Ties", "Under Shirt",
      "Body Armor", "Decals & Logos", "Shirt & Jackets"
    };
  }
}