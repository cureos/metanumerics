﻿using System;

using Meta.Numerics.Functions;

namespace Meta.Numerics {

    /// <summary>
    /// Provides simple functions of complex arguments. 
    /// </summary>
    public static class ComplexMath {

        // static pure imaginary

        /// <summary>
        /// Gets the unit imaginary number I.
        /// </summary>
        /// <value>The unit imaginary number.</value>
        public static Complex I {
            get {
                return (new Complex(0.0, 1.0));
            }
        }

        // basic functions of complex arguments

        /// <summary>
        /// Computes the absolute value of a complex number.
        /// </summary>
        /// <param name="z">The argument.</param>
        /// <returns>The value of |z|.</returns>
        /// <remarks>
        /// <para>The absolute value of a complex number is the distance of the number from the origin
        /// in the complex plane. This is a compatible generalization of the definition of the absolute
        /// value of a real number.</para>
        /// </remarks>
        /// <seealso cref="Math.Abs(Double)"/>
        public static double Abs (Complex z) {
            return (MoreMath.Hypot(z.Re, z.Im));
        }

        /// <summary>
        /// Computes the phase of a complex number.
        /// </summary>
        /// <param name="z">The argument.</param>
        /// <returns>The value of arg(z).</returns>
        /// <remarks>
        /// <para>The phase of a complex number is the angle between the line joining it to the origin and the real axis of the complex plane.</para>
        /// <para>The phase of complex numbers in the upper complex plane lies between 0 and &#x3C0;. The phase of complex numbers
        /// in the lower complex plane lies between 0 and -&#x3C0;. The phase of a positive real number is zero.</para>
        /// </remarks>
        public static double Arg (Complex z) {
            // returns 0 to PI in the upper complex plane (Im>=0),
            // 0 to -PI in the lower complex plane (Im<0)
            return (Math.Atan2(z.Im, z.Re));
        }

        /// <summary>
        /// Computes e raised to the power of a complex number.
        /// </summary>
        /// <param name="z">The argument.</param>
        /// <returns>The value of e<sup>z</sup>.</returns>
        public static Complex Exp (Complex z) {
            double m = Math.Exp(z.Re);
            return (new Complex(m * MoreMath.Cos(z.Im), m * MoreMath.Sin(z.Im)));
        }

        /// <summary>
        /// Computes the natrual logarithm of a complex number.
        /// </summary>
        /// <param name="z">The argument.</param>
        /// <returns>The value of ln(z).</returns>
        /// <remarks>
        /// <para>The image below shows the complex log function near the origin, using domain coloring.</para>
        /// <img src="../images/ComplexLogPlot.png" />
        /// <para>You can see the zero at (0, 1) and the branch cut extending along the negative real axis from the pole at the origin.</para>
        /// </remarks>
        public static Complex Log (Complex z) {
            return (new Complex(Math.Log(Abs(z)), Arg(z)));
        }


        /// <summary>
        /// Computes the square of a complex number.
        /// </summary>
        /// <param name="z">The argument.</param>
        /// <returns>The value of z<sup>2</sup>.</returns>
        public static Complex Sqr (Complex z) {
            return (z * z);
        }

