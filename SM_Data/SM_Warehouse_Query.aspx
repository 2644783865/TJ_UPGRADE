<%@ Page Language="C#" MasterPageFile="~/Masters/SMBaseMaster.Master" AutoEventWireup="true"
    CodeBehind="SM_Warehouse_Query.aspx.cs" Inherits="ZCZJ_DPF.SM_Data.SM_Warehouse_Query"
    Title="库存查询" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PrimaryContent" runat="server">

    <script src="SM_JS/superTables.js" type="text/javascript"></script>

    <script src="SM_JS/SelectCondition.js" type="text/javascript"></script>

    <link href="StyleFile/superTables.css" rel="stylesheet" type="text/css" />
    <link href="StyleFile/superTables_Default.css" rel="stylesheet" type="text/css" />
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnableScriptGlobalization="true"
        EnableScriptLocalization="true">
    </asp:ToolkitScriptManager>

    <script type="text/javascript" language="javascript">
 var postBack=true;
Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(BeginRequestHandler);
Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
function BeginRequestHandler(sender, args){
 if (args.get_postBackElement().id == "<%= btnClose.ClientID %>"||args.get_postBackElement().id=="<%= WarehouseDropDownList.ClientID %>"||args.get_postBackElement().id=="<%= TypeDropDownList.ClientID %>"||args.get_postBackElement().id == "<%= btnReset.ClientID %>")
   {
     postBack=false;
   }
   else
   {
    ActivateAlertDiv('visible', 'AlertDiv', '');
   }
    
}
function EndRequestHandler(sender, args){
if(postBack){
    document.getElementById("GridView1").parentNode.className = "fakeContainer";

    (function() {
        superTable("GridView1", {
            cssSkin : "Default",
           fixedCols : 2,
           onFinish : function () 
              {             
                for (var i=0, j=this.sDataTable.tBodies[0].rows.length-1; i<j; i++) 
                {
                   
                    this.sDataTable.tBodies[0].rows[i].onclick = this.sFDataTable.tBodies[0].rows[i].onclick = function (i) 
                    {
                        var clicked = false;

                        var dataRow = this.sDataTable.tBodies[0].rows[i];
                        var fixedRow = this.sFDataTable.tBodies[0].rows[i];
                        return function () 
                              {
                                    if (clicked) 
                                    {
                                        dataRow.style.backgroundColor = "#ffffff";
                                        fixedRow.style.backgroundColor = "#e4ecf7";
                                        fixedRow.getElementsByTagName("td")[0].getElementsByTagName("input")[0].checked=false;
                                        clicked = false;
                                    }
                                    else 
                                    {
                                        dataRow.style.backgroundColor = "#BFDFFF";
                                        fixedRow.style.backgroundColor = "#409FFF";
                                        fixedRow.getElementsByTagName("td")[0].getElementsByTagName("input")[0].checked=true;
                                        clicked = true;
                                    }
                                }
                                }.call(this, i);
                            }
                         return this;
                   }
        });
    })();
    
ActivateAlertDiv('hidden', 'AlertDiv', '');
}
postBack=true;
 }
         
function ActivateAlertDiv(visstring, elem, msg){
     var adiv = $get(elem);
     adiv.style.visibility = visstring;                
}

function confirmapp() {
        window.returnValue = true;
        window.close(); 
}

function cancelapp() {
    window.returnValue = false;
    window.close();
}

function closewin() {

if(window.opener!=null)
{
   window.opener.location = window.opener.location.href;
}
    window.close();
}

