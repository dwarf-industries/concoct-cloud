using System;
using System.Diagnostics;
using WWServer.LocalModels;

namespace WWServer.Interpritators
{
    class MoveNpcWaypoint : IDisposable
    {

        Stopwatch timer = new Stopwatch();
        public MoveNpcWaypoint(int millisecond, IncomingData data)
        {

            timer.Start();
            while (timer.IsRunning)
            {
                if (timer.ElapsedMilliseconds >= millisecond)
                {

                    timer.Stop();
                    Dispose();


                    return;
                }


            }
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
