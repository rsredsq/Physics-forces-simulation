using System;

namespace Simulation {
  public class PhysicsConstants {
    private const double SCALE = 1e3;

    public const double COULOMB_KOEF = 8.987552e9 * SCALE * SCALE;
    public const double LORENTZ_FOEF = 1e-7 / SCALE;

    public const float ACCURACY = 1e-6f;
  }
}
