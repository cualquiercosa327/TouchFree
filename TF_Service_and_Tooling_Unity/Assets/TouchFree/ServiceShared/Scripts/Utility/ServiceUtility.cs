﻿namespace Ultraleap.TouchFree.ServiceShared
{
    public static class ServiceUtility
    {
        // Store in M, display in CM
        public static readonly float ConfigToDisplayMeasurementMultiplier = 100;

        public static float MapRangeToRange(float _value, float _oldMin, float _oldMax, float _newMin, float _newMax)
        {
            float oldRange = (_oldMax - _oldMin);
            float newValue;
            if (oldRange == 0)
            {
                newValue = _newMin;
            }
            else
            {
                float newRange = (_newMax - _newMin);
                newValue = (((_value - _oldMin) * newRange) / oldRange) + _newMin;
            }
            return newValue;
        }
        public static int ToDisplayUnits(int _value)
        {
            return (int)(_value * ConfigToDisplayMeasurementMultiplier);
        }

        public static float ToDisplayUnits(float _value)
        {
            return _value * ConfigToDisplayMeasurementMultiplier;
        }

        public static int FromDisplayUnits(int _value)
        {
            return (int)(_value / ConfigToDisplayMeasurementMultiplier);
        }

        public static float FromDisplayUnits(float _value)
        {
            return _value / ConfigToDisplayMeasurementMultiplier;
        }

        /// <summary>
        ///    Ensure the calculated rotations make sense to the UI by avoiding large values.
        ///    Angles are centred around 0, with the smallest representation of the value
        /// </summary>
        public static float CentreRotationAroundZero(float angle)
        {
            angle = angle % 360;

            if (angle > 180)
            {
                return angle - 360;
            }
            else if (angle < -180)
            {
                return angle + 360;
            }
            else
            {
                return angle;
            }
        }
    }
}