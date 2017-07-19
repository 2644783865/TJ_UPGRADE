<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OM_FlowEdit.aspx.cs" Inherits="ZCZJ_DPF.OM_Data.OM_FlowEdit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>转正处理</title>
    <base target="_self" />

    <script src="../JS/DatePicker.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript">
        function CheckBoxList_Click(sender) {
            var container = sender.parentNode;
            if (container.tagName.toUpperCase() == "TD")
            // 服务器控件设置呈现为 table 布局（默认设置），否则使用流布局即为td
            {
                container = container.parentNode.parentNode; // 层次： <table><tr><td><input />
            }
            var chkList = container.getElementsByTagName("input");
            var senderState = sender.checked;
            for (var i = 0; i < chkList.length; i++) {
                chkList[i].checked = false;
            }
            sender.checked = senderState;
        }

        function Check() {
            var txt = document.getElementById('<%=txt_zdrYJ.ClientID%>');
            if (txt.value == "") {
                alert('没有填写制单人意见！');
                return false;
            }
        }
    </script>

    <link href="../Assets/main.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table cellpadding="4" cellspacing="1" style="background: #EEF7FD; white-space: nowrap;
            width: 500px;" class="toptable grid" border="1">
            <tr align="center">
                <td width="100px">
                    姓名：
                </td>
                <td>
                    <asp:Label ID="ST_NAME" runat="server" Width="150px"></asp:Label>
                </td>
                <td width="100px">
                    工号：
                </td>
                <td>
                    <asp:Label ID="ST_WORKNO" runat="server" Width="150px"></asp:Label>
                </td>
            </tr>
            <tr align="center">
                <td>
                    职位：
                </td>
                <td>
                    <asp:Label ID="DEP_POSITION" runat="server" Width="150px"></asp:Label>
                </td>
                <td>
                    电话：
                </td>
                <td>
                    <asp:Label ID="ST_TELE" runat="server" Width="150px"></asp:Label>
                </td>
            </tr>
            <tr align="center">
                <td>
                    转出部门：
                </td>
                <td>
                    <asp:Label ID="DEP_NAME" runat="server" Width="150px"></asp:Label>
                    <asp:Label ID="DEP_ID" runat="server" Visible="false"></asp:Label>
                </td>
                <td>
                    转入部门：
                </td>
                <td>
                    <asp:DropDownList ID="DEP_ZRU" runat="server" Width="100px">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td align="center">
                    备注
                </td>
                <td colspan="3">
                    <asp:TextBox ID="ST_REMARK" runat="server" Width="300px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    制单人意见：
                </td>
                <td class="category" colspan="3">
                    <asp:TextBox ID="txt_zdrYJ" runat="server" TextMode="MultiLine" Width="90%" Height="40px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    指定评审人员：
                </td>
                <td class="category" colspan="3">
                    <asp:Panel ID="Panel1" runat="server" EnableViewState="False">
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td align="right">
                    *注意事项
                </td>
                <td colspan="3">
                    1.请<span style="color: Red">不要全选！！！</span>仅选择需要审批的部门，全选将导致审批时间延长;<br />
                    2.同一部门只能选择一个人，多选无效;<br />
                </td>
            </tr>
        </table>
        <br />
        <div style="text-align: center">
            <asp:Button ID="btnConfirm" runat="server" Text="确 定" OnClick="btnConfirm_Click"
                OnClientClick="return Check();" />&nbsp;&nbsp;&nbsp;&nbsp;
            <input type="button" value="关 闭" onclick="javascript:window.close();" />
        </div>
    </div>
    </form>
</body>
</html>
