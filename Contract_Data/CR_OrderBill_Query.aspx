<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CR_OrderBill_Query.aspx.cs"
    MasterPageFile="~/Masters/RightCotentMaster.Master" Inherits="ZCZJ_DPF.Contract_Data.CR_OrderBill_Query" %>

<%@ Register Src="../Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="RightContentTitlePlace">
</asp:Content>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="PrimaryContent">
    <style type="text/css">
        .autocomplete_completionListElement
        {
            margin: 0px;
            background-color: #1C86EE;
            color: windowtext;
            cursor: 'default';
            text-align: left;
            list-style: none;
            padding: 0px;
            border: solid 1px gray;
            width: 400px !important;
        }
        .autocomplete_listItem
        {
            border-style: solid;
            border: #FFEFDB;
            border-width: 1px;
            background-color: #EEDC82;
            color: windowtext;
        }
        .autocomplete_highlightedListItem
        {
            background-color: #1C86EE;
            color: black;
            padding: 1px;
        }
    </style>
    <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnableScriptGlobalization="true"
        EnableScriptLocalization="true">
    </cc1:ToolkitScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="RightContent">
                <div class="box-wrapper">
                    <div style="height: 8px" class="box_top">
                    </div>
                    <div class="box-outer">
                        <table width="100%">
                            <tr>
                                <td>
                                    <asp:Button ID="btn_search" runat="server" Text="查 询" OnClick="btn_search_Click" />
                                    &nbsp;&nbsp;<asp:Button ID="btn_reset" runat="server" Text="重 置" OnClick="btn_Reset_Click" />
                                    &nbsp;|&nbsp;<asp:Button ID="btn_ViewDDBill" runat="server" Text="查 看" OnClick="btn_ViewDDBill_Click" />
                                    &nbsp;|&nbsp;<asp:Button ID="btn_EditDDBill" runat="server" Text="编 辑" OnClick="btn_EditDDBill_Click" />
                                    &nbsp;|&nbsp;<asp:Button ID="btn_DelDDBill" runat="server" Text="删 除" OnClick="btn_DelDDBill_Click"
                                        OnClientClick="javascript:return confirm('确定删除吗？')" />
                                    &nbsp;|&nbsp;<asp:Button ID="btn_ViewDD" runat="server" Text="查看订单" OnClick="btn_ViewDD_Click" />
                                </td>
                            </tr>
                        </table>
                        <asp:Panel ID="Pal_Query" runat="server">
                            <table width="100%">
                                <tr>
                                    <td style="width: 35%">
                                        记录编号：<asp:TextBox ID="txt_JLID" runat="server" Width="200px"></asp:TextBox>
                                    </td>
                                    <td style="width: 35%">
                                        采购订单：<asp:TextBox ID="txt_DDCode" runat="server" Width="200px"></asp:TextBox>
                                    </td>
                                    <td style="width: 30%">
                                        经 办 人：<asp:DropDownList ID="dplJBR" runat="server" AutoPostBack="true" OnSelectedIndexChanged="btn_search_Click">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        发票单号：<asp:TextBox ID="txt_BillCode" runat="server" Width="200px"></asp:TextBox>
                                    </td>
                                    <td>
                                        开票单位：<asp:TextBox ID="txt_KPDW" runat="server" Width="200px"></asp:TextBox>
                                        <cc1:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" TargetControlID="txt_KPDW"
                                            ServicePath="~/Ajax.asmx" CompletionSetCount="10" MinimumPrefixLength="1" CompletionInterval="10"
                                            ServiceMethod="GetCompleteProvider" FirstRowSelected="true" CompletionListCssClass="autocomplete_completionListElement"
                                            CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem">
                                        </cc1:AutoCompleteExtender>
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        开票时间：<asp:TextBox ID="sta_KPtime" runat="server" Width="90px"></asp:TextBox>
                                        &nbsp;至&nbsp;<asp:TextBox ID="end_KPtime" runat="server" Width="90px"></asp:TextBox>
                                        <cc1:CalendarExtender ID="calender_KPsta" runat="server" Format="yyyy-MM-dd" DaysModeTitleFormat="MM月,yyyy年"
                                            TodaysDateFormat="yyyy年MM月dd日" TargetControlID="sta_KPtime">
                                        </cc1:CalendarExtender>
                                        <cc1:CalendarExtender ID="calender_KPend" runat="server" Format="yyyy-MM-dd" DaysModeTitleFormat="MM月,yyyy年"
                                            TodaysDateFormat="yyyy年MM月dd日" TargetControlID="end_KPtime">
                                        </cc1:CalendarExtender>
                                    </td>
                                    <td>
                                        收票时间：<asp:TextBox ID="sta_SPtime" runat="server" Width="90px"></asp:TextBox>
                                        &nbsp;至&nbsp;<asp:TextBox ID="end_SPtime" runat="server" Width="90px"></asp:TextBox>
                                        <cc1:CalendarExtender ID="calender_SPsta" runat="server" Format="yyyy-MM-dd" DaysModeTitleFormat="MM月,yyyy年"
                                            TodaysDateFormat="yyyy年MM月dd日" TargetControlID="sta_SPtime">
                                        </cc1:CalendarExtender>
                                        <cc1:CalendarExtender ID="calender_SPend" runat="server" Format="yyyy-MM-dd" DaysModeTitleFormat="MM月,yyyy年"
                                            TodaysDateFormat="yyyy年MM月dd日" TargetControlID="end_SPtime">
                                        </cc1:CalendarExtender>
                                    </td>
                                    <td>
                                        每页显示：<asp:DropDownList ID="dpl_pageno" runat="server" AutoPostBack="true" OnSelectedIndexChanged="btn_search_Click">
                                            <asp:ListItem Text="10" Value="10"></asp:ListItem>
                                            <asp:ListItem Text="12" Value="12"></asp:ListItem>
                                            <asp:ListItem Text="15" Value="15"></asp:ListItem>
                                            <asp:ListItem Text="20" Value="20"></asp:ListItem>
                                        </asp:DropDownList>
                                        &nbsp;条记录
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                        <asp:Panel ID="PanelDDBill" runat="server" Style="width: 100%; height: auto; overflow: auto;">
                            <asp:GridView ID="grvDDBill" Width="100%" CssClass="toptable grid" runat="server"
                                AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" ShowFooter="true"
                                OnRowDataBound="grvDDBill_RowDataBound">
                                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                <RowStyle BackColor="#ffffff" />
                                <HeaderStyle BackColor="#A8B7EC" Font-Bold="True" ForeColor="White" />
                                <FooterStyle BackColor="#FFFFFF" Font-Bold="True" ForeColor="Blue" HorizontalAlign="Center" />
                                <Columns>
                                    <asp:TemplateField HeaderText="序号" ItemStyle-HorizontalAlign="Center" HeaderStyle-Wrap="false"
                                        ItemStyle-Wrap="false">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="CheckBox1" runat="server" />
                                            <asp:Label ID="lbl_id" runat="server" Text=""></asp:Label>
                                            <asp:Label ID="lbl_obcode" runat="server" Text='<%#Eval("OB_CODE")%>' Visible="false"></asp:Label>
                                            <asp:Label ID="lbl_obddcode" runat="server" Text='<%#Eval("OB_DDCODE")%>' Visible="false"></asp:Label>
                                            <asp:Label ID="lbl_jbr" runat="server" Text='<%#Eval("OB_JBR")%>' Visible="false"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="发票记录编号" ItemStyle-HorizontalAlign="Left" HeaderStyle-Wrap="false"
                                        ItemStyle-Wrap="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_dqcode" runat="server" Text='<%#Eval("OB_CODE") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField HeaderText="订单号" DataField="OB_DDCODE" ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-Wrap="false" ItemStyle-Wrap="false" />
                                    <asp:BoundField HeaderText="发票单号" DataField="OB_BILLCODE" ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-Wrap="false" ItemStyle-Wrap="false" />
                                    <asp:BoundField HeaderText="发票数量" DataField="OB_BILLNUM" ItemStyle-HorizontalAlign="Center"
                                        HeaderStyle-Wrap="false" ItemStyle-Wrap="false" />
                                    <asp:BoundField HeaderText="开票金额(含税)" DataField="OB_BILLJE" ItemStyle-HorizontalAlign="Right"
                                        HeaderStyle-Wrap="false" ItemStyle-Wrap="false" DataFormatString="{0:c}" />
                                    <asp:BoundField HeaderText="开票单位" DataField="OB_CSNAME" ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-Wrap="false" ItemStyle-Wrap="false" />
                                    <asp:BoundField HeaderText="开票时间" DataField="OB_KPDATE" ItemStyle-HorizontalAlign="Center"
                                        HeaderStyle-Wrap="false" ItemStyle-Wrap="false" DataFormatString="{0:d}" />
                                    <asp:BoundField HeaderText="收票日期" DataField="OB_SPDATE" ItemStyle-HorizontalAlign="Center"
                                        HeaderStyle-Wrap="false" ItemStyle-Wrap="false" DataFormatString="{0:d}" />
                                    <asp:BoundField HeaderText="凭证号" DataField="OB_PZH" ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-Wrap="false" ItemStyle-Wrap="false" />
                                    <asp:BoundField HeaderText="经办人" DataField="OB_JBR" ItemStyle-HorizontalAlign="Center"
                                        HeaderStyle-Wrap="false" ItemStyle-Wrap="false" />
                                    <asp:TemplateField HeaderText="财务编辑" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:HyperLink ID="bill_EditCW" Target="_blank" Visible="false" CssClass="link" NavigateUrl='<%#"CR_OrderBill_Add.aspx?Action=EditCW&billcode="+Eval("OB_CODE")%>'
                                                runat="server">
                                                <asp:Image ID="InfoImage_EditCW" ImageUrl="~/Assets/images/create.gif" border="0"
                                                    hspace="2" align="absmiddle" runat="server" />
                                                编辑
                                            </asp:HyperLink>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            <asp:Panel ID="pal_NoData" runat="server" HorizontalAlign="Center">
                                没有记录!</asp:Panel>
                        </asp:Panel>
                        <table width="100%">
                            <tr>
                                <td style="text-align: right">
                                    筛选结果：共<asp:Label ID="lb_select_num" runat="server" Text=""></asp:Label>条记录&nbsp;&nbsp;&nbsp;
                                    已选订单总金额：<asp:Label ID="lb_total_DDmoney" runat="server" Text="" ForeColor="Red"></asp:Label>
                                    &nbsp;&nbsp;&nbsp; 开票总金额(含税)：<asp:Label ID="lb_total_KPmoney" runat="server" Text=""
                                        ForeColor="Red"></asp:Label>
                                </td>
                                <td>
                                    <uc1:UCPaging ID="UCPaging1" runat="server" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

    <script type="text/javascript">
