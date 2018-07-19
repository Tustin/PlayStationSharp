using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;

namespace PSNSharp.Requests
{
    public class MessageData
    {
        public int fakeMessageUid = 1234;
        public string body;
        public int messageKind;

        public MessageData(string body, int messageKind) {
            this.body = body;
            this.messageKind = messageKind;
        }
    }

    public class MessageRequest
    {
        public List<string> to;
        public MessageData message;

        public MessageRequest(List<string> users, MessageData message) {
            this.message = message;
            this.to = users;
        }

        public HttpContent BuildTextMessage() {
            var multipart = new MultipartContent("mixed", "gc0p4Jq0M2Yt08jU534c0p");
            var content = new StringContent(JsonConvert.SerializeObject(this), Encoding.UTF8, "application/json");
            content.Headers.Add("Content-Description", "message");
            multipart.Add(content);
            return multipart;
        }
    }
}