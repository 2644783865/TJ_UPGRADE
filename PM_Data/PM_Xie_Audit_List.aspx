<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Masters/RightCotentMaster.Master" CodeBehind="PM_Xie_Audit_List.aspx.cs" Inherits="ZCZJ_DPF.PM_Data.PM1" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register src="../Controls/UCPaging.ascx" tagname="UCPaging" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
生产外协申请——审批
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
<link rel="Stylesheet" type="text/css" href="../Assets/main.css" />
    <link type="text/css" href="FixTable.css" rel="stylesheet" />
    <link href="../Assets/Setting.css" rel="stylesheet" type="text/css" />
    <script src="PcJs/rowcolor.js" type="text/javascript" charset="GB2312" language="javascript"></script>
    
    <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </cc1:ToolkitScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
         
        <ContentTemplate>       
            <div class="RightContent">
                <div class="box-inner">
                    <div class="box_right">
                        <div class="box-title">
                            <table width="100%">
                                <tr>
                                    <td>
                                        单据编号：
                                        <asp:TextBox ID="txtMP_PCODE" runat="server"></asp:TextBox>
                                        &nbsp;&nbsp;
                                        合同名称：
                                        <asp:TextBox ID="tb_pjinfo" runat="server"  AutoPostBack="True"></asp:TextBox>
                                        <%--<cc1:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" TargetControlID="tb_pjinfo"
                   ServicePath="../PC_Data/PC_Data_AutoComplete.asmx" CompletionSetCount="10" MinimumPrefixLength="1" CompletionInterval="10"
                   ServiceMethod="GetPJNAME" FirstRowSelected="true"  CompletionListCssClass="autocomplete_completionListElement"
                 CompletionListItemCssClass="autocomplete_listItem"  CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" >
                </cc1:AutoCompleteExtender>--%>
                                        &nbsp;&nbsp;
                                        设备名称：
                                        <asp:TextBox ID="tb_enginfo" runat="server"  AutoPostBack="True"></asp:TextBox>
                                       <%-- <cc1:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server" TargetControlID="tb_enginfo"
                   ServicePath="../PC_Data/PC_Data_AutoComplete.asmx" CompletionSetCount="10" MinimumPrefixLength="1" CompletionInterval="10"
                   ServiceMethod="GetENGNAME" FirstRowSelected="true"  CompletionListCssClass="autocomplete_completionListElement"
                  CompletionListItemCssClass="autocomplete_listItem"  CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" >
                </cc1:AutoCompleteExtender>--%>
                                        &nbsp;&nbsp;
                                   审批状态：
                                    <asp:DropDownList ID="rbl_type" runat="server"  AutoPostBack ="true"  OnSelectedIndexChanged="rbl_type_OnSelectedIndexChanged">
                                    <asp:ListItem Text="我的审批任务" Value="0" Selected="True"></asp:ListItem>
                                    <asp:ListItem Text="未审批" Value="1" ></asp:ListItem>
                                    <asp:ListItem Text="审批中" Value="2" ></asp:ListItem>
                                    <asp:ListItem Text="已通过" Value="3" ></asp:ListItem>
                                    <asp:ListItem Text="已驳回" Value="4" ></asp:ListItem>
                                    </asp:DropDownList> 
                                    &nbsp;&nbsp;
                                        <asp:Button ID="btn_Query" runat="server" Text="查 询"  OnClick="rbl_type_OnSelectedIndexChanged"/>
                                        &nbsp;
                                        <asp:Button ID="btn_Reset" runat="server" Text="清除筛选"  OnClick="btn_Reset_Click"/>                                      
                                    </td>
                                    
                                </tr>
                            </table>
                        </div>
                    </div>
                </div>
                <div class="box-wrapper">
                    <div class="box-outer"> 
                        <table width="100%" align="center"  class="nowrap cptable fullwidth">                       
                            <asp:Repeater ID="tbpc_otherpurbill_list_Repeater" runat="server" OnItemDataBound="EQU_Need_Audit_Repeater_ItemDataBound" >
                                <HeaderTemplate>
                                    <tr align="center" class="tableTitle" style="background-color:#5CACEE">
                                        <td>
                                            <strong>行号</strong>
                                        </td>
                                        <td>
                                            <strong>单据编号</strong>
                                        </td>
                                        <td>
                                            <strong>批号</strong>
                                        </td>
                                         <td>
                                            <strong>合同名称</strong>
                                        </td>
                                         <td>
                                            <strong>设备名称</strong>
                                        </td>
                                        <td>
                                            <strong>日期</strong>
                                        </td>                                        
                                        <td>
                                            <strong>制单人</strong>
                                        </td>                                        
                                        <td runat="server" id="haudit">
                                            <strong>审核</strong>
                                        </td>
                                        <td runat="server" id="hlookup">
                                            <strong>查看</strong>
                                        </td>
                                    </tr>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr class="baseGadget" onmouseover="this.className='highlight'" onmouseout="this.className='baseGadget'">
                                        <td runat="server" id="row_num">
                                            <asp:Label ID="rownum" runat="server" Text='<%#Container.ItemIndex + 1 %>'></asp:Label>
                                            &nbsp;
                                        </td>
                                        <td>
                                            <asp:Label ID="MP_PCODE" runat="server" Text='<%#Eval("PM_DocuNum")%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="PM_PID" runat="server" Text='<%#Eval("PM_PID")%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="PJ_NAME" runat="server" Text='<%#Eval("PM_PJID")%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="TSA_ENGNAME" runat="server" Text='<%#Eval("PM_ENGNAME")%>'></asp:Label>
                                            
                                        </td>
                                        <td>
                                            <asp:Label ID="SUBMITTM" runat="server" Text='<%#Eval("PM_SUBMITTM")%>'></asp:Label>
                                        </td>
                                        
                                        <td>                                            
                                            <asp:Label ID="ST_NAME" runat="server" Text='<%#Eval("SUBMITNM")%>'></asp:Label>
                                        </td>                                      
                                        
                                        <td runat="server" id="baudit">
                                            <%--<asp:HyperLink ID="hyp_edit" runat="server" ForeColor="Red" NavigateUrl='<%#"~/PC_Data/PC_TBPC_Otherpur_Bill_look.aspx?action=audit&mp_id="+Eval("MP_PCODE")%>'>审核</asp:HyperLink>--%>
                                             <asp:HyperLink ID="hyp_edit" runat="server" >
                                            <asp:Label ID="Label1" runat="server" Text="审核"></asp:Label></asp:HyperLink>
                                        </td>
                                        <td runat="server" id="blookup">
                                           <%-- <asp:HyperLink ID="HyperLink_lookup" runat="server" ForeColor="Red" NavigateUrl='<%#"~/PC_Data/PC_TBPC_Otherpur_Bill_look.aspx?action=view&mp_id="+Eval("MP_PCODE")%>'>查看</asp:HyperLink>--%>
                                            <asp:HyperLink ID="HyperLink_lookup" runat="server" >
                                            <asp:Label ID="PUR_DD" runat="server" Text="查看"></asp:Label></asp:HyperLink>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>                           
                        </table> 
                         <asp:Panel ID="NoDataPanel" runat="server" HorizontalAlign="Center" ForeColor="Red">没有记录!</asp:Panel>                          
                        <uc1:UCPaging ID="UCPaging1" runat="server" />
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>