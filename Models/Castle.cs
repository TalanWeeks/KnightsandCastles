namespace KnightsandCastles.Models
{
  public class Castle
  {
    public int Id { get; set; }
    public string CastleName { get; set; }

    public string CreatorId { get; set; }
    public string Creator { get; set; }
    public int Population { get; set; }    
  }
}