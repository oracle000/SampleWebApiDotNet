using System.Net;
using System.Net.Http;
using System.Web.Http;
using SampleWebApi.Data.Commands;
using SampleWebApi.Data.Queries;
using ShortBus;

namespace SampleWebApi.Controllers
{
    public class TodosController : ApiController
    {
        private readonly IMediator _mediator;

        public TodosController(IMediator mediator)
        {
            _mediator = mediator;
        }

        public HttpResponseMessage Get(int id)
        {
            if (id <= 0)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Invalid id");
            }

            var response = _mediator.Request(new GetTodosQuery
            {
                UserId = id
            });

            return Request.CreateResponse(HttpStatusCode.OK, response.Data);
        }

        public HttpResponseMessage Post([FromBody]PostTodosCommand request)
        {
            var response = _mediator.Request(request);

            if (response.HasException())
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, response.Exception.GetBaseException().Message);
            }

            return Request.CreateResponse(HttpStatusCode.Created, response.Data);
        }
    }
}
