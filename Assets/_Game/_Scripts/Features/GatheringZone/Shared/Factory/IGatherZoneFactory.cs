using UnityEngine;
namespace _Game._Scripts.Features.GatheringZone.Shared.Factory {
  public interface IGatherZoneFactory {
    GameObject Create(ZoneSpawnArgs args);
  }
}
