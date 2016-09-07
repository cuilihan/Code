$(function () {
    t.init();
});

var t = {
    init: function () {
        t.fnSetTabHeight();
        t.fnDefaultData();
        t.fnRouteTypeChange();
    },
    serverUrl: "Service/Hotel.ashx?xType=1&rnd=" + getRand(),
    fnSetTabHeight:function(){
        var h = document.documentElement.clientHeight - 40;
        $("#wraper").css("height", h + "px");
       
        $(window).resize(function () {
            t.fnSetTabHeight();
        });
    },
    fnRouteTypeChange: function () {
        $("#tabMenu").find("li").click(function () {
            $(this).removeClass("not").addClass("at").siblings("li").removeClass("at").addClass("not");
            var routeTypeID = $(this).prop("id");
            t.fnBindData(routeTypeID);
        });
    },
    fnDefaultData: function () {
        var routeTypeID = $(".at", "#tabMenu").prop("id");
        t.fnBindData(routeTypeID);
    },
    fnBindData: function (routeTypeID) {
        var u = t.serverUrl + "&action=5&routeTypeID=" + routeTypeID;
        dataService.ajaxGet(u, function (data) {
            $("#tblData").html(data);
        });
    },
    fnInfo: function (id) {
        var title = "查看酒店信息";
        var url = "HotelInfo.aspx?id=" + id;
        addTab(title, url, function () { });
    }
};

