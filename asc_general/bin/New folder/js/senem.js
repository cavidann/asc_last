jQuery(document).ready(function ($) {

    // *MENU*
    //menu searching starts
    var clickNumber = 0;

    $("#icons").find("li").eq(0).click(function () {
        var search = $(this).children();
        search.css({ "background-color": "#646a82", "color": "white", "border-color": "#646a82" })
        $(this).siblings().hide();
        $(".search_tool").show();
        search.addClass("no_hover").children().hide();
        clickNumber++;

        if (clickNumber == 2) {
            //ajax
            console.log("istek qeyde alindi");
            clickNumber = 0;
        }
    });

    $(window).click(function (e) {
        if ($(e.target).hasClass("fa-search") || $(e.target).parent().hasClass("search_tool")) {
            return true;
        }
        else {
            $(".search").removeClass("no_hover").css({ "background-color": "", "color": "" }).children().show();
            $(".search").parent().siblings().show();
            $(".search_tool").hide().find("input").val("");
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
        if (i == 0) {
            babyNamesColorChange(i + 1, i, "#FFAAAA");
        }
        else {
            babyNamesColorChange(i - 1, i, "#AAD4FF");
        }
    }

    // letter color change ends

    //*EDUCATION* 

    //education carousel starts
    $('#carousel-example-generic').carousel({
        pause: true
    });

    //education carousel ends


});