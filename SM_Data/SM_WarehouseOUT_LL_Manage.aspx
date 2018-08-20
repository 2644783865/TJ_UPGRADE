<%@ Page Language="C#" MasterPageFile="~/Masters/SMBaseMaster.Master" AutoEventWireup="true"
    CodeBehind="SM_WarehouseOUT_LL_Manage.aspx.cs" Inherits="ZCZJ_DPF.SM_Data.SM_WarehouseOUT_LL_Manage"
    Title="生产领料管理" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="../Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="uc1" %>
<%--<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
</asp:Content>--%>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
    <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnableScriptGlobalization="true"
        EnableScriptLocalization="true">
    </cc1:ToolkitScriptManager>

    <script src="SM_JS/superTables.js" type="text/javascript"></script>

    <script src="SM_JS/SelectCondition.js" type="text/javascript"></script>

    <link href="StyleFile/superTables.css" rel="stylesheet" type="text/css" />
    <link href="StyleFile/superTables_Default.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" language="javascript">

var postBack=true;

Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(BeginRequestHandler);
Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
function BeginRequestHandler(sender, args){
if (args.get_postBackElement().id == "<%= btnClose.ClientID %>"||args.get_postBackElement().id == "<%= btnReset.ClientID %>"||args.get_postBackElement().id == "<%= WarehouseDropDownList.ClientID %>")
{
 postBack=false;
}
else
{
    ActivateAlertDiv('visible', 'AlertDiv', '');}
}
        function EndRequestHandler(sender, args)
        {if(postBack){

document.getElementById("GridView1").parentNode.className = "fakeContainerNew";
document.getElementById("GridView1").parentNode.style.width=window.screen.availWidth-55;
document.getElementById("GridView1").parentNode.style.height=window.screen.availHeight-230;

            (function() {
                superTable("GridView1", {
                    cssSkin : "Default",
                   fixedCols : 3,
                   onFinish : function () 
                      {             
                        for (var i=0, j=this.sDataTable.tBodies[0].rows.length; i<j; i++) 
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
                                                var date=new Date();
                                                var time=date.getTime();
                                                if( dataRow.getElementsByTagName("td")[0].getElementsByTagName("span")[0]!=null)
                                                {
                                                var id = dataRow.getElementsByTagName("td")[0].getElementsByTagName("span")[0].innerHTML;
                                                window.open("SM_WarehouseOUT_LL.aspx?FLAG=OPEN&&ID=" + id+"&&tm="+time);
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
        window.open("SM_Warehouse_Query.aspx?FLAG=PUSHLLOUT&id="+time);
    }
    function toQROutData() {
     var date=new Date();
        var time=date.getTime();
        window.open("../QR_Interface/QROut_List.aspx?FLAG=PUSH&id="+time);
    }
    function outstorage() {
     var date=new Date();
        var time=date.getTime();
        window.open("SM_WarehouseOUT_LL_Auto.aspx?FLAG=PUSHBLUE&id="+time);
    }
   function OutExport()
   {
       var date=new Date();
       var time=date.getTime();
       var retVal = window.showModalDialog("SM_WarehouseOut_Export.aspx?id="+time, "", "dialogWidth=800px;dialogHeight=600px;status=no;help=no;scroll=yes");
   }

   
   function openout(tr) {
   var date=new Date();
        var time=date.getTime();
       var id = tr.getElementsByTagName("td")[3].getElementsByTagName("span")[0].innerHTML;
       window.open("SM_WarehouseOUT_LL.aspx?FLAG=OPEN&&ID=" + id+"&&tm="+time);
   }
  function viewCondition(){
   document.getElementById("<%=PanelCondition.ClientID%>").style.display='block';
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
                <asp:Label ID="ControlFinder" runat="server" Visible="false"></asp:Label>
                <table width="98%">
                    <tr>
                        <td align="left">
                            <asp:CheckBox ID="CheckBoxShow" runat="server" AutoPostBack="true" Text="单据头完整显示" 
                                OnCheckedChanged="CheckBoxShow_CheckedChanged" />
                            <asp:Label ID="LabelMessage" runat="server" ForeColor="Red" Visible="false"></asp:Label>
                            <asp:HiddenField ID="hfdTotalRN" runat="server" />
                            <asp:HiddenField ID="hfdTotalAmount" runat="server" />
                            <asp:HiddenField ID="hfdPageNum" runat="server" />                            
                            <asp:HiddenField ID="hfdTotalFRN" runat="server" />
                        </td>
                        <td align="right">
                            <asp:Button ID="btnShowPopup" runat="server" Text="筛选" OnClientClick="viewCondition()" />
                            <cc1:ModalPopupExtender ID="ModalPopupExtenderSearch" runat="server" TargetControlID="btnShowPopup"
                                PopupControlID="UpdatePanelCondition" Drag="false" Enabled="True" DynamicServicePath=""
                                X="10" Y="10">
                            </cc1:ModalPopupExtender>
                            <input id="Export" type="button" value="导出" onclick="OutExport()" runat="server"
                                visible="false" />
                            <asp:Button ID="BtnShowExport" runat="server" Text="导出" OnClick="BtnShowExport_Click" />
                            <input id="ToStorage" type="button" value="到库存" onclick="tostorage()" runat="server" />
                            <input id="Button1" type="button" value="到扫码出库物料" onclick="toQROutData()" runat="server" />
                            <input id="OutStorage" type="button" value="生产领料单" onclick="outstorage()" runat="server" />
                            <input id="BackStorage" type="button" value="退库" onclick="backstorage()" runat="server"
                                visible="false" />
                        </td>
                    </tr>
                </table>
                <asp:Panel ID="PanelCondition" runat="server" Width="100%" Style="display: none">
                    <asp:UpdatePanel ID="UpdatePanelCondition" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <table width="100%" style="background-color: #CCCCFF; border: solid 1px black;">
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
                                    
                                                                                   <asp:CheckBox ID="cz_gangcai" runat="server"  Text="钢材"/>
                                                                                       &nbsp;
                                               <asp:CheckBox ID="cz_wujin" runat="server" Text="五金" />
                                                               &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                      <asp:CheckBox ID="CheckBoxSearch" runat="server"  Text="在结果中查询"     />
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        <asp:Button ID="btnQuery" runat="server" OnClick="Query_Click" Text="查询" />
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        <asp:Button ID="btnClose" runat="server" Text="取消" OnClick="btnClose_Click" />
                                        &nbsp;&nbsp;&nbsp;
                                        <asp:Button ID="btnReset" runat="server" Text="重置" OnClick="btnReset_Click" />
                                </tr>
                                <tr>
                                    <td style="white-space: nowrap;" align="left">
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;状&nbsp;&nbsp;&nbsp;态：<asp:DropDownList ID="DropDownListState"
                                            runat="server">
                                            <asp:ListItem Text="--请选择--" Value=""></asp:ListItem>
                                            <asp:ListItem Text="未审核" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="已审核" Value="2"></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td style="white-space: nowrap;" align="left">
                                        &nbsp;&nbsp;&nbsp;&nbsp;红&nbsp;蓝&nbsp;字：<asp:DropDownList ID="DropDownListColour" runat="server">
                                            <asp:ListItem Text="--请选择--" Value=""></asp:ListItem>
                                            <asp:ListItem Text="蓝字" Value="0"></asp:ListItem>
                                            <asp:ListItem Text="红字" Value="1" style="color: Red;"></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td style="white-space: nowrap;" align="left">
                                        旧单编号：<asp:TextBox ID="TextBoxCode" runat="server"></asp:TextBox>
                                    </td>
                                    <td style="white-space: nowrap;" align="center">
                                        是否现场直发：<asp:DropDownList ID="drpifxczf" runat="server">
                                        <asp:ListItem Text="--请选择--" Value=""></asp:ListItem>
                                        <asp:ListItem Text="否" Value="0"></asp:ListItem>
                                        <asp:ListItem Text="是" Value="1"></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top" colspan="2">
                                         <table width="100%">
                                            <tr>
                                                <td style="white-space: nowrap;" align="left" width="50%">
                                                    &nbsp;&nbsp;&nbsp;新&nbsp;单&nbsp;编&nbsp;号：<asp:TextBox ID="TextBoxNewCode" runat="server"></asp:TextBox>
                                                </td>
                                                <td style="white-space: nowrap;" align="left" width="50%">
                                                    &nbsp;&nbsp;&nbsp;领&nbsp;料&nbsp;部&nbsp;门：<asp:TextBox ID="TextBoxDep" runat="server"></asp:TextBox>
                                                </td>
                                            </tr>
                                             <tr>
                                                <%--<td style="white-space: nowrap;" align="left" width="50%">
                                                    制单开始日期：<asp:TextBox ID="TextBoxZhDStartTime" runat="server"></asp:TextBox>
                                                    <cc1:CalendarExtender ID="CalendarExtenderTextBoxZhDStartTime" runat="server" Format="yyyy-MM-dd"
                                                        TargetControlID="TextBoxZhDStartTime">
                                                    </cc1:CalendarExtender>
                                                </td>
                                                <td style="white-space: nowrap;" align="left" width="50%">
                                                    制单截止日期：<asp:TextBox ID="TextBoxZhDEnd" runat="server"></asp:TextBox>
                                                    <cc1:CalendarExtender ID="CalendarExtenderTextBoxZhDEnd" runat="server" Format="yyyy-MM-dd"
                                                        TargetControlID="TextBoxZhDEnd">
                                                    </cc1:CalendarExtender>
                                                </td>--%>
                                            </tr>
                                            <tr>
                                                <td style="white-space: nowrap;" align="left" width="50%">
                                                    审核开始日期：<asp:TextBox ID="TextBoxStartDate" runat="server"></asp:TextBox>
                                                    <cc1:CalendarExtender ID="TextBoxStartDate_CalendarExtender" runat="server" Format="yyyy-MM"
                                                        TargetControlID="TextBoxStartDate">
                                                    </cc1:CalendarExtender>
                                                </td>
                                                <td style="white-space: nowrap;" align="left" width="50%">
                                                    审核截止日期：<asp:TextBox ID="TextBoxDate" runat="server"></asp:TextBox>
                                                    <cc1:CalendarExtender ID="TextBoxDate_CalendarExtender" runat="server" Format="yyyy-MM"
                                                        TargetControlID="TextBoxDate">
                                                    </cc1:CalendarExtender>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="white-space: nowrap;" align="left" width="50%">
                                                    &nbsp;&nbsp;&nbsp;物&nbsp;料&nbsp;代&nbsp;码：<asp:TextBox ID="TextBoxMCode" runat="server"></asp:TextBox>
                                                </td>
                                                <td style="white-space: nowrap;" align="left" width="50%">
                                                    &nbsp;&nbsp;&nbsp;物&nbsp;料&nbsp;名&nbsp;称：<asp:TextBox ID="TextBoxMName" runat="server"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="white-space: nowrap;" align="left" width="50%">
                                                    &nbsp;&nbsp;&nbsp;规&nbsp;格&nbsp;型&nbsp;号：<asp:TextBox ID="TextBoxMStandard" runat="server"></asp:TextBox>
                                                </td>
                                                <td style="white-space: nowrap;" align="left" width="50%">
                                                     &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;材&nbsp;&nbsp;&nbsp;质：<asp:TextBox ID="TextBoxCZ" runat="server"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="white-space: nowrap;" align="left" width="50%">
                                                    &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;制&nbsp;单&nbsp;人：<asp:TextBox ID="TextBoxZDR" runat="server"></asp:TextBox>
                                                </td>
                                                <td style="white-space: nowrap;" align="left" width="50%">
                                                    &nbsp;&nbsp;&nbsp;计划跟踪号：<asp:TextBox ID="TextBoxMPTC" runat="server"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="white-space: nowrap;" align="left" width="50%">
                                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;审&nbsp;核&nbsp;人：<asp:TextBox ID="TextBoxVrifier" runat="server"></asp:TextBox>
                                                </td>
                                                <td style="white-space: nowrap;" align="left" width="50%">
                                                    &nbsp;&nbsp;&nbsp;生&nbsp;产&nbsp;制&nbsp;号：<asp:TextBox ID="TextBoxEngid" runat="server"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="white-space: nowrap;" align="left" width="50%">
                                                    &nbsp;&nbsp;&nbsp;领&nbsp;料&nbsp;班&nbsp;组：<asp:TextBox ID="TextBoxZZBZ" runat="server"></asp:TextBox>
                                                </td>
                                                <td style="white-space: nowrap;" align="left" width="50%">
                                                    &nbsp;&nbsp; 制&nbsp;单&nbsp;日&nbsp;期：<asp:TextBox ID="TextBoxZDDate" runat="server"></asp:TextBox>
                                                    <cc1:CalendarExtender ID="CalendarExtenderZDDate" runat="server" Format="yyyy-MM-dd"
                                                        TargetControlID="TextBoxZDDate">
                                                    </cc1:CalendarExtender>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="white-space: nowrap;" align="left" width="50%">
                                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;发&nbsp;料&nbsp;人：<asp:TextBox ID="TextBoxSender" runat="server"></asp:TextBox>
                                                </td>
                                                <td style="white-space: nowrap;" align="left" width="50%">
                                                    &nbsp;&nbsp; 入&nbsp;库&nbsp;单&nbsp;号：<asp:TextBox ID="TextBoxInCode" runat="server"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="white-space: nowrap;" align="left" width="50%">
                                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;标&nbsp;识&nbsp;号：<asp:TextBox ID="TextBoxBshi" runat="server"></asp:TextBox>
                                                </td>
                                                <td style="white-space: nowrap;" align="left" width="50%">
                                                    &nbsp;&nbsp;&nbsp;计&nbsp;划&nbsp;模&nbsp;式：<asp:TextBox ID="TextBoxJhua" runat="server"></asp:TextBox>
                                                </td>
                                            </tr>
                                             <tr>
                                                <td style="white-space: nowrap;" align="left" width="50%">
                                                    &nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;仓&nbsp;&nbsp;&nbsp;&nbsp;库：<%--<asp:TextBox ID="TextBoxcangku" runat="server"></asp:TextBox>--%>
                                                    <asp:DropDownList ID="WarehouseDropDownList" runat="server"  
                                                          OnSelectedIndexChanged="WarehouseDropDownList_SelectedIndexChanged"
                                                                 AutoPostBack="true"></asp:DropDownList>
                                                </td>
                                                <td style="white-space: nowrap;" align="left" width="50%">
                                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;仓&nbsp;&nbsp;&nbsp;位：<%--<asp:TextBox ID="TextBoxwareposition" runat="server"></asp:TextBox>--%>
                                                <asp:DropDownList ID="ChildWarehouseDropDownList" runat="server"   >
                                                  </asp:DropDownList>
                                                </td>
                                            </tr>
                                             <tr>
                                                <td style="white-space: nowrap;" align="left" width="50%">
                                                    &nbsp;&nbsp; 实&nbsp;发&nbsp;数&nbsp;量：<asp:TextBox ID="TextBoxShifanum" runat="server"></asp:TextBox>
                                                </td>
                                                <td style="white-space: nowrap;" align="left" width="50%">
                                                    &nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 逻&nbsp;&nbsp;&nbsp;辑：
                                                    <asp:DropDownList ID="DropDownListFatherLogic" runat="server">
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
                                                            <asp:ListItem Value="0" Selected="True">左包含</asp:ListItem>
                                                            <%--<asp:ListItem Value="7">不包含</asp:ListItem>--%>
                                                            <asp:ListItem Value="8">左不包含</asp:ListItem>
                                                            <%--<asp:ListItem Value="9">右包含</asp:ListItem>--%>
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
    <asp:UpdatePanel ID="UpdatePanelBody" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="box-wrapper">
                <div class="box-outer">
                    <asp:Panel ID="PanelBody" runat="server">
                        <asp:Panel ID="NoDataPanel" runat="server" Visible="false">
                            没有相关记录!</asp:Panel>
                        <table id="GridView1">
                            <asp:Repeater ID="RepeaterLL" runat="server" OnItemDataBound="RepeaterLL_ItemDataBound">
                                <HeaderTemplate>
                                    <tr>
                                        <td>
                                        </td>
                                        <td>
                                            新单编号
                                        </td>
                                        <td>
                                            领料班组
                                        </td>
                                        <td>
                                            旧单编号
                                        </td>
                                        <td>
                                            生产制号
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
                                            单位
                                        </td>
                                        <td>
                                            实发数量
                                        </td>
                                        <td>
                                            张(支)
                                        </td>
                                        <td>
                                            单价
                                        </td>
                                        <td>
                                            金额
                                        </td>
                                        <td>
                                            发料人
                                        </td>
                                        <td>
                                            制单人
                                        </td>
                                        <td>
                                            制单日期
                                        </td>
                                        <td>
                                            审核人
                                        </td>
                                        <td>
                                            审核日期
                                        </td>
                                        <td>
                                            审核状态
                                        </td>
                                        <td>
                                            仓库
                                        </td>
                                        <td>
                                            仓位
                                        </td>
                                        <td>
                                            计划跟踪号
                                        </td>
                                        <td>
                                            领料部门
                                        </td>
                                        <td>
                                            计划模式
                                        </td>
                                        <td>
                                            标识号
                                        </td>
                                        <td>
                                            张数
                                        </td>
                                        <td>
                                            备注
                                        </td>
                                        <td>
                                            是否现场直发
                                        </td>
                                    </tr>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td>
                                            <%#Container.ItemIndex+1%>
                                        </td>
                                        <td>
                                            <asp:Label ID="LabelTrueCode" runat="server" Text='<%#Eval("TrueCode")%>'></asp:Label>
                                        </td>
                                        <td>
                                            <%#Eval("Comment")%>
                                        </td>
                                        
                                        <td>
                                            <asp:Label ID="LabelCode" runat="server" Text='<%#Eval("Code")%>' ></asp:Label>
                                        </td>
                                        <td>
                                            <%#Eval("TSAID")%>
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
                                            <%#Eval("Unit")%>
                                        </td>
                                        <td>
                                            <asp:Label ID="LabelRN" runat="server" Text='<%#Eval("RN")%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="LabelFRN" runat="server" Text='<%#Eval("RealSupportNumber")%>'></asp:Label>
                                        </td>
                                        <td>
                                            <%#Eval("UnitPrice")%>
                                        </td>
                                        <td>
                                            <asp:Label ID="LabelAmount" runat="server" Text='<%#Eval("Amount")%>'></asp:Label>
                                        </td>
                                        <td>
                                            <%#Eval("Sender")%>
                                        </td>
                                        <td>
                                            <%#Eval("Doc")%>
                                        </td>
                                        <td>
                                            <%#Eval("Date")%>
                                        </td>
                                        <td>
                                            <%#Eval("Verifier")%>
                                        </td>
                                        <td>
                                            <%#Eval("ApprovedDate")%>
                                        </td>
                                        <td>
                                            <%#convertState((string)Eval("State"))%>
                                        </td>
                                        <td>
                                            <%#Eval("Warehouse")%>
                                        </td>
                                        <td>
                                            <%#Eval("PositionOut")%>
                                        </td>
                                        <td>
                                            <%#Eval("PTC")%>
                                        </td>
                                        <td>
                                            <%#Eval("Dep")%>
                                        </td>
                                        <td>
                                            <%#Eval("PlanMode")%>
                                        </td>
                                        <td>
                                            <%#Eval("BSH")%>
                                        </td>
                                        <td>
                                            <asp:Label ID="LabelPageNum" runat="server" Text='<%#Eval("OP_PAGENUM")%>'></asp:Label>
                                        </td>
                                        <td>
                                            <%#Eval("Note")%>
                                        </td>
                                        <td>
                                            <%#Eval("OP_XCZF").ToString().Trim()==""?"否":"是"%>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <tr>
                                        <td>
                                        </td>
                                         <td>
                                        </td>
                                        <td>
                                            合计:
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
                                            <asp:Label ID="LabelTotalRN" runat="server" Text='<%#Eval("TotalRN")%>'></asp:Label>
                                        </td>
                                        <td>
                                          <asp:Label ID="LabelTotalFRN" runat="server" Text='<%#Eval("TotalFRN")%>'></asp:Label>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                            <asp:Label ID="LabelTotalAmount" runat="server" Text='<%#Eval("TotalAmount")%>'></asp:Label>
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
                                        </td>
                                        <td>
                                            总计:
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
                                            <asp:Label ID="TotalRN" runat="server"></asp:Label>
                                        </td>
                                        <td>
                                          <asp:Label ID="TotalFRN" runat="server"></asp:Label>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                            <asp:Label ID="TotalAmount" runat="server"></asp:Label>
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
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                            <asp:Label ID="TotalPageNum" runat="server"></asp:Label>
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
                </div>
                <!--box-outer END -->
            </div>
            <!--box-wrapper END -->

            <script type="text/javascript">
//<![CDATA[

document.getElementById("GridView1").parentNode.className = "fakeContainerNew";
document.getElementById("GridView1").parentNode.style.width=window.screen.availWidth-55;
document.getElementById("GridView1").parentNode.style.height=window.screen.availHeight-230;

(function() {
    superTable("GridView1", {
        cssSkin : "Default",
       fixedCols : 3,
       onFinish : function () 
       {             
                for (var i=0, j=this.sDataTable.tBodies[0].rows.length; i<j; i++) 
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
                            var date=new Date();
                            var time=date.getTime();
                            if( dataRow.getElementsByTagName("td")[0].getElementsByTagName("span")[0]!=null)
                            {
                                var id = dataRow.getElementsByTagName("td")[0].getElementsByTagName("span")[0].innerHTML;
                                window.open("SM_WarehouseOUT_LL.aspx?FLAG=OPEN&&ID=" + id+"&&tm="+time);
                            }
                        }
                    }.call(this, i);
                }
             return this;
       }
    });
})();

//]]>
            </script>

        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="Export" />
        </Triggers>
    </asp:UpdatePanel>
    <div id="AlertDiv" class="AlertStyle">
        <img id="laoding" src="../Assets/images/ajaxloader.gif" alt="downloading" />
    </div>
</asp:Content>
