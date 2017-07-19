<%@ Page Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master" AutoEventWireup="true"
    CodeBehind="CWCB_Hesuan.aspx.cs" Inherits="ZCZJ_DPF.FM_Data.CWCB_Hesuan" %>

<%@ Register Src="../Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="uc1" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="RightContentTitlePlace">
    成本核算
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
    <link href="StyleFile/superTables_compressed.css" rel="stylesheet" type="text/css" />

    <script src="StyleFile/superTables_compressed.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript">
$(function(){
$("#Checkbox2").click(function(){
if($("#Checkbox2").attr("checked")){
 $("#table1 input[type=checkbox]").attr("checked","true");
}
else{
 $("#table1 input[type=checkbox]").removeAttr("checked");
}
});})
    </script>

    <asp:Label ID="ControlFinder" runat="server" Visible="false"></asp:Label>
    <div class="box-inner">
        <div class="box_right">
            <div class="box-title" align="center">
                <table width="100%">
                    <tr>
                        <td>
                                全选/取消<input id="Checkbox2" type="checkbox" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            </td>
                        <td>
                            <asp:RadioButtonList ID="RadioButtonList1" runat="server" RepeatDirection="Horizontal"
                                AutoPostBack="true" OnSelectedIndexChanged="RadioButtonList1_SelectedIndexChanged">
                                <asp:ListItem Text="待核算" Value="0" Selected="True"></asp:ListItem>
                                <asp:ListItem Text="已核算" Value="1"></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                        <%--<td>
                            <asp:HyperLink ID="HyperLinkck" runat="server" NavigateUrl="~/FM_Data/CB_ProNum_Cost.aspx">查看</asp:HyperLink>
                        </td>--%>
                        <td align="center">
                            任务号:<asp:TextBox ID="txtrwh" runat="server"></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Button ID="btnQuery" runat="server" Text="查询" OnClick="btnQuery_OnClick" />
                        </td>
                        <td>
                            <asp:Button ID="btnHS" runat="server" Text="核算" OnClick="btnHS_Click" OnClientClick="return confirm('确定要核算吗？');" />
                        </td>
                        <td>
                            <asp:Button ID="btnFHS" Visible="false" runat="server" Text="反核算" OnClick="btnFHS_Click"
                                OnClientClick="return confirm('确定要反核算吗？');" />
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
    <div class="box-wrapper">
        <div class="box-outer">
            <div style=" overflow:scroll">
                <table id="table1" align="center" cellpadding="2" cellspacing="1" class="toptable grid nowrap"
                    border="1" width="100%">
                    <asp:Repeater ID="rptProNumCost" runat="server">
                        <HeaderTemplate>
                            <tr align="center">
                                <th>
                                    <strong>序号</strong>
                                </th>
                                <th>
                                    <strong>任务号</strong>
                                </th>
                                <th>
                                    <strong>项目名称</strong>
                                </th>
                                <th>
                                    <strong>设备名称</strong>
                                </th>
                                <th>
                                    <strong>当前状态</strong>
                                </th>
                                <th>
                                    <strong>核算时间</strong>
                                </th>
                            </tr>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr class="baseGadget" align="center">
                                <td>
                                    <%#Container.ItemIndex + 1%>&nbsp;&nbsp;<asp:CheckBox ID="checkbox" runat="server" />
                                </td>
                                <td>
                                    <asp:Label ID="lbrwh" runat="server" Text='<%#Eval("TASK_ID")%>'></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lbprojname" runat="server" Text='<%#Eval("PRJ")%>'></asp:Label>
                                </td>
                                <td style="width:500px;white-space:normal" align="center">
                                    <asp:Label ID="lbcpname" runat="server" Text='<%#Eval("ENG")%>'></asp:Label>
                                </td>
                                <td>
                                    <%#Eval("CWCB_STATE").ToString() == "0"? "待核算" : "已核算"%>
                                </td>
                                <td>
                                    <%#Eval("CWCB_HSDATE")%>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </table>
                <asp:Panel ID="NoDataPanel" HorizontalAlign="Center" runat="server" ForeColor="Red"
                    Visible="false">
                    没有记录!</asp:Panel>
                <uc1:UCPaging ID="UCPaging1" runat="server" />
            </div>
        </div>
    </div>
</asp:Content>