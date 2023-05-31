using NCrontab;

namespace DemoNCrontab
{
    internal class Runner
    {
        private readonly CrontabSchedule _crontabSchedule;
        private readonly int _millisecondsDelay;
        private DateTime _nextOccurrence;
        public Runner()
        {
            _millisecondsDelay = 1000;
            _crontabSchedule = CrontabSchedule.Parse("* * * * *");
            _nextOccurrence = _crontabSchedule.GetNextOccurrence(DateTime.Now);
        }
        public async Task Run()
        {
            do
            {
                if (DateTime.Now > _nextOccurrence)
                {
                    _nextOccurrence = _crontabSchedule.GetNextOccurrence(DateTime.Now);
                    Console.WriteLine(DateTime.Now);
                }
                await Task.Delay(_millisecondsDelay);
            }
            while (true);
        }
    }
}