        /// <summary>
        /// Computes the square root of a complex number.
        /// </summary>
        /// <param name="z">The argument.</param>
        /// <returns>The square root of the argument.</returns>
        /// <remarks>
        /// <para>The image below shows the complex square root function near the origin, using domain coloring.</para>
        /// <img src="../images/ComplexSqrtPlot.png" />
        /// <para>You can see the branch cut extending along the negative real axis from the zero at the origin.</para>
        /// </remarks>
        public static Complex Sqrt (Complex z) {
            if (z.Im == 0.0) {
                // Handle the degenerate case quickly.
                // This also eliminates need to worry about Im(z) = 0 in subsequent formulas.
                if (z.Re < 0.0) {
                    return (new Complex(0.0, Math.Sqrt(-z.Re)));
                } else {
                    return (new Complex(Math.Sqrt(z.Re), 0.0));
                }
            } else {

                // To find a fast formula for complex square root, note x + i y = \sqrt{a + i b} implies
                // x^2 + 2 i x y - y^2 = a + i b, so x^2 - y^2 = a and 2 x y = b. Cross-substitute and
                // use quadratic formula to solve for x^2 and y^2 to obtain
                //  x = \sqrt{\frac{\sqrt{a^2 + b^2} + a}{2}}  y = \pm \sqrt{\frac{\sqrt{a^2 + b^2} - a}{2}}
                // This gives complex square root in three square roots and a few flops.

                // Only problem is b << a case, where significant cancelation occurs in one of the formula.
                // (Which one depends on the sign of a.) Handle that case by series expansion.

                double p, q;
                if (Math.Abs(z.Im) < 0.25 * Math.Abs(z.Re)) {
                    double x2 = MoreMath.Sqr(z.Im / z.Re);
                    double t = x2 / 2.0;
                    double s = t;
                    // Find s = \sqrt{1 + x^2} - 1 using binomial expansion
                    for (int k = 2; true; k++ ) {
                        if (k > Global.SeriesMax) throw new NonconvergenceException();
                        double s_old = s;
                        t *= (1.5 / k - 1.0) * x2;
                        s += t;
                        if (s == s_old) break;
                    }
                    if (z.Re < 0.0) {
                        p = -z.Re * s;
                        q = -2.0 * z.Re + p;
                    } else {
                        q = z.Re * s;
                        p = 2.0 * z.Re + q;
                    }
                } else {
                    double m = ComplexMath.Abs(z);
                    p = m + z.Re;
                    q = m - z.Re;
                }

                double x = Math.Sqrt(p / 2.0);
                double y = Math.Sqrt(q / 2.0);
                if (z.Im < 0.0) y = -y;
                return (new Complex(x, y));

                /*
                if (Math.Abs(z.Im) < 0.125 * Math.Abs(z.Re)) {
                    // We should try to improve this by using a series instead of the full power algorithm.
                    return (Pow(z, 0.5));
                } else {
                    // This is a pretty fast formula for a complex square root, basically just
                    // three square roots and a few flops.
                    // But if z.Im << z.Re, then z.Re ~ m and it suffers from cancelations.
                    double m = Abs(z);
                    double x = Math.Sqrt((m + z.Re) / 2.0);
                    double y = Math.Sqrt((m - z.Re) / 2.0);
                    if (z.Im < 0.0) y = -y;
                    return (new Complex(x, y));
                }
                */
            }
        }

        /// <summary>
        /// Computes the sine of a complex number.
        /// </summary>
        /// <param name="z">The argument.</param>
        /// <returns>The value of sin(z).</returns>
        public static Complex Sin (Complex z) {
            double p = Math.Exp(z.Im);
            double q = 1 / p;
            double sinh = (p - q) / 2.0;
            double cosh = (p + q) / 2.0;
            return (new Complex(MoreMath.Sin(z.Re) * cosh, MoreMath.Cos(z.Re) * sinh));
        }

        /// <summary>
        /// Computes the hyperbolic sine of a complex number.
        /// </summary>
        /// <param name="z">The argument.</param>
        /// <returns>The value of sinh(z).</returns>
        public static Complex Sinh (Complex z) {
            // sinh(z) = -i sin(iz)
            Complex sin = Sin(new Complex(-z.Im, z.Re));
            return (new Complex(sin.Im, -sin.Re));
        }

        /// <summary>
        /// Computes the cosine of a complex number.
        /// </summary>
        /// <param name="z">The argument.</param>
        /// <returns>The value of cos(z).</returns>
        public static Complex Cos (Complex z) {
            double p = Math.Exp(z.Im);
            double q = 1 / p;
            double sinh = (p - q) / 2.0;
            double cosh = (p + q) / 2.0;
            return (new Complex(MoreMath.Cos(z.Re) * cosh, -MoreMath.Sin(z.Re) * sinh));
        }

        /// <summary>
        /// Computes the hyperbolic cosine of a complex number.
        /// </summary>
        /// <param name="z">The argument.</param>
        /// <returns>The value of cosh(z).</returns>
        public static Complex Cosh (Complex z) {
            // cosh(z) = cos(iz)
            return (Cos(new Complex(-z.Im, z.Re)));
        }

