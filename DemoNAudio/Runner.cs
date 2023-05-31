using NAudio.Wave;

namespace DemoNAudio
{
    internal class Runner
    {
        public Runner() 
        {

        }
        public Task Run()
        {
            var inputs = new string[] { "0.wav", "1.wav" };
            var output = "output.wav";
            var waveFormatConversionStream = WaveFormatConversionStream.CreatePcmStream(new WaveFileReader(inputs.First()));
            using var waveFileWriter = new WaveFileWriter(output, waveFormatConversionStream.WaveFormat);
            foreach (var input in inputs)
            {
                waveFormatConversionStream = WaveFormatConversionStream.CreatePcmStream(new WaveFileReader(input));
                var bytes = new byte[waveFormatConversionStream.Length];
                waveFormatConversionStream.Position = 0;
                waveFormatConversionStream.Read(bytes, 0, (int)waveFormatConversionStream.Length);
                waveFileWriter.Write(bytes, 0, bytes.Length);
            }
            return Task.CompletedTask;
        }
    }
}
