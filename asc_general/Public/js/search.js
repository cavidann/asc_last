jQuery(document).ready(function ($) {
    function appendBlogItem(id, title) {
        $("#searchingResult").show().append("<li><a href='/SingleBlog/Index/" + id + "'><p>" + title + "</p></a></li>");
    }

    $(function () {
        $("#searchString").keyup(function () {
            var searchString = $(this).val();
            $.ajax({
                url: "/Search/LiveSearch",
                type: "POST",
                dataType: "json",
                data: { searchString: searchString },
                success: function (data) {
                    if (searchString == "") {
                        $("#searchingResult").html("")
                    }
                    else {
                        $("#searchingResult").html("")
                        for (var i = 0; i < data.length; i++) {
                            if (data[i].blogTitle.length > 19) {
                                appendBlogItem(data[i].blogId, data[i].blogTitle + "..")
                            }
                            else {
                                appendBlogItem(data[i].blogId, data[i].blogTitle)
                            }
                        }
                    }
                },
                error: function () {
                    alert("error")
                }
            });
        })
    })
})