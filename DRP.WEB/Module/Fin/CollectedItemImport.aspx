<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CollectedItemImport.aspx.cs" Inherits="DRP.WEB.Module.Fin.CollectedItemImport" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>银行收款明细导入</title>
    <script type="text/javascript">
        function fnValid() {
            var file = $("#FileCollected").val();
            if (file == "") {
                Alert("请选择文件");
                return false;
            }
            return confirm("确定要导入吗");
        }

        function fnFinish(rec) {
            alert("成功导入 " + rec + " 条记录");
            closeTab("", "银行收款明细导入");
        }

    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div style="padding: 10px; margin-top: 10px;" class="line">
            一、 Excel导入要求
        </div>
        <div style="padding: 0px 40px; line-height: 26px;">
            <ul>
                <li>1、只能从Excel 2007及以上版本中导入；</li>
                <li>2、导入收款明细资料属性分别为：
                    <table class="tblEdit">
                        <tr>
                            <th>交易日期</th>
                            <th>交易时间</th>
                            <th>收入金额</th>
                            <th>摘要</th>
                            <th>交易行名</th>
                            <th>对方户名</th>
                        </tr>
                    </table>
                </li>
                <li>3、可以批量导入多个【Sheet】表，银行名称为Excel工作薄Sheet名称；
                </li>
                <li>4、表格模板下载【<a href='Resource/T.xlsx' target="_blank">下载</a>】
                </li>
            </ul>
        </div>
        <div style="padding: 10px; margin-top: 20px;" class="line">
            二、 导入收款明细资料
        </div>
        <div style="line-height: 26px; padding-left: 40px;">
            <asp:FileUpload ID="FileCollected" runat="server" ClientIDMode="Static" Width="300" />
            <asp:LinkButton ID="btnImport" OnClientClick="return fnValid()" OnClick="btnImport_Click" ClientIDMode="Static" CssClass="easyui-linkbutton" iconcls="icon-import" runat="server">立即导入</asp:LinkButton>
            <div style="padding-top: 10px; color: red;">
                注：导入过程可能需要一定的时间，请勿重复导入！
            </div>
        </div>
    </form>
</body>
</html>
