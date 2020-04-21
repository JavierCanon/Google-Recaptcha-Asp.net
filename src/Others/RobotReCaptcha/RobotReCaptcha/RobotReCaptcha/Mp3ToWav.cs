using NAudio.Wave;

namespace StructMp3ToWavConvert
{
    class Mp3ToWav
    {
        public void Mp3ToWavConvert(string pathIn, string pathOut)
        {
            using (Mp3FileReader mp3 = new Mp3FileReader(pathIn))
            {
                using (WaveStream wave = WaveFormatConversionStream.CreatePcmStream(mp3))
                {
                    WaveFileWriter.CreateWaveFile(pathOut, wave);
                }
            }
        }
    }
}
