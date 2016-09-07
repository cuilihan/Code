//综合业务报表

$(function () {
    p.init();
});


var p = {
    init: function () {
        p.fnBindData();
        $("#btnQuery").click(function () {
            p.fnBindData();
        });
    },
    fnGetUrl: function () {
        var y = $("#ddlYear option:selected").val();
        var deptID = $("#ddlDept option:selected").val();
        return "Service/RptUtility.ashx?xType=17&action=14&r=" + getRand() + "&y=" + y + "&deptID=" + deptID;
    },
    fnBindData: function () {
        var u = p.fnGetUrl();
        dataService.ajaxGet(u, function (data) { 
            if (data == "") {
                $("#tblData").html("<tr><td>未查询到数据</td></tr>");
            }
            else
                $("#tblData").html(data);
        });
    }
};