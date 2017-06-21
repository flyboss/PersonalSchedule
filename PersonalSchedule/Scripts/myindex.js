var login_btn_func = function (handleUrl) {
    $.post(
        handleUrl,
    {
        operate: "login",
        login_id: $("#login_id").val(),
        login_pwd: $("#login_pwd").val()
    },
    function (data) {
        //console.log(handleUrl);
        //console.log($("#login_id").val());
        //console.log($("#login_pwd").val());
        var data_str_list = String(data).split("\n");
        if (data_str_list.length > 0) {
            var error_message = data_str_list[0];
            $("#login_errorMessage").text(error_message);
            if (error_message.indexOf("用户") >= 0) {
                $("#login_id").val("");
                $("#login_pwd").val("");
                $("#login_id").focus();
            }
            if (error_message.indexOf("密码") >= 0) {
                $("#login_pwd").val("");
                $("#login_pwd").focus();
            }
            if (error_message.indexOf("成功") >= 0) {
                $('#login_errorMessage').attr("class", "successMessage");
                $('#login').modal('hide');
                location.reload();
            }
        }
    });
}

var register_btn_func = function (handleUrl) {
    if ($('#register_pwd').val() != $('#register_pwd_confirm').val()) {
        $('#register_errorMessage').text("*两次密码输入不匹配，请重新输入");
        $('#register_pwd_confirm').val("");
        $('#register_pwd_confirm').focus();
        return;
    }
    var pw = $('#register_pwd').val();
    if (pw.length < 5) {
        $('#register_errorMessage').text("*密码长度必须超过五个字符");
        $('#register_pwd_confirm').val("");
        $('#register_pwd_confirm').focus();
        return;
    }
    if (pw.length > 100) {
        $('#register_errorMessage').text("*密码长度太长");
        $('#register_pwd').val("");
        $('#register_pwd').focus();
        return;
    }
    $.post(
        handleUrl,
        {
            operate: "register",
            register_id: $('#register_id').val(),
            register_pwd: $('#register_pwd').val(),
            register_pwd_confirm: $('#register_pwd_confirm').val()
        },
        function (data) {
            var data_str_list = String(data).split("\n");
            if (data_str_list.length > 0) {
                var error_message = data_str_list[0];
                $("#register_errorMessage").text(error_message);
                if (error_message.indexOf("成功") >= 0) {
                    $('#register').modal('hide');
                    $('#login_errorMessage').attr("class", "successMessage");
                    $('#login_errorMessage').text("注册成功，请重新登陆");
                    $('#login').modal('show');
                    $('#login_errorMessage').attr("class", "errorMessage");
                }
                if (error_message.indexOf("已被") >= 0) {
                    $('#register_id').val("");
                    $('#register_id').focus();
                }
            }
        });
}

var logout_btn_func = function (handleUrl) {
    $.post(
        handleUrl,
    {
        operate: "logout"
    },
    function (data) {
        var data_str_list = String(data).split("\n");
        if (data_str_list.length > 0) {
            var error_message = data_str_list[0];
            location.reload();
        }
    });
}