var GigDetailsController = function (followingService) {
    var followButton;

    var init = function () {
        $(".js-toggle-following").click(toggleFollowing);
    };

    var toggleFollowing = function (e) {
        followButton = $(e.target);

        var followeeId = followButton.attr("data-user-id");

        if (followButton.hasClass("btn-default"))
            followingService.createFollowing(followeeId, done, fail)
        else
            followingService.deleteFollowing(followeeId, done, fail);
    };

    var done = function () {
        var text = (followButton.text() == "Seguir") ? "Siguiendo" : "Seguir";
        followButton.toggleClass("btn-info").toggleClass("btn-default").text(text);
    };

    var fail = function () {
        alert("Ocurrió un error");
    };

    return {
        init: init
    }
}(FollowingService);