jQuery(document).ready(function ($) {
    //add active class
    $("#main-menu").find("li").eq(3).mousemove(function () {
        console.log("yeap")
        $(this).toggleClass("myred");
    });


    //menu searching starts
    var clickNumber = 0;
    var searchString = "";

    $("#icons").find("li").eq(3).click(function () {
        console.log("click")
        $(this).hide().siblings().hide();
        $(".search_tool").show();
    });

    //Window clicking

    $(window).click(function (e) {
        if ($(e.target).hasClass("fa-search") || $(e.target).parent().hasClass("search_tool")) {
            clickNumber = 1;
            return true;
        }
        else {
            $("#icons").find("li").show();
            $(".search_tool").hide().find("input").val("");
            $("#searchingResult").find("li").remove().hide();
            clickNumber = 0;
        }
    });

    //menu searching ends

    // *NAME*

    // letter color change starts
    function babyNamesColorChange(x, eq, color) {
        $(".baby_names").eq(eq).find("a").on({
            mouseenter: function () {
                $(this).css("color", color);
            },

            mouseleave: function () {
                $(this).css("color", "#646a82");
            },

            click: function () {
                $(".baby_names").eq(x).find("a").css("color", "#646a82");
                $(this).unbind("mouseenter mouseleave").css("color", color).parent().siblings().find("a").css("color", "#646a82");
            }
        });
    }

    for (var i = 0; i < 2; i++) {
        if (i === 0) {
            babyNamesColorChange(i + 1, i, "#FFAAAA");
        }
        else {
            babyNamesColorChange(i - 1, i, "#AAD4FF");
        }
    }

    // letter color change ends    
});