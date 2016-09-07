function systole() {
    if (!$(".history").length) {
        return;
    }
    var $warpEle = $(".history-date"),
        $targetA = $warpEle.find("h2 a,ul li dl dt a"),
        parentH,
        eleTop = [];
    parentH = $warpEle.parent().height();
    $warpEle.parent().css({ "height": 59 });

    setTimeout(function () {
        $warpEle.find("ul").children(":not('h2:first')").each(function (idx) {
            eleTop.push($(this).position().top);
            $(this).css({ "margin-top": -eleTop[idx] }).children().hide();
        }).animate({ "margin-top": 0 }, 1600).children().fadeIn();
        $warpEle.parent().animate({ "height": parentH }, 2600);
        $warpEle.find("ul").children(":not('h2:first')").addClass("bounceInDown").css({ "-webkit-animation-duration": "2s", "-webkit-animation-delay": "0", "-webkit-animation-timing-function": "ease", "-webkit-animation-fill-mode": "both" }).end().children("h2").css({ "position": "relative" });
    }, 600);

    $targetA.click(function () {
        $(this).parent().css({ "position": "relative" });
        $(this).parent().siblings().slideToggle();
        $warpEle.parent().removeAttr("style");
        return false;
    });
};

$(function () {
    fnRouteSchedule();
});

function fnRouteSchedule() { //加载线路行程
    var u = "Service/RouteMgmt.ashx";
    var paras = "t=3&id=" + getQueryString("id");
    ajaxGet(false, u, paras, function (result) {
        if (result) {
            var sb = [];
            $(result.Data).each(function (idx) {
                var day = this.DayNum;
                var title = this.Title;
                var schedule = this.Schedule;
                var stay = this.Stay;
                var dinner = this.Dinner;

                var clsName = idx == 0 ? "first" : "date02";
                sb.push("<div class=\"history-date\">");
                sb.push("<ul>");
                sb.push("<h2 class='" + clsName + "'>");
                sb.push("<a href='#nogo'>第 " + day + " 天</a>");
                sb.push("" + title + "");
                sb.push("</h2>");

                //#region << 行程安排 >>
                if (schedule != "") {
                    sb.push("<li>");
                    sb.push("<h3>行程<span>D" + day + "</span></h3>");
                    sb.push("<dl>");
                    sb.push("<dt style='font-size:14px;'>" + schedule + "</dt>");
                    sb.push("</dl>");
                    sb.push("</li>");
                }

                if (stay != "") {
                    sb.push("<li>");
                    sb.push("<h3>住宿<span>D" + day + "</span></h3>");
                    sb.push("<dl>");
                    sb.push("<dt style='font-size:14px;'>" + stay + "</dt>");
                    sb.push("</dl>");
                    sb.push("</li>");
                }

                if (dinner != "") {
                    sb.push("<li>");
                    sb.push("<h3>用餐<span>D" + day + "</span></h3>");
                    sb.push("<dl>");
                    sb.push("<dt style='font-size:14px;'>" + dinner + "</dt>");                  
                    sb.push("</dl>");
                    sb.push("</li>");
                }
                //#endregion

                sb.push("</ul>");
                sb.push("</div>");
            });
            
            var strData = sb.length == 0 ? "无" : sb.join("");
            $("#routeschedule").html(strData);
            if (sb.length > 0) {
                systole();
            }
        }
    });
}



