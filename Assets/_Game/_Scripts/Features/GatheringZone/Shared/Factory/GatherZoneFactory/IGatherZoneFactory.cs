using _Game._Scripts.DataTypes.Zones;
using UnityEngine;
namespace _Game._Scripts.Features.GatheringZone.Shared.Factory.GatherZoneFactory {
  public interface IGatherZoneFactory {
    GameObject Create(ZoneSpawnArgs args);
  }
}
