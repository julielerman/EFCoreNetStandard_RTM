using SamuraiTracker.Domain.Enums;
using SamuraiTracker.Domain.Interfaces;

namespace SamuraiTracker.Domain
{
  public class SecretIdentity : IObjectWithState
  {
    public int Id { get; set; }
    public string RealName { get; set; }
    public Samurai Samurai { get; set; }
    public int SamuraiId { get; set; }
    public ObjectState State { get; set; }
  }
}