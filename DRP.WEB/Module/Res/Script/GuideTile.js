$(function () {
    t.init();
});

var t = {
    init: function () {
        t.fnSetTabHeight();
        t.fnBindData();
    },
    serverUrl: "Service/Guide.ashx?xType=1&rnd=" + getRand(),
    fnSetTabHeight: function () {
        var h = document.documentElement.clientHeight - 45;
        $("#wraper").css("height", h + "px");

        $(window).resize(function () {
            t.fnSetTabHeight();
        });
    },
    fnBindData: function () {
        var u = t.serverUrl + "&action=3";
        dataService.ajaxGet(u, function (data) {
            if (data != "") {
                var arr = data.split('@');
                if (arr.length == 2)
                {
                    $("#iCount").html(arr[0]);
                    $("#tblData").html(arr[1]);
                }
            } 
        });
    },
    fnInfo: function (id) {
        var url = "/Module/Res/GuideInfo.aspx?id=" + id;
        return openWindow({ "title": "查看导游信息", "width": 650, "height": 420, "url": url }, function () {
        }); 
    }
};

