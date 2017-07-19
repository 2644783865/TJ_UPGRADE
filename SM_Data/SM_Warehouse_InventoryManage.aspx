<%@ Page Language="C#" MasterPageFile="~/Masters/SMBaseMaster.master" AutoEventWireup="true"
    CodeBehind="SM_Warehouse_InventoryManage.aspx.cs" Inherits="ZCZJ_DPF.SM_Data.SM_Warehouse_InventoryManage"
    Title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="../Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PrimaryContent" runat="server">
    <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </cc1:ToolkitScriptManager>
    <link href="StyleFile/superTables.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" language="javascript">

function viewCondition()
{
   document.getElementById("<%=PanelCondition.ClientID%>").style.display='block';
}

    </script>

    <div class="box-inner">
        <div class="box_right">
            <div class="box-title">
                <asp:Label ID="ControlFinder" runat="server" Visible="false"></asp:Label>
                <table width="100%">
                    <tr>
                        <td align="left">
                            <asp:Label ID="LabelMessage" runat="server" ForeColor="Red" Visible="false"></asp:Label>
                        </td>
                        <td align="right">
                            <asp:Button ID="btnShowPopup" runat="server" Text="筛选" OnClientClick="viewCondition()" />&nbsp;
                            <cc1:ModalPopupExtender ID="ModalPopupExtenderSearch" runat="server" TargetControlID="btnShowPopup"
                                PopupControlID="UpdatePanelCondition" Drag="false" Enabled="True" DynamicServicePath=""
                                Y="10">
                            </cc1:ModalPopupExtender>
                            <asp:Button ID="BtnDelete" runat="server" Text="删除方案" OnClick="BtnDelete_Click" />&nbsp;
                            <asp:Button ID="Add" runat="server" Text="新建方案" OnClick="Add_Click" />&nbsp;
                            <asp:Button  ID="btnExport" runat="server" Text="导出汇总" OnClick="btnExport_Click" />&nbsp;
                            <asp:Button  ID="btnExportDif" runat="server" Text="导出差异" OnClick="btnExportDif_Click" />&nbsp;
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
    <div class="box-wrapper">
        <div class="box-outer">
            <asp:Panel ID="PanelCondition" runat="server" Width="100%" Style="display: none">
                <asp:UpdatePanel ID="UpdatePanelCondition" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <table width="100%" style="background-color: #CCCCFF; border: solid 1px black;">
                            <tr>
                                <td style="width: 25%; white-space: nowrap;" align="left">
                                    &nbsp;
                                </td>
                                <td style="width: 25%; white-space: nowrap;" align="left">
                                    &nbsp;
                                </td>
                                <td style="width: 25%; white-space: nowrap;" align="left">
                                    &nbsp;
                                </td>
                                <td style="width: 25%; white-space: nowrap;" align="left">
                                    <asp:Button ID="btnQuery" runat="server" OnClick="Query_Click" Text="查询" />&nbsp;&nbsp;&nbsp;
                                    <asp:Button ID="btnClose" runat="server" Text="取消" OnClick="btnClose_Click" />&nbsp;&nbsp;&nbsp;
                                    <asp:Button ID="btnReset" runat="server" Text="重置" OnClick="btnReset_Click" />
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 25%; white-space: nowrap;" align="left">
                                    &nbsp;&nbsp;&nbsp;盘点状态：<asp:DropDownList ID="DropDownListState" runat="server">
                                        <asp:ListItem Text="--请选择--" Value=""></asp:ListItem>
                                        <asp:ListItem Text="未提交" Value="0"></asp:ListItem>
                                        <asp:ListItem Text="待审核" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="待调整" Value="2"></asp:ListItem>
                                        <asp:ListItem Text="已调整" Value="3"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td style="width: 25%; white-space: nowrap;" align="left">
                                </td>
                                <td style="width: 25%; white-space: nowrap;" align="left">
                                </td>
                                <td style="width: 25%; white-space: nowrap;" align="left">
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 25%; white-space: nowrap;" align="left">
                                    &nbsp;&nbsp;&nbsp;盘点编号：<asp:TextBox ID="TextBoxCode" runat="server"></asp:TextBox>
                                </td>
                                <td style="width: 25%; white-space: nowrap;" align="left">
                                    制定日期：<asp:TextBox ID="TextBoxSchemaDate" runat="server"></asp:TextBox>
                                    <cc1:CalendarExtender ID="DateTextBox_CalendarExtender" runat="server" Format="yyyy-MM-dd"
                                        TargetControlID="TextBoxSchemaDate">
                                    </cc1:CalendarExtender>
                                </td>
                                <td style="width: 25%; white-space: nowrap;" align="left">
                                    盘点人：<asp:TextBox ID="TextBoxClerk" runat="server"></asp:TextBox>
                                </td>
                                <td style="width: 25%; white-space: nowrap;" align="left">
                                    审核人：<asp:TextBox ID="TextBoxVerifier" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </asp:Panel>
            <asp:UpdatePanel ID="UpdatePanelBody" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:Panel ID="PanelBody" runat="server" Width="100%">
                        <table id="superTable" width="100%" align="center" cellpadding="4" cellspacing="1"
                            class="toptable grid">
                            <asp:Repeater ID="Repeater1" runat="server" OnItemDataBound="Repeater1_ItemDataBound">
                                <HeaderTemplate>
                                    <tr align="center" class="tableTitle1" style="background-color: #e4ecf7">
                                        <td style="border-width: 0px 1px 1px 0px; border-style: solid; border-color: #adadad;">
                                            <strong></strong>
                                        </td>
                                        <td style="border-width: 0px 1px 1px 0px; border-style: solid; border-color: #adadad;">
                                            <strong>盘点类型</strong>
                                        </td>
                                        <td style="border-width: 0px 1px 1px 0px; border-style: solid; border-color: #adadad;">
                                            <strong>盘点编号</strong>
                                        </td>
                                        <td style="border-width: 0px 1px 1px 0px; border-style: solid; border-color: #adadad;">
                                            <strong>制定日期</strong>
                                        </td>
                                        <td style="border-width: 0px 1px 1px 0px; border-style: solid; border-color: #adadad;">
                                            <strong>制定人</strong>
                                        </td>
                                        <td style="border-width: 0px 1px 1px 0px; border-style: solid; border-color: #adadad;">
                                            <strong>盘点仓库</strong>
                                        </td>
                                        <td style="border-width: 0px 1px 1px 0px; border-style: solid; border-color: #adadad;">
                                            <strong>盘点物料</strong>
                                        </td>
                                        <td style="border-width: 0px 1px 1px 0px; border-style: solid; border-color: #adadad;">
                                            <strong>盘点状态</strong>
                                        </td>
                                        <td style="border-width: 0px 1px 1px 0px; border-style: solid; border-color: #adadad;">
                                            <strong>盘点人</strong>
                                        </td>
                                        <td style="border-width: 0px 1px 1px 0px; border-style: solid; border-color: #adadad;">
                                            <strong>盘点日期</strong>
                                        </td>
                                        <td style="border-width: 0px 1px 1px 0px; border-style: solid; border-color: #adadad;">
                                            <strong>制单人</strong>
                                        </td>
                                        <td style="border-width: 0px 1px 1px 0px; border-style: solid; border-color: #adadad;">
                                            <strong>审核人</strong>
                                        </td>
                                        <td style="border-width: 0px 1px 1px 0px; border-style: solid; border-color: #adadad;">
                                            <strong>审核日期</strong>
                                        </td>
                                        <td style="border-width: 0px 1px 1px 0px; border-style: solid; border-color: #adadad;">
                                            <strong>备注</strong>
                                        </td>
                                        <td style="border-width: 0px 1px 1px 0px; border-style: solid; border-color: #adadad;"
                                            id="BJH" runat="server">
                                            <strong>编辑</strong>
                                        </td>
                                        <td style="border-width: 0px 1px 1px 0px; border-style: solid; border-color: #adadad;"
                                            id="SHH" runat="server">
                                            <strong>审核</strong>
                                        </td>
                                        <td style="border-width: 0px 1px 1px 0px; border-style: solid; border-color: #adadad;"
                                            id="TZH" runat="server">
                                            <strong>调整</strong>
                                        </td>
                                        <td style="border-width: 0px 1px 1px 0px; border-style: solid; border-color: #adadad;"
                                            id="CKH" runat="server">
                                            <strong>查看</strong>
                                        </td>
                                    </tr>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr style="background-color: #ffffff; border-width: 0px 1px 1px 0px; border-style: solid;
                                        border-color: #000000; text-align: center;">
                                        <td style="border-width: 0px 1px 1px 0px; border-style: solid; border-color: #adadad;"
                                            align="left">
                                            <asp:CheckBox ID="CheckBox1" runat="server" /><%#Container.ItemIndex+1%>
                                        </td>
                                        <td style="border-width: 0px 1px 1px 0px; border-style: solid; border-color: #adadad;">
                                            <asp:Label ID="LabelProject" runat="server" Text='<%#Eval("PD_SCHEMATYPE")%>'></asp:Label>
                                        </td>
                                        <td style="border-width: 0px 1px 1px 0px; border-style: solid; border-color: #adadad;">
                                            <asp:Label ID="LabelCode" runat="server" Text='<%#Eval("Code")%>'></asp:Label>
                                        </td>
                                        <td style="border-width: 0px 1px 1px 0px; border-style: solid; border-color: #adadad;">
                                            <asp:Label ID="LabelDate" runat="server" Text='<%#Eval("Date")%>'></asp:Label>
                                        </td>
                                        <td style="border-width: 0px 1px 1px 0px; border-style: solid; border-color: #adadad;">
                                            <asp:Label ID="LabelPlaner" runat="server" Text='<%#Eval("Planer")%>'></asp:Label>
                                        </td>
                                        <td style="border-width: 0px 1px 1px 0px; border-style: solid; border-color: #adadad;">
                                            <asp:Label ID="LabelWarehouse" runat="server" Text='<%#Eval("Warehouse")%>'></asp:Label>
                                        </td>
                                        <td style="border-width: 0px 1px 1px 0px; border-style: solid; border-color: #adadad;">
                                            <asp:Label ID="LabelMaterialType" runat="server" Text='<%#Eval("MaterialType")%>'></asp:Label>
                                        </td>
                                        <td style="border-width: 0px 1px 1px 0px; border-style: solid; border-color: #adadad;">
                                            <asp:Label ID="LabelState" runat="server" Text='<%#convertState((string)Eval("State"))%>'></asp:Label>
                                        </td>
                                        <td style="border-width: 0px 1px 1px 0px; border-style: solid; border-color: #adadad;">
                                            <asp:Label ID="LabelClerk" runat="server" Text='<%#Eval("Clerk")%>'></asp:Label>
                                        </td>
                                        <td style="border-width: 0px 1px 1px 0px; border-style: solid; border-color: #adadad;">
                                            <asp:Label ID="LabelDoneDate" runat="server" Text='<%#Eval("DoneDate")%>'></asp:Label>
                                        </td>
                                        <td style="border-width: 0px 1px 1px 0px; border-style: solid; border-color: #adadad;">
                                            <asp:Label ID="LabelZDR" runat="server" Text='<%#Eval("ZDRNM")%>'></asp:Label>
                                        </td>
                                        <td style="border-width: 0px 1px 1px 0px; border-style: solid; border-color: #adadad;">
                                            <asp:Label ID="LabelVerifier" runat="server" Text='<%#Eval("Manager")%>'></asp:Label>
                                        </td>
                                        <td style="border-width: 0px 1px 1px 0px; border-style: solid; border-color: #adadad;">
                                            <asp:Label ID="LabelApproveDate" runat="server" Text='<%#Eval("ApproveDate")%>'></asp:Label>
                                        </td>
                                        <td style="border-width: 0px 1px 1px 0px; border-style: solid; border-color: #adadad;">
                                            <asp:Label ID="LabelComment" runat="server" Text='<%#Eval("Comment")%>' ForeColor="Red"></asp:Label>
                                        </td>
                                        <td style="border-width: 0px 1px 1px 0px; border-style: solid; border-color: #adadad;"
                                            id="BJ" runat="server">
                                            <asp:HyperLink ID="HyperLinkModify" Target="_blank" NavigateUrl='<%#"~/SM_Data/SM_Warehouse_InventoryReport.aspx?FLAG=MODIFY&&ID="+Eval("Code")%>'
                                                runat="server">编辑</asp:HyperLink>
                                        </td>
                                        <td style="border-width: 0px 1px 1px 0px; border-style: solid; border-color: #adadad;"
                                            id="SH" runat="server">
                                            <asp:HyperLink ID="HyperLinkVerify" Target="_blank" NavigateUrl='<%#"~/SM_Data/SM_Warehouse_InventoryReport.aspx?FLAG=VERIFY&&ID="+Eval("Code")%>'
                                                runat="server">审核</asp:HyperLink>
                                        </td>
                                        <td style="border-width: 0px 1px 1px 0px; border-style: solid; border-color: #adadad;"
                                            id="TZ" runat="server">
                                            <asp:HyperLink ID="HyperLinkAdjust" Target="_blank" NavigateUrl='<%#"~/SM_Data/SM_Warehouse_InventoryReport.aspx?FLAG=ADJUST&&ID="+Eval("Code")%>'
                                                runat="server">调整</asp:HyperLink>
                                        </td>
                                        <td style="border-width: 0px 1px 1px 0px; border-style: solid; border-color: #adadad;"
                                            id="CK" runat="server">
                                            <asp:HyperLink ID="HyperLinkRead" Target="_blank" NavigateUrl='<%#"~/SM_Data/SM_Warehouse_InventoryReport.aspx?FLAG=READ&&ID="+Eval("Code")%>'
                                                runat="server">查看</asp:HyperLink>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                            <asp:Panel ID="NoDataPanel" runat="server" Visible="false">
                                没有相关记录!</asp:Panel>
                        </table>
                    </asp:Panel>
                    <uc1:UCPaging ID="UCPaging1" runat="server" />
                </ContentTemplate>
            </asp:UpdatePanel>
            <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanelBody"
                DisplayAfter="1">
                <ProgressTemplate>
                    <img src="../Assets/images/ajaxloader.gif" style="position: absolute; left: 50%;
                        top: 50%;" alt="loading" />
                </ProgressTemplate>
            </asp:UpdateProgress>

            <script type="text/javascript">
  
   var sDataTable=document.getElementById("superTable")
  
   function RowClick()
   {
           for (var i=1, j=sDataTable.tBodies[0].rows.length; i<j; i++) 
          {
            sDataTable.tBodies[0].rows[i].onclick= function (i) 
            {
                var clicked = false;
                var dataRow = sDataTable.tBodies[0].rows[i];
                return function () 
                      {
                            if (clicked) 
                            {
                                dataRow.style.backgroundColor = "#ffffff";
                              
                                clicked = false;
                            }
                            else 
                            {
                                dataRow.style.backgroundColor = "#eeeeee";
                                clicked = true;
                            }
                        }
             }.call(this, i);
           }
   }

   RowClick();

            </script>

        </div>
        <!--box-outer END -->
    </div>
    <!--box-wrapper END -->
</asp:Content>
