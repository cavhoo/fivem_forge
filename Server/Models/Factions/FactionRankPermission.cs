namespace Server.Models.Factions
{
  public class FactionRankPermissions
  {
    
    public string RankId { get; set; }
    public bool CanRenameRanks { get; set; }
    public bool AddRemoveRanks { get; set; }
    public bool AssignRanks { get; set; }
    public bool AddRemoveMembers { get; set; }
    public bool AccessFinance { get; set; }
  }
}