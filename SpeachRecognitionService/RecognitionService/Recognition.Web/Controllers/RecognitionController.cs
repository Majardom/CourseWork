using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
using Recognition.Services.Interfaces.Services;

namespace Recognition.Web.Controllers
{
    public class RecognitionController : ApiController
    {
        private readonly IRecognitionService _service;

        public RecognitionController(IRecognitionService service)
        {
            _service = service;
        }

        [HttpPost]
        public HttpResponseMessage Sample(string sampleAuthorName, [FromBody]SampleDto sample)
        {
            var response = new HttpResponseMessage(HttpStatusCode.OK);
            try
            {
                var samples = sample.SamplesBase64.Select(x => ConvertToByteArray(x)).ToList();
                _service.AddVoiceSamples(sampleAuthorName, samples);
            }
            catch 
            {
                response = new HttpResponseMessage(HttpStatusCode.InternalServerError);
			}
            
            return response;
        }

        [HttpPost]
        [Route("api/recognition/identify")]
        public HttpResponseMessage Indentify([FromBody]SampleDto sample)
        {
            var samples = sample.SamplesBase64.Select(x => ConvertToByteArray(x)).ToList();
            var identity = _service.RecognizeSpeaker(samples.FirstOrDefault());

            var response = new HttpResponseMessage(HttpStatusCode.OK);
            response.Content = new StringContent($"identify.Key:{identity.Key}");
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("text/plain");

            return response;
        }

        private byte[] ConvertToByteArray(string base64String)
        {
            byte[] decodedByteArray = Convert.FromBase64CharArray(base64String.ToCharArray(), 0, base64String.Length);

            return (decodedByteArray);
        }
    }

	public class SampleDto
	{
        public List<string> SamplesBase64 { get; set; }
    }

}
