<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="QC_TargetAnalyze_Upload.aspx.cs"
    Inherits="ZCZJ_DPF.QC_Data.QC_TargetAnalyze_Upload" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <base target="_self" />
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <title>�����޸�</title>
    <link href="../Assets/main.css" rel="Stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server" enctype="multipart/form-data" method="post">
    <div class="box-wrapper">
        <div class="box-outer">
            <table width="100%" align="center" cellpadding="4" cellspacing="1" class="toptable grid"
                border="1">
                <tr>
                    <td align="center" style="width: 50px">
                        <strong>����:</strong>
                    </td>
                    <td colspan="3">
                        <asp:TextBox ID="txtName" runat="server" Width="370px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="center" style="width: 50px">
                        <strong>��ע:</strong>
                    </td>
                    <td colspan="3">
                        <asp:TextBox ID="txtBZ" runat="server" TextMode="MultiLine" Width="370px" Height="50"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="4" align="center">
                        <asp:Button ID="btnSave" runat="server" Text="�� ��" OnClick="btnSave_OnClick" OnClientClick="return confirm('ȷ���ύ��');" />&nbsp;&nbsp;&nbsp;&nbsp;
                        <input id="btnCancel" type="button" value="�� ��" title="����������,ֱ�ӹرմ���" onclick="window.close();" />
                    </td>
                </tr>
            </table>
            <input type="hidden" id="method" runat="server" />
            <input type="hidden" id="hidTargetId" runat="server" />
        </div>
    </div>
    </form>
</body>
</html>
