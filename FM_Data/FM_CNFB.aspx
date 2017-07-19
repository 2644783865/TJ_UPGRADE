<%@ Page Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master" AutoEventWireup="true" CodeBehind="FM_CNFB.aspx.cs" Inherits="ZCZJ_DPF.FM_Data.FM_CNFB" Title="无标题页" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="uc1" %>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
 <script src="../JS/StyleControl.js" type="text/javascript" charset="GB2312"></script>

    <script src="../JS/DatePicker.js" type="text/javascript" charset="GB2312"></script>

    <script src="../JS/FM_Cost.js" type="text/javascript" charset="GB2312"></script>

    <script type="text/javascript">
$(function(){
$("#Checkbox2").click(function(){
if($("#Checkbox2").attr("checked")){
 $("#table1 input[type=checkbox]").attr("checked","true");
}
else{
 $("#table1 input[type=checkbox]").removeAttr("checked");
}
});})//jquery的写法，先声明一个函数，然后捕捉触发事件的对象，触发该对象时执行的事件（函数），遍历某些特定的控件，判断对象是否触发，执行事件；
    </script>

    <div class="box-wrapper">
        <asp:Label ID="ControlFinder" runat="server" Visible="False"></asp:Label>
        <div class="box-outer">
            <div class="box_right">
                <div class="box-title">
                    <table width="100%">
                        <tr>
                            <td>
                                全选/取消<input id="Checkbox2" type="checkbox" />
                            </td>
                            <td style="width: 23%;" align="left">
                                <strong>时间：</strong>
                                <asp:DropDownList ID="dplYear" runat="server" AutoPostBack="false">
                                </asp:DropDownList>
                                &nbsp;年&nbsp;
                                <asp:DropDownList ID="dplMoth" runat="server" AutoPostBack="false">
                                </asp:DropDownList>
                                &nbsp;月&nbsp;
                            </td>
                            <td align="left">
                                <strong>任务号:</strong><asp:TextBox ID="txtrwh" runat="server"></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:Button ID="btnQuery" runat="server" Text="查询" OnClick="btnQuery_OnClick" />
                            </td>
                            <td align="right">
                                <asp:Button ID="btnexport" runat="server" Text="导出" OnClick="btnexport_Click"></asp:Button>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            </td>
                          </tr>  
                    </table>
                </div>
            </div>
        </div>
        <div  style="overflow: scroll;height: 400px;">
                <table id="table1" align="center" cellpadding="2" cellspacing="1" class="toptable grid nowrap"
                    border="1" width="100%" >
                    <asp:Repeater ID="rptProNumCost" runat="server" OnItemDataBound="rptProNumCost_OnItemDataBound">
                        <HeaderTemplate>
                            <tr align="center" class="tableTitle headcolor">
                                <th>
                                    序号
                                </th>
                                <th>
                                    项目名称
                                </th>
                                <th>
                                    合同号
                                </th>
                                <th>
                                    任务号
                                </th>
                                <th>
                                    图号
                                </th>
                                <th>
                                    设备名称
                                </th>
                                <%--<th>
                                    数量
                                </th>--%>
                                <th>
                                    本月明义结算金额（元）
                                </th>
                                <th>
                                    本月实际结算金额（元）
                                </th>
                                <th>
                                    年份
                                </th>
                                <th>
                                    月份
                                </th>
                                <th>
                                    班组
                                </th>
                            </tr>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr class="baseGadget" align="center" onmouseover="this.className='highlight'" onclick="javascript:change(this);"
                                ondblclick="javascript:changeback(this);" onmouseout="this.className='baseGadget'">
                                <asp:Label ID="lblID" runat="server" visible="false" Text='<%#Eval("ID")%>'></asp:Label>
                                <td>
                                    <%# Convert.ToInt32(Container.ItemIndex + 1)%>&nbsp;<asp:CheckBox ID="chkDel"
                                        runat="server" />    
                                </td>
                                <td align="center">
                                    <asp:Label ID="lbprojname" runat="server" Text='<%#Eval("CNFB_PROJNAME")%>'></asp:Label>
                                </td>
                                <td align="center">
                                    <asp:Label ID="lbprojid" runat="server" Text='<%#Eval("CNFB_HTID")%>'></asp:Label>
                                </td>
                                <td align="center">
                                    <asp:Label ID="lbrwh" runat="server" Text='<%#Eval("CNFB_TSAID")%>'></asp:Label>
                                </td>
                                <td align="center">
                                    <asp:Label ID="lbth" runat="server" Text='<%#Eval("CNFB_TH")%>'></asp:Label>
                                </td>
                                <td style="width:300px;white-space:normal" align="center">
                                    <asp:Label ID="lbsbname" runat="server" Text='<%#Eval("CNFB_SBNAME")%>'></asp:Label>
                                </td>
                                <%--<td align="center">
                                    <asp:Label ID="lbsl" runat="server" Text='<%#Eval("CNFB_NUM")%>'></asp:Label>
                                </td>--%>
                                <td align="center">
                                    <asp:Label ID="lbbymymoney" runat="server" align="center" Text='<%#Eval("CNFB_BYMYMONEY")%>'></asp:Label>
                                </td>
                                <td align="center">
                                    <asp:Label ID="lbbyrealmoney" runat="server" align="center" Text='<%#Eval("CNFB_BYREALMONEY")%>'></asp:Label>
                                </td>
                                <td align="center">
                                    <asp:Label ID="lbyear" runat="server" align="center" Text='<%#Eval("CNFB_YEAR")%>'></asp:Label>
                                </td>
                                
                                
                                <td align="center">
                                    <asp:Label ID="lbmonth" runat="server" align="center" Text='<%#Eval("CNFB_MONTH")%>'></asp:Label>
                                </td>
                                <td align="center">
                                    <asp:Label ID="lbtype" runat="server" align="center" Text='<%#Eval("CNFB_TYPE")%>'></asp:Label>
                                </td>
                            </tr>
                        </ItemTemplate>
                        <FooterTemplate>
                            <tr>
                                <td colspan="6" align="right">
                                    合计：
                                </td>
                                <td align="center">
                                    <asp:Label ID="lbmyjehj" runat="server"></asp:Label>
                                </td>
                                <td align="center">
                                    <asp:Label ID="lbsjjehj" runat="server"></asp:Label>
                                </td>
                            </tr>
                        </FooterTemplate>
                    </asp:Repeater>
                </table>
                </div>
                <asp:Panel ID="palNoData" runat="server" Visible="false" HorizontalAlign="Center">
                    没有记录!</asp:Panel>
            </div>
            <uc1:UCPaging ID="UCPaging1" runat="server" />
</asp:Content>
