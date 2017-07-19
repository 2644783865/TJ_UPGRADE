<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Masters/RightCotentMaster.Master"
    CodeBehind="EQU_Repair_edit.aspx.cs" Inherits="ZCZJ_DPF.ESM_Data.EQU_Repair_edit" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
    <link rel="Stylesheet" type="text/css" href="../Assets/main.css" />
    <link type="text/css" href="FixTable.css" rel="stylesheet" />
    <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </cc1:ToolkitScriptManager>
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
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="RightContent" style="overflow: hidden">
                <div class="box-inner">
                    <div class="box_right">
                        <div class="box-title">
                            <table width="100%">
                                <tr>
                                    <td align="right">
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
                                        维&nbsp;&nbsp;&nbsp;修&nbsp;&nbsp;&nbsp;单
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 25%;" align="left">
                                        &nbsp;&nbsp;&nbsp;使&nbsp;&nbsp;用&nbsp;&nbsp;部&nbsp;&nbsp;门：
                                        <asp:TextBox ID="tb_dep" runat="server" Text="" Enabled="false"></asp:TextBox>
                                        <asp:TextBox ID="tb_depid" runat="server" Text="" Visible="false"></asp:TextBox>
                                    </td>
                                    <td style="width: 25%;" align="left">
                                        &nbsp;&nbsp;&nbsp;日&nbsp;&nbsp;&nbsp;期：
                                        <asp:TextBox ID="Tb_shijian" runat="server" Text="" Enabled="false" Width="150px"></asp:TextBox>
                                    </td>
                                    <td style="width: 25%;" align="left">
                                        &nbsp;&nbsp;&nbsp;编&nbsp;&nbsp;&nbsp;号：
                                        <asp:TextBox ID="TextBox_pid" runat="server" Text="" Width="200px" Enabled="false"></asp:TextBox>
                                    </td>
                                    <td style="width: 25%;" align="left">
                                        &nbsp;&nbsp;&nbsp;维&nbsp;&nbsp;修&nbsp;&nbsp;类&nbsp;&nbsp;别：
                                        <asp:DropDownList ID="Type" runat="server" AutoPostBack="true">
                                            <asp:ListItem Selected="True">--请选择--</asp:ListItem>
                                            <asp:ListItem Value="0">机械报修</asp:ListItem>
                                            <asp:ListItem Value="1">电气报修</asp:ListItem>
                                            <asp:ListItem Value="2">行政报修</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <cc1:TabContainer ID="TabContainer1" runat="server" Width="100%" TabStripPlacement="Top"
                            ActiveTabIndex="0" BorderStyle="None">
                            <cc1:TabPanel ID="TabPanel1" runat="server" HeaderText="维修单" TabIndex="0" BorderStyle="None">
                                <ContentTemplate>
                                    <div style="border: 1px solid #000000; height: 300px">
                                        <div class="cpbox6 xscroll">
                                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                <ContentTemplate>
                                                    <div>
                                                        <table id="tab" width="100%" class="nowrap cptable fullwidth">
                                                            <asp:Repeater ID="EQU_Repair_List_Repeater" runat="server" OnItemDataBound="EQU_Repair_List_Repeater_ItemDataBound">
                                                                <HeaderTemplate>
                                                                    <tr align="center" class="tableTitle" style="background-color: #5CACEE">
                                                                        <td>
                                                                            <strong>行号</strong>
                                                                        </td>
                                                                        <td>
                                                                            <strong>名称</strong>
                                                                        </td>
                                                                        <td>
                                                                            <strong>型号</strong>
                                                                        </td>
                                                                        <td>
                                                                            <strong>故障内容</strong>
                                                                        </td>
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <tr id="Tr1" class="baseGadget" onmouseover="this.className='highlight'" onmouseout="this.className='baseGadget'"
                                                                        runat="server" align="center">
                                                                        <td>
                                                                            <asp:Label ID="Label1" runat="server" Text='<%#Container.ItemIndex + 1 %>'></asp:Label>
                                                                            <asp:CheckBox ID="CHK" CssClass="checkBoxCss" BorderStyle="None" runat="server" Checked="false"
                                                                                onclick="checkme(this)"></asp:CheckBox>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="EquName" runat="server" AutoPostBack="True" OnTextChanged="EquName_TextChanged" Text='<%#Eval("EquName")%>'></asp:TextBox>
                                                                            <cc1:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server"  TargetControlID="EquName"
                                                                                ServicePath="EQU_Data_AutoComplete.asmx" CompletionSetCount="10" MinimumPrefixLength="1"
                                                                                CompletionInterval="10" ServiceMethod="GetNAME" FirstRowSelected="true" CompletionListCssClass="autocomplete_completionListElement"
                                                                                CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem">
                                                                            </cc1:AutoCompleteExtender>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="EquType" runat="server" Text='<%#Eval("EquType")%>'></asp:TextBox>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="Reason" Width="400px" runat="server" Text='<%#Eval("Reason")%>'></asp:TextBox>
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
                        <div>
                            <table width="100%" style="text-align: center">
                                <tr>
                                    <td>
                                        负责人:
                                        <asp:DropDownList ID="cob_fuziren" runat="server" Width="120px">
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        申请人:
                                        <asp:DropDownList ID="cob_sqren" runat="server" Width="120px">
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        制单人:<asp:TextBox ID="TextBoxexecutor" runat="server" Enabled="false"></asp:TextBox>
                                        <asp:TextBox ID="TextBoxexecutorid" runat="server" Visible="false"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
