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
    fnCustomerVisit();
});

function fnCustomerVisit() { //加载客户销售线索
    var u = "Service/Customer.ashx?action=7&id=" + request("id") + "&r=" + getRand();
    dataService.ajaxGet(u, function (data) {
        if (data != "") {
            var sb = [];
            $(eval(data)).each(function (idx) {
                var y = this.year;
                var info = this.info;
                var clsName = idx == 0 ? "first" : "date02";
                sb.push("<div class=\"history-date\">");
                sb.push("<ul>");
                sb.push("<h2 class='" + clsName + "'>");
                sb.push("<a href='#nogo'>" + y + "年</a>");
                if (idx == 0)
                    sb.push("销售线索记录");
                sb.push("</h2>");

                $(info).each(function (i) {
                    var title = "";
                    if (this.itemType != "") {
                        title = "[<a style='color:#0088CC; font-size:18px;'>" + this.itemType + "</a>]"
                    }
                    title += this.Comment;
                    sb.push("<li>");
                    sb.push("<h3>" + this.date + "<span>" + y + "</span></h3>");
                    sb.push("<dl>");
                    sb.push("<dt>" + title + "<span>拜访客户：" + this.customer + "&nbsp;&nbsp;&nbsp;&nbsp;登记人：" + this.creator + "&nbsp;&nbsp;&nbsp;&nbsp;登记日期：" + this.createdate + " &nbsp;&nbsp;&nbsp;&nbsp;预计成单日期：" + this.TradeDate + "</span></dt>");
                    if ((info.length - 1 == i) && (eval(data).length == idx + 1))
                        sb.push("<br/><br/><br/><br/><br/><br/><br/>");
                    sb.push("</dl>");
                    sb.push("</li>");
                });

                sb.push("</ul>");
                sb.push("</div>");
            }, false);

            if (sb.length == 0)
                $("#customervisit").hide();
            else {
                $("#customervisit").show();
                $("#customervisit").html(sb.join(""));
                systole();
            }
        }
    });
}