function storageexport(type,time) {

 var date=new Date();
var time=date.getTime();

    var retVal = window.showModalDialog("SM_WarehouseStorage_Export.aspx?type="+type, "", "dialogWidth=650px;dialogHeight=400px;help=no;scroll=no");
}

      
function viewCondition(){
   document.getElementById("<%=PanelCondition.ClientID%>").style.display='block';
}
    </script>

    <div class="box-inner">
        <div class="box_right">
            <div class="box-title">
                <asp:Label ID="ControlFinder" runat="server" Visible="false"></asp:Label>
                <asp:HiddenField ID="hfdtn" runat="server" />
                <asp:HiddenField ID="hfdtp" runat="server" />
                <table width="98%">
                    <tr>
                        <td align="left">
                            <asp:Label ID="LabelMessage" runat="server" ForeColor="Red"></asp:Label>
                            &nbsp;&nbsp;&nbsp;
                            <asp:HyperLink ID="hplfjcb" runat="server" NavigateUrl="~/SM_Data/SM_MatCompare.aspx"
                        Target="_blank" Font-Underline="false">
                        <asp:Image ID="Image2" ImageUrl="~/assets/images/res.gif" hspace="2" align="absmiddle"
                                                    runat="server" />物料比对</asp:HyperLink>&nbsp;&nbsp;&nbsp;
                        </td>
                        <td align="right">
                            <asp:Button ID="Append" runat="server" Text="追加" OnClick="Append_Click" />&nbsp;
                            <input id="Cancel" type="button" value="取消" onclick="cancelapp()" runat="server" />&nbsp;
                            <asp:Button ID="btnShowPopup" runat="server" Text="筛选" OnClientClick="viewCondition()" />&nbsp;
                            <asp:ModalPopupExtender ID="ModalPopupExtenderSearch" runat="server" TargetControlID="btnShowPopup"
                                PopupControlID="UpdatePanelCondition" Drag="false" Enabled="True" DynamicServicePath=""
                                Y="10">
                            </asp:ModalPopupExtender>
                            <input id="StorageExport" type="button" value="导出" onclick="storageexport()" runat="server"
                                visible="false" />
                            <asp:Button ID="BtnShowExport" runat="server" Text="导出" OnClick="BtnShowExport_Click" />&nbsp;
                            <asp:Label ID="LabelPush" runat="server" Text="下推:" Visible="false"></asp:Label><asp:DropDownList
                                ID="DropDownListPush" runat="server" OnSelectedIndexChanged="DropDownListPush_SelectedIndexChanged"
                                AutoPostBack="true" Visible="false">
                                <asp:ListItem Text="--请选择--" Value=""></asp:ListItem>
                                <asp:ListItem Text="生产领料库单" Value="0"></asp:ListItem>
                                <asp:ListItem Text="委外出库单" Value="1"></asp:ListItem>
                                <asp:ListItem Text="销售出库单" Value="2"></asp:ListItem>
                                <%--<asp:ListItem Text="调拨单" Value="3" ></asp:ListItem>--%>
                                <asp:ListItem Text="MTO调整单" Value="4"></asp:ListItem>
                                 <asp:ListItem Text="项目结转" Value="5"></asp:ListItem>
                            </asp:DropDownList>
                            <asp:Button ID="btn_llout" runat="server" Text="生产领料库单" OnCommand="btn_out_Command"
                                CommandArgument="0" />
                            <asp:Button ID="btn_wwout" runat="server" Text="委外出库单" OnCommand="btn_out_Command"
                                CommandArgument="1" />
                            <asp:Button ID="btn_xsout" runat="server" Text="销售出库单" OnCommand="btn_out_Command"
                                CommandArgument="2" />
                            <asp:Button ID="btn_alout" runat="server" Text="调拨单" OnCommand="btn_out_Command"
                                CommandArgument="3" />
                            <asp:Button ID="btn_mtoout" runat="server" Text="MTO调整单" OnCommand="btn_out_Command"
                                CommandArgument="4" />&nbsp;
                            <asp:Button ID="btn_projtemp" runat="server" Text="项目结转" OnCommand="btn_out_Command"
                                CommandArgument="5" />&nbsp;
                            <asp:Button ID="btn_qtout" runat="server" Text="其他出库" OnCommand="btn_out_Command"
                                CommandArgument="6" />&nbsp;
                            <input id="Close" type="button" value="关闭" onclick="closewin()" runat="server" />&nbsp;
                            <asp:Button ID="btn_QRExport" runat="server" Text="导出二维码信息" OnClick="btn_QRExport_Click" />
                        </td>
                    </tr>
                </table>
                <asp:Panel ID="PanelCondition" runat="server" Width="100%" Style="display: none">
                    <asp:UpdatePanel ID="UpdatePanelCondition" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <table width="100%" style="background-color: #CCCCFF; border: solid 1px black;">
                                <tr>
                                    <td style="white-space: nowrap;" align="left" colspan="2">
                                        &nbsp;&nbsp;&nbsp;显示条数：<asp:TextBox ID="TextBoxCount" runat="server"></asp:TextBox>
                                    </td>
                                    <td style="white-space: nowrap;" align="left">
                                        &nbsp;
                                    </td>
                                    <td style="white-space: nowrap;" align="left">
                                        <asp:Button ID="QueryButton" runat="server" OnClick="QueryButton_Click" Text="查询" />
                                        &nbsp;&nbsp;&nbsp;
                                        <asp:Button ID="btnClose" runat="server" Text="取消" OnClick="btnClose_Click" />&nbsp;&nbsp;&nbsp;
                                        <asp:Button ID="btnReset" runat="server" Text="重置" OnClick="btnReset_Click" />&nbsp;&nbsp;&nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td style="white-space: nowrap; width: 25%;" align="left">
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;仓库：<asp:DropDownList ID="WarehouseDropDownList"
                                            runat="server" OnSelectedIndexChanged="WarehouseDropDownList_SelectedIndexChanged"
                                            AutoPostBack="true">
                                        </asp:DropDownList>
                                    </td>
                                    <td style="white-space: nowrap; width: 25%;" align="left">
                                        &nbsp;&nbsp;&nbsp;子仓库：<asp:DropDownList ID="ChildWarehouseDropDownList" runat="server">
                                        </asp:DropDownList>
                                    </td>
                                    <td style="white-space: nowrap; width: 25%;" align="left">
                                        &nbsp;&nbsp;&nbsp;物料种类：<asp:DropDownList ID="TypeDropDownList" runat="server" OnSelectedIndexChanged="TypeDropDownList_SelectedIndexChanged"
                                            AutoPostBack="true">
                                        </asp:DropDownList>
                                    </td>
                                    <td style="white-space: nowrap; width: 25%;" align="left">
                                        物料子类：<asp:DropDownList ID="SubTypeDropDownList" runat="server">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" valign="top">
                                        <table width="100%">
                                            <tr>
                                                <td style="white-space: nowrap; width: 50%;" align="left">
                                                    &nbsp;&nbsp;&nbsp;截止日期：<asp:TextBox ID="DateTextBox" runat="server"></asp:TextBox><asp:CalendarExtender
                                                        ID="DateTextBox_CalendarExtender" runat="server" Format="yyyy-MM-dd" TargetControlID="DateTextBox">
                                                    </asp:CalendarExtender>
                                                </td>
                                                <td style="white-space: nowrap; width: 50%;" align="left">
                                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;合并：<asp:DropDownList ID="DropDownListMerge" runat="server">
                                                        <asp:ListItem Text="--请选择--" Value="0"></asp:ListItem>
                                                        <asp:ListItem Text="按物料" Value="1"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="white-space: nowrap; width: 50%;" align="left">
                                                    &nbsp;&nbsp;&nbsp;物料代码：<asp:TextBox ID="TextBoxCode" runat="server"></asp:TextBox>
                                                </td>
                                                <td style="white-space: nowrap; width: 50%;" align="left">
                                                    &nbsp;&nbsp;&nbsp;物料名称：<asp:TextBox ID="TextBoxName" runat="server"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="white-space: nowrap; width: 50%;" align="left">
                                                    &nbsp;&nbsp;&nbsp;规格型号：<asp:TextBox ID="TextBoxStandard" runat="server"></asp:TextBox>
                                                </td>
                                                <td style="white-space: nowrap; width: 50%;" align="left">
                                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;材&nbsp;&nbsp;&nbsp;质：<asp:TextBox ID="TextBoxAttribute"
                                                        runat="server"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="white-space: nowrap; width: 50%;" align="left">
                                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;标识号：<asp:TextBox ID="TextBoxNO" runat="server"></asp:TextBox>
                                                </td>
                                                <td style="white-space: nowrap; width: 50%;" align="left">
                                                    计划跟踪号：<asp:TextBox ID="TextBoxPTC" runat="server"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="white-space: nowrap; width: 50%;" align="left">
                                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;长：<asp:TextBox
                                                        ID="TextBoxLength" runat="server"></asp:TextBox>
                                                </td>
                                                <td style="white-space: nowrap; width: 50%;" align="left">
                                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;宽：<asp:TextBox
                                                        ID="TextBoxWidth" runat="server"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="white-space: nowrap; width: 50%;" align="left">
                                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;批&nbsp;&nbsp;&nbsp;号：<asp:TextBox ID="TextBoxLotNumber"
                                                        runat="server"></asp:TextBox>
                                                </td>
                                                <td style="white-space: nowrap; width: 50%;" align="left">
                                                     &nbsp;&nbsp;&nbsp;是否定尺：<asp:TextBox ID="TextBoxFixed" runat="server"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                              <td>
                                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;备&nbsp;&nbsp;&nbsp;注：<asp:TextBox ID="TextBoxNote"
                                                        runat="server"></asp:TextBox>
                                              </td>
                                               <td>
                                                 &nbsp;&nbsp;&nbsp;订单编号：<asp:TextBox ID="TextBoxOrderCode" runat="server"></asp:TextBox>                                                       
                                              </td>                                         
                                              
                                            </tr>
                                            <tr>
                                              <td></td>
                                              <td>
                                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;逻&nbsp;&nbsp;&nbsp;辑：<asp:DropDownList ID="DropDownListFatherLogic"
                                                        runat="server">
                                                        <asp:ListItem Value="AND" Selected="True">并且</asp:ListItem>
                                                        <asp:ListItem Value="OR">或者</asp:ListItem>
                                                        <asp:ListItem></asp:ListItem>
                                                    </asp:DropDownList>
                                              </td>                                            
                                            </tr>
                                        </table>
                                    </td>
                                    <td colspan="2">
                                        <asp:GridView ID="GridViewSearch" runat="server" AutoGenerateColumns="False" CssClass="toptable grid"
                                            CellPadding="3" Font-Size="9pt" BackColor="White" BorderColor="Black" BorderStyle="Solid"
                                            BorderWidth="1px" OnDataBound="GridViewSearch_DataBound">
                                            <FooterStyle BackColor="White" ForeColor="#000066" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="逻辑">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="tb_logic" runat="server" Text='' onclick="infoc(this)" Width="60px"></asp:TextBox>
                                                        <asp:DropDownList ID="DropDownListLogic" runat="server" Width="60px" Style="display: none">
                                                            <%--<asp:ListItem Value="NO" Text=""></asp:ListItem>--%>
                                                            <asp:ListItem Value="OR" Selected="True">或者</asp:ListItem>
                                                            <asp:ListItem Value="AND">并且</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="80px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="名称">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="tb_name" runat="server" Text='' onclick="infoc(this)" Width="128px"></asp:TextBox>
                                                        <asp:DropDownList ID="DropDownListName" runat="server" Width="128px" Style="display: none">
                                                        </asp:DropDownList>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="130px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="比较关系">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="tb_relation" runat="server" Text='' onclick="infoc(this)" Width="80px"></asp:TextBox>
                                                        <asp:DropDownList ID="DropDownListRelation" runat="server" Width="80px" Style="display: none">
                                                            <%--<asp:ListItem Value="NO" Text=""></asp:ListItem>--%>
                                                            <asp:ListItem Value="0" Selected="True">包含</asp:ListItem>  
                                                            <asp:ListItem Value="7">不包含</asp:ListItem>
                                                            <asp:ListItem Value="8">左包含</asp:ListItem>
                                                            <asp:ListItem Value="9">右包含</asp:ListItem>                                                  
                                                            <asp:ListItem Value="1">等于</asp:ListItem>
                                                            <asp:ListItem Value="2">不等于</asp:ListItem>
                                                            <asp:ListItem Value="3">大于</asp:ListItem>
                                                            <asp:ListItem Value="4">大于或等于</asp:ListItem>
                                                            <asp:ListItem Value="5">小于</asp:ListItem>
                                                            <asp:ListItem Value="6">小于或等于</asp:ListItem>
                                                            
                                                        </asp:DropDownList>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="90px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="数值">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="TextBoxValue" runat="server" Width="128px"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="140px" />
                                                </asp:TemplateField>
                                            </Columns>
                                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                            <RowStyle BackColor="#EFF3FB" />
                                            <PagerStyle CssClass="bomcolor" ForeColor="#EEF7FD" HorizontalAlign="Center" />
                                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                            <HeaderStyle BackColor="#A8B7EC" Font-Bold="True" ForeColor="White" />
                                            <EditRowStyle BackColor="#2461BF" />
                                            <AlternatingRowStyle BackColor="White" />
                                        </asp:GridView>
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </asp:Panel>
            </div>
        </div>
    </div>
    <div class="box-wrapper">
        <div class="box-outer">
            <asp:UpdatePanel ID="UpdatePanelBody" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <div style="width: 100%">
                        <table style="width: 100%">
                            <tr>
                                <td style="width: 15%">
                                    <asp:Panel ID="Panel_Operation" runat="server" Visible="false">
                                        &nbsp;&nbsp;&nbsp;<input id="all" type="button" value="全选" onclick="allsel()" />&nbsp;&nbsp;&nbsp;
                                        <input id="continue" type="button" value="连选" onclick="consel()" />&nbsp;&nbsp;&nbsp;
                                        <input id="Button1" type="button" value="取消" onclick="cancelsel()" />
                                    </asp:Panel>
                                </td>
                                <td valign="middle" align="right" style="width: 30%">
                                    <asp:DropDownList ID="DropDownListType" runat="server" OnSelectedIndexChanged="TypeOrOrderBy_SelectedIndexChanged"
                                        AutoPostBack="True">
                                        <asp:ListItem Value="MaterialCode" Selected="True">物料代码</asp:ListItem>
                                        <asp:ListItem Value="MaterialName">物料名称</asp:ListItem>
                                        <asp:ListItem Value="Attribute">材质</asp:ListItem>
                                        <asp:ListItem Value="Standard">规格</asp:ListItem>
                                        <asp:ListItem Value="PTC">计划号</asp:ListItem>
                                        <asp:ListItem Value="LotNumber">批号</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td valign="top" align="left" style="width: 55%">
                                    <asp:RadioButtonList ID="RadioButtonListOrderBy" runat="server" RepeatDirection="Horizontal"
                                        BorderStyle="None" OnSelectedIndexChanged="TypeOrOrderBy_SelectedIndexChanged"
                                        AutoPostBack="True">
                                        <asp:ListItem Value="1">降序</asp:ListItem>
                                        <asp:ListItem Value="0" Selected="True">升序</asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <asp:Panel ID="PanelBody" runat="server">
                        <asp:Panel ID="NoDataPanel" runat="server" Visible="false">
                            没有相关物料信息!</asp:Panel>
                        <table id="GridView1">
                            <asp:Repeater ID="Repeater1" runat="server" OnItemDataBound="Repeater1_ItemDataBound">
                                <HeaderTemplate>
                                    <tr>
                                        <td>
                                            序号
                                        </td>
                                        <td>
                                            计划跟踪号
                                        </td>
                                        <td>
                                            物料代码
                                        </td>
                                        <td>
                                            物料名称
                                        </td>
                                        <td>
                                            规格型号
                                        </td>
                                        <td>
                                            材质
                                        </td>
                                        <td>
                                            是否定尺
                                        </td>
                                        <td>
                                            长
                                        </td>
                                        <td>
                                            宽
                                        </td>
                                        <td>
                                            国标
                                        </td>
                                        <td>
                                            批号
                                        </td>
                                        <td>
                                            单位
                                        </td>
                                        <td>
                                            数量
                                        </td>
                                        <td>
                                            张(支)
                                        </td>
                                        <td>
                                            仓库名称
                                        </td>
                                        <td>
                                            仓位名称
                                        </td>
                                        <td>
                                            订单编号
                                        </td>
                                        <td>
                                            计划模式
                                        </td>
                                        <td>
                                            标识号
                                        </td>
                                        <td>
                                            备注
                                        </td>
                                        
                                        <td>
                                            MTO调出数量
                                        </td>
                                        <td>
                                            MTO调出张(支)
                                        </td>
                                    </tr>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td>
                                            <asp:CheckBox ID="CheckBox1" runat="server" /><%#Container.ItemIndex + Convert.ToInt32(Eval("RowIndex"))%>
                                            <asp:Label ID="LabelSQCODE" runat="server" Text='<%#Eval("SQCODE")%>' Visible="false"></asp:Label>
                                            <asp:HiddenField ID="hid_ptc" runat="server" Value='<%#Eval("PTC")%>' />
                                            <asp:Label ID="lb_symbol" runat="server" Width="25px" Height="12px" Visible="false" ToolTip="该项目提过物料代用"></asp:Label>
                                        </td>
                                        <td>
                                            <%#Eval("PTC")%>
                                        </td>
                                        <td>
                                            <%#Eval("MaterialCode")%>
                                        </td>
                                        <td>
                                            <%#Eval("MaterialName")%>
                                        </td>
                                        <td>
                                            <%#Eval("MaterialStandard")%>
                                        </td>
                                        <td>
                                            <%#Eval("Attribute")%>
                                        </td>
                                        <td>
                                            <%#Eval("Fixed")%>
                                        </td>
                                        <td>
                                            <%#Eval("Length")%>
                                        </td>
                                        <td>
                                            <%#Eval("Width")%>
                                        </td>
                                        <td>
                                            <%#Eval("GB")%>
                                        </td>
                                        <td>
                                            <%#Eval("LotNumber")%>
                                        </td>
                                        <td>
                                            <%#Eval("Unit")%>
                                        </td>
                                        <td>
                                            <asp:Label ID="LabelNumber" runat="server" Text='<%#Eval("Number")%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="LabelQuantity" runat="server" Text='<%#Eval("Quantity")%>'></asp:Label>
                                        </td>
                                        <td>
                                            <%#Eval("Warehouse")%>
                                        </td>
                                        <td>
                                            <%#Eval("Position")%>
                                        </td>
                                        <td>
                                            <%#Eval("OrderCode")%>
                                        </td>
                                        <td>
                                            <%#Eval("PlanMode")%>
                                        </td>
                                        <td>
                                            <%#Eval("CGMODE")%>
                                        </td>
                                        <td>
                                            <%#Eval("Comment")%>
                                        </td>
                                        <td>
                                            <%#Eval("OUTTZNUM")%>
                                        </td>
                                        <td>
                                            <%#Eval("OUTTZFZNUM")%>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <tr>
                                        <td>
                                        </td>
                                        <td>
                                            总计：
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                            <asp:Label ID="LabelTN" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="LabelTP" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                </FooterTemplate>
                            </asp:Repeater>
                        </table>
                    </asp:Panel>
                    <uc1:UCPaging ID="UCPaging1" runat="server" />

                    <script type="text/javascript">
  
