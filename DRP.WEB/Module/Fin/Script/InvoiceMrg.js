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
    url: "Service/InvoiceMrg.ashx",
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
                        {
                            field: 'IsOverAmt', title: '超额',align:'center', width: 40, sortable: true, sortName: "IsOverAmt", formatter: function (val, rec) {
                                if (val == "True") {
                                    return "<span title='超额开票' style='color:red;'>√</span>";
                                }
                            }
                        },
                        { field: 'CreateUserName', title: '申请人', sortable: true, sortName: "CreateUserName", width: 70, align: 'center' },
                        { field: 'CreateDate', title: '申请日期', sortable: true, sortName: "CreateDate", width: 70, align: 'center' },
                        {
                            field: 'InvoiceName', title: "发票抬头", width: 170, sortable: true, sortName: "InvoiceName", formatter: function (val, rec) {
                                return "<a href='javascript:;' title='查看发票' onclick=\"o.fnViewInvoice('" + rec.ID + "')\">" + val + "</a>";
                            }
                        }

            ]],
            columns: [[
                        { field: 'InvoiceItem', title: "开票内容", width: 110, sortable: true, sortName: "InvoiceItem" },
                        { field: 'InvoiceAmt', title: '开票金额', sortable: true, sortName: "InvoiceAmt", width: 70, align: 'right' },
                        { field: 'OrderName', title: '订单名称', width: 150, sortable: true, sortName: "OrderName" },
                        { field: 'OrderNum', title: '订单数', width: 60, align: 'center', sortable: true, sortName: "OrderNum" },
                        { field: 'FetchType', title: '领取方式', width: 100, sortable: true, sortName: "FetchType" },
                        { field: 'InvoiceNo', title: "发票编号", width: 60, sortable: true, sortName: "InvoiceNo" },
                        {
                            field: 'InvoiceStatus', title: '发票状态', sortable: true, sortName: "InvoiceStatus", width: 80, align: 'center', formatter: function (val, rec) {
                                var s = "";
                                switch (val) {
                                    case "1": s = "未开票"; break;
                                    case "2": s = "<span style='color:blue;'>已开票</span>"; break;
                                    case "3": s = "<span style='color:red;'>已退回</span>"; break;
                                    case "4": s = "已作废"; break;
                                }
                                return s;
                            }
                        },
                        {
                            field: 'Opt', title: '操作', width: 110, align: 'center', formatter: function (val, rec) {
                                var view = "<a href='javascript:;' title='查看发票' onclick=\"o.fnViewInvoice('" + rec.ID + "')\">查看</a>";
                                var invoice = "<a href='javascript:;' title='开票' onclick=\"o.fnCreateInvoice('" + rec.ID + "')\">开票</a>";
                                var deny = "<a href='javascript:;' title='退回开票申请' onclick=\"o.fnInvoiceOp('" + rec.ID + "','1')\">退回</a>";
                                var cancel = "<a href='javascript:;' title='发票作废' onclick=\"o.fnInvoiceOp('" + rec.ID + "','2')\">作废</a>";
                                var sb = [];
                                sb.push(view);
                                switch (rec.InvoiceStatus) {
                                    case "1":
                                        sb.push(invoice);
                                        sb.push(deny);
                                        break;
                                    case "2":
                                        sb.push(cancel);
                                        break;
                                }
                                return sb.join(" | ");
                            }
                        }

            ]],
            rowStyler: function (rowIndex, rec) {
                if (rec.InvoiceStatus == "4")
                    return "color:red;";
            },
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
        var status = $("#ddlInvoiceStatus option:selected").val();
        var name = $("#txtInvoiceName").val();
        var no = $("#txtInvoiceNo").val();
        var sDate = $("#sDate").val();
        var eDate = $("#eDate").val();
        $('#tblData').datagrid("reload", { "Status": status, "InvoiceName": name, "InvoiceNo": no, "sDate": sDate, "eDate": eDate });
    },
    fnViewInvoice: function (id) { //查看发票信息
        var u = "/Module/Order/InvoiceInfo.aspx?id=" + id;
        addTab("发票信息", u, function () { });
    },
    fnCreateInvoice: function (id) { //开票
        var u = "/Module/Fin/CreateInvoice.aspx?id=" + id;
        return openWindow({ "title": "开票", "width": "750", "height": "450", "url": u }, function () {
            $('#tblData').datagrid("reload");
        });
    },
    fnInvoiceOp: function (id, action) { //退回或作废
        var u = "/Module/Fin/InvoiceOp.aspx?id=" + id + "&action=" + action;
        var title = action == "1" ? "退回开票申请" : "发票作废";
        return openWindow({ "title": title, "width": "750", "height": "400", "url": u }, function () {
            $('#tblData').datagrid("reload");
        });
    }
};