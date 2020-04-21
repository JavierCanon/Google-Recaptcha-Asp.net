using System;
using System.Collections.Generic;
using System.Linq;
using StructSendPOST;
using StructScreenShot;
using StructMouseBase;

namespace StructSendMoves
{
    class SendMoves
    {
        public void Start()
        {
            int numAudioBase = 1;
            int maxLoop = 10; //Input your value

            while (numAudioBase < maxLoop)
            {                
                //string pathImage = @""; // YOUR PATH
                //string LocalnameImage = pathImage + numAudioBase + ".jpg";                

                var exec = new Base();                                
               
                exec.ClickOnReCAPTCHACheckbox(50,634);                
                exec.ClickToAudioChallenge(150, 894);
                exec.ClickToDownloadAudio(214, 680);
                exec.RightButtonToDownload();
                exec.SaveAs();
                exec.Save();
                exec.BackToPage();
                
                var Post = new SendPOST();
                var Decode = Post.GetAndSendAudio(numAudioBase);
                numAudioBase++;
                RootObject result = new RootObject();
                
                result = Newtonsoft.Json.JsonConvert.DeserializeObject<RootObject>(Decode);
                string test = result.NBest.FirstOrDefault().MaskedITN;
                var answer = String.Join("", System.Text.RegularExpressions.Regex.Split(test, @"[^\d]"));
                exec.AudioAnswer(168,638);
                exec.InputKey(answer);
                exec.CheckAudio(294, 736);                                    
                exec.Send(80, 694);                
            }
        }
    }

    public class NBest
    {
        public double Confidence { get; set; }
        public string Lexical { get; set; }
        public string ITN { get; set; }
        public string MaskedITN { get; set; }
        public string Display { get; set; }
    }

    public class RootObject
    {
        public string RecognitionStatus { get; set; }
        public int Offset { get; set; }
        public int Duration { get; set; }
        public List<NBest> NBest { get; set; }
    }
}
