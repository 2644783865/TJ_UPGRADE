<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Masters/RightCotentMaster.Master" CodeBehind="PM_Expense_edit.aspx.cs" Inherits="ZCZJ_DPF.PM_Data.PM_Expense_edit" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
    <link rel="Stylesheet" type="text/css" href="../Assets/main.css" />
    <link type="text/css" href="FixTable.css" rel="stylesheet" />
    <style type="text/css"> 
     .autocomplete_completionListElement 
     {  
     	margin : 0px; 
     	background-color : #1C86EE; 
     	color : windowtext; 
     	cursor : 'default'; 
     	text-align : left; 
     	list-style:none; 
     	padding:0px;
        border: solid 1px gray; 
        width:400px!important;   
     }
     .autocomplete_listItem 
     {   
     	border-style : solid; 
     	border :#FFEFDB; 
     	border-width : 1px;  
     	background-color : #EEDC82; 
     	color : windowtext;  
     } 
     .autocomplete_highlightedListItem 
     { 
     	background-color: #1C86EE; 
     	color: black; 
     	padding: 1px; 
     } 
  </style> 
    <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </cc1:ToolkitScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="RightContent" style="overflow: hidden">
                <div class="box-inner">
                    <div class="box_right">
                        <div class="box-title">
                            <table width="100%">
                                <tr>
                                    <td align="right">
                                        <asp:Button ID="btn_addrow" runat="server" Text="增加行" OnClick="btn_addrow_Click" />
                                        &nbsp;
                                        <asp:Button ID="btn_delectrow" runat="server" Text="删除行" OnClick="btn_delectrow_Click" />
                                        &nbsp;
                                        <asp:Button ID="btn_save" runat="server" Text="保存" OnClick="btn_save_Click" />
                                        &nbsp;
                                        <asp:Button ID="btn_back" runat="server" Text="返回" OnClientClick="history.go(-1)" />
                                        &nbsp; &nbsp;&nbsp; &nbsp;
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </div>
                <div class="box-wrapper">
                    <div class="box-outer">
                        <div>
                            <table width="100%">
                                <tr>
                                    <td style="font-size: x-large; text-align: center;" colspan="3">
                                                      生产工时管理
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="gongshiyear" runat="server">年份：</asp:Label>
                                        <asp:DropDownList ID="ddlgongshiyear" runat="server" AutoPostBack="true" >
                                            <asp:ListItem Selected="True" Value="%">全部</asp:ListItem>
                                            <asp:ListItem Value="2010">2010</asp:ListItem>
                                            <asp:ListItem Value="2011">2011</asp:ListItem>
                                            <asp:ListItem Value="2012">2012</asp:ListItem>
                                            <asp:ListItem Value="2013">2013</asp:ListItem>
                                            <asp:ListItem Value="2014">2014</asp:ListItem>
                                            <asp:ListItem Value="2015">2015</asp:ListItem>
                                            <asp:ListItem Value="2016">2016</asp:ListItem>
                                            <asp:ListItem Value="2017">2017</asp:ListItem>
                                            <asp:ListItem Value="2018">2018</asp:ListItem>
                                            <asp:ListItem Value="2019">2019</asp:ListItem>
                                            <asp:ListItem Value="2020">2020</asp:ListItem>
                                        </asp:DropDownList> 
                                        <asp:Label ID="gongshimonth" runat="server">月份：</asp:Label>
                                        <asp:DropDownList ID="ddlgongshimonth" runat="server" AutoPostBack="true" >
                                            <asp:ListItem Selected="True" Value="%">全部</asp:ListItem>
                                            <asp:ListItem Value="1">1</asp:ListItem>
                                            <asp:ListItem Value="2">2</asp:ListItem>
                                            <asp:ListItem Value="3">3</asp:ListItem>
                                            <asp:ListItem Value="4">4</asp:ListItem>
                                            <asp:ListItem Value="5">5</asp:ListItem>
                                            <asp:ListItem Value="6">6</asp:ListItem>
                                            <asp:ListItem Value="7">7</asp:ListItem>
                                            <asp:ListItem Value="8">8</asp:ListItem>
                                            <asp:ListItem Value="9">9</asp:ListItem>
                                            <asp:ListItem Value="10">10</asp:ListItem>
                                            <asp:ListItem Value="11">11</asp:ListItem>
                                            <asp:ListItem Value="12">12</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    
                                    <td style="width: 25%;" align="left">
                                        
                                        <asp:TextBox ID="txt_docunum" runat="server" Text="" Width="200px" Visible="false"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <cc1:TabContainer ID="TabContainer1" runat="server" Width="100%" TabStripPlacement="Top"
                            ActiveTabIndex="0" BorderStyle="None">
                            <cc1:TabPanel ID="TabPanel1" runat="server" HeaderText="工时" TabIndex="0" BorderStyle="None">
                                <ContentTemplate>
                                    <div style="border: 1px solid #000000; height: 300px">
                                        <div class="cpbox6 xscroll">
                                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                <ContentTemplate>
                                                    <div>
                                                        <table id="tab" width="100%" class="nowrap cptable fullwidth">
                                                            <asp:Repeater ID="PM_GongShi_List_Repeater" runat="server" OnItemDataBound="PM_GongShi_List_Repeater_ItemDataBound">
                                                                <HeaderTemplate>
                                                                    <tr align="center" class="tableTitle" style="background-color: #B9D3EE">
                                                                        <td>
                                                                            <strong>行号</strong>
                                                                        </td>
                                                                        <td>
                                                                            <strong>任务单号</strong>
                                                                        </td>
                                                                        <td>
                                                                            <strong>顾客名称</strong>
                                                                        </td>
                                                                        <td>
                                                                            <strong>合同号</strong>
                                                                        </td>
                                                                        
                                                                        <td>
                                                                            <strong>图号</strong>
                                                                        </td>
                                                                        <td>
                                                                            <strong>备注</strong>
                                                                        </td>
                                                                        <td>
                                                                            <strong>总计工时</strong>
                                                                        </td>
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <tr id="Tr1" class="baseGadget" onmouseover="this.className='highlight'" onmouseout="this.className='baseGadget'" runat="server" align="center">
                                                                        <td>
                                                                            <asp:Label ID="rownum" runat="server" Text='<%#Container.ItemIndex + 1 %>'></asp:Label>
                                                                            <asp:CheckBox ID="CHK" CssClass="checkBoxCss" BorderStyle="None" runat="server"
                                                                                onclick="checkme(this)" Checked="false"></asp:CheckBox>&nbsp;
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="TSA_ID" runat="server" Text='<%#Eval("TSA_ID")%>' OnTextChanged="TSA_ID_Textchanged" AutoPostBack="true"
                                                                         Width="100px"></asp:TextBox>
                                                                        <cc1:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" TargetControlID="TSA_ID"
                                                                            ServicePath="PM_Data_AutoComplete.asmx" CompletionSetCount="15" MinimumPrefixLength="1" CompletionInterval="10" 
                                                                            ServiceMethod="GetCompletebytsaid" FirstRowSelected="true" CompletionListCssClass="autocomplete_completionListElement"
                                                                            CompletionListItemCssClass="autocomplete_listItem"  CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" >
                                                                        </cc1:AutoCompleteExtender>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="CM_CUSNAME" runat="server" Text='<%#Eval("CM_CUSNAME")%>'/>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="CM_CONTR" runat="server" Text='<%#Eval("CM_CONTR")%>'/>
                                                                        </td>
                                                                        
                                                                        <td>
                                                                            <asp:TextBox ID="TSA_MAP" runat="server" Text='<%#Eval("TSA_MAP")%>'/>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="GS_NOTE" runat="server" Text='<%#Eval("GS_NOTE")%>'/>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="GS_HOURS" runat="server" Text='<%#Eval("GS_HOURS")%>'/>
                                                                        </td>
                                                                    </tr>
                                                                </ItemTemplate>
                                                            </asp:Repeater>
                                                            <tr>
                                                                <td colspan="16" align="center">
                                                                    <asp:Panel ID="NoDataPane" runat="server" Visible="false">
                                                                        没有数据！</asp:Panel>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                </ContentTemplate>
                            </cc1:TabPanel>
                        </cc1:TabContainer>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
