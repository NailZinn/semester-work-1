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

$("#posts").scroll(function () {
    if ($("#posts").scrollTop() >= $("#posts").prop("scrollHeight") - $("#posts").prop("offsetHeight") - 1) {
        loadPosts();
    }
});