document.getElementById("GridView1").parentNode.className = "fakeContainer";

(function() {
    superTable("GridView1", {
        cssSkin : "Default",
        fixedCols : 2,
        onFinish : function () 
        {  
                for (var i=0, j=this.sDataTable.tBodies[0].rows.length-1; i<j; i++) 
                {
                    
                    this.sDataTable.tBodies[0].rows[i].onclick = this.sFDataTable.tBodies[0].rows[i].onclick = function (i) 
                    {
                        var clicked = false;
                        var dataRow = this.sDataTable.tBodies[0].rows[i];
                        var fixedRow = this.sFDataTable.tBodies[0].rows[i];

                        return function () 
                        {
                            if (clicked) 
                            {
                              
                                dataRow.style.backgroundColor = "#ffffff";
                                fixedRow.style.backgroundColor = "#e4ecf7";
                                fixedRow.getElementsByTagName("td")[0].getElementsByTagName("input")[0].checked=false;
                                clicked = false;
                             
                            }
                            else 
                            {
                              if( fixedRow.getElementsByTagName("td")[0].getElementsByTagName("input")[0]!=null)
                              {
                                dataRow.style.backgroundColor = "#BFDFFF";
                                fixedRow.style.backgroundColor = "#409FFF";
                                 
                                fixedRow.getElementsByTagName("td")[0].getElementsByTagName("input")[0].checked=true;
                                clicked = true;
                              }
                            }
                        }
                    }.call(this, i);
                }
             return this;
       }
    });
})();

