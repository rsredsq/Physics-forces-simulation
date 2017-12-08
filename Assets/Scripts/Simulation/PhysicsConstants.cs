using System;

namespace Simulation {
  public static class PhysicsConstants {
    private const float SCALE = 1e7f;

    public const float COULOMB_KOEF = 8.988e9f * SCALE * SCALE;
    public const float LORENTZ_FOEF = 1e-7f * SCALE * SCALE;

    public const float ACCURACY = 1e-6f;
  }
}
