//线路发布
var r = {
    init: function () {
        r.setClickEvent();
        r.setRichText();
        r.fnSetDestination();
        r.fnSetDefaultServiceItem();
        navTab("ul_tabs", "tab_conbox");
        checkInt();
        r.fnCalculateAmount();
    },
    serviceURL: "Service/RouteTemplate.ashx",
    setClickEvent: function () { //注册点击事件
        $("#btnAddDay").click(function () {
            var tDays = $("#tblSchedule").find("table").size();
            tDays += 1;
            $("#Days").val(tDays);
            r.setRouteSchedule();
        });
        $("#Days").blur(function () {
            r.setRouteSchedule();
        });
        $("#btnSave").click(function () {
            r.fnSave();
            return false;
        });
        $("#RouteType").change(function () {
            r.fnInitDestination();
        });
        $("#btnAddServiceItem").click(function () {
            r.fnAddServiceItem();
        });
    },
    toolbarItems: ['fontname', 'fontsize', '|', 'forecolor', 'hilitecolor', 'bold', 'italic', 'underline',
                    'removeformat', '|', 'justifyleft', 'justifycenter', 'justifyright', 'insertorderedlist',
                    'insertunorderedlist', '|', 'emoticons', 'link', '|', 'plainpaste', 'source'],
    setRichText: function () { //设置富文本编辑框  
        r.createRichText("Feature");
        r.createRichText("SelfItem");
        r.createRichText("Notes");
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
        var strDays = $("#Days").val();
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
                sb.push("<td rowspan='4' style='text-align:center;width:30px;'><a href='javascript:;' onclick=\"r.fnDeleteSchedule(this)\">删除</a></td>");
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
                sb.push("</table>");
                $("#tblSchedule").append(sb.join(""));
            }
        }
    },
    fnInitDestination: function () { //目的地 
        var routeTypeID = $("#RouteType option:selected").val();
        $('#Destination').combotree({
            url: r.serviceURL + "?action=5&routeTypeID=" + routeTypeID
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
            sb.push("<data>");
            sb.push("<daynum>" + (i + 1) + "</daynum>");
            sb.push("<title>" + title + "</title>");
            sb.push("<schedule>" + $.trim(content) + "</schedule>");
            sb.push("<stay>" + stay + "</stay>");
            sb.push("<dinner>" + dinner + "</dinner>");
            sb.push("</data>");
        });
        sb.push("</document>");
        return sb.join("");
    },
    fnAddServiceItem: function () { //添加服务标准
        var sb = [];
        var i = $("#tblServiceItem").find("tr").size() + 1;
        sb.push("<tr>");
        sb.push("<td style='text-align:center;'>" + i + "</td>");
        sb.push("<td>" + r.fnAddItemOption() + "</td>");
        sb.push("<td><input type='text' class='textbox' style='width:96%;height:26px;' /></td>");
        sb.push("<td><input type='text' onblur='r.fnCalculateAmount()' class='textbox checkInt' style='width:50px;height:26px; text-align:center;' /></td>");
        sb.push("<td><input type='text' onblur='r.fnCalculateAmount()' class='textbox checkInt' style='width:50px;height:26px; text-align:center;' /></td>");
        sb.push("<td style='text-align:right;'></td>");
        sb.push("<td style='text-align:center;'><a href='javascript:;' onclick='r.fnDeleteServiceItem(this)'>删除</a></td>");
        sb.push("</tr>");

        $("#tblServiceItem").append(sb.join(""));
        checkInt();
    },
    fnSetDefaultServiceItem: function () { //设置默认的服务标准
        if (request("id") == "") {
            r.fnAddServiceItem();
            r.fnAddServiceItem();
            r.fnAddServiceItem();

            $("#tblServiceItem").find("select").eq(0).val("门票");
            $("#tblServiceItem").find("select").eq(1).val("住宿");
            $("#tblServiceItem").find("select").eq(2).val("用餐");
        }
    },
    fnCalculateAmount: function () { //计算团款
        var itemCost = 0;//服务成本总额
        $("#tblServiceItem").find("tr").each(function () {
            var tdArr = $(this).find("td");
            var strPrice = $("input[type='text']", tdArr[3]).val();
            var strNum = $("input[type='text']", tdArr[4]).val();
            var price = strPrice == "" ? 0 :parseFloat(strPrice);
            var num = strNum == "" ? 0 : parseInt(strNum);
            var sum = price * num;
            $(tdArr[5]).html(sum.toFixed(2).toString());
            itemCost += sum;
        });

        var strVisitorNum = $("#VisitorNum").val();//团队人数
        var visitorNum = strVisitorNum == "" ? 0 : parseInt(strVisitorNum);
        var strProfit = $("#Profit").val();//整团毛利
        var profit = strProfit == "" ? 0 : parseInt(strProfit);
        var strChildPrice = $("#ChildPrice").val();//儿童报价总额
        var childPrice = strChildPrice == "" ? 0 : parseInt(strChildPrice);
        var strChildCost = $("#ChildCost").val();//儿童成本
        var childCost = strChildCost == "" ? 0 : parseInt(strChildCost);
        var totalCost = itemCost + childCost;//总成本
        var tourAmount = totalCost + profit + childPrice;//报价总金额
        var avgCost = "-";
        if (visitorNum > 0) //人均报价
            avgCost = Math.abs(tourAmount / visitorNum).toFixed(2).toString();

        $("#tourCost").html(totalCost.toString()); //成本总额
        $("#tourAvgPrice").html(avgCost);//人均报价
        $("#tourVisitorNum").html(visitorNum.toString());
        $("#tourProfit").html(profit.toString());//预期毛利       
        $("#tourTotalAmount").html(tourAmount.toString());//报价总额
    },
    fnAddItemOption: function () { //服务标准项目
        var sb = [];
        sb.push("<select class='textbox' style='width:140px; height:26px;'>");
        sb.push("<option value='门票'>门票</option>");
        sb.push("<option value='住宿'>住宿</option>");
        sb.push("<option value='用餐'>用餐</option>");
        sb.push("<option value='全陪'>全陪</option>");
        sb.push("<option value='地陪'>地陪</option>");
        sb.push("<option value='交通(飞机)'>交通(飞机)</option>");
        sb.push("<option value='交通(船)'>交通(船)</option>");
        sb.push("<option value='交通(火车)'>交通(火车)</option>");
        sb.push("<option value='交通(汽车)'>交通(汽车)</option>");
        sb.push("<option value='交通(接送)'>交通(接送)</option>");
        sb.push("<option value='保险(责任险)'>保险(责任险)</option>");
        sb.push("<option value='保险(意外险)'>保险(意外险)</option>");
        sb.push("<option value='保险(航空险)'>保险(航空险)</option>");
        sb.push("<option value='其他'>其他</option>");
        sb.push("</select>");
        return sb.join("");
    },
    fnDeleteServiceItem: function (o) { //删除服务标准项目
        $(o).parent().parent().remove();
    },
    fnGetServiceItem: function () { //获取服务标准
        var sb = [];
        sb.push("<document>");
        $("#tblServiceItem").find("tr").each(function (i) {
            var tdArr = $(this).find("td");
            var itemName = $("select", tdArr[1]).val();
            var itemRemark = $("input[type='text']", tdArr[2]).val();
            var itemPrice = $("input[type='text']", tdArr[3]).val();
            var itemNum = $("input[type='text']", tdArr[4]).val();

            sb.push("<data>");
            sb.push("<itemName>" + itemName + "</itemName>");
            sb.push("<itemRemark>" + itemRemark + "</itemRemark>");
            sb.push("<itemPrice>" + itemPrice + "</itemPrice>");
            sb.push("<itemNum>" + itemNum + "</itemNum>");
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
        var days = $("#Days").val();
        var visitorNum = $("#VisitorNum").val();
        var stay = $("#Stay option:selected").val();
        var dinner = $("#Dinner option:selected").val();
        var viewSpot = $("#ViewSpot").val();
        var feature = $("#Feature").val();
        var schedule = r.fnGetScheduleItem();
        var items = r.fnGetServiceItem();
        var selfItem = $("#SelfItem").val();
        var notes = $("#Notes").val();
        var comment = $("#Comment").val();
        var cost = $("#tourCost").text();
        var avgPrice = $("#tourAvgPrice").text();
        var profit = $("#Profit").val();
        var childPrice = $("#ChildPrice").val();
        var childCost = $("#ChildCost").val();
        var Remark = $("#Remark").val();
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
        if (profit == "") {
            Alert("请填写预期毛利");
            return false;
        }
        var data = { "ID": keyID, "RouteName": routeName, "RouteNo": routeNo, "Days": days, "RouteType": routeTypeName, "RouteTypeID": routeTypeID, "DestinationID": destinationID, "VisitorNum": visitorNum, "Stay": stay, "Dinner": dinner, "ViewSpot": viewSpot, "Feature": feature, "Schedule": schedule, "Items": items, "SelfItem": selfItem, "Notes": notes, "Comment": comment, "Cost": cost, "AvgPrice": avgPrice, "Profit": profit, "ChildPrice": childPrice, "ChildCost": childCost, "Remark": Remark };

        dataService.ajaxPost(r.serviceURL + "?action=2", data, function (r) {
            var tabTitle = copy == "copy" ? "复制报价单" : "报价单维护";
            closeTab((r == "1" ? "保存成功" : "保存失败"), tabTitle);
        });
    }
};

$(function () {
    r.init();
});