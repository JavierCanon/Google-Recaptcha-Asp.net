using System;
using System.IO;
using System.Net;
using StructMp3ToWavConvert;

namespace StructSendPOST
{
    class SendPOST
    {
        //private static string requestUri = "https://speech.platform.bing.com/speech/recognition/<RECOGNITION_MODE>/cognitiveservices/v1?language=<LANGUAGE_TAG>&format=<OUTPUT_FORMAT>";
        private static string requestUri = "https://speech.platform.bing.com/speech/recognition/conversation/cognitiveservices/v1?language=en-US&format=detailed";
        private static FileStream fs;
        public string responseString;
        /*Yout Path
            EX: string pathIn = @"C:\Audio\";
            EX: string pathOut = @"C:\Send\";
        */
        string pathIn = "";
        string pathOut = "";

        public string GetAndSendAudio(int numAudio)
        {
            HttpWebRequest request = null;
            request = (HttpWebRequest)HttpWebRequest.Create(requestUri);
            request.SendChunked = true;
            request.Accept = @"application/json;text/xml";
            request.Method = "POST";
            request.ProtocolVersion = HttpVersion.Version11;
            request.ContentType = @"audio/wav; codec=audio/pcm; samplerate=16000";
            request.Headers["Ocp-Apim-Subscription-Key"] = ""; //INPUT YOUR API KEY access the link https://azure.microsoft.com/en-us/services/cognitive-services/speech/ and get a key

            string audioIn = pathIn + "audio (" + numAudio + ").mp3";
            string audioOut = pathOut + "teste" + numAudio + ".wav";
            var convert = new Mp3ToWav();
            convert.Mp3ToWavConvert(audioIn, audioOut);

            // Send an audio file by 1024 byte chunks
            using (fs = new FileStream(audioOut, FileMode.Open, FileAccess.Read))
            {
                /*
                * Open a request stream and write 1024 byte chunks in the stream one at a time.
                */
                byte[] buffer = null;
                int bytesRead = 0;

                using (Stream requestStream = request.GetRequestStream())
                {
                    /*
                    * Read 1024 raw bytes from the input audio file.
                    */
                    buffer = new Byte[checked((uint)Math.Min(1024, (int)fs.Length))];
                    while ((bytesRead = fs.Read(buffer, 0, buffer.Length)) != 0)
                    {
                        requestStream.Write(buffer, 0, bytesRead);
                    }

                    // Flush
                    requestStream.Flush();
                }

                /*
                * Get the response from the service.
                */
                //Console.WriteLine("Response:");
                using (WebResponse response = request.GetResponse())
                {
                    Console.WriteLine(((HttpWebResponse)response).StatusCode);

                    using (StreamReader sr = new StreamReader(response.GetResponseStream()))
                    {
                        responseString = sr.ReadToEnd();
                    }

                    //Console.WriteLine(responseString);
                    //Console.ReadLine();
                }
            }
            return responseString;
        }
    }
}


