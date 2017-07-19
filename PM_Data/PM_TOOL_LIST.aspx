<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Masters/RightCotentMaster.Master" CodeBehind="PM_TOOL_LIST.aspx.cs" Inherits="ZCZJ_DPF.PM_Data.PM_TOOL_LIST" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    刀具库存查询&nbsp;&nbsp;&nbsp;&nbsp;
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
                                <td align="left">
                                    <label>刀具类别：</label>
                                    <asp:DropDownList ID="ddltooltype" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddltooltype_SelectedIndexChanged">
                                    <asp:ListItem Text="全部" Value="%" Selected="True"></asp:ListItem>
                                    <asp:ListItem Text="焊接车刀" Value="焊接车刀"></asp:ListItem>
                                    <asp:ListItem Text="机加工刀片" Value="机加工刀片"></asp:ListItem>
                                    <asp:ListItem Text="钻头" Value="钻头"></asp:ListItem>
                                    <asp:ListItem Text="丝锥" Value="丝锥"></asp:ListItem>
                                    <asp:ListItem Text="板牙" Value="板牙"></asp:ListItem>
                                    <asp:ListItem Text="铣刀" Value="铣刀"></asp:ListItem>
                                    <asp:ListItem Text="绞刀" Value="绞刀"></asp:ListItem>
                                    <asp:ListItem Text="库存工具" Value="库存工具"></asp:ListItem>
                                    <asp:ListItem Text="堆焊配件" Value="堆焊配件"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td align="center">
                                    <asp:Button ID="btnPush" runat="server" Text="下推" OnClick="btnPush_OnClick" OnClientClick="return PushConfirm()" Visible="false" />
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
                                        <strong>刀具类别</strong>
                                    </td>
                                    <td>
                                        <strong>名称</strong>
                                    </td>
                                    <td>
                                        <strong>规格型号</strong>
                                    </td>
                                    <td>
                                        <strong>数量</strong>
                                    </td>
                                    <td>
                                        <strong>单位</strong>
                                    </td>
                                    <td>
                                        <strong>备注</strong>
                                    </td>
                                </tr>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr class="baseGadget" onmouseover="this.className='highlight'" onmouseout="this.className='baseGadget'">
                                    <td>
                                        <asp:CheckBox ID="CheckBox1" runat="server" CssClass="checkBoxCss" /><%#Container.ItemIndex+1%>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblType" runat="server" Text='<%#Eval("TYPE")%>'></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblName" runat="server" Text='<%#Eval("NAME")%>'></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblModel" runat="server" Text='<%#Eval("MODEL")%>'></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblNumber" runat="server" Text='<%#Eval("NUMBER")%>'></asp:Label>
                                    </td>
                                     <td>
                                        <asp:Label ID="lblUnit" runat="server" Text='<%#Eval("UNIT")%>'></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblNote" runat="server" Text='<%#Eval("NOTE")%>'></asp:Label>
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </table>
                    <asp:Panel ID="NoDataPanel" runat="server">
                        没有相关刀具信息!</asp:Panel>
                    <uc1:UCPaging ID="UCPaging1" runat="server" />
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>