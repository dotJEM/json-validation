using System.Collections.Generic;
using DotJEM.Json.Validation.Constraints.Common;
using DotJEM.Json.Validation.Constraints.Comparables;
using DotJEM.Json.Validation.Constraints.String;
using DotJEM.Json.Validation.Constraints.Types;
using DotJEM.Json.Validation.Descriptive;
using DotJEM.Json.Validation.IntegrationTest.Util;
using DotJEM.Json.Validation.Results;
using Newtonsoft.Json.Linq;
using NUnit.Framework;

namespace DotJEM.Json.Validation.IntegrationTest.DataDriven.JsonPlaceholder
{
    [TestFixture]
    [Parallelizable(ParallelScope.Self)]
    public class JsonPlaceholderTest
    {

        [TestCaseSource(nameof(Users))]
        public void Validate_Users(JObject entity)
        {
            ValidatorResult result = new UserValidator().Validate(entity, null);
            Assert.That(result.IsValid, result.Describe());
        }

        [TestCaseSource(nameof(Posts))]
        public void Validate_Posts(JObject entity)
        {
            ValidatorResult result = new PostValidator().Validate(entity, null);
            Assert.That(result.IsValid, result.Describe());
        }

        [TestCaseSource(nameof(Photos))]
        public void Validate_Photos(JObject entity)
        {
            ValidatorResult result = new PhotoValidator().Validate(entity, null);
            Assert.That(result.IsValid, result.Describe());
        }

        [TestCaseSource(nameof(Albums))]
        public void Validate_Albums(JObject entity)
        {
            ValidatorResult result = new AlbumValidator().Validate(entity, null);
            Assert.That(result.IsValid, result.Describe());
        }

        [TestCaseSource(nameof(Comments))]
        public void Validate_Comment(JObject entity)
        {
            ValidatorResult result = new CommentValidator().Validate(entity, null);
            Assert.That(result.IsValid, result.Describe());
        }

        [TestCaseSource(nameof(Todos))]
        public void Validate_Todos(JObject entity)
        {
            ValidatorResult result = new TodoValidator().Validate(entity, null);
            Assert.That(result.IsValid, result.Describe());
        }

        public static IEnumerable<JObject> Albums => TestDataLoader.LoadFor<JsonPlaceholderTest>();
        public static IEnumerable<JObject> Comments => TestDataLoader.LoadFor<JsonPlaceholderTest>();
        public static IEnumerable<JObject> Photos => TestDataLoader.LoadFor<JsonPlaceholderTest>();
        public static IEnumerable<JObject> Posts => TestDataLoader.LoadFor<JsonPlaceholderTest>();
        public static IEnumerable<JObject> Todos => TestDataLoader.LoadFor<JsonPlaceholderTest>();
        public static IEnumerable<JObject> Users => TestDataLoader.LoadFor<JsonPlaceholderTest>();
    }

    public class AlbumValidator : JsonValidator
    {
        /* {
         *   "postId": 1,
         *   "id": 1,
         *   "name": "id labore ex et quam laborum",
         *   "email": "Eliseo@gardner.biz",
         *   "body": "laudantium enim quasi est quidem magnam voluptate ipsam eos\ntempora quo necessitatibus\ndolor quam autem quasi\nreiciendis et nam sapiente accusantium"
         * }
         */
        public AlbumValidator()
        {
        }
    }

    public class CommentValidator : JsonValidator
    {
        /* {
         *   "postId": 1,
         *   "id": 1,
         *   "name": "id labore ex et quam laborum",
         *   "email": "Eliseo@gardner.biz",
         *   "body": "laudantium enim quasi est quidem magnam voluptate ipsam eos\ntempora quo necessitatibus\ndolor quam autem quasi\nreiciendis et nam sapiente accusantium"
         * }
         */
        public CommentValidator()
        {
        }
    }

    public class TodoValidator : JsonValidator
    {
        /* {
         *   "userId": 1,
         *   "id": 1,
         *   "title": "delectus aut autem",
         *   "completed": false
         * }
         */
        public TodoValidator()
        {
        }
    }

    public class PhotoValidator : JsonValidator
    {
        /* {
         *   "albumId": 100,
         *   "id": 5000,
         *   "title": "error quasi sunt cupiditate voluptate ea odit beatae",
         *   "url": "http://placehold.it/600/6dd9cb",
         *   "thumbnailUrl": "http://placehold.it/150/6dd9cb"
         * }
         */
        public PhotoValidator()
        {
            When(Any)
                .Then(Field("id", Is.Required() & Must.Be.Integer() & Must.Be.GreaterOrEqualTo(1))
                      & Field("albumId", Is.Required() & Must.Be.Integer() & Must.Be.GreaterOrEqualTo(1))
                      & Field("title", Is.Required() & Has.MinLength(1))
                      & Field("url", Is.Required() & Must.Match("http://\\w+\\.\\w+/\\d+/[0-9A-Za-z]{3,6}"))
                );

            When(Any)
                .Then("thumbnailUrl", ComparedTo("url", ctx =>
                {
                    string value = (string)(JValue) ctx;
                    return Is.EqualTo(value.Replace("/600/", "/150/"));
                }));
        }
    }

    public class PostValidator : JsonValidator
    {
        /* {
         *   "userId": 1,
         *   "id": 1,
         *   "title": "sunt aut facere repellat provident occaecati excepturi optio reprehenderit",
         *   "body": "quia et suscipit\nsuscipit recusandae consequuntur expedita et cum\nreprehenderit molestiae ut ut quas totam\nnostrum rerum est autem sunt rem eveniet architecto"
         * }
         */
        public PostValidator()
        {
            When(Any)
                .Then(Field("id", Is.Required() & Must.Be.Integer() & Must.Be.GreaterOrEqualTo(1))
                      & Field("userId", Is.Required() & Must.Be.Integer() & Must.Be.GreaterOrEqualTo(1))
                      & Field("title", Is.Required() & Has.MinLength(10))
                      & Field("body", Is.Required() & Has.MinLength(10))
                );
        }
    }

    public class UserValidator : JsonValidator
    {
        /* {
         *   "id": 1,
         *   "name": "Leanne Graham",
         *   "username": "Bret",
         *   "email": "Sincere@april.biz",
         *   "address": {
         *     "street": "Kulas Light",
         *     "suite": "Apt. 556",
         *     "city": "Gwenborough",
         *     "zipcode": "92998-3874",
         *     "geo": {
         *       "lat": "-37.3159",
         *       "lng": "81.1496"
         *     }
         *   },
         *   "phone": "1-770-736-8031 x56442",
         *   "website": "hildegard.org",
         *   "company": {
         *     "name": "Romaguera-Crona",
         *     "catchPhrase": "Multi-layered client-server neural-net",
         *     "bs": "harness real-time e-markets"
         *   }
         * }
         */
        public UserValidator()
        {
            When(Any)
                .Then(Field("id", Is.Required() & Must.Be.Integer() & Must.Be.GreaterOrEqualTo(1))
                    & Field("name", Is.Required() & Must.Be.String() & Must.Have.MinLength(5))
                    & Field("username", Is.Required())
                    & Field("email", Is.Required() & Must.Match("\\w+\\@\\w+\\.\\w+"))
                );
        }
    }
}
