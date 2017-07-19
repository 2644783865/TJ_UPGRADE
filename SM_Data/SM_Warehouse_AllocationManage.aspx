<%@ Page Language="C#" MasterPageFile="~/Masters/SMBaseMaster.master" AutoEventWireup="true" CodeBehind="SM_Warehouse_AllocationManage.aspx.cs" Inherits="ZCZJ_DPF.SM_Data.SM_Warehouse_AllocationManage" Title="调拨管理" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<%@ Register src="../Controls/UCPaging.ascx" tagname="UCPaging" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PrimaryContent" runat="server">



<script src="SM_JS/superTables.js" type="text/javascript"></script>
 
<link href="StyleFile/superTables.css" rel="stylesheet" type="text/css" />
    
<link href="StyleFile/superTables_Default.css" rel="stylesheet" type="text/css" />

<link rel="stylesheet" type="text/css" href="stylesheets/superTables.css" />

<asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
</asp:ToolkitScriptManager>

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
     
     document.getElementById("superTable").parentNode.className = "fakeContainer";

    (function() {
        superTable("superTable", {
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
                                        if(fixedRow.getElementsByTagName("td")[1].getElementsByTagName("span")[0]!=null)
                                        {
                                            var id = fixedRow.getElementsByTagName("td")[1].getElementsByTagName("span")[0].innerHTML;
                                            window.open("SM_Warehouse_Allocation.aspx?FLAG=OPEN&&ID=" + id+"&&tm="+time);
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

    window.open('SM_Warehouse_Query.aspx?FLAG=PUSHAL');
}

function alexport() {
    var retVal = window.showModalDialog("SM_WarehouseAL_Export.aspx", "", "dialogWidth=800px;dialogHeight=600px;status=no;help=no;scroll=yes");
}

function viewCondition()
{
   document.getElementById("<%=PanelCondition.ClientID%>").style.display='block';
}

</script>
     
    <div class="box-inner">
    <div class="box_right">
    <div class="box-title">
    
    <%--<asp:Label ID="ControlFinder" runat="server" Visible="false"></asp:Label>--%>
      
    <table width="98%">
        <tr>
            <td align="left">
             <asp:CheckBox ID="CheckBoxShow" runat="server" AutoPostBack="true" Text="单据头完整显示" OnCheckedChanged="CheckBoxShow_CheckedChanged" />
                <asp:Label ID="LabelMessage" runat="server" ForeColor="Red" Visible="false" ></asp:Label>
            </td>
            <td align="right">
                <asp:Button ID="btnShowPopup" runat="server" Text="筛选" OnClientClick="viewCondition()"/>&nbsp;&nbsp;
                <asp:ModalPopupExtender ID="ModalPopupExtenderSearch" runat="server" 
                    TargetControlID="btnShowPopup" PopupControlID="UpdatePanelCondition"  Drag="false"  
                    Enabled="True"  DynamicServicePath=""  Y="30" >
                </asp:ModalPopupExtender> 
                
                <input id="ALExport" type="button" value="导出" onclick="alexport()" runat="server" />&nbsp;&nbsp;
                <input id="ToStorage" type="button" value="到库存" onclick="tostorage()" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;

            </td>
        </tr>
    </table>
    </div>
    </div>
    </div>
    
    <div class="box-wrapper">
    <div class="box-outer">
    
    <asp:Panel ID="PanelCondition" runat="server" Width="98%" style="display:none" > 
    <asp:UpdatePanel ID="UpdatePanelCondition" runat="server" UpdateMode="Conditional">
    <ContentTemplate> 
    
     <table width="98%" style="background-color:#CCCCFF; border:solid 1px black;">
         <tr>
            <td style="white-space:nowrap;" align="left">
                &nbsp;</td>
            <td style="white-space:nowrap;" align="left">
                &nbsp;</td>
            <td style="white-space:nowrap;" align="left">
                        &nbsp;</td>
            <td style="white-space:nowrap;" align="left">
               <asp:Button ID="QueryButton" runat="server" Text="查询"  OnClick="QueryButton_Click" />&nbsp;&nbsp;&nbsp;
               <asp:Button ID="btnClose" runat="server" Text="取消" OnClick="btnClose_Click" />&nbsp;&nbsp;&nbsp;
               <asp:Button ID="btnReset" runat="server" Text="重置" OnClick="btnReset_Click"/>&nbsp;&nbsp;&nbsp;
            </td>   
         </tr>
        <tr>
            <td style="width:25%;white-space:nowrap;" align="left">
                调拨单状态:<asp:DropDownList ID="DropDownListState" runat="server" >
                                <asp:ListItem Text="--请选择--" Value=""></asp:ListItem>
                                <asp:ListItem Text="未提交" Value="1"></asp:ListItem>
                                <asp:ListItem Text="待审核" Value="2"></asp:ListItem>
                                <asp:ListItem Text="已审核" Value="3"></asp:ListItem>
                            </asp:DropDownList>
            </td>
            <td style="width:25%;white-space:nowrap;" align="left">
            </td>
            <td style="width:25%;white-space:nowrap;" align="left">
            </td>
            <td style="width:25%;white-space:nowrap;" align="left">
            </td>        
        </tr>
        <tr>
            <td style="width:25%;white-space:nowrap;" align="left">
                 调拨单号：<asp:TextBox ID="TextBoxALCode" runat="server"></asp:TextBox>
            </td>
            <td style="width:25%;white-space:nowrap;" align="left">
                调出仓库：<asp:TextBox ID="TextBoxWarehouseOut" runat="server"></asp:TextBox>
            </td>
            <td style="width:25%;white-space:nowrap;" align="left">
                调入仓库：<asp:TextBox ID="TextBoxWarehouseIn" runat="server"></asp:TextBox>
            </td>
            <td style="width:25%;white-space:nowrap;" align="left">
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;日&nbsp;&nbsp;&nbsp;期：<asp:TextBox ID="TextBoxDate" runat="server"></asp:TextBox>
            </td>        
        </tr>
        <tr>
            <td style="width:25%;white-space:nowrap;" align="left">
                 物料代码：<asp:TextBox ID="TextBoxCode" runat="server"></asp:TextBox>
            </td>
            <td style="width:25%;white-space:nowrap;" align="left">
                物料名称：<asp:TextBox ID="TextBoxName" runat="server"></asp:TextBox>
            </td>
            <td style="width:25%;white-space:nowrap;" align="left">
                规格型号：<asp:TextBox ID="TextBoxStandard" runat="server"></asp:TextBox>
            </td>
            <td style="width:25%;white-space:nowrap;" align="left">
                计划跟踪号：<asp:TextBox ID="TextBoxPTC" runat="server"></asp:TextBox>
            </td>
        </tr>
    </table>
    
     </ContentTemplate>
    </asp:UpdatePanel>  
    </asp:Panel>
    
        
    <asp:UpdatePanel ID="UpdatePanelBody" runat="server" UpdateMode="Conditional">
    <ContentTemplate>

    <div>
    <asp:Panel ID="NoDataPanel" runat="server" Visible="false">没有相关记录!</asp:Panel>
    
       <table id="superTable" width="100%">

        <asp:Repeater ID="Repeater1" runat="server"   onitemdatabound="Repeater1_ItemDataBound">
            <HeaderTemplate>
            <tr >
                <td></td>
                <td>单据编号</td>
                
               
                <td>物料代码</td>
                <td>物料名称</td>
                <td>型号规格</td>
                <td>材质</td>
                <td>是否定尺</td>
                <td>长</td>
                <td>宽</td>
                <td>批号</td>
                <td>单位</td>
                <td>调出仓库</td>
                <td>调出仓位</td>
                <td>调入仓库</td>
                <td>调入仓位</td>
                <td>数量</td>
                <td>张(支)数</td>                
                <td>制单人</td>  
                <td>制单日期</td>
                <td>审核人</td> 
                <td>审核日期</td>
                <td>审核状态</td>  
                 <td>计划跟踪号</td> 
                                     
            </tr>
            </HeaderTemplate>
             <ItemTemplate>
            <tr>
                <td><%#Container.ItemIndex+1%></td>         
                <td><asp:Label ID="LabelCode" runat="server" Text='<%#Eval("Code")%>'></asp:Label></td>
               
               
                <td><%#Eval("MaterialCode")%></td>
                <td><%#Eval("MaterialName")%></td>
                <td><%#Eval("MaterialStandard")%></td>
                <td><%#Eval("Attribute")%></td>
                <td><%#Eval("Fixed")%></td>
                <td><%#Eval("Length")%></td>
                <td><%#Eval("Width")%></td>
                <td><%#Eval("LotNumber")%></td>
                <td><%#Eval("Unit")%></td>
                <td><%#Eval("WarehouseOut")%></td>
                <td><%#Eval("LocationOut")%></td>
                <td><%#Eval("WarehouseIn")%></td>
                <td><%#Eval("LocationIn")%></td>
                <td><asp:Label ID="LabelNumber" runat="server" Text='<%#Eval("Number")%>'></asp:Label></td>
                <td><asp:Label ID="LabelQuantity" runat="server" Text='<%#Eval("Quantity")%>'></asp:Label></td>
                <td><%#Eval("Document")%></td>
                <td><%#Eval("Date")%></td>
                <td><%#Eval("Verifier")%></td>
                <td><%#Eval("VerifyDate")%></td>
                <td><%#convertState((string)Eval("State")+(string)Eval("VState"))%></td>
                <td><%#Eval("PTC")%></td>
                     
            </tr>
            </ItemTemplate>
            <FooterTemplate>
            <tr>
                <td></td>
                <td>合计：</td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td><asp:Label ID="LabelTotalNumber" runat="server"></asp:Label></td>
                <td><asp:Label ID="LabelTotalQuantity" runat="server"></asp:Label></td>  
                <td></td>
                <td></td>
                <td></td>
                <td></td>   
            </tr>
            </FooterTemplate>
        </asp:Repeater>
       
    </table>
    </div>
    
    <uc1:UCPaging ID="UCPaging1" runat="server" /> 
    <script type="text/javascript">
//<![CDATA[

     document.getElementById("superTable").parentNode.className = "fakeContainer";

    (function() {
        superTable("superTable", {
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
                                         if(fixedRow.getElementsByTagName("td")[1].getElementsByTagName("span")[0]!=null)
                                        {
                                          var id = fixedRow.getElementsByTagName("td")[1].getElementsByTagName("span")[0].innerHTML;
                                          window.open("SM_Warehouse_Allocation.aspx?FLAG=OPEN&&ID=" + id+"&&tm="+time);
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

    </asp:UpdatePanel>

     <div id="AlertDiv" class="AlertStyle">
     <img id="laoding" src="../Assets/images/ajaxloader.gif"  alt="downloading" />
     </div>
    </div><!--box-outer END -->
    </div> <!--box-wrapper END -->                 

</asp:Content>
