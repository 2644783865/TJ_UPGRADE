<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Masters/RightCotentMaster.Master"
    CodeBehind="EQU_Part_Store.aspx.cs" Inherits="ZCZJ_DPF.ESM_Data.EQU_Part_Store"
%>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    设备备件库存查询&nbsp;&nbsp;&nbsp;&nbsp;
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <link href="../Assets/main.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" language="javascript">
        function PushConfirm()
        {
            var retVal=confirm("确定将所选定项目下推生成出库单？");
            return retVal;
        }
    </script>
    
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="box-inner">
                <div class="box_right">
                    <div class="box-title">
                        <table width="100%">
                            <tr>
                                <td align="right">
                                    名称：
                                    <asp:TextBox ID="txtName" runat="server" Text=""></asp:TextBox>&nbsp;&nbsp;&nbsp;
                                </td>
                                <td align="left">
                                    <asp:Button ID="btnQuery" runat="server" Text="查  询" OnClick="btnQuery_OnClick" />&nbsp;&nbsp;&nbsp;
                                    <asp:Button ID="btnReset" runat="server" Text="重  置" OnClick="btnReset_OnClick" />
                                </td>
                                <td align="center">
                                    <asp:Button ID="btnPush" runat="server" Text="下推" OnClick="btnPush_OnClick" OnClientClick="return PushConfirm()"
                                        Visible="false" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>
            <div class="box-wrapper">
                <div class="box-outer">
                    <table width="100%" align="center" cellpadding="4" cellspacing="1" class="toptable grid"
                        border="1">
                        <asp:Repeater ID="Repeater1" runat="server">
                            <HeaderTemplate>
                                <tr align="center" class="tableTitle">
                                    <td>
                                        <strong>序号</strong>
                                    </td>
                                    <td>
                                        <strong>备件名称</strong>
                                    </td>
                                    <td>
                                        <strong>规格型号</strong>
                                    </td>
                                    <td>
                                        <strong>存货数量</strong>
                                    </td>
                                </tr>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr class="baseGadget" onmouseover="this.className='highlight'" onmouseout="this.className='baseGadget'">
                                    <td>
                                        <asp:CheckBox ID="CheckBox1" runat="server" CssClass="checkBoxCss" /><%#Container.ItemIndex + Convert.ToInt32(Eval("RowIndex"))%>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblName" runat="server" Text='<%#Eval("ParName")%>'></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblType" runat="server" Text='<%#Eval("ParType")%>'></asp:Label>
                                    </td>
                                    <td>
                                        <%#Eval("ParNumSto")%>
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </table>
                    <asp:Panel ID="NoDataPanel" runat="server">
                        没有相关物料信息!</asp:Panel>
                    <uc1:UCPaging ID="UCPaging1" runat="server" />
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnQuery" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="btnReset" EventName="Click" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
