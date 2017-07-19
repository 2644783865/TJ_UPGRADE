<%@ Page Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master" AutoEventWireup="true" CodeBehind="OM_GDGZJLCK.aspx.cs" Inherits="ZCZJ_DPF.OM_Data.OM_GDGZJLCK" Title="固定工资记录" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
     <script src="../JS/StyleControl.js" type="text/javascript" charset="GB2312"></script>
     <style type="text/css">
        .show
        {
            display: block;
        }
        .completionListElement
        {
            margin: 0px;
            color: windowtext;
            cursor: 'default';
            text-align: left;
            list-style: none;
            padding: 0px;
            border: solid 1px gray;
            width: 150px !important;
            background-color: White;
            font-size: small;
        }
        .listItem
        {
            border-style: solid;
            border: #FFEFDB;
            border-width: 1px;
            color: windowtext;
            font-size: small;
        }
        .highlightedListItem
        {
            background-color: #1C86EE;
            color: black;
            font-size: small;
        }
    </style>
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
                 <div class="box-wrapper">
                    <div class="box-outer">
                        <div>
                           <table width="100%" id="table2">
                             <tr>
                               <td>
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<strong>部门：</strong>
                            <asp:DropDownList ID="ddldpt" AutoPostBack="true" runat="server" DataTextField="DEP_NAME"
                                        DataValueField="DEP_CODE" OnSelectedIndexChanged="ddl_dpt_SelectedIndexChanged">
                            </asp:DropDownList>
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    <strong>姓名：</strong>
                                    <asp:TextBox runat="server" ID="tbxName" Width="80px"></asp:TextBox>
                                    <asp:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" TargetControlID="tbxName"
                                        ServicePath="~/OM_Data/OM_Data_Autocomplete.asmx" CompletionSetCount="100" MinimumPrefixLength="1"
                                        CompletionInterval="100" ServiceMethod="Getdata" FirstRowSelected="true" CompletionListCssClass="completionListElement"
                                        CompletionListItemCssClass="listItem" CompletionListHighlightedItemCssClass="highlightedListItem"
                                        UseContextKey="false" EnableCaching="false">
                                    </asp:AutoCompleteExtender>
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    <strong>修改日期:</strong>
                                    从<asp:TextBox ID="txtStartTime" Width="100px" class="easyui-datebox" runat="server" onClick="setday(this)"></asp:TextBox>
                                    到
                                    <asp:TextBox ID="txtEndTime" runat="server" Width="100px" class="easyui-datebox" onClick="setday(this)"></asp:TextBox>
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<strong>岗位序列：</strong><asp:TextBox ID="txtgwxl" runat="server" Width="90px"></asp:TextBox>
                                    <asp:Button runat="server" ID="btnQuery" Text="查询" OnClick="btnQuery_OnClick" />
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:Button runat="server" ID="btn_export" Text="导出" OnClick="btn_export_OnClick" />
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:Button runat="server" ID="btn_plexport" Text="批量导出" OnClick="btn_plexport_OnClick" />
                                    &nbsp;&nbsp;&nbsp;
                              </td>
                            </tr>
                          </table>
                        </div>
                        <div id="div_statistcs" style="width: 100%; height: auto; overflow: scroll; display: block;">
                            <table id="table1" align="center" cellpadding="2" cellspacing="1" class="toptable grid nowrap"
                                border="1" width="100%">
                                <asp:Repeater ID="rptGDGZrecord" runat="server">
                                    <HeaderTemplate>
                                        <tr align="center" style="background-color: #B9D3EE;">
                                            <td align="center">
                                                序号
                                            </td>
                                            <td align="center">
                                                工号
                                            </td>
                                            <td align="center">
                                                姓名
                                            </td>
                                            <td align="center">
                                                部门
                                            </td>
                                            <td align="center">
                                                调整前固定工资
                                            </td>
                                            <td align="center">
                                                调整额度
                                            </td>
                                            <td align="center">
                                                固定工资
                                            </td>
                                            <td align="center">
                                                修改人
                                            </td>
                                            <td align="center">
                                                修改时间
                                            </td>
                                            <td align="center">
                                                备注
                                            </td>
                                        </tr>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr class="baseGadget" onmouseover="this.className='highlight'" onclick="javascript:change(this);"
                                            ondblclick="javascript:changeback(this);" onmouseout="this.className='baseGadget'">
                                            <td align="center">
                                                <asp:Label ID="lbID" runat="server" Text='<%#Eval("ID")%>' Visible="false"></asp:Label>
                                                <asp:CheckBox ID="cbxSelect" runat="server" Checked="false" />
                                                <asp:Label ID="LineNumber" runat="server" Text='<%#Container.ItemIndex+1%>'></asp:Label>
                                                <asp:Label ID="lbstid" runat="server" Text='<%#Eval("ST_ID")%>' Visible="false"></asp:Label>
                                            </td>
                                            <td runat="server" align="center">
                                                <asp:Label ID="lb_gh" runat="server" Text='<%#Eval("Person_GH")%>'></asp:Label>
                                            </td>
                                            <td runat="server" align="center">
                                                <asp:Label ID="lb_name" runat="server" Text='<%#Eval("ST_NAME")%>'></asp:Label>
                                            </td>
                                            <td runat="server" align="center">
                                                <asp:Label ID="lb_bm" runat="server" Text='<%#Eval("DEP_NAME")%>'></asp:Label>
                                            </td>
                                            
                                            <td runat="server" align="center">
                                                <asp:Label ID="lblastgdgz" runat="server" BorderStyle="None" BackColor="Transparent"
                                                    Width="50px" Text='<%#Eval("lastgdgz")%>'></asp:Label>
                                            </td>
                                            <td runat="server" align="center">
                                                <asp:Label ID="lbtzedu" runat="server" BorderStyle="None" BackColor="Transparent"
                                                    Width="50px" Text='<%#Eval("tzedu").ToString()=="0"?"":Eval("tzedu")%>'></asp:Label>
                                            </td>
                                            
                                            <td runat="server" align="center">
                                                <asp:Label ID="lb_gdgz" runat="server" BorderStyle="None" BackColor="Transparent"
                                                    Width="50px" Text='<%#Eval("GDGZ")%>'></asp:Label>
                                            </td>
                                            <td runat="server" align="center">
                                                <asp:Label ID="lbxgr" runat="server" Text='<%#Eval("XGRST_NAME")%>'></asp:Label>
                                            </td>
                                            <td runat="server" align="center">
                                                <asp:Label ID="lb_time" runat="server" Text='<%#Eval("XGTIME")%>'></asp:Label>
                                            </td>
                                            <td runat="server" align="center">
                                                <asp:TextBox ID="ptcode" runat="server" Width="150px" Text='<%#Eval("NOTE")%>'
                                                    BorderStyle="None" Style="background-color: Transparent; text-align: center"
                                                    ToolTip='<%#Eval("NOTE")%>'></asp:TextBox>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </table>
                            <asp:Panel ID="palNoData" runat="server" Visible="false" ForeColor="Red" HorizontalAlign="Center">
                                没有记录!<br />
                                <br />
                            </asp:Panel>
                        </div>
                    </div>
                    <uc1:UCPaging ID="UCPaging1" runat="server" />
                </div>
</asp:Content>
