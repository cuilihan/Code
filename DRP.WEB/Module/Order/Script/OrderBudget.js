//团队订单的预算
$(function () {
    c.init();
});

var c = {
    init: function () {
        c.fnBindCost();
        $("#btnAddCostItem").click(function () { //增加成本
            var itemType = $("#ddlCostItemType option:selected").val();
            var itemName = $("#ddlCostItemType option:selected").text();
            var isCustom = $("#isCustom").prop("checked");
            if (itemType == "") {
                Alert("请选择项目类型");
            }
            else if (isCustom) {
                comm.fnAddCustomCostItem(itemType, itemName);
            }
            else {
                comm.fnAddCostItem(itemType, itemName);
            }
        });
        $("#btnSave").click(function () { //提交预算
            c.fnSave();
        });
    },
    url: "Service/OrderInfo.ashx?xType=" + request("xType"),
    fnBindCost: function () { //绑定成本
        var isEdit = $("#hfIsEdit").val() == "1";
        if (!isEdit)
            c.fnDefaultItem();
    },
    fnDefaultItem: function () { //设置默认值
        comm.fnAddCostItem("1", "供应商");
        comm.fnAddCostItem("5", "车费");
        comm.fnAddCostItem("2", "景点门票");
        comm.fnAddCostItem("9", "票务机构");
        comm.fnAddCostItem("4", "房费");
        comm.fnAddCostItem("3", "导服费用");
        comm.fnAddCostItem("6", "签证费用");
        comm.fnAddCostItem("7", "保险费用");
        comm.fnAddCostItem("10", "其他");
    },
    fnSetDefaultComment: function () { //默认备注
        c.fnSetBudgetComment("住宿");
        c.fnSetBudgetComment("用餐");
        c.fnSetBudgetComment("购票方式");
        c.fnSetBudgetComment("用车");
        c.fnSetBudgetComment("帽子胸贴水");
        c.fnSetBudgetComment("地接付款");
        c.fnSetBudgetComment("代收款");
        c.fnSetBudgetComment("其他");
    },
    fnSetBudgetComment: function (name) { //预算备注  
        var sb = [];
        var i = $("#tblComment").find("tr").size() + 1;
        sb.push("<tr>");
        sb.push("<td style='text-align:center;'>" + i + "</td>");
        sb.push("<td>" + name + "</td>");
        sb.push("<td><input type='text' class='textbox' style='height:26px; width:90%;' /></td>");
        sb.push("</tr>");
        $("#tblComment").append(sb.join(""));
    },
    fnGetBudgetComment: function () { //获取预算备注
        var sb = [];
        $("#tblComment").find("tr").each(function () {
            var tdArr = $(this).find("td");
            var name = $.trim($(tdArr[1]).text());
            var comment = $(tdArr[2]).find("input[type='text']").val();
            if (comment.indexOf("<") > -1)
                comment = comment.replace("<", "");
            if (comment.indexOf(">") > -1)
                comment = comment.replace(">", ""); 
            if (comment != "") {
                sb.push("<data>");
                sb.push("<name>" + name + "</name>");
                sb.push("<comment>" + comment + "</comment>");
                sb.push("</data>");
            }
        });
        return sb.length > 0 ? "<document>" + sb.join("") + "</document>" : "";
    },
    fnSave: function () { //提交预算单
        var orderID = request("id");
        var orderAmt = $("#OrderAmt").val();
        var adultNum = $("#AdultNum").val();
        var childNum = $("#ChildNum").val();
        var drawMoney = $("#DrawMoney").val();
        var method = $("#Method option:selected").val();
        var drawmoneyComment = $("#Comment").val();
        var costItem = comm.fnGetCostItem();
        var comment = c.fnGetBudgetComment();

        if (orderAmt == "") {
            Alert("请填写团队的应收款");
            return false;
        }
        if (adultNum == "") {
            Alert("请填写团队人数");
            return false;
        }
        $.ajax({
            type: "post",
            url: c.url + "&action=11",
            data: { "OrderID": orderID, "AdultNum": adultNum, "ChildNum": childNum, "OrderAmt": orderAmt, "DrawMoney": drawMoney, "DrawMoneyMethod": method, "DrawMoneyComment": drawmoneyComment, "CostItem": costItem, "Comment": comment, "DataStatus": "1", "BudgetStatus": "4" },
            beforeSend: function (XMLHttpRequest) {
                $("#btnOpt").addClass("hide");
                $("#tips").removeClass("hide");
            },
            success: function (data, textStatus) {
                $("#tips").addClass("hide");
                if (data == "1") {
                    var t = "【" + request("orderNo") + "】";
                    closeTab("保存成功", t + "预算");
                }
                else {
                    $("#btnOpt").removeClass("hide");
                    Alert("保存数据失败");
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
