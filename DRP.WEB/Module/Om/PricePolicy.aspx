<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PricePolicy.aspx.cs" Inherits="DRP.WEB.Module.Om.PricePolicy" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <style type="text/css">
        #tblData input{height:25px; line-height:25px;padding:2px 4px;}
    </style>
    
    <script src="Script/PricePolicy.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="easyui-panel" title="产品价格策略设置" iconcls="icon-reload" style="padding: 10px;">
            <table class="tblEdit">
                <tr>
                    <th style="width: 40px;">序
                    </th>
                    <th style="width: 250px;">策略名称
                    </th>
                      <th style="width: 90px;">用户数
                    </th>
                    <th style="width: 90px;">产品价格
                    </th>
                    <th>备注
                    </th>
                    <th style="width: 40px;"></th>
                </tr>
                <tbody id="tblData"></tbody>
                <tr>
                    <td colspan="6" style="text-align: right;">
                     <span style="float:left;color:red;">备注：所有的价格以年为单位计算，即XXX元/年</span>   <a href="javascript:;" id="btnAdd">增加策略</a>
                    </td>
                </tr>
            </table>
            <div style="padding: 10px 0px; text-align: center;">

                <a id="btnSave" class="easyui-linkbutton" iconCls="icon-save">保存</a>
            </div>
        </div>
    </form>
</body>
</html>
