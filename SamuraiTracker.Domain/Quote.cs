using SamuraiTracker.Domain.Enums;
using SamuraiTracker.Domain.Interfaces;

namespace SamuraiTracker.Domain
{
  public class Quote : IObjectWithState
  {
    public int Id { get; set; }
    public string Text { get; set; }
    public ObjectState State { get; set; }

    //public Samurai Samurai { get; set; }
    public int SamuraiId { get; set; }
  }
}