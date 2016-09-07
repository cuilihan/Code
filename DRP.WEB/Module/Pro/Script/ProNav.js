//自主班产品预订目的地导航

$(function () {
    t.init();
});

var t = {
    init: function () {
        t.fnSetTabHeight();
        t.fnDefaultData();
        t.fnRouteTypeChange();

        $("#btnQuery").click(function () {
            var sDate = $("#sDate").val();
            var eDate = $("#eDate").val();
            var u = "ProList.aspx?sDate=" + sDate + "&eDate=" + eDate;
            window.location.href = u;
        });
    },
    serverUrl: "Service/ProSearch.ashx?rnd=" + getRand(),
    fnSetTabHeight: function () {
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
        var u = t.serverUrl + "&action=1&routeTypeID=" + routeTypeID;
        dataService.ajaxGet(u, function (data) {
            $("#tblData").html(data);
        });
    }
};

