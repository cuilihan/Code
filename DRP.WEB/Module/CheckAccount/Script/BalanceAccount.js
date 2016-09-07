$(function () {
    t.initData();
    $("#btnSave").click(function () {
        t.fnSave(1);
    });
});

//导游报账
var t = {
    initData: function () {
        var keyID = request("id");
        if (keyID == "") {
            $("#btnOpt").addClass("hide");
            return;
        }
        if ($("#HasUpLoadData").val() != "0") { //已报账或已确认报账 
            t.fnLoadData();
        }
        else {
            t.fnLoadEmptyData();
        }
        t.fnCalculate();
        $("#chkToTravelAmt").click(function () {
            t.fnCalculate();
        });
    },
    fnLoadData: function () { //查询已报账的数据
        var keyID = request("orderBalanceID");//报账单ID  Ord_OrderBalance表主键
        var url = "Service/OrderCheckAccount.ashx?action=4&r=" + getRand() + "&id=" + keyID;
        dataService.ajaxGet(url, function (s) {
            if (s == "") {
                alert("未查询到已报账的数据");
                t.fnLoadEmptyData();
            }
            else {
                $("#tblItem").html(s);
                checkNumber();
                t.fnCalculate();
                t.fnInputBlurEvent();
            }
        });
    },
    fnLoadEmptyData: function () { //待报账表格
        var mp_n = 6;
        var djzf_n = 1;
        var dpjd_n = 1;
        var zsf_n = 5;
        var cf_n = 6;
        var qb_n = 1;
        var glf_n = 1;
        var qt_n = 3;
        var rowspan = mp_n + djzf_n + zsf_n + cf_n + qb_n + glf_n + qt_n + 2;
        var sb = [];
        sb.push(t.fnHeader(rowspan)); //以下所有行数+2
        sb.push(t.fnAddItem("门票", "1", mp_n));
        sb.push(t.fnAddItem("综费", "2", djzf_n));
        // sb.push(t.fnAddItem("地陪景导", "3", dpjd_n));
        sb.push(t.fnAddItem("房费", "4", zsf_n));
        sb.push(t.fnAddItem("餐费", "5", cf_n));
        sb.push(t.fnAddItem("导服", "6", qb_n));
        sb.push(t.fnAddItem("过路费", "7", glf_n));
        sb.push(t.fnAddItem("其他", "8", qt_n));
        sb.push(t.fnFooter());
        $("#tblItem").html(sb.join(""));
        checkNumber();
        t.fnInputBlurEvent();
    },
    fnHeader: function (rowspan) {
        var sb = [];
        var GuideDrawAmount = $("#GuideDrawAmount").val();
        sb.push("<tr>");
        sb.push("<th style=\"width: 30px; text-align: center;\">人数</th>");
        sb.push("<td colspan='7' style='padding-left:20px;'>成人：<input style='width:60px;' id='txtAdult' class='txt checkNum' />儿童：<input style='width:60px;' id='txtChild' class='txt checkNum' /></td>");
        sb.push("</tr>");

        sb.push("<tr>");
        sb.push("<th rowspan=\"4\" style=\"width: 30px; text-align: center;\">收<br />入</th>");
        sb.push("<th style=\"width: 80px; text-align: center;\">预领团款</th>");
        sb.push("<td colspan='5'><input style='width:96%;' id='txtYLTK' value='" + GuideDrawAmount + "' class='txt checkNum' /></td>");
        sb.push("<td style='width:80px;'><input style='width:96%;' class='txt' id='txtYLTKRemark' /></td>");
        sb.push("</tr>");
        sb.push("<tr>");
        sb.push("<th style=\"text-align: center;\">现收团款</th>");
        sb.push("<td colspan='5'><input style='width:96%;' id='txtXSTK' class='txt checkNum' /></td>");
        sb.push("<td><input style='width:96%;' class='txt' id='txtXSTKRemark' /></td>");
        sb.push("</tr>");

        sb.push("<tr>");
        sb.push("<th style=\"text-align: center;\">汇地接社款</th>");
        sb.push("<td colspan='5'><input style='width:96%;' id='txtYHZZ' class='txt checkNum' /></td>");
        sb.push("<td style='text-align:center;'><input type='checkbox' id='chkToTravelAmt' />计算收入</td>");
        sb.push("</tr>");

        sb.push("<tr>");
        sb.push("<th style=\"text-align: center;\">其他收入</th>");
        sb.push("<td colspan='5'><input style='width:96%;' id='txtQTSR' class='txt checkNum' /></td>");
        sb.push("<td><input style='width:96%;' class='txt' id='txtQTSRRemark' /></td>");
        sb.push("</tr>");

        sb.push("<tr>");
        sb.push("<th rowspan=\"" + rowspan + "\">支<br />出</th>");
        sb.push("<th>&nbsp;</th>");
        sb.push("<th>景点</th>");
        sb.push("<th style=\"width: 70px;\">单价</th>");
        sb.push("<th style=\"width: 70px;\">数量</th>");
        sb.push("<th style=\"width: 70px;\">总价</th>");
        sb.push("<th style=\"width: 70px;\">是否签单</th>");
        sb.push("<th>是否有发票</th>");
        sb.push("</tr>");
        return sb.join("");
    },
    fnFooter: function () {
        var sb = [];
        sb.push("<tr>");
        sb.push("<th>总计支出</th>");
        sb.push("<td style=\"text-align: right; font-family:arial; font-weight:bold;\" colspan=\"4\" id='TotalCost'></td>");
        sb.push("<td colspan='2' style=\"text-align: center;\"></td>");
        sb.push("</tr>");

        sb.push("<tr>");
        sb.push("<th colspan=\"2\">收支盈余</th>");
        sb.push("<td style=\"text-align: right;font-family:arial;font-weight:bold;\" colspan=\"4\" id='TotalProfit'></td>");
        sb.push("<td colspan='2' style=\"text-align: center;\"></td>");
        sb.push("</tr>");

        return sb.join("");
    },
    fnAddItem: function (itemName, itemType, rowSpan) {
        if (rowSpan == "" || typeof (rowSpan) == "undefined")
            rowSpan = 1;
        var hfCtrl = "<input type='hidden' value='" + itemType + "' />"; //大类标识
        var sb = [];
        for (var i = 0; i < rowSpan; i++) {
            sb.push("<tr>");
            if (rowSpan > 1) {
                if (i == 0)
                    sb.push("<th rowspan='" + rowSpan + "'>" + itemName + hfCtrl + "</th>");
            }
            else
                sb.push("<th>" + itemName + hfCtrl + "</th>");
            sb.push("<td style=\"text-align: center;\"><input type=\"text\" class=\"txt\" style=\"width: 96%;\" /></td>");
            sb.push("<td style=\"text-align: center;\"><input type=\"text\" class=\"txt checkNum txtRight\" style=\"width: 65px;\" /></td>");
            sb.push("<td style=\"text-align: center;\"><input type=\"text\" class=\"txt checkNum txtRight\" style=\"width: 65px;\" /></td>");
            sb.push("<td style=\"text-align: center;\"><input type=\"text\" name='amt' class=\"txt checkNum txtRight\" style=\"width: 65px;\" /></td>");
            sb.push("<td style=\"text-align: center;\"><input type=\"checkbox\" name=\"chkSignature\" onclick=\"t.fnCalculate()\" /></td>");
            sb.push("<td style=\"text-align: center;\"><input type=\"checkbox\" name=\"chkInvoice\" /></td>");
            sb.push("</tr>");
        }
        return sb.join("");
    },
    fnInputBlurEvent: function () {
        $(".checkNum", "#tblItem").blur(function () {
            t.fnCalculate();
        });
    },
    fnCalculate: function () {
        var strYltk = $("#txtYLTK").val();
        var strXSTK = $("#txtXSTK").val();
        var strYHZZ = $("#txtYHZZ").val(); //汇地接社款
        var isToTravelAmt = $("#chkToTravelAmt").attr("checked") == "checked";
        var strQTSR = $("#txtQTSR").val();
        var yltk = strYltk == "" ? 0 : parseFloat(strYltk);
        var xstk = strXSTK == "" ? 0 : parseFloat(strXSTK);
        var yhzz = strYHZZ == "" ? 0 : parseFloat(strYHZZ);
        var qtsr = strQTSR == "" ? 0 : parseFloat(strQTSR);
        var totalIncome = yltk + xstk + qtsr;
        if (isToTravelAmt)
            totalIncome += yhzz;

        var totalCost = 0;
        var n = $("#tblItem").find("tr").size();
        $("#tblItem").find("tr").each(function (idx) {
            if (idx > 5 && idx < n - 2) {
                var tdArr = $(this).find("td");
                var strPrice = $("input[type='text']", tdArr[1]).val();
                var strNum = $("input[type='text']", tdArr[2]).val();
                var __amt = $("input[type='text']", tdArr[3]).val();
                var isSign = $("input[type='checkbox']", tdArr[4]).attr("checked") == "checked"; //是否签单
                var price = strPrice == "" ? 0 : parseFloat(strPrice);
                var num = strNum == "" ? 0 : parseFloat(strNum);
                var t = price * num;
                if (__amt == "" || __amt == "0.00") {
                    $("input[type='text']", tdArr[3]).val(t.toFixed(2).toString());
                    if (!isSign)
                        totalCost += t;
                }
                else {
                    if (!isSign)
                        totalCost += parseFloat(__amt);
                }
            }
        });
        var profit = totalIncome - totalCost;
        $("#TotalCost").text(totalCost.toFixed(2).toString());
        $("#TotalProfit").text(profit.toFixed(2).toString());
    },
    fnGetItemData: function () {
        var sb = [];
        sb.push("<document>");
        var itemType = 0;
        var itemName = "";
        var n = $("#tblItem").find("tr").size();

        $("#tblItem").find("tr").each(function (idx) {
            if (idx > 5 && idx < n - 2) {
                var tdArr = $(this).find("td");
                var thCtrl = $(this).find("th").eq(0);
                var __itemName = $.trim($(thCtrl).text());
                if (__itemName != "") {
                    itemName = __itemName;
                    itemType = $(thCtrl).find("input[type='hidden']").val();
                }
                var sName = $("input[type='text']", tdArr[0]).val();
                var strPrice = $("input[type='text']", tdArr[1]).val();
                var strNum = $("input[type='text']", tdArr[2]).val();
                var strAmt = $("input[type='text']", tdArr[3]).val();
                var isSign = $("input[type='checkbox']", tdArr[4]).attr("checked") == "checked";
                var isInvoice = $("input[type='checkbox']", tdArr[5]).attr("checked") == "checked";
                var price = strPrice == "" ? 0 : parseFloat(strPrice);
                var num = strNum == "" ? 0 : parseFloat(strNum);
                var t = price * num;
                sb.push("<data type='" + itemType + "' name='" + itemName + "'>");
                sb.push("<sName>" + sName + "</sName>");
                sb.push("<Price>" + price + "</Price>");
                sb.push("<Num>" + num + "</Num>");
                sb.push("<ItemAmt>" + strAmt + "</ItemAmt>");
                sb.push("<Total>" + t + "</Total>");
                sb.push("<isSign>" + (isSign == true ? "1" : "0") + "</isSign>");
                sb.push("<isInvoice>" + (isInvoice == true ? "1" : "0") + "</isInvoice>");
                sb.push("</data>");
            }
        });
        sb.push("</document>");
        return sb.join("");
    },
    fnSave: function (dataStatus) {
        var adultNum = $("#txtAdult").val();
        var childNum = $("#txtChild").val();
        var strYltk = $("#txtYLTK").val();
        var strXSTK = $("#txtXSTK").val();
        var strYHZZ = $("#txtYHZZ").val();
        var strQTSR = $("#txtQTSR").val();
        var strYltkRemark = $("#txtYLTKRemark").val();
        var strXSTKRemark = $("#txtXSTKRemark").val();
        var strYHZZRemark = $("#chkToTravelAmt").attr("checked") == "checked" ? "1" : "0";
        var strQTSRRemark = $("#txtQTSRRemark").val();
        var orderID = $("#OrderID").val();
        var orderGuideID = $("#OrderGuideID").val();
        var guideName = $.trim($("#GuideName").text());
        var guideMobile = $.trim($("#GuidePhone").text());
        var xmlData = t.fnGetItemData();
        var orderBalanceID = request("orderBalanceID");
        if (adultNum == "" && childNum == "") {
            alert("请填写人数")
            return false;
        }
        if (!confirm("确定要提交报账数据吗"))
            return false;

        $.ajax({
            type: "post",
            url: "Service/OrderCheckAccount.ashx?action=3&r=" + getRand(),
            data: { "OrderGuideID": request("id"), "ID": orderBalanceID, "OrderID": orderID, "OrderType": request("xType"), "AdultNum": adultNum, "ChildNum": childNum, "YLTK": strYltk, "XSTK": strXSTK, "YHZZ": strYHZZ, "QTSR": strQTSR, "YLTKRemak": strYltkRemark, "XSTKRemark": strXSTKRemark, "YHZZRemark": strYHZZRemark, "QTSRRemark": strQTSRRemark, "GuideName": guideName, "GuideMobile": guideMobile, "Data": xmlData, "DataStatus": dataStatus },
            beforeSend: function (XMLHttpRequest) {
                $("#btnOpt").addClass("hide");
                $("#tips").removeClass("hide");
            },
            success: function (data, textStatus) {
                $("#tips").addClass("hide");
                if (data == "1") {
                    alert("报账数据提交成功");
                    if (request("t") != "1")
                        window.location.href = "OrderSelect.aspx";
                }
                else {
                    $("#btnOpt").removeClass("hide");
                    alert("保存数据失败");
                }
            },
            error: function () {
                $("#btnOpt").removeClass("hide");
                $("#tips").addClass("hide");
                Alert("操作失败");
            }
        });
    }
};