function allsel()
{
    for (var i=0, j=this.sDataTable.tBodies[0].rows.length-1; i<j; i++) 
    {
//    减1，减的是表尾
       this.sFDataTable.tBodies[0].rows[i].getElementsByTagName("td")[0].getElementsByTagName("input")[0].checked=true;
       this.sDataTable.tBodies[0].rows[i].style.backgroundColor = "#BFDFFF";
       this.sFDataTable.tBodies[0].rows[i].style.backgroundColor = "#409FFF";
       this.sDataTable.tBodies[0].rows[i].onclick = this.sFDataTable.tBodies[0].rows[i].onclick = function (i) 
       {
            var clicked = this.sFDataTable.tBodies[0].rows[i].getElementsByTagName("td")[0].getElementsByTagName("input")[0].checked;
            var dataRow = this.sDataTable.tBodies[0].rows[i];
            var fixedRow = this.sFDataTable.tBodies[0].rows[i];
           
            return function () 
            {
                if (clicked) 
                {
                    dataRow.style.backgroundColor = "#ffffff";
                    fixedRow.style.backgroundColor = "#e4ecf7";
                    fixedRow.getElementsByTagName("td")[0].getElementsByTagName("input")[0].checked=false;
                    clicked = false;
                }
                else 
                {
                     dataRow.style.backgroundColor = "#BFDFFF";
                     fixedRow.style.backgroundColor = "#409FFF";
                    fixedRow.getElementsByTagName("td")[0].getElementsByTagName("input")[0].checked=true;
                    clicked = true;
                }
            }
        }.call(this, i);
    }
 return this;
}

