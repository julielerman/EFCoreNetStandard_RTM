
using SamuraiTracker.Domain.Enums;

namespace SamuraiTracker.Domain.Interfaces

{
  public interface IObjectWithState
  {
    ObjectState State { get; set; }
  }
  
}

