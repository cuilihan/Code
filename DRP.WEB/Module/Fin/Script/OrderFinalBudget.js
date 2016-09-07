//团队订单的预算
$(function () {
    c.init();
});

var c = {
    init: function () {
        $("#btnAddCostItem").click(function () { //增加成本
            var itemType = $("#ddlCostItemType option:selected").val();
            var itemName = $("#ddlCostItemType option:selected").text();
            if (itemType == "") {
                Alert("请选择项目类型");
            } else {
                c.fnAddCostItem(itemType, itemName);
            }
        });
        $("#btnSave").click(function () { //提交决算
            c.fnSave('5');
        });
        $("#btnFinalSave").click(function () {
            if (confirm('确定要提交决算并确认吗，确认后无法修改预决算表'))
                c.fnSave('7');
            else
                return;
        });
        comm.fnUploadFile(); //上传附件
    },
    fnAddCostItem: function (itemType, itemName) { //增加成本       
        var arr = [];
        var iSize = $("#tblCostItem").find("tr").size() + 1;
        var hfItemType = "<input type='hidden' value='" + itemType + "' />";//资源类型
        var hfCostItemID = "<input type='hidden' />";//成本表主键ID
        arr.push("<tr>");
        arr.push("<td style='text-align:center;'>" + iSize.toString() + hfCostItemID + "</td>");
        arr.push("<td>" + itemName + hfItemType + "</td>");
        arr.push("<td></td>");
        arr.push("<td></td>");
        arr.push("<td></td>");
        arr.push("<td>" + comm.fnQuerySupplier(itemType) + "</td>");
        arr.push("<td><input name='amt' style='width:80px;height:26px; text-align:right; padding-right:10px;' class='checkInt textbox' /></td>");
        arr.push("<td><input style='width:96%; height:26px;' type='text' class='textbox' /></td>");
        arr.push("<td style='text-align:center;'><a onclick='comm.fnDeleteCostItem(this)' href='javascript:;'>删除</a></td>");
        arr.push("</tr>");
        $("#tblCostItem").append(arr.join(""));
        checkInt();
    },
    fnGetCostItem: function () { //获取订单成本项目
        var xml = [];
        $("#tblCostItem").find("tr").each(function () {
            var tdArr = $(this).find("td");
            var itemID = $("input[type='hidden']", tdArr[0]).val();
            var itemType = $("input[type='hidden']", tdArr[1]).val();
            var itemName = $.trim($(tdArr[1]).text());
            var supplier = $(tdArr[5]).find("select").eq(0).find("option:selected").text()
            var supplierID = $("select", tdArr[5]).val();
            var costAmt = $(tdArr[6]).find("input").eq(0).val();
            var comment = $(tdArr[7]).find("input").eq(0).val();
            if (typeof (supplier) == "undefined") supplier = "";
            if (supplierID == "" && costAmt == "" && comment == "") { }
            else {
                xml.push("<data>");
                xml.push("<ID>" + itemID + "</ID>");
                xml.push("<ItemType>" + itemType + "</ItemType>");
                xml.push("<ItemName>" + itemName + "</ItemName>");
                xml.push("<SupplierID>" + supplierID + "</SupplierID>");
                xml.push("<Supplier>" + supplier + "</Supplier>");
                xml.push("<CostAmt>" + costAmt + "</CostAmt>");
                xml.push("<Comment>" + comment + "</Comment>");
                xml.push("</data>");
            }
        });
        if (xml.length == 0) return "";
        return "<document>" + xml.join("") + "</document>";
    },
    url: "Service/OrderInfo.ashx?xType=" + request("xType"),
    fnSave: function (budgetStatus) { //提交决算单
        var orderID = request("id");
        var orderAmt = $("#OrderAmt").val();
        var adultNum = $("#AdultNum").val();
        var childNum = $("#ChildNum").val();
        var comment = $("#txtComment").val();
        var costItem = c.fnGetCostItem();
        var fileIDs = comm.fnGetFileID();

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
            url: c.url + "&action=3",
            data: { "OrderID": orderID, "AdultNum": adultNum, "ChildNum": childNum, "OrderAmt": orderAmt, "CostItem": costItem, "Comment": comment, "DataStatus": "2", "BudgetStatus": budgetStatus, "FileID": fileIDs },
            beforeSend: function (XMLHttpRequest) {
                $("#btnOpt").addClass("hide");
                $("#tips").removeClass("hide");
            },
            success: function (data, textStatus) {
                $("#tips").addClass("hide");
                if (data == "1") {
                    var t = "【" + request("orderNo") + "】";
                    closeTab("保存成功", t + "决算");
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
