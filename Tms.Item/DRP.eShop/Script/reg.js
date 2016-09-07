var reg = {
    fnInit: function () {
        $("#btnSendSms").click(function () {
            reg.fnRequestCode();
        });
    },
    fnRequestCode: function () {
        var mobile = $("#txtMobile").val();
        if (mobile == "") {
            alert("请填写手机号");
            return;
        }
        $("#btnSendSms").addClass("hide");
        fnResetTimer();
        var orgID = request("appid"); 
        var u = "/Service/Reg.ashx?action=1&m=" + mobile + "&appid=" + orgID + "&r=" + getRand();
        dataService.ajaxGet(u, function (data) {
            if (data != "1") alert(data);
        });
    }
};

function fnResetTimer() { //60秒可以重发短信
    var timer = 60;
    var t = setInterval(function () {
        if (timer <= 0) {
            clearInterval(t);
            $("#spTimer").html("");
            $("#btnSendSms").removeClass("hide");
        }
        var s = "(" + timer + ")秒后重发";
        timer--;
        $("#spTimer").html(s);
    }, 1000);
}

$(function () {
    reg.fnInit();
});
