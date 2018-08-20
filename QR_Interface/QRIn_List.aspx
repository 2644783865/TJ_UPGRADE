<%@ Page Language="C#" MasterPageFile="~/masters/BaseMaster.master" AutoEventWireup="true" CodeBehind="QRIn_List.aspx.cs" Inherits="ZCZJ_DPF.QR_Interface.QRIn_List" Title="扫码入库物料管理" %>
<%@ Register Src="~/Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PrimaryContent" runat="server">
    <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnableScriptGlobalization="True">
    </cc1:ToolkitScriptManager>

    <script type="text/javascript">


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






        //初始化弹窗
        $(function() {
            $("#win").show().dialog({
                title: '分配项目',
                width: 700,
                height: 500,
                closed: true,
                cache: false,
                modal: true,
                buttons: '#buttons'
            });
        });
        //初始化数据
        function showAssigedPlanno(obj) {
            var QRIn_ID=obj.parentNode.parentNode.getElementsByTagName("td")[0].getElementsByTagName("input")[1].value;
            $('#hidQRID').val(QRIn_ID);
            var MaterialCode=obj.parentNode.parentNode.getElementsByTagName("td")[0].getElementsByTagName("input")[2].value;
            $("#win").dialog("open");
            $('#dg').datagrid({
                        type: "GET",
                        url:'PTCAssignedAjaxHandler.aspx?method=getInPTC&MaterialCode='+MaterialCode,
                        pagination:false,
                        loadMsg: 'Loading……',
		                autoRowHeight:true,
		                remoteSort:true,
		                nowrap:false,
		                rownumbers: true,
		                singleSelect:true,
                        columns:[[
                            {field:'qrptcode',title:'计划跟踪号',align:'center',width:150},
                            {field:'orderno',title:'订单号',align:'center',width:100},
                            {field:'suppliernm',title:'供应商',align:'center',width:100},
		                    {field:'marid',title:'物料编码',align:'center',width:80},
		                    {field:'marnm',title:'物料名称',align:'center',width:80},
		                    {field:'margg',title:'型号规格',align:'center',width:50},
		                    {field:'marcz',title:'材质',align:'center',width:50}
	                    ]]
                  });
        }
        function savePlanno() {
            var QRIn_ID=$('#hidQRID').val();
            var row = $('#dg').datagrid('getSelected');
            var QRPTC="";
			if (row){
			    QRPTC=row.qrptcode;
			    $.ajax({
                    type: "POST",
                    url: 'PTCAssignedAjaxHandler.aspx',
                    data: {"QRIn_ID":QRIn_ID,"QRPTC":QRPTC,"method": "AssignedInPTC"},
                    success: function(msg) {
                        if (msg == "true") {
                            alert("添加成功！");
                        } else {
                            alert("添加失败！");

                        }
                        $("#win").dialog("close");
                        window.location.reload();
                    }
                });
			}
			else
			{
			    alert("请选择相应数据！");
			}
        }
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
                            <input id="cancel" type="button" value="取消" runat="server" onclick="Cancelapp()" visible="false" />&nbsp;&nbsp;&nbsp;
                            <asp:Button ID="btnShowPopup" runat="server" Text="筛选" OnClientClick="viewCondition()" />&nbsp;&nbsp;&nbsp;
                            <cc1:ModalPopupExtender ID="ModalPopupExtenderSearch" runat="server" TargetControlID="btnShowPopup"
                                PopupControlID="UpdatePanelCondition" Drag="false" Enabled="True" DynamicServicePath=""
                                Y="10">
                            </cc1:ModalPopupExtender>
                            <input id="InExport" type="button" value="导出" onclick="orderexport()" runat="server"
                                visible="false" />
                            <asp:Button ID="BtnShowExport" runat="server" Text="导出" OnClick="BtnShowExport_Click" visible="false" />&nbsp;&nbsp;&nbsp;
                            <asp:Button ID="Push" runat="server" Text="下推" OnClick="Push_Click" OnClientClick="return PushConfirm()" />
                            &nbsp;&nbsp;
                            <input id="closewin" type="button" value="关闭" onclick="closewindow()" visible="false" runat="server" />&nbsp;&nbsp;
                        </td>
                    </tr>
                </table>
                <asp:Panel ID="PanelCondition" runat="server" Style="display: none">
                    <asp:UpdatePanel ID="UpdatePanelCondition" runat="server" UpdateMode="Conditional">
                        <Triggers><asp:PostBackTrigger ControlID="btnQuery" /></Triggers>
                        <Triggers><asp:PostBackTrigger ControlID="btnReset" /></Triggers>
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
                                <td>
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
                                                入库状态：
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="DropDownListQRInState" runat="server">
                                                    <asp:ListItem Value="0" Selected="True">未入库</asp:ListItem>
                                                    <asp:ListItem Value="1">已入库</asp:ListItem>
                                                    <asp:ListItem Value="">全部</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td style="white-space: nowrap;" align="left">
                                                项目分配状态：
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="DropDownListPlanno" runat="server">
                                                    <asp:ListItem Value="0">未分配</asp:ListItem>
                                                    <asp:ListItem Value="1">已分配</asp:ListItem>
                                                    <asp:ListItem Value="" Selected="True">全部</asp:ListItem>
                                                </asp:DropDownList>
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
                            </table>
                   </ContentTemplate>
                    </asp:UpdatePanel>
                </asp:Panel>
            </div>
        </div>
    </div>
  <div class="RightContent">
    <div class="box-wrapper">
        <div class="box-outer">
                        <asp:Panel ID="NoDataPanel" runat="server" Visible="False">
                            没有相关记录!</asp:Panel>
                   <div id="div_statistcs" style="width: 100%; height: auto; overflow: scroll; display: block;">
                        <table id="GridView1" width="1000px" align="center" cellpadding="4" cellspacing="1" class="toptable grid nowrap"
                        border="1">
                            <asp:Repeater ID="Repeater1" runat="server" OnItemDataBound="Repeater1_ItemDataBound">
                                <HeaderTemplate>
                                    <tr align="center">
                                        <td>
                                            行号
                                        </td>
                                        <td>
                                            分配项目
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
                                            实收数量
                                        </td>
                                        <td>
                                            扫码时间
                                        </td>
                                        <td>
                                            是否入库
                                        </td>
                                        <td>
                                            扫码备注
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
                                        
                                    </tr>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td>
                                            <asp:CheckBox ID="CheckBox1" runat="server" CssClass="checkBoxCss" /><%#Container.ItemIndex + Convert.ToInt32(Eval("RowIndex"))%>
                                            <asp:Label ID="lbpurchangestate" runat="server" Width="20px" Height="12px" Text="" ToolTip="该计划跟踪号下物料提过物料减少" Visible="false"></asp:Label>
                                            <input id="QRIn_ID" runat="server" type="hidden" value='<%#Eval("QRIn_ID")%>' />
                                            <input id="MaterialCode" runat="server" type="hidden" value='<%#Eval("MaterialCode")%>' />
                                            <asp:Label ID="QRIn_ID2" runat="server" Text='<%#Eval("QRIn_ID")%>' Visible="false"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:HyperLink ID="HypAssigedPlanno" runat="server" onClick="showAssigedPlanno(this)">
                                                <asp:Image ID="Image5" ImageUrl="~/Assets/images/create.gif" border="0" hspace="2"
                                                    align="absmiddle" runat="server" />
                                                添加
                                            </asp:HyperLink>
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
                                            <asp:Label ID="LabelQRIn_Num" runat="server" Text='<%#Eval("QRIn_Num")%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="LabelQRIn_Time" runat="server" Text='<%#Eval("QRIn_Time")%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="LabelQRIn_State" runat="server" Text='<%#Eval("QRIn_State")%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="QRIn_Note" runat="server" ForeColor="#1A438E" Style="background-color: Transparent;text-align: center" Width="150px" Text='<%#Eval("QRIn_Note")%>' ToolTip='<%#Eval("QRIn_Note")%>'></asp:TextBox>
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
                                            <asp:Label ID="LabelOrderState" runat="server" Text='<%#ConvertOS(Eval("OrderState").ToString().Trim()) %>'></asp:Label>
                                            <asp:Label ID="LabelOS" runat="server" Text='<%#Eval("OrderState")%>' Visible="false"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="LabelPushState" runat="server" Text='<%#ConvertLS(Eval("PushState").ToString().Trim()) %>'></asp:Label>
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
                                        
                                    </tr>
                                </FooterTemplate>
                            </asp:Repeater>
                        </table>
                    </div>
              <uc1:UCPaging ID="UCPaging1" runat="server" />
        </div>
    </div>
  </div>
    <div>
        <div id="win" visible="false">
            <strong>选择项目：</strong>
            <input type="hidden" id="hidQRID" value="" />
            <div>
                <table id="dg">
                </table>
            </div>
        </div>
        <div id="buttons" style="text-align: right; display: none">
            <a class="easyui-linkbutton" data-options="iconCls:'icon-ok',plain:true" onclick="savePlanno();">
                保存</a> <a class="easyui-linkbutton" data-options="iconCls:'icon-cancel',plain:true"
                    onclick="javascript:$('#win').dialog('close')">取消</a>
        </div>
    </div>
</asp:Content>
