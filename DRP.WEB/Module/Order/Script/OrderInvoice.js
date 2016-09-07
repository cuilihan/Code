var invoice = {
    init: function () {
        checkInt();
        invoice.fnSetCalculate();
        invoice.fnCalculate();
        $("#btnSave").click(function () {
            invoice.fnSave();
        });
    },
    fnSetCalculate: function () {
        $("#tblInvoice").find("input[type='text']").blur(function () {
            invoice.fnCalculate();
        });
    },
    fnCalculate: function () { //开票金额
        var total = 0;
        $("#tblInvoice").find("tr").each(function () {
            var tdArr = $(this).find("td");
            var a = $(tdArr[6]).find("input[type='text']").val();
            var i = a == "" ? 0 : parseFloat(a);
            total += i;
        });
        $("#InvoiceAmt").html(total.toFixed(2));
    },
    fnGetOrderAmt: function () { //可开票的订单金额
        var total = 0;
        $("#tblInvoice").find("tr").each(function () {
            var tdArr = $(this).find("td");
            var a = $.trim($(tdArr[4]).text());//订单金额
            var b = $.trim($(tdArr[5]).text());//已开票金额
            var __a = a == "" ? 0 : parseFloat(a);
            var __b = b == "" ? 0 : parseFloat(b);
            total += (__a - __b);
        });
        return total;
    },
    fnGetInvoiceItem: function () {
        var sb = [];
        $("#tblInvoice").find("tr").each(function () {
            var tdArr = $(this).find("td");
            var id = $(tdArr[0]).find("input[type='hidden']").val();
            var a = $(tdArr[6]).find("input[type='text']").val();
            var i = a == "" ? 0 : parseFloat(a);
            if (i > 0) {
                sb.push("<data>");
                sb.push("<orderID>" + id + "</orderID>");
                sb.push("<invoiceAmt>" + i + "</invoiceAmt>");
                sb.push("</data>");
            }
        });
        return sb.length == 0 ? "" : "<document>" + sb.join("") + "</document>";
    },
    fnSave: function () {
        var OrderID = request("id");
        var xType = request("xType");
        var InvoiceName = $("#InvoiceName").val();
        var InvoiceUnit = $("#InvoiceUnit").val();
        var InvoiceItem = $("#InvoiceItem option:selected").val();
        var FetchType = $("#FetchType option:selected").val();
        var Comment = $("#Comment").val();
        var xmlItem = invoice.fnGetInvoiceItem();
        var strAmt = $.trim($("#InvoiceAmt").text());
        var iAmt = strAmt == "" ? 0 : parseFloat(strAmt);
        if (iAmt <= 0) {
            Alert("开票金额须大于0");
            return false;
        }
        var isOver = false;
        var enableInvoiceAmt = invoice.fnGetOrderAmt();//可开票金额
        if (enableInvoiceAmt - iAmt < 0) {
            if (!confirm("开票金额超出订单金额，是否继续申请开票？"))
                return false;
            isOver = true;
        }
        if (InvoiceName == "") {
            Alert("发票抬头不能为空");
            return false;
        }
        if (InvoiceUnit == "") {
            Alert("开票单位不能为空");
            return false;
        }
        if (InvoiceItem == "") {
            Alert("开票项目不能为空");
            return false;
        }
        var u = "Service/OrderInvoice.ashx?xType=" + request("xType") + "&action=1&r=" + getRand();
        $.ajax({
            type: "post",
            url: u,
            data: { "InvoiceName": InvoiceName, "InvoiceUnit": InvoiceUnit, "InvoiceItem": InvoiceItem, "FetchType": FetchType, "Comment": Comment, "IsOver": isOver, "XmlItem": xmlItem },
            beforeSend: function (XMLHttpRequest) {
                $("#btnOpt").addClass("hide");
                $("#tips").removeClass("hide");
            },
            success: function (data, textStatus) {
                $("#tips").addClass("hide");
                if (data == "1") {
                    closeTab("保存成功", "开票申请");
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


$(function () {
    invoice.init();
});