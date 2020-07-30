using System;

namespace WWServer.Interpritators
{
    class SunPossition
    {
        static double d0, T0, S0, SG, S, d;
        static double C; //Sun's Center
        static double L0; //Sun's mean longitude
        static double M0; //Sun's mean anomaly
        static double LS; //Ecliptical longitude of the Sun
        static double H; //angle of object
        static double K; //obliquity of the ecliptic 
        static double RS; //right ascention
        static double DS; //declination
        static double T; //Number of centuries
        static double tUT = 2; //Universal Time

        static double L = 41; //Longitude
        static double B = 29; //Latitude
        static double A; //Azimuth from North

        static double Y = 2007, M = 10, D = 9;
        static double Ran2Deg = 100 / Math.PI;
        public static void CalculateElevation()
        {
            //1. Number of days and centuries from epoch J2000.0
            //Let Y be the year, M the month number (1 = January, 2 = February etc.), and D the day of the month. We first have to find the number of days do from the reference time 2000 January 1, 12h Universal Time (epoch J2000.0). This quantity can be obtained from the formula from 
            d0 = 367 * Y - Math.Round((7 / 4) * (Y + Math.Round(M + 9 / 12))) + Math.Round(275 * M / 9) + D - 730531.5;
            //Then we obtain the number of Julian centuries To from J2000.0 from 
            T0 = d0 / 36525;
            //2. Sidereal Time
            //The sidereal time So (in hours) for the meridian of Greenwich, at midnight (00h) UT for a given date, can be calculated as follows 
            S0 = 6.6974 + 2400.0513 * T0;
            //The sidereal time SG of the Greenwich meridian for the universal time tUT is 
            SG = S0 + (366.2422 / 365.2422) * tUT;
            //where 365.2422 is the length of the tropical year in days. We finally obtain the local sidereal time S for the geographical longitude L
            S = SG + L;
            //3. Solar Coordinates
            //We first find the number of days d from J2000.0 for the given date and time 
            d = d0 + tUT / 24;
            //where do can be found from relation [1]. The number of centuries T from the reference time is 
            T = d / 36525;
            //We can now find the Sun's mean longitude Lo and its mean anomaly Mo from 
            L0 = 280.466 + 36000.770 * T;
            M0 = 357.529 + 35999.050 * T;
            //The Sun's equation of center C, which accounts for the eccentrisity of Earth's orbit around the Sun, is given by 
            C = (1.915 - 0.005 * T) * Math.Sin(M0) + 0.020 * Math.Sin(2 * M0);
            //We can now find the true ecliptical longitude of the Sun LS by adding C to Lo, i.e
            LS = L0 + C;
            //In order to find the Sun's equatorial coordinates, right ascension RS and declination DS, we need to know the value of K, the obliquity of the ecliptic 
            K = 23.439 - 0.013 * T;
            //The Sun's right ascension can now be found from 
#pragma warning disable CS1030 // #warning: 'AiciIiModificat'
#warning AiciIiModificat
            RS = Math.Atan(Math.Tan(LS) * Math.Cos(K));
#pragma warning restore CS1030 // #warning: 'AiciIiModificat'
            //where it has to be remembered that RS is in the same quadrant as LS. 
            //Finally, the declination is given by 
#pragma warning disable CS1030 // #warning: 'AiciIiModificat'
#warning AiciIiModificat
            DS = Math.Atan(Math.Sin(RS) * Math.Sin(K));
#pragma warning restore CS1030 // #warning: 'AiciIiModificat'
            //4. Horizontal Coordinates
            //We now proceed to find the horizontal coordinates for the Sun (or other astronomical objects) for a place at geographical longitude L and latitude B. 
            //First we have to know the hour angle of the object H. It is given from the definition of the sidereal time: S = H + RS, i.e. 
            H = S - RS;
            //The true altitude (angular elevation) h of the Sun is found from 
#pragma warning disable CS1030 // #warning: 'AiciIiModificat'
#warning AiciIiModificat
            H = Math.Asin(Math.Sin(B) * Math.Sin(DS) + Math.Cos(B) * Math.Cos(DS) * Math.Cos(H));
#pragma warning restore CS1030 // #warning: 'AiciIiModificat'
            Console.WriteLine(H);
            Console.WriteLine($"Rand To Deg {H * Ran2Deg}");
            //The azimuth A, measured eastward from the North, is given by 
            A = Math.Atan(-Math.Sin(H) / (Math.Tan(DS) * Math.Cos(B) - Math.Sin(B) * Math.Cos(H)));
            Console.WriteLine(A);
            Console.WriteLine(Math.Tan(A));
        }

    }
}
