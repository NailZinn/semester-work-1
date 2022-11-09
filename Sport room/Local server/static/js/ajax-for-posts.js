var lastScrollTop = 0;
var page = 0;

function loadPosts() {
    if (page > -1) {
        lastScrollTop = $("#posts").scrollTop();
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

$("#posts").scroll(function () { 
    if ($("#posts").scrollTop() - lastScrollTop > $("#posts").height()) {
        loadPosts();
    }
});