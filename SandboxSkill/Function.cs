using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Alexa.NET;
using Alexa.NET.Request;
using Alexa.NET.Request.Type;
using Alexa.NET.Response;
using Alexa.NET.Response.Ssml;
using Amazon.Lambda.Core;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]

namespace SandboxSkill
{
    public class Function
    {
        public SkillResponse FunctionHandler(SkillRequest input, ILambdaContext context)
        {
            context.Logger.LogLine("Request Type: " + input.GetRequestType().Name);

            if (input.GetRequestType() == typeof(LaunchRequest))
            {
                SkillResponse response = ResponseBuilder.AudioPlayerPlay(Alexa.NET.Response.Directive.PlayBehavior.ReplaceAll, "https://s3-eu-west-1.amazonaws.com/rtg-dispatcher/streaming-test/Dispatcher_Ready_Question.wav", "token");
                response.Response.OutputSpeech = new PlainTextOutputSpeech()
                {
                    Text = "Playing"
                };
                response.Response.ShouldEndSession = false;

                return response;
            }
            else if (input.GetRequestType() == typeof(IntentRequest))
            {
                IntentRequest request = input.Request as IntentRequest;
                return request.Intent.Name == "AMAZON.YesIntent" ? ResponseBuilder.AudioPlayerPlay(Alexa.NET.Response.Directive.PlayBehavior.ReplaceAll, "https://s3-eu-west-1.amazonaws.com/rtg-dispatcher/streaming-test/Caller_Birth.wav", "token") : ResponseBuilder.Empty();
            }

            return ResponseBuilder.Empty();
        }
    }
}
