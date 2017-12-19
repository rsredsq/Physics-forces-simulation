using System;

namespace Simulation {
  public static class PhysicsConstants {
    private const float SCALE = 1e4f;

    public const float COULOMB_KOEF = 8.988e9f * SCALE * SCALE;
    public const float LORENTZ_FOEF = 1e-7f * SCALE * SCALE;

    public const float ACCURACY = 1e-13f;

    public const float DEFAULT_CHARGE = 1.5e-5f;
  }
}