function cancelsel()
{
   for (var i=0, j=this.sDataTable.tBodies[0].rows.length-1; i<j; i++) 
   {
        this.sFDataTable.tBodies[0].rows[i].getElementsByTagName("td")[0].getElementsByTagName("input")[0].checked=false;
        this.sFDataTable.tBodies[0].rows[i].style.backgroundColor = "#e4ecf7";
        this.sDataTable.tBodies[0].rows[i].style.backgroundColor = "#ffffff";
        this.sDataTable.tBodies[0].rows[i].onclick = this.sFDataTable.tBodies[0].rows[i].onclick = function (i) 
        {
            var clicked = false;
            var dataRow = this.sDataTable.tBodies[0].rows[i];
            var fixedRow = this.sFDataTable.tBodies[0].rows[i];
           
            return function () 
            {
                if (clicked) 
                {
                    dataRow.style.backgroundColor = "#ffffff";
                    fixedRow.style.backgroundColor = "#e4ecf7";
                    fixedRow.getElementsByTagName("td")[0].getElementsByTagName("input")[0].checked=false;
                    clicked = false;
                }
                else 
                {
                     dataRow.style.backgroundColor = "#BFDFFF";
                     fixedRow.style.backgroundColor = "#409FFF";
                     fixedRow.getElementsByTagName("td")[0].getElementsByTagName("input")[0].checked=true;
                     clicked = true;
                }
            }
        }.call(this, i);
    }
 return this;
}

