<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="QC_Internal_Audit_Edit.aspx.cs"
    Inherits="ZCZJ_DPF.QC_Data.QC_Internal_Audit_Edit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <base target="_self" />
    <title>无标题页</title>
    <link href="../Assets/main.css" rel="Stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server" enctype="multipart/form-data" method="post">
    <asp:HiddenField ID="HiddenFieldContent" runat="server" />
    <div class="box-wrapper">
        <div class="box-outer">
            <table width="100%" align="center" cellpadding="4" cellspacing="1" class="toptable grid"
                border="1">
                <tr>
                    <td align="right">
                        <strong>名称</strong>
                    </td>
                    <td>
                        <asp:TextBox ID="TxtName" runat="server"></asp:TextBox>
                    </td>
                    <td align="right">
                        <strong>时间</strong>
                    </td>
                    <td>
                        <asp:TextBox ID="TxtTime" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <strong>上传附件</strong>
                    </td>
                    <td colspan="3">
                        <asp:FileUpload ID="FileUpload1" runat="server" Style="width: 150px" />
                        <asp:Button ID="ButUpload" runat="server" Text="上传附件" CausesValidation="false" OnClick="ButUpload_Click" />
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <strong>附件名称:</strong>
                    </td>
                    <td id="trFileName" colspan="3">
                        <asp:Label runat="server" ID="filename" Text=""></asp:Label>
                          <asp:Label ID="filesError" runat="server" ForeColor="Red" Visible="False" EnableViewState="false"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <strong>备注:</strong>
                    </td>
                    <td colspan="3">
                        <asp:TextBox ID="txtBZ" runat="server" TextMode="MultiLine" Width="90%" Height="50"></asp:TextBox>
                    </td>
                </tr>
                <asp:HiddenField ID="HiddenFieldType" runat="server" />
                <tr>
                    <td colspan="4" align="center">
                        <asp:Button ID="ButCon" runat="server" Text="保 存" Width="40px" OnClick="ButCon_Click" />
                        <input id="btnCancel" type="button" value="关 闭" title="不保存数据,直接关闭窗口" onclick="window.close();" />
                    </td>
                </tr>
            </table>
            <input type="hidden" id="method" runat="server" />
        </div>
    </div>
    </form>
</body>
</html>
