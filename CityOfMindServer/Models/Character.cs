using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FiveMForge.Models
{
  public class Character
  {
    public int Id { get; set; }
    public string Gender { get; set; }
    public string LastPos { get; set; }
    [Key]
    public string CharacterUuid { get; set; }
    public string Firstname { get; set; }
    public string Lastname { get; set; }
    public string Birthdate { get; set; }
    public bool InUse { get; set; }

    public Player Owner { get; set; }
    [ForeignKey("Owner")] public string AccountUuid { get; set; }

    public Job Job { get; set; }
    [ForeignKey("Job")] public string JobUuid { get; set; }
    [DefaultValue(0)]
    public int Mom { get; set; }
    [DefaultValue(0)]
    public int Dad { get; set; }
    [DefaultValue(0.5f)]
    public float FaceResemblence { get; set; }
    [DefaultValue(0.5f)]
    public float SkinResemblence { get; set; }
    
    /// <summary>
    /// Nose Values
    /// </summary>

    [DefaultValue(0.5f)]
    public float NoseWidth { get; set; }
    [DefaultValue(0.5f)]
    public float NoseTipLength { get; set; }
    [DefaultValue(0.5f)]
    public float NoseTipHeight { get; set; }
    [DefaultValue(0.5f)]
    public float NoseBoneBend { get; set; }
    [DefaultValue(0.5f)]
    public float NoseBoneOffset { get; set; }
    [DefaultValue(0.5f)]
    public float NoseTipLowering { get; set; }

    /// <summary>
    /// Eye Values
    /// </summary>
    [DefaultValue(0.5f)]
    public float EyeOpening { get; set; }

    [DefaultValue(0)]
    public int EyeColor { get; set; }
    [DefaultValue(0.5f)]
    public float EyeBrowHeight { get; set; }
    [DefaultValue(0.5f)]
    public float EyeBrowBulkiness { get; set; }
    [DefaultValue(0.5f)]
    public int EyeBrowStyle { get; set; }
    [DefaultValue(0.5f)]
    public int EyeBrowColor { get; set; }

    /// <summary>
    /// Cheek Values
    /// </summary>
    [DefaultValue(0.5f)]
    public float CheekWidth { get; set; }
    [DefaultValue(0.5f)]
    public float CheekBoneHeight { get; set; }
    [DefaultValue(0.5f)]
    public float CheekBoneWidth { get; set; }

    /// <summary>
    /// MakeUp Values
    /// </summary>
    [DefaultValue(0.5f)]
    public int MakeUpVariant { get; set; }

    [DefaultValue(0.5f)]
    public int MakeUpColor { get; set; }
    [DefaultValue(0.5f)]
    public int BlushVariant { get; set; }
    [DefaultValue(0.5f)]
    public int BlushColor { get; set; }

    /// <summary>
    /// Hair Values
    /// </summary>
    [DefaultValue(0)]
    public int HairStyle { get; set; }
    [DefaultValue(0)]
    public int HairColor { get; set; }
    [DefaultValue(0.5f)]
    public int HairHighlightColor { get; set; }

    /// <summary>
    /// Chin Values
    /// </summary>
    [DefaultValue(0.5f)]
    public float ChinHeight { get; set; }
    [DefaultValue(0.5f)]
    public float ChinGapSize { get; set; }
    [DefaultValue(0.5f)]
    public float ChinForward { get; set; }

    /// <summary>
    /// Lip Values
    /// </summary>
    [DefaultValue(0.5f)]
    public float LipThickness { get; set; }
    [DefaultValue(0)]
    public int LipStickVariant { get; set; }
    [DefaultValue(0)]
    public int LipStickColor { get; set; }

    /// <summary>
    /// Beard Values
    /// </summary>
    [DefaultValue(0)]
    public int BeardVariant { get; set; }
    [DefaultValue(0)]
    public int BeardColor { get; set; }

    /// <summary>
    /// Chesthair Values
    /// </summary>
    [DefaultValue(0)]
    public int ChestHairVariant { get; set; }
    [DefaultValue(0)]
    public int ChestHairColor { get; set; }
  }
}