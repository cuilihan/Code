$(function () {
    o.init();
});

var o = {
    init: function () {
        o.bindData();
        $("#btnQuery").click(function () {
            o.fnQuery();
        });
    },
    url: "Service/Payable.ashx",
    bindData: function () {
        $('#tblData').datagrid({
            loadMsg: "正在加载数据...",
            nowrap: true,
            striped: true,
            border: false,
            height: document.documentElement.clientHeight - 35,
            collapsible: false, //是否可折叠的       
            url: o.url + "?action=1",
            idField: 'ID',
            frozenColumns: [[
                        { field: "ID", checkbox: true },
                        {
                            field: 'xType', title: '供应商类型', sortable: true, sortName: "xType", width: 90, align: 'center', formatter: function (val, rec) {
                                var s = "";
                                switch (val) {
                                    case "1": s = "供应商"; break;
                                    case "2": s = "景点门票"; break;
                                    case "3": s = "导游"; break;
                                    case "4": s = "酒店"; break;
                                    case "5": s = "车队"; break;
                                    case "6": s = "签证机构"; break;
                                    case "7": s = "保险公司"; break;
                                    case "8": s = "购物店"; break;
                                    case "9": s = "票务机构"; break;
                                    case "10": s = "其他供应商"; break;
                                }
                                return s;
                            }
                        },
                        {
                            field: 'Name', title: '供应商名称', width: 300, sortable: true, sortName: "Name", formatter: function (val, rec) {
                                return "【" + rec.Spell + "】" + "<a href='javascript:;' onclick=\"o.fnItemView('" + rec.Name + "','" + rec.SupplierID + "','" + rec.xType + "')\">" + val + "</a>";
                            }
                        }
            ]],
            columns: [[
                        {
                            field: 'CostAmt', title: "应付款", width: 100, sortable: true, sortName: "CostAmt", align: 'right', formatter: function (val, rec) {
                                return "<a href='javascript:;' title='查看应付款明细' onclick=\"o.fnPayableItem('" + rec.SupplierID + "','" + rec.Name + "')\">" + val + "</a>";
                            }
                        },
                        {
                            field: 'PaidAmt', title: '已付款', sortable: true, sortName: "PaidAmt", width: 100, align: 'right', formatter: function (val, rec) {
                                return "<a href='javascript:;' title='查看已付款明细' onclick=\"o.fnPaidItem('" + rec.SupplierID + "','" + rec.Name + "')\">" + val + "</a>";
                            }
                        },
                        { field: 'UnPayAmt', title: '未付款', width: 100, sortable: true, sortName: "UnPayAmt", align: 'right' },
                        {
                            field: 'Opt', title: '操作', width: 70, align: 'center', formatter: function (val, rec) {
                                return "<a href='javascript:;'  onclick=\"o.fnToPay('" + rec.SupplierID + "','" + rec.Name + "')\">付款</a>";
                            }
                        }

            ]],
            singleSelect: true, //是否单选 
            pagination: true, //分页控件  
            rownumbers: true, //行号  
            pageSize: 20,
            toolbar: "#toolbar"
        });

        $(window).resize(function () {
            $('#tblData').datagrid('resize');
        });
    },
    fnQuery: function () {
        var xType = $("#ddlType option:selected").val();
        var name = $("#txtName").val();
        var amt = $("#txtPayAmt").val();
        $('#tblData').datagrid("reload", { "xType": xType, "Name": name, "PayAmt": amt });
    },
    fnItemView: function (name, id, xType) { //查看供应商
        var s = "";
        switch (xType) {
            case "1": s = "TravelAgencyInfo.aspx"; break;
            case "2": s = "ScenicSpotInfo.aspx"; break;
            case "3": s = "GuideInfo.aspx"; break;
            case "4": s = "HotelInfo.aspx"; break;
            case "5": s = "MotorcadeInfo.aspx"; break;
            case "6": s = "VisaInfo.aspx"; break;
            case "7": s = "InsurerInfo.aspx"; break;
            case "8": s = "ShoppingInfo.aspx"; break;
            case "9": s = "TicketInfo.aspx"; break;
            case "10": s = "OtherResInfo.aspx"; break;
        }
        var u = "/Module/Res/" + s + "?id=" + id;
        addTab(name, u, function () { });
    },
    fnToPay: function (supplierID, name) { //付款
        var u = "PayableAction.aspx?id=" + supplierID + "&name=" + escape(name);
        addTab(name + "-付款", u, function () {
            o.fnQuery();
        });
    },
    fnPayableItem: function (supplierID, supplierName) { //应付款明细
        var u = "PayableItem.aspx?id=" + supplierID;
        addTab(supplierName + "-应付款", u, function () { });
    },
    fnPaidItem: function (supplierID, supplierName) { //已付款明细
        var u = "PaidItem.aspx?id=" + supplierID;
        addTab(supplierName + "-已付款", u, function () { });
    }
};