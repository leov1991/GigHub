using GigHub.Core;
using Microsoft.AspNet.Identity;
using System.Web.Http;

namespace GigHub.Controllers.Api
{
    [Authorize]
    public class GigsController : ApiController
    {
        private readonly IUnitOfWork _unitOfWork;

        public GigsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpDelete]
        public IHttpActionResult Cancel(int id)
        {
            var userId = User.Identity.GetUserId();

            // Load a gig with its attendees
            var gig = _unitOfWork.Gigs.GetGigWithAttendees(id);

            // If the gig is already canceled return not found status code
            if (gig.IsCanceled)
                return NotFound();

            // If the gig artist is not the logged user, return unauthorized
            if (gig.ArtistId != userId)
                return Unauthorized();

            gig.Cancel();

            // Save changes to the database
            _unitOfWork.Complete();

            // Return Ok status code
            return Ok();
        }
    }
}