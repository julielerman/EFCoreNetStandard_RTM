using SamuraiTracker.Domain.Enums;
using SamuraiTracker.Domain.Interfaces;
using System;
using System.Collections.Generic;

namespace SamuraiTracker.Domain
{
  public class Battle : IObjectWithState
  {
    public Battle() {
      //is empty ctor still required by EF (would think so)
    }

    public Battle(string name, DateTime start, DateTime end) {
      Name = name;
      StartDate = start;
      EndDate = end;
    }

    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime StartDate { get; private set; }
    public DateTime EndDate { get; private set; }
    public ObjectState State { get; set; }

    public Location Location { get; set; }
    public List<SamuraiBattle> SamuraiBattles { get; set; }
  }
}