function consel()
{
    for (var i=0; i<this.sDataTable.tBodies[0].rows.length-1; i++) 
    {
      obj=this.sFDataTable.tBodies[0].rows[i].getElementsByTagName("td")[0].getElementsByTagName("input")[0];
      if(obj.type.toLowerCase()=="checkbox" && obj.value!="")
      {
       if(obj.checked)
        {
              obj.checked=true;
              for (var j=i+1; j<this.sDataTable.tBodies[0].rows.length-1; j++) 
              {
                  var nextobj=this.sFDataTable.tBodies[0].rows[j].getElementsByTagName("td")[0].getElementsByTagName("input")[0];
                  if(nextobj!=null)
                  {
                        if(nextobj.type.toLowerCase()=="checkbox" && nextobj.value!="")
                        {
                            if(nextobj.checked)
                            {
                                for(var k=i+1;k<j;k++)
                                {
                                    this.sFDataTable.tBodies[0].rows[k].getElementsByTagName("td")[0].getElementsByTagName("input")[0].checked=true;
                                    this.sDataTable.tBodies[0].rows[k].style.backgroundColor = "#BFDFFF";
                                    this.sFDataTable.tBodies[0].rows[k].style.backgroundColor ="#409FFF";
                                }
                              break;
                            }
                        } 
                  } 
              }
        }
      }
      this.sDataTable.tBodies[0].rows[i].onclick = this.sFDataTable.tBodies[0].rows[i].onclick = function (i) 
      {
            var clicked = this.sFDataTable.tBodies[0].rows[i].getElementsByTagName("td")[0].getElementsByTagName("input")[0].checked;
            var dataRow = this.sDataTable.tBodies[0].rows[i];
            var fixedRow = this.sFDataTable.tBodies[0].rows[i];
           
            return function () 
            {
                if (clicked) 
                {
                    dataRow.style.backgroundColor = "#ffffff";
                    fixedRow.style.backgroundColor = "#e4ecf7";
                    fixedRow.getElementsByTagName("td")[0].getElementsByTagName("input")[0].checked=false;
                    clicked = false;
                }
                else 
                {
                     dataRow.style.backgroundColor = "#BFDFFF";
                     fixedRow.style.backgroundColor = "#409FFF";
                    fixedRow.getElementsByTagName("td")[0].getElementsByTagName("input")[0].checked=true;
                    clicked = true;
                }
            }
        }.call(this, i);
    }
 return this;
}


                    </script>

                </ContentTemplate>
            </asp:UpdatePanel>
            <div id="AlertDiv" class="AlertStyle">
                <img id="laoding" src="../Assets/images/ajaxloader.gif" alt="downloading" />
            </div>
        </div>
        <!--box-outer END -->
    </div>
    <!--box-wrapper END -->
</asp:Content>
