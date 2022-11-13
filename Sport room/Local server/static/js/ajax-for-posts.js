var page = 0;

function loadPosts() {
    if (page > -1) {
        page++;
        $.ajax({
            type: "GET",
            url: "/posts/pages/" + page,
            success: function (response) {
                if (response != "") {
                    $("#posts").append(response);
                }
                else {
                    page = -1;
                }
            }
        });
    }
}

$(window).scroll(function () {
    if ($(window).scrollTop() >= $("#posts").height() - $(window).height() + 100) {
        loadPosts();
    }
});