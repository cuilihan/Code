var c = {
    init: function () {
        c.BindData(1);
        $("#moreData").click(function () {
            var p = $("#pIndex").val();
            c.BindData(parseInt(p) + 1)
        })
    },
    BindData: function (p) {
        var aurl = "Service/CustomerHandler.ashx?r=" + Math.random();
        var arrdata = { "rows": 15, "page": p, "action": 1 };


        $.ajax({
            type: "post",
            url: aurl,
            data: arrdata,
            beforeSend: function () {
            },
            success: function (data, textStatus) {
                var d = "(" + data + ")";
                var t = 0;
                var _row = "";
                $(eval(d)).each(function () {
                    t = this.total;
                    _row = this.rows
                })
                $("#total").html("[" + t + "]");
                $("#tblData").append(c.BindTdRow(_row));

                $("#pIndex").val(p);
            },
            error: function (ex) {
            }
        });

    },
    BindTdRow: function (_row) {
        var sb = [];
        var _index = $("#tblData").find("tr").size() + 1;
        $(_row).each(function () {
            sb.push("<tr>");
            sb.push("<td>" + _index++ + "</td>");
            sb.push("<td>" + this.Name + "</td>");
            sb.push("<td>" + this.Sex + "</td>");
            sb.push("<td>" + this.Mobile + "</td>");
            sb.push("<td>" + this.CustomerType + "</td>");
            sb.push("<td>" + this.Company + "</td>");
            sb.push("<td align=\"center\">" + this.TradeNum + "</td>");
            sb.push("<td align=\"right\">￥" + this.TradeAmt.toFixed(2) + "</td>");
            sb.push("<td align=\"center\">" + this.CommunicateNum + "</td>");
            sb.push("</tr>");

        })

        return sb.join("");
    }


}