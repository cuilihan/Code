

var login = {
    tipText: "请输入用户登录账号",
    init: function () {
        var v = $("#txtUserID").val();
        if (v == "") {
            $("#txtUserID").val(login.tipText);
        }
        $("#txtUserID").focus(function () {
            if ($(this).val() == login.tipText) {
                $(this).val("");
            }
            $(this).css("color", "#000");
        });
        $("#txtUserID").blur(function () {
            var t = $(this).val();
            if (t == "") {
                $(this).val(login.tipText);
                $(this).css("color", "#ccc");
            }
            else {
                if (t.length != 11) {
                    $(this).css("color", "red");
                }
            }
        });
        $("#txtCode").focus(function () {
            var v = $("#txtCode").val();
            if (v == "验证码")
                $("#txtCode").val("");
        });
        $("#txtCode").blur(function () {
            var v = $("#txtCode").val();
            if (v == "")
                $("#txtCode").val("验证码");
        });

        $("#btnLogin").on("click", function () {
            var uid = $(".toolbar").find("li[class='toolbar_on']").attr("id");//1：用户登录  2：导游报账登录
            $("#hfUserType").val(uid);
            var v = $("#txtUserID").val();
            if (v == "" || v == login.tipText) {
                alert("请输入登录账号");
                return false;
            }
            var p = $("#txtPwd").val();
            if (p == "") {
                alert("请输入密码");
                return false;
            }
            var code = $("#txtCode").val();
            if (code == "" || code == "验证码") {
                alert("请输入验证码");
                return false;
            }
            code = code.toLowerCase();
            var chkCode = $("#hfVerifyCode").val();
            if (code != chkCode) {
                alert("验证码输入错误");
                return false;
            }
            return true;
        });
        $(document).bind("contextmenu", function (e) {
            return false;
        });
        $(".toolbar").find("li").click(function () {
            $(".toolbar").find("li").removeClass("toolbar_on");
            $(this).addClass("toolbar_on");
        });
        login.fnCreateCode();
        $("#btnRefresh").click(function () {
            login.fnCreateCode();
        });
        $("#btnCode").hover(function () { //二维码登录
            $(this).addClass("hover").find("div.jq_hidebox").show();
        }, function () {
            $(this).removeClass("hover").find("div.jq_hidebox").hide();
        });
    },
    fnCheckBrowser: function () {
        var userAgent = window.navigator.userAgent.toLowerCase();
        var version = $.browser.version;
        $.browser.msie10 = $.browser.msie && /msie 10\.0/i.test(userAgent);
        $.browser.msie9 = $.browser.msie && /msie 9\.0/i.test(userAgent);
        $.browser.msie8 = $.browser.msie && /msie 8\.0/i.test(userAgent);
        $.browser.msie7 = $.browser.msie && /msie 7\.0/i.test(userAgent);
        $.browser.msie6 = !$.browser.msie8 && !$.browser.msie7 && $.browser.msie && /msie 6\.0/i.test(userAgent);
        if ($.browser.msie6) {
            alert("您的IE版本(6.0)太低，请升级至IE9.0");
        }
    },
    fnCreateCode: function () {
        var arr = "1,2,3,4,5,6,7,8,9,a,b,c,d,e,f,g,h,i,j,k,l,m,n,p,q,r,s,t,u,v,w,x,y,z".split(',');
        var sb = [];
        while (sb.length < 4) {
            var r = Math.floor(Math.random() * 22 + 1);
            if (r < arr.length) {
                sb.push(arr[r]);
            }
        }
        var code = sb.join("");
        $("#hfVerifyCode").val(code);
        var u = "/Service/VerifyCode.ashx?code=" + code;
        $("#ImgVerifyCode").attr("src", u);
    }
};


$(function () {
    login.init();
    login.fnCheckBrowser();
});

//登录用户多个角色时弹出窗口选择角色
function fnUserRolesSelect() {
    new Dialog({ type: 'id', value: 'userRolesWrap' }).show();
}