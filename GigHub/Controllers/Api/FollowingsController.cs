using GigHub.Core;
using GigHub.Core.DTOs;
using GigHub.Core.Models;
using Microsoft.AspNet.Identity;
using System.Web.Http;

namespace GigHub.Controllers.Api
{
    [Authorize]
    public class FollowingsController : ApiController
    {
        private readonly IUnitOfWork _unitofWork;

        public FollowingsController(IUnitOfWork unitOfWork)
        {
            _unitofWork = unitOfWork;
        }

        [HttpPost]
        public IHttpActionResult Follow(FollowingDTO dto)
        {
            var userId = User.Identity.GetUserId();

            var following = _unitofWork.Followings.GetFollowing(userId, dto.FolloweeId);

            if (null != following)
                return BadRequest("Ya estás siguiendo a este usuario.");

            following = new Following
            {
                FollowerId = userId,
                FolloweeId = dto.FolloweeId
            };

            _unitofWork.Followings.Add(following);

            _unitofWork.Complete();

            return Ok();
        }

        [HttpDelete]
        public IHttpActionResult Unfollow(string id)
        {
            var userId = User.Identity.GetUserId();

            var following = _unitofWork.Followings.GetFollowing(userId, id);

            if (null == following)
                return NotFound();

            _unitofWork.Followings.Remove(following);
            _unitofWork.Complete();

            return Ok(id);
        }
    }
}