namespace KnightsandCastles
{
  public class Knight
  {
    public int Id { get; set; }
    public string KnightName { get; set; }

    public string Castle { get; set; }

    public int? CastleId { get; set; }
    public string Weapon { get; set; }
  }
}