        /// <summary>
        /// Computes the tangent of a complex number.
        /// </summary>
        /// <param name="z">The argument.</param>
        /// <returns>The value of tan(z).</returns>
        public static Complex Tan (Complex z) {
            // tan z = [sin(2x) + I sinh(2y)]/[cos(2x) + I cosh(2y)]
            double x2 = 2.0 * z.Re;
            double y2 = 2.0 * z.Im;
            double p = Math.Exp(y2);
            double q = 1 / p;
            double cosh = (p + q) / 2.0;
            if (Math.Abs(z.Im) < 4.0) {
                double sinh = (p - q) / 2.0;
                double D = MoreMath.Cos(x2) + cosh;
                return (new Complex(MoreMath.Sin(x2) / D, sinh / D));
            } else {
                // when Im(z) gets too large, sinh and cosh individually blow up
                // but ratio is still ~1, so rearrage to use tanh instead
                double F = (1.0 + Math.Cos(x2) / cosh);
                return (new Complex(MoreMath.Sin(x2) / cosh / F, Math.Tanh(y2) / F));
            }
        }

        /// <summary>
        /// Computes the hyperbolic tangent of a complex number.
        /// </summary>
        /// <param name="z">The argument.</param>
        /// <returns>The value of tanh(z).</returns>
        public static Complex Tanh (Complex z) {
            // tanh(z) = -i tan(iz)
            Complex tan = Tan(new Complex(-z.Im, z.Re));
            return (new Complex(tan.Im, -tan.Re));
        }

        // pure complex binary operations

        /// <summary>
        /// Raises a complex number to an arbitrary real power.
        /// </summary>
        /// <param name="z">The argument.</param>
        /// <param name="p">The power.</param>
        /// <returns>The value of z<sup>p</sup>.</returns>
        public static Complex Pow (Complex z, double p) {
            double m = Math.Pow(Abs(z), p);
            double t = Arg(z) * p;
            return (new Complex(m * MoreMath.Cos(t), m * MoreMath.Sin(t)));
        }

        /// <summary>
        /// Raises a real number to an arbitrary complex power.
        /// </summary>
        /// <param name="x">The real base, which must be non-negative.</param>
        /// <param name="z">The complex exponent.</param>
        /// <returns>The value of x<sup>z</sup>.</returns>
        public static Complex Pow (double x, Complex z) {
            if (x < 0.0) throw new ArgumentOutOfRangeException("x");
            if (z == Complex.Zero) return (Complex.One);
            if (x == Complex.Zero) return (Complex.Zero);
            double m = Math.Pow(x, z.Re);
            double t = Math.Log(x) * z.Im;
            return (new Complex(m * MoreMath.Cos(t), m * MoreMath.Sin(t)));
        }

        /// <summary>
        /// Raises a complex number to an integer power.
        /// </summary>
        /// <param name="z">The argument.</param>
        /// <param name="n">The power.</param>
        /// <returns>The value of z<sup>n</sup>.</returns>
        public static Complex Pow (Complex z, int n) {

            // this is a straight-up copy of MoreMath.Pow with x -> z, double -> Complex

            if (n < 0) return (1.0 / Pow(z, -n));

            switch (n) {
                case 0:
                    // we follow convention that 0^0 = 1
                    return (1.0);
                case 1:
                    return (z);
                case 2:
                    // 1 multiply
                    return (z * z);
                case 3:
                    // 2 multiplies
                    return (z * z * z);
                case 4: {
                        // 2 multiplies
                        Complex z2 = z * z;
                        return (z2 * z2);
                    }
                case 5: {
                        // 3 multiplies
                        Complex z2 = z * z;
                        return (z2 * z2 * z);
                    }
                case 6: {
                        // 3 multiplies
                        Complex z2 = z * z;
                        return (z2 * z2 * z2);
                    }
                case 7: {
                        // 4 multiplies
                        Complex z3 = z * z * z;
                        return (z3 * z3 * z);
                    }
                case 8: {
                        // 3 multiplies
                        Complex z2 = z * z;
                        Complex z4 = z2 * z2;
                        return (z4 * z4);
                    }
                case 9: {
                        // 4 multiplies
                        Complex z3 = z * z * z;
                        return (z3 * z3 * z3);
                    }
                case 10: {
                        // 4 multiplies
                        Complex z2 = z * z;
                        Complex z4 = z2 * z2;
                        return (z4 * z4 * z2);
                    }
                default:
                    return (ComplexMath.Pow(z, (double)n));
            }

        }


    }
}
