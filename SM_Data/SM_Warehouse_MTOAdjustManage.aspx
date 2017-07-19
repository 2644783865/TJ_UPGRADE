<%@ Page Language="C#" MasterPageFile="~/Masters/SMBaseMaster.master" AutoEventWireup="true"
    CodeBehind="SM_Warehouse_MTOAdjustManage.aspx.cs" Inherits="ZCZJ_DPF.SM_Data.SM_Warehouse_MTOAdjustManage"
    Title="MTO管理" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="../Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="uc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
    <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </cc1:ToolkitScriptManager>
    <link href="StyleFile/superTables.css" rel="stylesheet" type="text/css" />
    <link href="StyleFile/superTables_Default.css" rel="stylesheet" type="text/css" />

    <script src="SM_JS/superTables.js" type="text/javascript"></script>
    <script src="SM_JS/SelectCondition.js" type="text/javascript"></script>
    <script type="text/javascript" language="javascript">

var postBack=true;
         
Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(BeginRequestHandler);
Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
function BeginRequestHandler(sender, args)
{
   if (args.get_postBackElement().id == "<%= btnClose.ClientID %>"||args.get_postBackElement().id == "<%= btnReset.ClientID %>"||args.get_postBackElement().id == "<%= drp_Warhouse.ClientID %>")
   {
     postBack=false;
   }
   else
   {
    ActivateAlertDiv('visible', 'AlertDiv', '');
   }
}
function EndRequestHandler(sender, args)
{
if(postBack){
    document.getElementById("GridView1").parentNode.className = "fakeContainer";

    (function() {
        superTable("GridView1", {
            cssSkin : "Default",
           fixedCols : 2,
           onFinish : function () 
              {             
                for (var i=0, j=this.sDataTable.tBodies[0].rows.length-2; i<j; i++) 
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
                                        clicked = false;
                                    }
                                    else 
                                    {
                                        dataRow.style.backgroundColor = "#eeeeee";
                                        fixedRow.style.backgroundColor = "#adadad";
                                        clicked = true;
                                    }
                                }
                                }.call(this, i);
                    
                                this.sDataTable.tBodies[0].rows[i].ondblclick= this.sFDataTable.tBodies[0].rows[i].ondblclick = function (i) 
                                {
                                   var dataRow = this.sDataTable.tBodies[0].rows[i];
                                   var fixedRow = this.sFDataTable.tBodies[0].rows[i];
                                    return function () 
                                    {
                                        if(fixedRow.getElementsByTagName("td")[1].getElementsByTagName("span")[0]!=null)
                                        {
                                           var id = fixedRow.getElementsByTagName("td")[1].getElementsByTagName("span")[0].innerHTML;
                    
                                           window.open("SM_Warehouse_MTOAdjust.aspx?FLAG=OPEN&&ID=" + id);
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
function ActivateAlertDiv(visstring, elem, msg)
{
     var adiv = $get(elem);
     adiv.style.visibility = visstring;                
}

function tostorage() {

        var date=new Date();
        var time=date.getTime();

    window.open('SM_Warehouse_Query.aspx?FLAG=PUSHMTO&id='+time);
}

//function mtoexport() {
//  var date=new Date();
//  var time=date.getTime();
//  var retVal = window.showModalDialog("SM_WarehouseMTO_Export.aspx?id="+time, "", "dialogWidth=800px;dialogHeight=600px;status=no;help=no;scroll=yes");
//}

function MTOOutExport() {
  var date=new Date();
  var time=date.getTime();
  var retVal = window.showModalDialog("SM_WarehouseMTO_Export.aspx?id="+time, "", "dialogWidth=800px;dialogHeight=600px;status=no;help=no;scroll=yes");
}

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
                            <asp:CheckBox ID="CheckBoxShow" runat="server" AutoPostBack="true" Text="单据头完整显示"
                                OnCheckedChanged="CheckBoxShow_CheckedChanged" />
                            <asp:Label ID="LabelMessage" runat="server" ForeColor="Red" Visible="false"></asp:Label>                            
                            <asp:HiddenField ID="hfdTotalAdjN" runat="server" />
                            <asp:HiddenField ID="hfdTotalAdjQN" runat="server" />
                            <asp:HiddenField ID="hfdTotalWN" runat="server" />                            
                            <asp:HiddenField ID="hfdTotalWQN" runat="server" />
                        </td>
                        <td align="right">
                            <asp:Button ID="btnShowPopup" runat="server" Text="筛选" OnClientClick="viewCondition()" />&nbsp;&nbsp;&nbsp;
                            <cc1:ModalPopupExtender ID="ModalPopupExtenderSearch" runat="server" TargetControlID="btnShowPopup"
                                PopupControlID="UpdatePanelCondition" Drag="false" Enabled="True" DynamicServicePath=""
                                Y="10">
                            </cc1:ModalPopupExtender>
                            <%--<input id="MTOExport" type="button" value="导出" onclick="mtoexport()" runat="server" />&nbsp;&nbsp;&nbsp;--%>
                            
                            <asp:Button runat="server"  ID="MTOOutExport" OnClick="MTOOutExport_Click"  Text="导出"/>&nbsp;&nbsp;&nbsp;
                            <input id="ToStorage" type="button" value="到库存" onclick="tostorage()" runat="server" />&nbsp;&nbsp;&nbsp;
                        </td>
                    </tr>
                </table>
                <asp:Panel ID="PanelCondition" runat="server" Width="98%" Style="display: none">
                    <asp:UpdatePanel ID="UpdatePanelCondition" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <table width="98%" style="background-color: #CCCCFF; border: solid 1px black;">
                                <tr>
                                    <td style="white-space: nowrap;" align="left">
                                        &nbsp;
                                    </td>
                                    <td style="white-space: nowrap;" align="left">
                                        &nbsp;
                                    </td>
                                    <td style="white-space: nowrap;" align="left">
                                        &nbsp;
                                    </td>
                                    <td style="white-space: nowrap;" align="left">
                                        <asp:Button ID="btnQuery" runat="server" OnClick="Query_Click" Text="查询" />&nbsp;&nbsp;&nbsp;
                                        <asp:Button ID="btnClose" runat="server" Text="取消" OnClick="btnClose_Click" />&nbsp;&nbsp;&nbsp;
                                        <asp:Button ID="btnReset" runat="server" Text="重置" OnClick="btnReset_Click" />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 25%; white-space: nowrap;" align="left">
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;MTO状态：<asp:DropDownList ID="DropDownListState" runat="server">
                                            <asp:ListItem Text="--请选择--" Value=""></asp:ListItem>
                                            <asp:ListItem Text="未审核" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="已审核" Value="2"></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td style="width: 25%; white-space: nowrap;" align="left">
                                                  &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 逻辑：<asp:DropDownList ID="DropDownListFatherLogic" runat="server">
                                                        <asp:ListItem Value="AND" Selected="True">并且</asp:ListItem>
                                                        <asp:ListItem Value="OR">或者</asp:ListItem>
                                                        <asp:ListItem></asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                    </td>
                                    <td style="width: 25%; white-space: nowrap;" align="left">
                                    </td>
                                    <td style="width: 25%; white-space: nowrap;" align="left">
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" valign="top">
                                        <table width="100%" >
                                            <tr>
                                                <td style="width: 50%; white-space: nowrap;" align="left">
                                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;编&nbsp;&nbsp;&nbsp;号：<asp:TextBox ID="TextBoxMTOCode" runat="server"></asp:TextBox>
                                                </td>
                                                <td style="width: 50%; white-space: nowrap;" align="left">
                                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;日&nbsp;&nbsp;&nbsp;期：<asp:TextBox ID="TextBoxDate" runat="server"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 50%; white-space: nowrap;" align="left">
                                                   &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;部&nbsp;&nbsp;&nbsp;门：<asp:TextBox
                                                        ID="TextBoxDep" runat="server"></asp:TextBox>
                                                </td>
                                                <td style="width: 50%; white-space: nowrap;" align="left">
                                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;计划员：<asp:TextBox ID="TextBoxPlaner"
                                                        runat="server"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 50%; white-space: nowrap;" align="left">
                                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;物料代码：<asp:TextBox ID="TextBoxCode" runat="server"></asp:TextBox>
                                                </td>
                                                <td style="width: 50%; white-space: nowrap;" align="left">
                                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;物料名称：<asp:TextBox ID="TextBoxName" runat="server"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 50%; white-space: nowrap;" align="left">
                                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;型号规格：<asp:TextBox ID="TextBoxGuiGe" runat="server"></asp:TextBox>
                                                </td>
                                                <td style="width: 50%; white-space: nowrap;" align="left">
                                                   &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;材&nbsp;&nbsp;&nbsp;质：<asp:TextBox ID="TextBoxCaiZhi" runat="server"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 50%; white-space: nowrap;" align="left">
                                                    从计划跟踪号：<asp:TextBox ID="TextBoxPTCFrom" runat="server"></asp:TextBox>
                                                </td>
                                                <td style="width: 50%; white-space: nowrap;" align="left">
                                                    到计划跟踪号：<asp:TextBox ID="TextBoxPTCTo" runat="server"></asp:TextBox>
                                                </td>
                                            </tr>
                                             <tr>
                                                <td style="width: 50%; white-space: nowrap;" align="left">
                                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;仓&nbsp;&nbsp;&nbsp;库：<asp:DropDownList ID="drp_Warhouse" runat="server" AutoPostBack="true" OnSelectedIndexChanged="drp_Warhouse_Changed"></asp:DropDownList> </td>
                                                <td style="width: 50%; white-space: nowrap;" align="left">
                                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;仓&nbsp;&nbsp;&nbsp;位：<asp:DropDownList ID="drp_xposition" runat="server"></asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 50%; white-space: nowrap;" align="left">
                                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;调整数量：<asp:TextBox ID="TextBoxTiaozh" runat="server"></asp:TextBox>
                                                </td>
                                                <td style="width: 50%; white-space: nowrap;" align="left">
                                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;可调数量：<asp:TextBox ID="TextBoxKetiao" runat="server"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 50%; white-space: nowrap;" align="left">
                                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;批&nbsp;&nbsp;&nbsp;号：<asp:TextBox ID="TextBoxPhao" runat="server"></asp:TextBox>
                                                </td>
                                                <td style="width: 50%; white-space: nowrap;" align="left">
                                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;制单人：<asp:TextBox ID="TextBoxZHDan" runat="server"></asp:TextBox>
                                                </td>
                                            </tr>
                                             <tr>
                                                <td style="width: 50%; white-space: nowrap;" align="left">
                                                  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;审核人：<asp:TextBox ID="TextBoxshher" runat="server"></asp:TextBox>
                                                </td>
                                                <td style="width: 50%; white-space: nowrap;" align="left">
                                                  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;备&nbsp;&nbsp;&nbsp;注：<asp:TextBox ID="TextBoxBz" runat="server"></asp:TextBox>
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
                                <tr>
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
                    <asp:Panel ID="PanelBody" runat="server">
                        <asp:Panel ID="NoDataPanel" runat="server" Visible="false">
                            没有相关记录!</asp:Panel>
                        <table id="GridView1">
                            <asp:Repeater ID="Repeater1" runat="server" OnItemDataBound="Repeater1_ItemDataBound">
                                <HeaderTemplate>
                                    <tr>
                                        <td>
                                        </td>
                                        <td>
                                            单据编号
                                        </td>
                                        <td>
                                            物料代码
                                        </td>
                                        <td>
                                            物料名称
                                        </td>
                                        <td>
                                            型号规格
                                        </td>
                                        <td>
                                            材质
                                        </td>
                                        <td>
                                            批号
                                        </td>
                                        <td>
                                            长
                                        </td>
                                        <td>
                                            宽
                                        </td>
                                        <td>
                                            到计划跟踪号
                                        </td>
                                        <td>
                                            单位
                                        </td>
                                        <td>
                                            调整数量
                                        </td>
                                        <td>
                                            调整张(支)数
                                        </td>
                                        <td>
                                            从计划跟踪号
                                        </td>
                                        <td>
                                            可调数量
                                        </td>
                                        <td>
                                            可调张(支)数
                                        </td>
                                        <td>
                                            仓库
                                        </td>
                                        <td>
                                            仓位
                                        </td>
                                        <td>
                                            制单人
                                        </td>
                                        <td>
                                            审核人
                                        </td>
                                        <td>
                                            审核日期
                                        </td>
                                        <td>
                                            计划员
                                        </td>
                                        <td>
                                            部门
                                        </td>
                                        <td>
                                            审核状态
                                        </td>
                                    </tr>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td>
                                            <%#Container.ItemIndex+1%>
                                        </td>
                                        <td>
                                            <asp:Label ID="LabelCode" runat="server" Text='<%#Eval("Code")%>' Width="80px"></asp:Label>
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
                                            <%#Eval("LotNumber")%>
                                        </td>
                                        <td>
                                            <%#Eval("Length")%>
                                        </td>
                                        <td>
                                            <%#Eval("Width")%>
                                        </td>
                                        <td>
                                            <%#Eval("PTCTo")%>
                                        </td>
                                        <td>
                                            <%#Eval("Unit")%>
                                        </td>
                                        <td>
                                            <asp:Label ID="LabelAdjN" runat="server" Text='<%#Eval("AdjN")%>' Width="60px"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="LabelAdjQN" runat="server" Text='<%#Eval("AdjQN")%>' Width="80px"></asp:Label>
                                        </td>
                                        <td>
                                            <%#Eval("PTCFrom")%>
                                        </td>
                                        <td>
                                            <asp:Label ID="LabelWN" runat="server" Text='<%#Eval("WN")%>' Width="60px"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="LabelWQN" runat="server" Text='<%#Eval("WQN")%>' Width="80px"></asp:Label>
                                        </td>
                                        <td>
                                            <%#Eval("Warehouse")%>
                                        </td>
                                        <td>
                                            <%#Eval("Position")%>
                                        </td>
                                        <td>
                                            <%#Eval("Document")%>
                                        </td>
                                        <td>
                                            <%#Eval("Verifier")%>
                                        </td>
                                        <td>
                                            <%#Eval("ApproveDate")%>
                                        </td>
                                        <td>
                                            <%#Eval("Planer")%>
                                        </td>
                                        <td>
                                            <%#Eval("Dep")%>
                                        </td>
                                        <td>
                                            <%#convertState((string)Eval("State"))%>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <tr>
                                        <td>
                                        </td>
                                        <td>
                                            合计：
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
                                            <asp:Label ID="LabelTotalAdjN" runat="server"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="LabelTotalAdjQN" runat="server"></asp:Label>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                            <asp:Label ID="LabelTotalWN" runat="server"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="LabelTotalWQN" runat="server"></asp:Label>
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
                                            <asp:Label ID="TotalAdjN" runat="server"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="TotalAdjQN" runat="server"></asp:Label>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                            <asp:Label ID="TotalWN" runat="server"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="TotalWQN" runat="server"></asp:Label>
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
                for (var i=0, j=this.sDataTable.tBodies[0].rows.length-2; i<j; i++) 
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
                                clicked = false;
                            }
                            else 
                            {
                                dataRow.style.backgroundColor = "#eeeeee";
                                fixedRow.style.backgroundColor = "#adadad";
                                clicked = true;
                            }
                        }
                    }.call(this, i);
                    
                     this.sDataTable.tBodies[0].rows[i].ondblclick= this.sFDataTable.tBodies[0].rows[i].ondblclick = function (i) 
                    {
                        var dataRow = this.sDataTable.tBodies[0].rows[i];
                        var fixedRow = this.sFDataTable.tBodies[0].rows[i];
                        
                        return function () 
                        {
                            if(fixedRow.getElementsByTagName("td")[1].getElementsByTagName("span")[0]!=null)
                            {
                               var id = fixedRow.getElementsByTagName("td")[1].getElementsByTagName("span")[0].innerHTML;
        
                               window.open("SM_Warehouse_MTOAdjust.aspx?FLAG=OPEN&&ID=" + id);
                             }
                        }
                    }.call(this, i);
                }
             return this;
       }
    });
})();

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
