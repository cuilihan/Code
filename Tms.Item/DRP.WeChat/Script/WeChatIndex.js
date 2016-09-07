var c = {
    init: function () {
        c.BindData(1);
    },
    BindData: function (p) {
        var aurl = "/Service/WeChatHandler.ashx?r=" + Math.random();
        alert(aurl);
        $.ajax({
            type: "post",
            url: aurl,
            data: { "action": 1 },
            success: function (data, textStatus) {
                $(eval(data)).each(function () {
                    $("#CustomerNum").html(this.CustomerNum);
                    $("#CustomerNumDay").html(this.CustomerNumDay);
                    $("#OrderNum").html(this.OrderNum);
                    $("#OrderNumDay").html(this.OrderNumDay);
                    $("#OrdeAmt").html(this.OrdeAmt.toFixed(2));
                    $("#OrderAmtDay").html(this.OrderAmtDay.toFixed(2));
                    $("#OrderCost").html(this.OrderCost.toFixed(2));
                    $("#OrderCostDay").html(this.OrderCostDay.toFixed(2));
                    $("#total").html((parseFloat(this.OrdeAmt) - parseFloat(this.OrderCost)).toFixed(2));
                    $("#totalPert").html((parseFloat(this.OrderAmtDay) - parseFloat(this.OrderCostDay)).toFixed(2));
                })
            },
            error: function (ex) {
                alert(1);
            }
        });
    }
}