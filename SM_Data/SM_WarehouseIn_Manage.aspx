<%@ Page Language="C#" MasterPageFile="~/Masters/SMBaseMaster.master" AutoEventWireup="true"
    CodeBehind="SM_WarehouseIn_Manage.aspx.cs" Inherits="ZCZJ_DPF.SM_Data.SM_WarehouseIn_Manage"
    Title="采购入库" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="../Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="uc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
    <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnableScriptGlobalization="True">
    </cc1:ToolkitScriptManager>
    <link href="StyleFile/superTables.css" rel="stylesheet" type="text/css" />
    <link href="StyleFile/superTables_Default.css" rel="stylesheet" type="text/css" />

    <script src="SM_JS/superTables.js" type="text/javascript"></script>

    <script src="SM_JS/SelectCondition.js" type="text/javascript"></script>

    <script type="text/javascript" language="javascript">
 var postBack=true;
Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(BeginRequestHandler);
Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
function BeginRequestHandler(sender, args){
 if (args.get_postBackElement().id == "<%= btnClose.ClientID %>"||args.get_postBackElement().id == "<%= btnReset.ClientID %>")
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
                for (var i=0, j=this.sDataTable.tBodies[0].rows.length-2; i<j; i++) 
                {
                   
                                this.sDataTable.tBodies[0].rows[i].onclick = this.sFDataTable.tBodies[0].rows[i].onclick = function (i) 
                                {
                                   
                                    var dataRow = this.sDataTable.tBodies[0].rows[i];
                                    var fixedRow = this.sFDataTable.tBodies[0].rows[i];
            //                        var clicked =  fixedRow.getElementsByTagName("td")[0].getElementsByTagName("input")[0].checked;
                                    return function () 
                                    {
                                        if (fixedRow.getElementsByTagName("td")[0].getElementsByTagName("input")[0].checked) 
                                        {
                                            dataRow.style.backgroundColor = "#ffffff";
                                            fixedRow.style.backgroundColor = "#e4ecf7";
                                            fixedRow.getElementsByTagName("td")[0].getElementsByTagName("input")[0].checked=false;
            //                                clicked = false;
                                        }
                                        else 
                                        {
                                            //被选中
                                            dataRow.style.backgroundColor = "#eeeeee";
                                            fixedRow.style.backgroundColor = "#adadad";
                                            fixedRow.getElementsByTagName("td")[0].getElementsByTagName("input")[0].checked=true;
            //                                clicked = true;
                                            var odataRow;
                                            var ofixedRow;
                                            for(var m=0, n=fixedRow.parentNode.rows.length-2; m<n; m++)
                                            {
                                               odataRow=dataRow.parentNode.rows[m];
                                               ofixedRow=fixedRow.parentNode.rows[m];
                                               if(m!=i)
                                               {
                                                 odataRow.style.backgroundColor = "#ffffff";
                                                 ofixedRow.style.backgroundColor = "#e4ecf7";
                                                 ofixedRow.getElementsByTagName("td")[0].getElementsByTagName("input")[0].checked=false;
                                               }
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
                                       if(fixedRow.getElementsByTagName("td")[1].getElementsByTagName("span")[0]!=null)
                                       {
                                       
                                           var date=new Date();
                                           var time=date.getTime();
                                           var id = fixedRow.getElementsByTagName("td")[1].getElementsByTagName("span")[0].innerHTML;
                    
                                           window.open("SM_WarehouseIN_WG.aspx?FLAG=READ&&ID=" + id+"&&tm="+time);
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

function order() {
    var date=new Date();
        var time=date.getTime();
        window.open("SM_WarehouseIN_WGPush.aspx?FLAG=PUSH&id="+time);
    }

function inexport() {
     var date=new Date();
        var time=date.getTime();
        var retVal = window.showModalDialog("SM_WarehouseIn_Export.aspx?id="+time, "", "dialogWidth=900px;dialogHeight=600px;help=no;scroll=yes");
    }
function SearchUp(id) {
     var date=new Date();
        var time=date.getTime();
        window.open("../PC_Data/TBPC_Purordertotal_list.aspx?action=SearchUpOrDown&id="+id+"&time="+time);
    }
    
function SearchDown(id) {
     var date=new Date();
        var time=date.getTime();
        window.open("../FM_Data/FM_Invoice_Managemnt.aspx?action=SearchUpOrDown&id="+id+"&time="+time);
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
                            <asp:Label ID="LabelMessage" runat="server" ForeColor="Red" Font-Size="Medium" Visible="false"></asp:Label>
                            <asp:HiddenField ID="hfdTotalNum" runat="server" />
                            <asp:HiddenField ID="hfdTotalAmount" runat="server" />
                            <asp:HiddenField ID="hfTotalzhang" runat="server" />
                        </td>
                        <td align="right">
                            <asp:Button ID="ButtonSearchUp" runat="server" Text="上查" OnClick="ButtonSearchUp_Click" />&nbsp;&nbsp;&nbsp;
                            <asp:Button ID="ButtonSearchDown" runat="server" Text="下查" OnClick="ButtonSearchDown_Click" />&nbsp;&nbsp;&nbsp;
                            <%--<input id="Display" type="button" value="隐藏条件" onclick="Disp()" />&nbsp;&nbsp;&nbsp;--%>
                            <asp:Button ID="btnShowPopup" runat="server" Text="筛选" OnClientClick="viewCondition()" />&nbsp;&nbsp;&nbsp;
                            <cc1:ModalPopupExtender ID="ModalPopupExtenderSearch" runat="server" TargetControlID="btnShowPopup"
                                PopupControlID="UpdatePanelCondition" Drag="false" Enabled="True" DynamicServicePath=""
                                Y="10">
                            </cc1:ModalPopupExtender>
                            <input id="InExport" type="button" value="导出" onclick="inexport()" runat="server"
                                visible="false" />
                            <asp:Button ID="BtnShowExport" runat="server" Text="导出" OnClick="BtnShowExport_Click" />&nbsp;&nbsp;&nbsp;
                            <input id="toorder" type="button" value="到订单" onclick="order()" runat="server" />&nbsp;&nbsp;&nbsp;
                        </td>
                    </tr>
                </table>
                <asp:Panel ID="PanelCondition" runat="server" Width="98%" Style="display: none">
                    <asp:UpdatePanel ID="UpdatePanelCondition" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <table width="98%" style="background-color: #CCCCFF; border: solid 1px black;">
                                <tr>
                                    <td width="50%">
                                        <table cellpadding="4">
                                            <tr>
                                                <td style="white-space: nowrap; width: 25%" align="left">
                                                    旧单编号：
                                                </td>
                                                <td style="white-space: nowrap; width: 34%">
                                                </td>
                                                <td style="white-space: nowrap; width: 25%; display: none">
                                                    打印标识<asp:CheckBox runat="server" ID="CheckBoxPrint" Visible="false" />
                                                </td>
                                                <td style="white-space: nowrap;" align="left">
                                                    数量超额：
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="DropDownListcesl" runat="server">
                                                        <asp:ListItem Text="--请选择--" Value=""></asp:ListItem>
                                                        <asp:ListItem Text="超过5%" Value="1"></asp:ListItem>
                                                        <asp:ListItem Text="超过10%" Value="2"></asp:ListItem>
                                                        <asp:ListItem Text="超过15%" Value="3"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td width="50%" align="right">
                                        <asp:Button ID="btnQuery" runat="server" Text="查询" OnClick="Query_Click" />&nbsp;&nbsp;&nbsp;
                                        <asp:Button ID="btnClose" runat="server" Text="取消" OnClick="btnClose_Click" />&nbsp;&nbsp;&nbsp;
                                        <asp:Button ID="btnReset" runat="server" Text="重置" OnClick="btnReset_Click" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td width="50%">
                                        <table cellpadding="4">
                                            <tr>
                                                <td style="white-space: nowrap;" align="left">
                                                    审核状态：
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="DropDownListState" runat="server">
                                                        <asp:ListItem Text="--请选择--" Value=""></asp:ListItem>
                                                        <asp:ListItem Text="未审核" Value="1"></asp:ListItem>
                                                        <asp:ListItem Text="已审核" Value="2"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                <td style="white-space: nowrap;" align="left">
                                                    红蓝字：
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="DropDownListColour" runat="server">
                                                        <asp:ListItem Text="--请选择--" Value=""></asp:ListItem>
                                                        <asp:ListItem Text="蓝字" Value="0"></asp:ListItem>
                                                        <asp:ListItem Text="红字" Value="1"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="white-space: nowrap;" align="left">
                                                    勾稽状态：
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="DropDownListGJ" runat="server">
                                                        <asp:ListItem Text="--请选择--" Value=""></asp:ListItem>
                                                        <asp:ListItem Text="未勾稽" Value="0"></asp:ListItem>
                                                        <asp:ListItem Text="已勾稽" Value="1"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                <td style="white-space: nowrap;" align="left">
                                                    核销状态：
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="DropDownListHX" runat="server">
                                                        <asp:ListItem Text="--请选择--" Value=""></asp:ListItem>
                                                        <asp:ListItem Text="未核销" Value="0"></asp:ListItem>
                                                        <asp:ListItem Text="已核销" Value="1"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="white-space: nowrap;" align="left">
                                                    单据编号：
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="TextBoxCodeWG" runat="server"></asp:TextBox>
                                                </td>
                                                <td style="white-space: nowrap;" align="left">
                                                    供应商：
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="TextBoxSupplierWG" runat="server"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="white-space: nowrap;" align="left">
                                                    开始日期：
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="TextBoxStartDate" runat="server"></asp:TextBox>
                                                    <cc1:CalendarExtender ID="TextBoxStartDate_CalendarExtender" runat="server" Format="yyyy-MM-dd"
                                                        TargetControlID="TextBoxStartDate">
                                                    </cc1:CalendarExtender>
                                                </td>
                                                <td style="white-space: nowrap;" align="left">
                                                    截至到日期：
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="TextBoxDateWG" runat="server"></asp:TextBox>
                                                    <cc1:CalendarExtender ID="DateTextBox_CalendarExtender" runat="server" Format="yyyy-MM-dd"
                                                        TargetControlID="TextBoxDateWG">
                                                    </cc1:CalendarExtender>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="white-space: nowrap;" align="left">
                                                    物料代码：
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="TextBoxMCodeWG" runat="server"></asp:TextBox>
                                                </td>
                                                <td style="white-space: nowrap;" align="left">
                                                    物料名称：
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="TextBoxMNameWG" runat="server"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="white-space: nowrap;" align="left">
                                                    规格型号：
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="TextBoxMStandardWG" runat="server"></asp:TextBox>
                                                </td>
                                                <td style="white-space: nowrap;" align="left">
                                                    材质：
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="TextBoxCZ" runat="server"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="white-space: nowrap;" align="left">
                                                    标识号：
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="TextBoxTH" runat="server"></asp:TextBox>
                                                </td>
                                                <td style="white-space: nowrap;" align="left">
                                                    计划跟踪号：
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="TextBoxMPTCWG" runat="server"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="white-space: nowrap;" align="left">
                                                    批号：
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="TextBoxPH" runat="server"></asp:TextBox>
                                                </td>
                                                <td style="white-space: nowrap;" align="left">
                                                    物料类型：
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="TextBoxMType" runat="server"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="white-space: nowrap;" align="left">
                                                    制单人：
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="TextBoxZDR" runat="server"></asp:TextBox>
                                                </td>
                                                <td style="white-space: nowrap;" align="left">
                                                    业务员：
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="TextBoxClerkWG" runat="server"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    订单编号：
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="TextBoxHDBH" runat="server"></asp:TextBox>
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
                    <asp:Panel ID="PanelWG" runat="server">
                        <asp:Panel ID="NoDataPanel" runat="server" Visible="False">
                            没有相关记录!</asp:Panel>
                        <table id="GridView1">
                            <asp:Repeater ID="RepeaterWG" runat="server" OnItemDataBound="RepeaterWG_ItemDataBound">
                                <HeaderTemplate>
                                    <tr>
                                        <td>
                                        </td>
                                        <td>
                                            单据编号
                                        </td>
                                        <td>
                                            货单编号
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
                                            长
                                        </td>
                                        <td>
                                            宽
                                        </td>
                                        <td>
                                            单位
                                        </td>
                                        <td>
                                            实收数量
                                        </td>
                                        <td>
                                            张(支)
                                        </td>
                                        <td>
                                            订货数量
                                        </td>
                                        <td>
                                            单价
                                        </td>
                                        <td>
                                            金额
                                        </td>
                                        <td>
                                            仓库
                                        </td>
                                        <td>
                                            仓位
                                        </td>
                                        <td>
                                            批号
                                        </td>
                                        <td>
                                            部门
                                        </td>
                                        <td>
                                            业务员
                                        </td>
                                        <td>
                                            收料人
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
                                            勾稽状态
                                        </td>
                                        <td>
                                            核销状态
                                        </td>
                                        <td>
                                            订单编号
                                        </td>
                                        <td>
                                            供应商名称
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
                                    </tr>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td>
                                            <asp:CheckBox ID="CheckBox1" runat="server" CssClass="checkBoxCss" /><%#Container.ItemIndex+1%>
                                        </td>
                                        <td>
                                            <asp:Label ID="LabelCode" runat="server" Text='<%#Eval("Code")%>'></asp:Label>
                                        </td>
                                        <td>
                                            <%#Eval("WG_HDBH")%>
                                        </td>
                                        <td>
                                            <asp:Label ID="LabelPTC" runat="server" Text='<%#Eval("PTC")%>'></asp:Label>
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
                                            <%#Eval("WG_LENGTH")%>
                                        </td>
                                        <td>
                                            <%#Eval("WG_WIDTH")%>
                                        </td>
                                        <td>
                                            <%#Eval("Unit")%>
                                        </td>
                                        <td>
                                            <asp:Label ID="LabelRN" runat="server" Text='<%#Eval("RN")%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="LabelRFN" runat="server" Text='<%#Eval("WG_RSFZNUM")%>'></asp:Label>
                                        </td>
                                        <td>
                                            <%#Eval("PO_ZXNUM")%>
                                        </td>
                                        <td>
                                            <%#Eval("UnitPrice")%>
                                        </td>
                                        <td>
                                            <asp:Label ID="LabelAmount" runat="server" Text='<%#Eval("Amount")%>'></asp:Label>
                                        </td>
                                        <td>
                                            <%#Eval("Warehouse")%>
                                        </td>
                                        <td>
                                            <%#Eval("WL_NAME")%>
                                        </td>
                                        <td>
                                            <%#Eval("WG_LOTNUM")%>
                                        </td>
                                        <td>
                                            <%#Eval("Dep")%>
                                        </td>
                                        <td>
                                            <%#Eval("Clerk")%>
                                        </td>
                                        <td>
                                            <%#Eval("ReveicerName")%>
                                        </td>
                                        <td>
                                            <%#Eval("DocName")%>
                                        </td>
                                        <td>
                                            <%#Eval("Date")%>
                                        </td>
                                        <td>
                                            <%#Eval("VerfierName")%>
                                        </td>
                                        <td>
                                            <%#Eval("VerifyDate")%>
                                        </td>
                                        <td>
                                            <%#convertSH((string)Eval("State"))%>
                                        </td>
                                        <td>
                                            <%#convertGJ((string)Eval("GJState"))%>
                                        </td>
                                        <td>
                                            <%#convertHX((string)Eval("HXState"))%>
                                        </td>
                                        <td>
                                            <%#Eval("WG_ORDERID")%>
                                        </td>
                                        <td>
                                            <%#Eval("Supplier")%>
                                        </td>
                                        <td>
                                            <%#Eval("WG_PMODE")%>
                                        </td>
                                        <td>
                                            <%#Eval("WG_CGMODE")%>
                                        </td>
                                        <td>
                                            <%#Eval("Comment")%>
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
                                            <asp:Label ID="LabelTotalNum" runat="server"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="LabelSumzhang" runat="server"></asp:Label>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                            <asp:Label ID="LabelTotalAmount" runat="server"></asp:Label>
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
                                            <asp:Label ID="TotalNum" runat="server"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="Labelsumzjzhang" runat="server"></asp:Label>
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
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <!--box-outer END -->
    </div>
    <!--box-wrapper END -->

    <script type="text/javascript">
    
document.getElementById("GridView1").parentNode.className = "fakeContainer";
 document.getElementById("GridView1").parentNode.style.width=window.screen.availWidth-55;
document.getElementById("GridView1").parentNode.style.height=window.screen.availHeight-230;
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
                       
                        var dataRow = this.sDataTable.tBodies[0].rows[i];
                        var fixedRow = this.sFDataTable.tBodies[0].rows[i];
//                        var clicked =  fixedRow.getElementsByTagName("td")[0].getElementsByTagName("input")[0].checked;
                        return function () 
                        {
                            if (fixedRow.getElementsByTagName("td")[0].getElementsByTagName("input")[0].checked) 
                            {
                                dataRow.style.backgroundColor = "#ffffff";
                                fixedRow.style.backgroundColor = "#e4ecf7";
                                fixedRow.getElementsByTagName("td")[0].getElementsByTagName("input")[0].checked=false;
//                                clicked = false;
                            }
                            else 
                            {
                                //被选中
                                dataRow.style.backgroundColor = "#eeeeee";
                                fixedRow.style.backgroundColor = "#adadad";
                                fixedRow.getElementsByTagName("td")[0].getElementsByTagName("input")[0].checked=true;
//                                clicked = true;
                                var odataRow;
                                var ofixedRow;
                                for(var m=0, n=fixedRow.parentNode.rows.length-2; m<n; m++)
                                {
                                   odataRow=dataRow.parentNode.rows[m];
                                   ofixedRow=fixedRow.parentNode.rows[m];
                                   if(m!=i)
                                   {
                                     odataRow.style.backgroundColor = "#ffffff";
                                     ofixedRow.style.backgroundColor = "#e4ecf7";
                                     ofixedRow.getElementsByTagName("td")[0].getElementsByTagName("input")[0].checked=false;
                                   }
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
                            if(fixedRow.getElementsByTagName("td")[1].getElementsByTagName("span")[0]!=null)
                            { 
                            
                                var date=new Date();
                                
                                var time=date.getTime();
                                
                                var id = fixedRow.getElementsByTagName("td")[1].getElementsByTagName("span")[0].innerHTML;
        
                                window.open("SM_WarehouseIN_WG.aspx?FLAG=READ&&ID=" + id+"&&tm="+time);
                                
                            }
                        }
                    }.call(this, i);
                }
             return this;
       }
    });
})();

    </script>

    <div id="AlertDiv" class="AlertStyle">
        <img id="laoding" src="../Assets/images/ajaxloader.gif" alt="downloading" />
    </div>
</asp:Content>
