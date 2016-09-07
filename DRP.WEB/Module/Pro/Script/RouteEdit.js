//线路发布
var r = {
    init: function () {
        r.setClickEvent();
        r.setRichText();
        r.fnSetDestination();
        navTab("ul_tabs", "tab_conbox");
    },
    serviceURL: "Service/RouteMrg.ashx",
    setClickEvent: function () { //注册点击事件
        $("#btnAddDay").click(function () {
            var tDays = $("#tblSchedule").find("table").size();
            tDays += 1;
            $("#ScheduleDays").val(tDays);
            r.setRouteSchedule();
        });
        $("#ScheduleDays").blur(function () {
            r.setRouteSchedule();
        });
        $("#btnSave").click(function () {
            r.fnSave();
            return false;
        });
        $("#RouteType").change(function () {
            r.fnInitDestination();
        });
    },
    toolbarItems: ['fontname', 'fontsize', '|', 'forecolor', 'hilitecolor', 'bold', 'italic', 'underline',
                    'removeformat', '|', 'justifyleft', 'justifycenter', 'justifyright', 'insertorderedlist',
                    'insertunorderedlist', '|', 'emoticons', 'link', '|', 'plainpaste', 'source'],


    setRichText: function () { //设置富文本编辑框  
        r.createRichText("Feature");
        r.createRichText("PriceInclude");
        r.createRichText("PriceNonIncude");
        r.createRichText("SelfItem");
        r.createRichText("Remind");
        r.createRichText("Shopping");
        r.createRichText("Comment");
    },
    createRichText: function (objID) { //设置控件为富文本框  
        KindEditor.ready(function (K) {
            K.create('#' + objID, {
                resizeType: 1,
                height: 300,
                allowPreviewEmoticons: false,
                allowImageUpload: false,
                afterBlur: function () { this.sync(); },
                items: r.toolbarItems
            });
        });
    },
    setRouteSchedule: function () { //线路行程
        var tDays = $("#tblSchedule").find("table").size();
        var strDays = $("#ScheduleDays").val();
        var days = strDays == "" ? 0 : parseInt(strDays);

        var delt = tDays - days;
        if (delt > 0) {
            for (var i = 0; i < delt; i++) {
                $("#tblSchedule").find("table:last").remove();
            }
        }
        else {
            delt = delt * -1;
            if (delt == 0) delt = 1;
            for (var i = 0; i < delt; i++) {
                var sb = [];
                var d = (tDays + i + 1);
                var ctrlID = "tblSchedule_content_" + d + "_" + getRand();
                sb.push("<table class=\"tblInfo\" style='margin-bottom:15px;'>");
                sb.push("<tr>");
                sb.push("<td class='rowlabel_90'>第<span name='d'>" + d + "</span>天</td>");
                sb.push("<td><input type='text' class='textbox' style=\"width: 90%; height:24px;\" /></td>");
                sb.push("<td rowspan='5' style='text-align:center;width:30px;'><a href='javascript:;' onclick=\"r.fnDeleteSchedule(this)\">删除</a></td>");
                sb.push("</tr>");
                sb.push("<tr>");
                sb.push("<td class='rowlabel_90'>行程</td>");
                sb.push("<td><textarea id='" + ctrlID + "'style=\"width: 90%; height: 160px; overflow: auto;\"></textarea></td>");
                sb.push("</tr>");
                sb.push("<tr>");
                sb.push("<td class='rowlabel_90'>住宿</td>");
                sb.push("<td><input type='text'class='textbox' style=\"width: 90%;height:24px;\" /></td>")
                sb.push("</tr>");
                sb.push("<tr>");
                sb.push("<td class='rowlabel_90'>用餐</td>");
                sb.push("<td><input type='text' class='textbox' style=\"width: 90%;height:24px;\" /></td>")
                sb.push("</tr>");
                sb.push("<tr>");
                sb.push("<td class='rowlabel_90'>交通</td>");
                sb.push("<td><input type='text' class='textbox' style=\"width: 90%;height:24px;\" /></td>")
                sb.push("</tr>");
                sb.push("</table>");
                $("#tblSchedule").append(sb.join(""));
            }
        }
    },
    fnInitDestination: function () { //目的地 
        var routeTypeID = $("#RouteType option:selected").val();
        $('#Destination').combotree({
            url: r.serviceURL + "?action=4&routeTypeID=" + routeTypeID
        });
    },
    fnSetDestination: function () { //设置目的地
        var id = $("#DestinationID").val();
        if (id != "") {
            r.fnInitDestination();
            $('#Destination').combotree("setValue", id);
        }
    },
    fnDeleteSchedule: function (obj) { //删除行程 
        var i = $("#ScheduleDays").val();
        if (i != "") {
            var a = parseInt(i) - 1;
            if (a >= 0) {
                $("#ScheduleDays").val(a.toString())
                r.setRouteSchedule();
            }
        }
    },
    fnGetScheduleItem: function () { //获取行程内容
        var sb = [];
        sb.push("<document>");
        $("#tblSchedule").find("table").each(function (i) {
            var trArr = $(this).find("tr");
            var title = $("input[type='text']", trArr[0]).val();
            var content = $("textarea", trArr[1]).val();
            var stay = $("input[type='text']", trArr[2]).val();
            var dinner = $("input[type='text']", trArr[3]).val();
            var traffic = $("input[type='text']", trArr[4]).val();
            sb.push("<data>");
            sb.push("<daynum>" + (i + 1) + "</daynum>");
            sb.push("<title>" + title + "</title>");
            sb.push("<schedule>" + $.trim(content) + "</schedule>");
            sb.push("<stay>" + stay + "</stay>");
            sb.push("<dinner>" + dinner + "</dinner>");
            sb.push("<traffic>" + traffic + "</traffic>");
            sb.push("</data>");
        });
        sb.push("</document>");
        return sb.join("");
    },
    fnSave: function () {
        var keyID = request("id");
        var copy = request("xType");
        if (copy == "copy")
            keyID = "";
        var routeName = $("#RouteName").val();
        var routeNo = $("#RouteNo").val();
        var routeTypeID = $("#RouteType option:selected").val();
        var routeTypeName = $("#RouteType option:selected").text();
        var destinationID = $("#Destination").combotree("getValue"); //目的地
        var days = $("#ScheduleDays").val();
        var feature = $("#Feature").val();
        var priceInclude = $("#PriceInclude").val();
        var priceNoneInclude = $("#PriceNonIncude").val();
        var selfItem = $("#SelfItem").val();
        var remind = $("#Remind").val();
        var shopping = $("#Shopping").val();
        var comment = $("#Comment").val();
        var schedule = r.fnGetScheduleItem();
        var routeSourceID = $("#RouteSource option:selected").val();
        var routeSource = $("#RouteSource option:selected").text();

        if (routeName == "") {
            Alert("线路标题不能为空");
            return false;
        }
        if (routeTypeID == "") {
            Alert("线路类型不能为空");
            return false;
        }
        if (destinationID == "") {
            Alert("目的地不能为空");
            return false;
        }
        if (days == "") {
            Alert("行程天数不能为空");
            return false;
        }
        
        var data = { "ID": keyID, "RouteName": routeName, "RouteNo": routeNo, "Days": days, "RouteType": routeTypeName, "RouteTypeID": routeTypeID, "DestinationID": destinationID, "Feature": feature, "Schedule": schedule, "PriceInclude": priceInclude, "PriceNoneInclude": priceNoneInclude, "SelfItem": selfItem, "Remind": remind, "Shopping": shopping, "Comment": comment, "RouteSource": routeSource, "RouteSourceID": routeSourceID };

        dataService.ajaxPost(r.serviceURL + "?action=2", data, function (r) {
            var tabTitle = copy == "copy" ? "复制线路信息" : "线路信息维护";
            closeTab((r == "1" ? "保存成功" : "保存失败"), tabTitle);
        });
    }
};

$(function () {
    r.init();
});