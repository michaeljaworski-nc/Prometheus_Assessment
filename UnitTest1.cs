using System.Net;
using System.Text.Json.Serialization;
using FluentAssertions;
using RestSharp;

namespace TestProject1
{
    public class Tests
    {
        private const string JSONAPI = "https://jsonplaceholder.typicode.com/";

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            string baseUrl = JSONAPI + "todos";
            RestClient client = new RestClient(baseUrl);
            RestRequest restRequest = new RestRequest(baseUrl, Method.Get);
            RestResponse restResponse = client.Execute(restRequest);
            Assert.Pass();
        }

        [Test]
        public void TestGet()
        {
            string baseUrl = JSONAPI + "todos";
            RestClient client = new RestClient(baseUrl);
            RestRequest restRequest = new RestRequest(baseUrl, Method.Get);
            RestResponse restResponse = client.Execute(restRequest);
            restResponse.StatusCode.Should().Be(HttpStatusCode.OK);
            restResponse.Content.Should().NotBeNull();
        }

        [Test]
        public void TestPost()
        {
            string baseUrl = JSONAPI + "todos";
            RestClient client = new RestClient(baseUrl);
            var body = BuildBodyRequest();
            RestRequest restRequest = new RestRequest(baseUrl, Method.Post);
            restRequest.AddBody(body, ContentType.Json);
            RestResponse restResponse = client.Execute(restRequest);
            restResponse.StatusCode.Should().Be(HttpStatusCode.Created);
            restResponse.Content.Should().NotBeNull();
        }

        [Test]
        public void TestPut()
        {
            string baseUrl = JSONAPI + "todos/1";
            RestClient client = new RestClient(baseUrl);
            var body = BuildBodyRequest();
            string newTitle = "Test Book 1";
            body.Title = newTitle;
            RestRequest restRequest = new RestRequest(baseUrl, Method.Post);
            restRequest.AddBody(body, ContentType.Json);
            restRequest = new RestRequest(baseUrl, Method.Put);
            restRequest.AddBody(body, ContentType.Json);
            RestResponse restResponse = client.Execute(restRequest);

            restResponse.StatusCode.Should().Be(HttpStatusCode.OK);
            restResponse.Content.Should().NotBeNull();
            restResponse.ResponseUri.Should().NotBeNull();

            string responseBody = restResponse.Content;
            Assert.That(restResponse.ResponseUri.ToString, Is.EqualTo(baseUrl));
            Assert.IsTrue(responseBody.Contains(newTitle));
        }

        [Test]
        public void TestDelete()
        {
            string baseUrl = JSONAPI + "todos/1";
            RestClient client = new RestClient(baseUrl);
            RestRequest restRequest = new RestRequest(baseUrl, Method.Delete);
            RestResponse restResponse = client.Execute(restRequest);
            restResponse.StatusCode.Should().Be(HttpStatusCode.OK);
            restResponse.Content.Should().NotBeNull();
        }

        [Test]
        public void TestNegative()
        {
            string baseUrl = JSONAPI + "todos";
            RestClient client = new RestClient(baseUrl);
            RestRequest restRequest = new RestRequest(baseUrl, Method.Put);
            RestResponse restResponse = client.Execute(restRequest);
            restResponse.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        public static FakeApiEntities BuildBodyRequest()
        {
            return new FakeApiEntities
            {
                Id = 100,
                Title = "Test Book",
                Description = "Mussum Ipsum, cacilds vidis litro abertis.  Quem num gosta di mim que vai caçá sua turmis!",
                Excerpt = "uem num gosta di mim que vai caçá sua turmis!",
                PageCount = 100,
                PublishDate = "2023-09-03T13:50:32.6884665+00:00"
            };
        }
    }


    public class FakeApiEntities
    {
        [JsonPropertyName("id")]
        public int? Id { get; set; }
        [JsonPropertyName("title")]
        public string Title { get; set; }
        [JsonPropertyName("description")]
        public string Description { get; set; }
        [JsonPropertyName("pageCount")]
        public int PageCount { get; set; }
        [JsonPropertyName("excerpt")]
        public string Excerpt { get; set; }
        [JsonPropertyName("publishDate")]
        public string PublishDate { get; set; }
    }
}