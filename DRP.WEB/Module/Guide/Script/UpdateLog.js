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
    fnShowTraceLog();
});

function fnShowTraceLog() { //系统更新日志
    var u = "Service/Index.ashx?action=2&r=" + getRand();
    dataService.ajaxGet(u, function (data) {
        if (data != "") {
            var sb = [];
            if (data == "" || data == "[]")
                sb.push("<div>未查询到数据</div>");
            $(eval(data)).each(function (idx) {
                var y = this.year;
                var info = this.info;
                var clsName = idx == 0 ? "first" : "date02";
                sb.push("<div class=\"history-date\">");
                sb.push("<ul>");
                sb.push("<h2 class='" + clsName + "'>");
                sb.push("<a href='#nogo'>" + y + "年</a>");
                if (idx == 0)
                    sb.push("旅管家更新日志");
                sb.push("</h2>");

                $(info).each(function (i) {
                    var title ="【<a style='color:#0088CC; font-size:15px;'>" + this.xType + "</a>】更新日期：" + this.createdate + "&nbsp;&nbsp;&nbsp;&nbsp;更新人：" + this.creator;

                    title += "<div style='padding-left:5px; margin-top:5px;line-height:28px; font:15px 微软雅黑;color:#737373;'>" + this.Comment + "</div>";
                    sb.push("<li>");
                    sb.push("<h3>" + this.date + "<span>" + y + "</span></h3>");
                    sb.push("<dl>");
                    sb.push("<dt>" + title +"</dt>");
                    if ((info.length - 1 == i) && (eval(data).length == idx + 1))
                        sb.push("<br/><br/><br/><br/><br/><br/><br/>");
                    sb.push("</dl>");
                    sb.push("</li>");
                });
                sb.push("</ul>");
                sb.push("</div>");
            }, false);

            $("#TraceLog").html(sb.join(""));
            systole();
        }
    });
}