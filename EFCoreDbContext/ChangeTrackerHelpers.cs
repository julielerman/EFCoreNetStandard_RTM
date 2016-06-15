using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using SamuraiTracker.Domain.Enums;
using SamuraiTracker.Domain.Interfaces;

namespace EFCoreDbContext
{
  public static class ChangeTrackerHelpers
    {
    public static void ConvertStateOfNode(EntityEntryGraphNode node) {
      IObjectWithState entity = (IObjectWithState)node.Entry.Entity;
      node.Entry.State = ConvertToEFState(entity.State);
    }

    private static EntityState ConvertToEFState(ObjectState objectState) {
      EntityState efState = EntityState.Unchanged;
      switch (objectState) {
        case ObjectState.Added:
          efState = EntityState.Added;
          break;
        case ObjectState.Modified:
          efState = EntityState.Modified;
          break;
        case ObjectState.Deleted:
          efState = EntityState.Deleted;
          break;
        case ObjectState.Unchanged:
          efState = EntityState.Unchanged;
          break;
      }
      return efState;
    }
  }
}

