var p = {
    init: function () {
        p.fnInitRouteType();
        p.fnSetQryParameter();
        p.fnQueryData();
        $("#btnQuery").click(function () {
            p.fnQueryData();
        });
    },
    serverURL: "/Module/Order/Service/OrderUtiltity.ashx",
    fnInitRouteType: function () { //线路类型
        $("#RouteTypeID").combobox({
            mode: 'remote',
            url: p.serverURL + "?action=1",
            valueField: 'id',
            textField: 'text',
            onSelect: function (rec) {
                var routeTypeID = rec.id;
                if (routeTypeID != "") {
                    p.fnInitDestination(routeTypeID);
                }
            }
        });
    },
    fnInitDestination: function (routeTypeID) { //创建目的地树 
        $('#DestinationID').combotree({
            url: p.serverURL + "?action=2&routeTypeID=" + routeTypeID + "&r=" + getRand(),
            idField: 'id', textField: 'text'
        });
    },
    fnSetQryParameter: function () { //设置查询参数
        var routeTypeID = request("rid");
        var destinationID = request("did");
        var sDate = request("sDate");
        var eDate = request("eDate");
        if (routeTypeID != "") {
            $("#RouteTypeID").combobox("setValue", routeTypeID);
        }
        if (destinationID != "") {
            p.fnInitDestination(routeTypeID);
            $('#DestinationID').combotree("setValue", destinationID);
        }
        if (sDate != "") $("#sDate").val(sDate);
        if (eDate != "") $("#eDate").val(eDate);
    },
    fnQueryData: function () {
        var u = "Service/ProSearch.ashx?action=2";
        var sDate = $("#sDate").val();
        var eDate = $("#eDate").val();
        var rid = $("#RouteTypeID").combobox("getValue");
        var did = $('#DestinationID').combotree("getValue");
        var key = $("#TourName").val();
        var EffectiveDays = $("#EffectiveDays").prop("checked");
        if (sDate != "")
            u += "&sDate=" + sDate;
        if (eDate != "")
            u += "&eDate=" + eDate;
        if (rid != "")
            u += "&rid=" + rid;
        if (did != "")
            u += "&did=" + did;
        if (key != "")
            u += "&key=" + encodeURI(key);

        u += "&EffectiveDays=" + EffectiveDays;
        $('#tblData').datagrid({
            loadMsg: "正在加载数据...",
            height: document.documentElement.clientHeight - 32,
            nowrap: true,
            border: 0,
            iconCls: 'icon-reload',
            url: u,
            rownumbers: true, //行号 
            toolbar: "#toolbar",
            frozenColumns: [[
                { field: 'TourNo', title: '班次编号', width: 140, sortable: true, sortName: "TourNo", align: 'center' },
                { field: 'Destination', title: '目的地', width: 60, sortable: true, sortName: "Destination", align: 'center' },
                {
                    field: 'TourName', title: '线路名称', width: 200, sortable: true, sortName: "TourName", formatter: function (val, rec) {
                        return "<a href='RouteInfo.aspx?id=" + rec.RouteID + "' target='_blank'>" + val + "</a>";
                    }
                }
            ]],
            columns: [[
                { field: 'TourDays', title: '行程天数', width: 60, align: 'center', sortable: true, sortName: "TourDays", align: 'center' },
                {
                    field: 'tDate', title: '出团日期', width: 110, align: 'center', sortable: true, sortName: "TourDate", formatter: function (val, rec) {
                        return val + "(<span style='color:blue;'>" + rec.tDW + "</span>)"
                    }
                },
                {
                    field: 'eDate', title: '报名截止日期', width: 110, align: 'center', sortable: true, sortName: "ExpiryDate", formatter: function (val, rec) {
                        return val + "(<span style='color:blue;'>" + rec.eDW + "</span>)"
                    }
                },
                {
                    field: 'DefaultPrice', title: '报名价格', width: 80, align: 'right', sortable: true, sortName: "DefaultPrice", formatter: function (val, rec) {
                        return "<a href='javascript:;' onmouseout='p.fnPriceOut()' onmouseover=\"p.fnPriceHover(this,'" + rec.ID + "')\">" + val + "</a>";
                    }
                },
                { field: 'PlanNum', title: '计划', width: 60, sortable: true, align: 'center', sortName: "PlanNum" },
                { field: 'VisitorNum', title: '已订', width: 60, sortable: true, align: 'center', sortName: "VisitorNum" },
                { field: 'SurplusNum', title: '剩余', width: 60, sortable: true, align: 'center', sortName: "SurplusNum" },
                {
                    field: 'Opt', title: "预订", width: 60, align: 'center', formatter: function (val, rec) {
                        if (parseInt(rec.SurplusNum) > 0) {
                            if (rec.TourStatus == "2") return "<span style='color:red;'>停售</span>";
                            else
                                return "<a href=\"ProBook.aspx?id=" + rec.ID + "\">预订</a>";
                        }

                    }
                }
            ]],
            singleSelect: true, //是否单选 
            pagination: true, //分页控件    
            pageSize: 20
        });

        $(window).resize(function () {
            $('#tblData').datagrid('resize');
        });
    },
    fnPriceHover: function (e, tourID) {
        var id = "PriceDetail";
        var obj = $(e).parent().parent();
        var top = $(obj).offset().top;
        var left = $(obj).offset().left;
        $("#" + id).removeClass("hide");
        $("#" + id).css({ "top": (top) + "px", "left": (left - 340) + "px" });
        p.fnLoadPriceDetail(tourID);
    },
    fnLoadPriceDetail: function (tourID) {
        var u = "Service/ProSearch.ashx?action=3&tourID=" + tourID;
        dataService.ajaxGet(u, function (data) {
            $("#tblPriceData").html(data);
        });
    },
    fnPriceOut: function () {
        $("#PriceDetail").addClass("hide");
    }
};

$(function () {
    p.init();
});