//单击行变色
function RowClick(obj)
{
//判断是否单击的已选择的行，如果是则取消该行选择
    if(obj.getElementsByTagName("td")[0].getElementsByTagName("input")[0].checked==false)
    {
       obj.style.backgroundColor='#ffffff';
       obj.getElementsByTagName("td")[0].getElementsByTagName("input")[0].checked=false;
    }
    else
    {
       var table=obj.parentNode.parentNode;
       var tr=table.getElementsByTagName("tr");
       
       var ss=tr.length;
       for(var i=1;i<ss-1;i++)
       {
           tr[i].style.backgroundColor=(tr[i].style.backgroundColor == '#87CEFF') ? '#ffffff' : '#ffffff';
          tr[i].getElementsByTagName("td")[0].getElementsByTagName("input")[0].checked=false; 
       }
       obj.style.backgroundColor=(obj.style.backgroundColor == '#ffffff') ? '#87CEFF' : '#ffffff';
       obj.getElementsByTagName("td")[0].getElementsByTagName("input")[0].checked=true;
   }
}

//查看发票信息
function ViewBill(ID,action)
{
var autonum=Math.round(10000*Math.random()); 
window.open("CR_OrderBill_Add.aspx?Action="+action+"&autonum="+autonum+"&billcode="+ID); 
}


//查看订单信息
function View_PurOrder(orderid)
{
      var autonum=Math.round(10000*Math.random());
      window.open("../PC_Data/TBPC_Purordertotal_list.aspx?autonum="+autonum+"&TotalOrder="+orderid+"");       
} 

    </script>

</asp:Content>
