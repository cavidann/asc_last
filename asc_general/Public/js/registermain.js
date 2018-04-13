jQuery(document).ready(function ($) {
    $("#register").click(function () {
         event.preventDefault();       
        url = $(this).parent().parent().attr('action');
      
        username = $("#reg_username").val();
        email = $("#reg_email").val();
        password = $("#reg_password").val();
        password1 = $("#reg_password1").val();
       
        $.ajax({
            url: url,
            type: "POST",
            data: {
                username: username,
                email: email,
                password: password,
                password1: password1
            },
            success: function (result) {         
                if (result == true && password==password1) {                    
                    $(':input').val('');
                    window.location.href = "Index";
                    //location.reload();
                }
                else {
                    $("#message-reg").html(
                     "INCORRECT."
                   );
                }
              
            }
        });
       
    })
    //$("#login").click(function () {
    //    event.preventDefault();

    //    username = $("#log_username").val();
    //    password = $("#log_password").val();
    //    console.log(username + " " + password);

    //    url = $(this).parent().parent().attr('action');

    //    $.ajax({
    //        url: url,
    //        type: "POST",
    //        data: {
    //            username: username,           
    //            password: password
    //        },
    //        error:function(){
    //            console.log("error");
    //        },
    //        success: function (result) {
    //            $(':input').val('');
    //            if (result==true) {
    //                location.reload(); 
    //            }
    //            else {
    //                $("#message-login").append(
    //                 "INCORRECT."
    //               );
    //            }
    //        }
    //    });
    //});

    $.validate({
        modules: 'location, date, security, file'
       
    });

})
