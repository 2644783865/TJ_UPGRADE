<%@ Page Language="C#" MasterPageFile="~/Masters/SMBaseMaster.master" AutoEventWireup="true"
    CodeBehind="SM_WarehouseIN_WGPush.aspx.cs" Inherits="ZCZJ_DPF.SM_Data.SM_WarehouseIN_WGPush"
    Title="订单管理" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="../Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PrimaryContent" runat="server">

    <script src="SM_JS/SelectCondition.js" type="text/javascript"></script>

    <script src="SM_JS/superTables.js" type="text/javascript"></script>

    <link href="StyleFile/superTables.css" rel="stylesheet" type="text/css" />
    <link href="StyleFile/superTables_Default.css" rel="stylesheet" type="text/css" />
    <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnableScriptGlobalization="True">
    </cc1:ToolkitScriptManager>

    <script type="text/javascript" language="javascript">
  var postBack=true;
Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(BeginRequestHandler);
Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
function BeginRequestHandler(sender, args)
{
if (args.get_postBackElement().id == "<%= btnClose.ClientID %>"||args.get_postBackElement().id == "<%= btnReset.ClientID %>")
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
document.getElementById("GridView1").parentNode.className = "fakeContainerNew";

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
           if(fixedRow.getElementsByTagName("td")[0].getElementsByTagName("input")[0]!=null)
           {
                dataRow.style.backgroundColor = "#ffffff";
                fixedRow.style.backgroundColor = "#e4ecf7";
           
                fixedRow.getElementsByTagName("td")[0].getElementsByTagName("input")[0].checked=false;
                clicked = false;
            }
        }
        else 
        {
          if(fixedRow.getElementsByTagName("td")[0].getElementsByTagName("input")[0]!=null)
           {
               dataRow.style.backgroundColor = "#BFDFFF";
               fixedRow.style.backgroundColor = "#409FFF";
         
                fixedRow.getElementsByTagName("td")[0].getElementsByTagName("input")[0].checked=true;
                clicked = true;
            }
        }
    }
}.call(this, i);

 this.sDataTable.tBodies[0].rows[i].ondblclick= this.sFDataTable.tBodies[0].rows[i].ondblclick = function (i) 
 {
 
   var dataRow = this.sDataTable.tBodies[0].rows[i];
   var fixedRow = this.sFDataTable.tBodies[0].rows[i];
    return function () 
    {
       if(dataRow.getElementsByTagName("td")[0].getElementsByTagName("span")[0]!=null)
       {
           var id = dataRow.getElementsByTagName("td")[0].getElementsByTagName("span")[0].innerHTML;
           window.open("../PC_Data/PC_TBPC_PurOrder.aspx?FLAG=PUSHREAD&&orderno=" + id);
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


function PushConfirm() 
{
    var retVal = confirm("确定将所选定项目下推生成入库单？");
    return retVal;
}

function Confirmapp() {
    window.returnValue = true;
    window.close();
}

function Cancelapp() {
    window.returnValue = false;
    window.close();
}
function chakan(tr) {
     
var id = tr.getElementsByTagName("td")[4].getElementsByTagName("span")[0].innerHTML;

window.open("../PC_Data/PC_TBPC_PurOrder.aspx?FLAG=PUSHREAD&&orderno=" + id);
        
}

function ShowResultModal(id) {
     
window.open("../QC_Data/QC_Inspection_Add.aspx?ACTION=VIEW&&back=1&&id="+id);
        
}


function closewindow() {
    window.close();
}
function viewCondition(){
   document.getElementById("<%=PanelCondition.ClientID%>").style.display='block';
}

function orderexport() {
     var date=new Date();
        var time=date.getTime();
        var retVal = window.showModalDialog("SM_WarehouseIN_WGPush_Export.aspx?id="+time, "", "dialogWidth=880px;dialogHeight=600px;help=no;scroll=yes");
    }

function init()
{
    window.moveTo(0,0);//移动窗口到0,0坐标
    window.resizeTo(window.screen.availWidth,window.screen.availHeight);
}
document.onload=init();

    </script>

    <div class="box-inner">
        <div class="box_right">
            <div class="box-title">
                <table width="98%">
                    <tr>
                        <td align="left">
                            <asp:Label ID="LabelMessage" runat="server" ForeColor="Red"></asp:Label>
                        </td>
                        <td align="right">
                            <asp:Button ID="Append" runat="server" Text="追加" OnClick="Append_Click" />&nbsp;&nbsp;&nbsp;
                            <input id="cancel" type="button" value="取消" runat="server" onclick="Cancelapp()" />&nbsp;&nbsp;&nbsp;
                            <asp:Button ID="btnShowPopup" runat="server" Text="筛选" OnClientClick="viewCondition()" />&nbsp;&nbsp;&nbsp;
                            <cc1:ModalPopupExtender ID="ModalPopupExtenderSearch" runat="server" TargetControlID="btnShowPopup"
                                PopupControlID="UpdatePanelCondition" Drag="false" Enabled="True" DynamicServicePath=""
                                Y="10">
                            </cc1:ModalPopupExtender>
                            <input id="InExport" type="button" value="导出" onclick="orderexport()" runat="server"
                                visible="false" />
                            <asp:Button ID="BtnShowExport" runat="server" Text="导出" OnClick="BtnShowExport_Click" />&nbsp;&nbsp;&nbsp;
                            <asp:Button ID="Push" runat="server" Text="下推" OnClick="Push_Click" OnClientClick="return PushConfirm()" />
                            &nbsp;&nbsp;
                            <input id="closewin" type="button" value="关闭" onclick="closewindow()" visible="false" runat="server" />&nbsp;&nbsp;
                        </td>
                    </tr>
                </table>
                <asp:Panel ID="PanelCondition" runat="server" Style="display: none">
                    <asp:UpdatePanel ID="UpdatePanelCondition" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <table width="100%" style="background-color: #CCCCFF; border: solid 1px black;">
                                <tr>
                                    <td align="left">
                                        &nbsp;&nbsp;显示条数:<asp:TextBox ID="TextBoxCount" runat="server"></asp:TextBox>
                                    </td>
                                    <td align="right">
                                        <asp:Button ID="btnQuery" runat="server" OnClick="Query_Click" Text="查询" />&nbsp;&nbsp;&nbsp;
                                        <asp:Button ID="btnClose" runat="server" Text="取消" OnClick="btnClose_Click" />&nbsp;&nbsp;&nbsp;
                                        <asp:Button ID="btnReset" runat="server" Text="重置" OnClick="btnReset_Click" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    </td>
                                </tr>
                                <td width="50%">
                                    <table cellpadding="4">
                                       <tr>
                                            <td style="white-space: nowrap;" align="left">
                                                订单编号：
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TextBoxOrderCode" runat="server"></asp:TextBox>
                                            </td>
                                            <td style="white-space: nowrap;" align="left">
                                                供应商：
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TextBoxSupplier" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                        <td style="white-space: nowrap;" align="left">
                                                部门：
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TextBoxDep" runat="server"></asp:TextBox>
                                            </td>
                                            <td style="white-space: nowrap;" align="left">
                                                业务员：
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TextBoxClerk" runat="server"></asp:TextBox>
                                            </td>
                                            
                                        </tr>
                                        <tr>
                                            <td style="white-space: nowrap;" align="left">
                                                下单日期：
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TextBoxDate" runat="server"></asp:TextBox>
                                                <cc1:CalendarExtender ID="DateTextBox_CalendarExtender" runat="server" Format="yyyy-MM-dd"
                                                    TargetControlID="TextBoxDate">
                                                </cc1:CalendarExtender>
                                            </td>
                                            <td style="white-space: nowrap;" align="left">
                                                交货日期：
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TextBoxJhuo" runat="server"></asp:TextBox>
                                                <cc1:CalendarExtender ID="CalendarExtenderJhuo" runat="server" Format="yyyy-MM-dd"
                                                    TargetControlID="TextBoxJhuo">
                                                </cc1:CalendarExtender>
                                            </td>
                                        </tr>
                                        
                                        <tr>
                                            <td style="white-space: nowrap;" align="left">
                                                物料代码：
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TextBoxCode" runat="server"></asp:TextBox>
                                            </td>
                                            <td style="white-space: nowrap;" align="left">
                                                物料名称：
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TextBoxName" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="white-space: nowrap;" align="left">
                                                规格型号：
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TextBoxStandard" runat="server"></asp:TextBox>
                                            </td>
                                            <td style="white-space: nowrap;" align="left">
                                                计划跟踪号：
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TextBoxPTC" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                           <tr>
                                            <td style="white-space: nowrap;" align="left">
                                                材质：
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TextBoxcaizhi" runat="server"></asp:TextBox>
                                            </td>
                                            <td style="white-space: nowrap;" align="left">
                                                国标：
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TextBoxgb" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                           <tr>
                                            <td style="white-space: nowrap;" align="left">
                                                订货数量：
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TextBoxDnum" runat="server"></asp:TextBox>
                                            </td>
                                            <td style="white-space: nowrap;" align="left">
                                                到货数量：
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TextBoxAnum" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                           <tr>
                                            <td style="white-space: nowrap;" align="left">
                                                标识号：
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TextBoxBSHNUM" runat="server"></asp:TextBox>
                                            </td>
                                            <td style="white-space: nowrap;" align="left">
                                                计划类型：
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TextBoxPlanTyple" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="white-space: nowrap;" align="left">
                                                订单状态：
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="DropDownListOrderState" runat="server">
                                                    <asp:ListItem Value="0">-请选择-</asp:ListItem>
                                                    <asp:ListItem Value="1" Selected="True">采购中</asp:ListItem>
                                                    <asp:ListItem Value="2">采购完成</asp:ListItem>
                                                    <asp:ListItem Value="3">手动关闭</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td style="white-space: nowrap;" align="left">
                                                行业务状态：
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="DropDownListPushState" runat="server">
                                                    <asp:ListItem Value="0">-请选择-</asp:ListItem>
                                                    <asp:ListItem Value="1" Selected="True">采购中</asp:ListItem>
                                                    <asp:ListItem Value="2">采购完成</asp:ListItem>
                                                    <asp:ListItem Value="3">手动关闭</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="white-space: nowrap;" align="left">
                                                质检结果：
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="DropDownListQCResult" runat="server">
                                                    <asp:ListItem Value="all" Selected="True">-请选择-</asp:ListItem>
                                                    <asp:ListItem Value="——">未报检</asp:ListItem>
                                                    <asp:ListItem Value="待检">待检</asp:ListItem>
                                                    <asp:ListItem Value="报废">报废</asp:ListItem>
                                                    <asp:ListItem Value="整改">整改</asp:ListItem>
                                                    <asp:ListItem Value="待定">待定</asp:ListItem>
                                                    <asp:ListItem Value="让步接收">让步接收</asp:ListItem>
                                                    <asp:ListItem Value="部分合格">部分合格</asp:ListItem>
                                                    <asp:ListItem Value="合格">合格</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td style="white-space: nowrap;" align="left">
                                                逻辑
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="DropDownListFatherLogic" runat="server">
                                                    <asp:ListItem Value="AND" Selected="True">并且</asp:ListItem>
                                                    <asp:ListItem Value="OR">或者</asp:ListItem>
                                                    <asp:ListItem></asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td width="50%">
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
                            </table>
                   </ContentTemplate>
                    </asp:UpdatePanel>
                </asp:Panel>
            </div>
        </div>
    </div>
    <div class="box-wrapper">
        <div class="box-outer">
            <div>
                &nbsp;&nbsp;&nbsp;<input id="all" type="button" value="全选" onclick="allsel()" />&nbsp;&nbsp;&nbsp;<input
                    id="continue" type="button" value="连选" onclick="consel()" />
                &nbsp;&nbsp;&nbsp;<input id="Button2" type="button" value="取消" onclick="cancelsel()" />
            </div>
            <asp:UpdatePanel ID="UpdatePanelBody" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:Panel ID="PanelBody" runat="server">
                        <asp:Panel ID="NoDataPanel" runat="server" Visible="False">
                            没有相关记录!</asp:Panel>
                        <table id="GridView1">
                            <asp:Repeater ID="Repeater1" runat="server" OnItemDataBound="Repeater1_ItemDataBound">
                                <HeaderTemplate>
                                    <tr align="center">
                                        <td>
                                            行号
                                        </td>
                                        <td>
                                            供应商
                                        </td>
                                        <td>
                                            订单编号
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
                                            型号规格
                                        </td>
                                        <td>
                                            材质
                                        </td>
                                        <td>
                                            国标
                                        </td>
                                        <td>
                                            长
                                        </td>
                                        <td>
                                            宽
                                        </td>
                                        <td>
                                            单位
                                        </td>
                                        <td>
                                            订货数量
                                        </td>
                                        <td>
                                            到货数量
                                        </td>
                                        <td>
                                            定额数量
                                        </td>
                                        <td>
                                            辅助单位
                                        </td>
                                        <td>
                                            订货辅助数量
                                        </td>
                                        <td>
                                            交货日期
                                        </td>
                                        <td>
                                            单价
                                        </td>
                                        <td>
                                            税率
                                        </td>
                                        <td>
                                            含税单价
                                        </td>
                                        <td>
                                            金额
                                        </td>
                                        <td>
                                            含税金额
                                        </td>
                                        <td>
                                            下单日期
                                        </td>
                                        <td>
                                            部门
                                        </td>
                                        <td>
                                            业务员
                                        </td>
                                        <td>
                                            订单状态
                                        </td>
                                        <td>
                                            行业务状态
                                        </td>
                                        <td>
                                            计划类型
                                        </td>
                                        <td>
                                            标识号
                                        </td>
                                        <td>
                                            质检结果
                                        </td>
                                        <td>
                                            备注
                                        </td>
                                    </tr>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td>
                                            <asp:CheckBox ID="CheckBox1" runat="server" CssClass="checkBoxCss" /><%#Container.ItemIndex + Convert.ToInt32(Eval("RowIndex"))%>
                                            <asp:Label ID="lbpurchangestate" runat="server" Width="20px" Height="12px" Text="" ToolTip="该计划跟踪号下物料提过物料减少" Visible="false"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="LabelSupplier" runat="server" Text='<%#Eval("Supplier")%>'></asp:Label>
                                            <asp:Label ID="LabelSupplierCode" runat="server" Text='<%#Eval("SupplierCode")%>'
                                                Visible="false"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="LabelCode" runat="server" Text='<%#Eval("Code")%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="LabelPTC" runat="server" Text='<%#Eval("PTC")%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="LabelMaterialCode" runat="server" Text='<%#Eval("MaterialCode")%>'></asp:Label>                                            
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
                                            <%#Eval("GB")%>
                                        </td>
                                        <td>
                                           <%#Eval("length")%>
                                        </td>
                                        <td>
                                            <%#Eval("width")%>
                                        </td>
                                        <td>
                                            <%#Eval("Unit")%>
                                        </td>
                                        <td>
                                          <asp:Label runat="server" ID="LabelNumber"  Text='<%#Eval("Number")%>'></asp:Label>
                                        </td>
                                        <td>
                                          <asp:Label runat="server" ID="LabelArrivedNumber"  Text='<%#Eval("ArrivedNumber")%>'></asp:Label>   
                                        </td>
                                        <td>
                                          <asp:Label runat="server" ID="lbdingenum"  Text='<%#Eval("dingenum")%>'></asp:Label>
                                        </td>
                                        <td>
                                            <%#Eval("marfzunit")%>
                                        </td>
                                        <td>
                                          <asp:Label runat="server" ID="LabelQUANTITY"  Text='<%#Eval("QUANTITY")%>'></asp:Label>
                                            
                                        </td>
                                        <td>
                                            <%#Eval("ArrivedDate")%>
                                        </td>
                                        <td>
                                            <%#Eval("UnitPrice")%>
                                        </td>
                                        <td>
                                            <%#Eval("TaxRate")%>%
                                        </td>
                                        <td>
                                            <%#Eval("CTUP")%>
                                        </td>
                                        <td>
                                          <asp:Label runat="server" ID="LabelAmount"  Text='<%#Eval("Amount")%>'></asp:Label>
                                            
                                        </td>
                                        <td>
                                          <asp:Label runat="server" ID="LabelCTA"  Text='<%#Eval("CTA")%>'></asp:Label>
                                            
                                        </td>
                                        <td>
                                            <%#Eval("Date")%>
                                        </td>
                                        <td>
                                            <%#Eval("Dep")%>
                                        </td>
                                        <td>
                                            <%#Eval("Clerk")%>
                                        </td>
                                        <td>
                                            <asp:Label ID="LabelOrderState" runat="server" Text='<%#ConvertOS((string)Eval("OrderState")) %>'></asp:Label>
                                            <asp:Label ID="LabelOS" runat="server" Text='<%#Eval("OrderState")%>' Visible="false"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="LabelPushState" runat="server" Text='<%#ConvertLS((string)Eval("PushState")) %>'></asp:Label>
                                            <asp:Label ID="LabelPS" runat="server" Text='<%#Eval("PushState")%>' Visible="false"></asp:Label>
                                        </td>
                                        <td>
                                            <%#Eval("PO_MASHAPE")%>
                                        </td>
                                        <td>
                                            <%#Eval("PO_TUHAO")%>
                                        </td>
                                        <td>
                                            <asp:Label ID="LabelResult" runat="server" Text='<%#Eval("RESULT")%>'></asp:Label>
                                        </td>
                                        <td>
                                            <%#Eval("detailnote")%>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <tr>
                                        <td>
                                        </td>
                                        <td>
                                            合计:
                                        </td>
                                        <td>
                                            <asp:Label ID="LabelCount" runat="server" Text=""></asp:Label>条记录
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
                                            <asp:Label ID="Labeldhnum" runat="server"></asp:Label>                                          
                                        </td>
                                        <td>
                                            <asp:Label ID="Labeltohnum" runat="server"></asp:Label>                                         
                                        </td>
                                        <td>
                                            <asp:Label ID="lbdingenumhj" runat="server"></asp:Label>                                         
                                        </td>
                                        <td>
                                        </td>
                                        <td> 
                                            <asp:Label ID="Labelfzhnum" runat="server"></asp:Label>
                                         
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
                                            <asp:Label ID="Labeljine" runat="server"></asp:Label>
                                        
                                        </td>
                                        <td>
                                            <asp:Label ID="Labeltaxjine" runat="server"></asp:Label>
                                        
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
                                    </tr>
                                </FooterTemplate>
                            </asp:Repeater>
                        </table>
                    </asp:Panel>
                    <uc1:UCPaging ID="UCPaging1" runat="server" />

                    <script type="text/javascript" language="javascript">

document.getElementById("GridView1").parentNode.className = "fakeContainerNew";

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
                               if(fixedRow.getElementsByTagName("td")[0].getElementsByTagName("input")[0]!=null)
                               {
                                    dataRow.style.backgroundColor = "#ffffff";
                                    fixedRow.style.backgroundColor = "#e4ecf7";
                               
                                    fixedRow.getElementsByTagName("td")[0].getElementsByTagName("input")[0].checked=false;
                                    clicked = false;
                                }
                            }
                            else 
                            {
                              if(fixedRow.getElementsByTagName("td")[0].getElementsByTagName("input")[0]!=null)
                               {
                                   dataRow.style.backgroundColor = "#BFDFFF";
                                   fixedRow.style.backgroundColor = "#409FFF";
                             
                                    fixedRow.getElementsByTagName("td")[0].getElementsByTagName("input")[0].checked=true;
                                    clicked = true;
                                }
                            }
                        }
                    }.call(this, i);
                    
                     this.sDataTable.tBodies[0].rows[i].ondblclick= this.sFDataTable.tBodies[0].rows[i].ondblclick = function (i) 
                     {
                     
                       var dataRow = this.sDataTable.tBodies[0].rows[i];
                       var fixedRow = this.sFDataTable.tBodies[0].rows[i];
                        return function () 
                        {
                           if(dataRow.getElementsByTagName("td")[0].getElementsByTagName("span")[0]!=null)
                           {
                               var id = dataRow.getElementsByTagName("td")[0].getElementsByTagName("span")[0].innerHTML;
                               window.open("../PC_Data/PC_TBPC_PurOrder.aspx?FLAG=PUSHREAD&&orderno=" + id);
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
                        if(fixedRow.getElementsByTagName("td")[0].getElementsByTagName("input")[0]!=null)
                        {
                            fixedRow.getElementsByTagName("td")[0].getElementsByTagName("input")[0].checked=false;
                            clicked = false;
                        }
                    }
                    else 
                    {
                       dataRow.style.backgroundColor = "#BFDFFF";
                       fixedRow.style.backgroundColor = "#409FFF";
                       if(fixedRow.getElementsByTagName("td")[0].getElementsByTagName("input")[0]!=null)
                       {
                            fixedRow.getElementsByTagName("td")[0].getElementsByTagName("input")[0].checked=true;
                            clicked = true;
                       }
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
                        if(fixedRow.getElementsByTagName("td")[0].getElementsByTagName("input")[0]!=null)
                        {
                            fixedRow.getElementsByTagName("td")[0].getElementsByTagName("input")[0].checked=false;
                            clicked = false;
                        }
                    }
                    else 
                    {
                        dataRow.style.backgroundColor = "#BFDFFF";
                        fixedRow.style.backgroundColor = "#409FFF";
                        if(fixedRow.getElementsByTagName("td")[0].getElementsByTagName("input")[0]!=null)
                        {
                            fixedRow.getElementsByTagName("td")[0].getElementsByTagName("input")[0].checked=true;
                            clicked = true;
                        }
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
                                   if(this.sFDataTable.tBodies[0].rows[k].getElementsByTagName("td")[0].getElementsByTagName("input")[0]!=null)
                                   {
                                        this.sFDataTable.tBodies[0].rows[k].getElementsByTagName("td")[0].getElementsByTagName("input")[0].checked=true;
                                        this.sDataTable.tBodies[0].rows[k].style.backgroundColor = "#BFDFFF";
                                        this.sFDataTable.tBodies[0].rows[k].style.backgroundColor = "#409FFF";
                                   }
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
                    if(fixedRow.getElementsByTagName("td")[0].getElementsByTagName("input")[0]!=null)
                    {
                        fixedRow.getElementsByTagName("td")[0].getElementsByTagName("input")[0].checked=false;
                        clicked = false;
                    }
                }
                else 
                {
                    dataRow.style.backgroundColor = "#BFDFFF";
                    fixedRow.style.backgroundColor = "#409FFF";
                    if(fixedRow.getElementsByTagName("td")[0].getElementsByTagName("input")[0]!=null)
                    {
                        fixedRow.getElementsByTagName("td")[0].getElementsByTagName("input")[0].checked=true;
                        clicked = true;
                    }
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
            <%--  <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanelBody" DisplayAfter="1">
        <ProgressTemplate>
                <img src="../Assets/images/ajaxloader.gif" style="position:absolute;left:48%;top:50%;" alt="downloading" />
        </ProgressTemplate>
        </asp:UpdateProgress>--%>
        </div>
        <!--box-outer END -->
    </div>
    <!--box-wrapper END -->
</asp:Content>
