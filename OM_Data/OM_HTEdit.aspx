<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OM_HTEdit.aspx.cs" Inherits="ZCZJ_DPF.OM_Data.OM_HTEdit" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>合同信息</title>
    <base target="_self" />

    <script src="../JS/DatePicker.js" type="text/javascript"></script>

    <link href="../Assets/main.css" rel="stylesheet" type="text/css" />

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
            var txt = document.getElementById('TabContainer1_Tab_txt_zdrYJ');
            if (txt.value == "") {
                alert('没有填写制单人意见！');
                return false;
            }
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
        </asp:ToolkitScriptManager>
        <asp:TabContainer ID="TabContainer1" runat="server" Width="100%" TabStripPlacement="Top"
            ActiveTabIndex="0">
            <asp:TabPanel ID="Tab" runat="server" TabIndex="0" HeaderText="评审信息">
                <HeaderTemplate>
                    评审信息
                </HeaderTemplate>
                <ContentTemplate>
                    <table cellpadding="4" cellspacing="1" style="background: #EEF7FD; white-space: nowrap;
                        width: 480px;" class="toptable grid" border="1">
                        <tr>
                            <td width="100px">
                                姓名：
                            </td>
                            <td>
                                <asp:Label ID="ST_NAME" runat="server"></asp:Label>
                            </td>
                            <td width="100px">
                                部门：
                            </td>
                            <td>
                                <asp:Label ID="DEP_NAME" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                职位：
                            </td>
                            <td>
                                <asp:Label ID="DEP_POSITION" runat="server"></asp:Label>
                            </td>
                            <td>
                                电话：
                            </td>
                            <td>
                                <asp:Label ID="ST_TELE" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                合同起始时间：
                            </td>
                            <td>
                                <asp:TextBox ID="ST_CONTRSTART" runat="server" onclick="setday(this)" 
                                    Width="120px"></asp:TextBox>
                            </td>
                            <td>
                                合同终止时间：
                            </td>
                            <td>
                                <asp:TextBox ID="ST_CONTREND" runat="server" onclick="setday(this)" 
                                    Width="120px"></asp:TextBox>
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
                                <asp:TextBox ID="txt_zdrYJ" runat="server" Height="40px" TextMode="MultiLine" 
                                    Width="90%"></asp:TextBox>
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
                </ContentTemplate>
            </asp:TabPanel>
            <asp:TabPanel ID="Tab1" runat="server" TabIndex="1" HeaderText="合同历史">
                <ContentTemplate>
                    <div style="text-align: center">
                        <strong>合同历史</strong></div>
                    <br />
                    <table cellpadding="4" cellspacing="1" class="toptable grid" border="1">
                        <asp:Repeater ID="rep_HT" runat="server">
                            <HeaderTemplate>
                                <tr align="center" class="tableTitle headcolor">
                                    <td>
                                        序号
                                    </td>
                                    <td>
                                        起始时间
                                    </td>
                                    <td>
                                        终止时间
                                    </td>
                                </tr>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr class='baseGadget'>
                                    <td>
                                        <%# Convert.ToInt32(Container.ItemIndex +1) %>
                                    </td>
                                    <td>
                                        <%#Eval("ST_START") %>
                                    </td>
                                    <td>
                                        <%#Eval("ST_END") %>
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </table>
                    <asp:Panel ID="NoDataPane1" runat="server" HorizontalAlign="Center" ForeColor="Red">
                        没有记录!</asp:Panel>
                </ContentTemplate>
            </asp:TabPanel>
        </asp:TabContainer>
    </div>
    </form>
</body>
</html>
