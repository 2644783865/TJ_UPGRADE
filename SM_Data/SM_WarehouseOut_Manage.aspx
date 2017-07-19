<%@ Page Language="C#" MasterPageFile="~/Masters/BaseMaster.master" AutoEventWireup="true" CodeBehind="SM_WarehouseOut_Manage.aspx.cs" Inherits="ZCZJ_DPF.SM_Data.SM_WarehouseOut_Manage" Title="出库管理" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
    <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </cc1:ToolkitScriptManager>

    <script type="text/javascript" language="javascript">
    function Disp()
    {
        var i = document.getElementById("<%=PanelCondition.ClientID%>");
        var j = document.getElementById("Display");
        if(i.style.display=="none")
        {
            i.style.display='block';
            j.value = "隐藏条件";
        }
        else
        {
            i.style.display="none";
            j.value = "显示条件";
        }
    }

    function Disp2() {
        var i = document.getElementById("<%=PanelCondition2.ClientID%>");
        var j = document.getElementById("Display2");
        if (i.style.display == "none") {
            i.style.display = 'block';
            j.value = "隐藏条件";
        }
        else {
            i.style.display = "none";
            j.value = "显示条件";
        }
    }

    function Disp3() {
        var i = document.getElementById("<%=PanelCondition3.ClientID%>");
        var j = document.getElementById("Display3");
        if (i.style.display == "none") {
            i.style.display = 'block';
            j.value = "隐藏条件";
        }
        else {
            i.style.display = "none";
            j.value = "显示条件";
        }
    }
    
     function Disp4() {
        var i = document.getElementById("<%=PanelCondition4.ClientID%>");
        var j = document.getElementById("Display4");
        if (i.style.display == "none") {
            i.style.display = 'block';
            j.value = "隐藏条件";
        }
        else {
            i.style.display = "none";
            j.value = "显示条件";
        }
    }

    function antiverifytip() {
        var retVal = confirm("确定要反审该出库单？");
        return retVal;
    }

    function deltip() {
        var retVal = confirm("确定要删除？");
        return retVal;
    }

    function tostorage() {
        window.open('SM_Warehouse_Query.aspx?FLAG=PUSHOUT');
    }
    
   function OutExport()
   {
       var retVal = window.showModalDialog("SM_WarehouseOut_Export.aspx", "", "dialogWidth=800px;dialogHeight=600px;status=no;help=no;scroll=yes");
   }

   function OutExport2() {
       var retVal = window.showModalDialog("SM_WarehouseOut_Export2.aspx", "", "dialogWidth=800px;dialogHeight=600px;status=no;help=no;scroll=yes");
   }

   function OutExport3() {
       var retVal = window.showModalDialog("SM_WarehouseOut_Export3.aspx", "", "dialogWidth=800px;dialogHeight=600px;status=no;help=no;scroll=yes");
   }
   
   function OutExport4() {
       var retVal = window.showModalDialog("SM_WarehouseOut_Export4.aspx", "", "dialogWidth=800px;dialogHeight=600px;status=no;help=no;scroll=yes");
   }

   function openout(tr) {
       var id = tr.getElementsByTagName("td")[3].getElementsByTagName("span")[0].innerHTML;
       window.open("SM_WarehouseOUT_LL.aspx?FLAG=OPEN&&ID=" + id);
   }

   function openout2(tr) {
       var id = tr.getElementsByTagName("td")[3].getElementsByTagName("span")[0].innerHTML;
       window.open("SM_WarehouseOUT_WW.aspx?FLAG=OPEN&&ID=" + id);
   }

   function openout3(tr) {
       var id = tr.getElementsByTagName("td")[3].getElementsByTagName("span")[0].innerHTML;
       window.open("SM_WarehouseOUT_XS.aspx?FLAG=OPEN&&ID=" + id);
   }
   function openout4(tr) {
       var id = tr.getElementsByTagName("td")[3].getElementsByTagName("span")[0].innerHTML;
       window.open("SM_WarehouseOUT_Red.aspx?FLAG=OPEN&&ID=" + id);
   }
   

  function tobackred() {
        window.open('SM_WarehouseOUT_Red.aspx?FLAG=PUSHRED');
    }
        
</script>


    <cc1:TabContainer ID="TabContainer1" runat="server" AutoPostBack="true" OnActiveTabChanged="TabContainer1_ActiveTabChanged">
    
    <cc1:TabPanel ID="Tab1" runat="server" HeaderText="生产领料">
    <ContentTemplate>
    
    <div class="box-inner">
    <div class="box_right">
    <div class="box-title">
    <table width="100%">
        <tr>
            <td align="left">
                <asp:Label ID="LabelMessage" runat="server" ForeColor="Red"></asp:Label>
            </td>
            <td align="right">
                <input id="Display" type="button" value="隐藏条件" onclick="Disp()" />&nbsp;&nbsp;&nbsp;
                <asp:Button ID="Query" runat="server" Text="查询" OnClick="Query_Click" />&nbsp;&nbsp;&nbsp;
                <asp:Button ID="Refresh" runat="server" Text="重置" OnClick="Refresh_Click" />&nbsp;&nbsp;&nbsp; 
                <asp:Button ID="Related" runat="server" Text="关联单据" OnClick="Related_Click" />&nbsp;&nbsp;&nbsp;                                         
                <input id="Export" type="button" value="导出" onclick="OutExport()" />&nbsp;&nbsp;&nbsp;
                <input id="ToStorage" type="button" value="到库存" onclick="tostorage()" />&nbsp;&nbsp;&nbsp;
            </td>
        </tr>
    </table>
    </div>
    </div>
    </div>
    
    <div class="box-wrapper">
    <div class="box-outer">
    <asp:Panel ID="PanelCondition" runat="server" Width="100%" Visible="true">
    <table width="100%">
        <tr>
            <td style="white-space:nowrap;" align="left">
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;状态：<asp:DropDownList ID="DropDownListState" runat="server" AutoPostBack ="true" OnSelectedIndexChanged="DropDownListState_SelectedIndexChanged">
                    <asp:ListItem Text="--请选择--" Value=""></asp:ListItem>
                    <asp:ListItem Text="未审核" Value="1"></asp:ListItem>
                    <asp:ListItem Text="已审核" Value="2"></asp:ListItem>
                </asp:DropDownList>
            </td>
            <td style="white-space:nowrap;" align="left">
                &nbsp;&nbsp;&nbsp;红蓝字：<asp:DropDownList ID="DropDownListColour" runat="server"  AutoPostBack ="true" OnSelectedIndexChanged="DropDownListColour_SelectedIndexChanged">
                    <asp:ListItem Text="--请选择--" Value=""></asp:ListItem>
                    <asp:ListItem Text="蓝字" Value="0"></asp:ListItem>
                    <asp:ListItem Text="红字" Value="1" style="color:Red;"></asp:ListItem>                    
                </asp:DropDownList>     
            </td>
            <td style="white-space:nowrap;" align="left">
            </td>            
            <td style="white-space:nowrap;" align="center">
            </td>
        </tr>
        <tr>
            <td style="white-space:nowrap;" align="left">
                领料单编号：<asp:TextBox ID="TextBoxCode" runat="server"></asp:TextBox>
            </td> 
            <td style="white-space:nowrap;" align="left">
                领料部门：<asp:TextBox ID="TextBoxDep" runat="server"></asp:TextBox>
            </td>
            <td style="white-space:nowrap;" align="left">
                发料仓库：<asp:TextBox ID="TextBoxWarehouse" runat="server"></asp:TextBox>
            </td>
            <td style="white-space:nowrap;" align="left">
               &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;日&nbsp;&nbsp;&nbsp;期：<asp:TextBox ID="TextBoxDate" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="white-space:nowrap;" align="left">
                &nbsp;&nbsp;&nbsp;物料代码：<asp:TextBox ID="TextBoxMCode" runat="server"></asp:TextBox>
            </td>
            <td style="white-space:nowrap;" align="left">
                物料名称：<asp:TextBox ID="TextBoxMName" runat="server"></asp:TextBox>
            </td>
             <td style="white-space:nowrap;" align="left">
                规格型号：<asp:TextBox ID="TextBoxMStandard" runat="server"></asp:TextBox>
            </td>
            <td style="white-space:nowrap;" align="left">
                计划跟踪号：<asp:TextBox ID="TextBoxMPTC" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="white-space:nowrap;" align="left">
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;制单人：<asp:TextBox ID="TextBoxZDR" runat="server"></asp:TextBox>
            </td>
            <td style="white-space:nowrap;" align="left">
                &nbsp;&nbsp;&nbsp;材&nbsp;&nbsp;&nbsp;质：<asp:TextBox ID="TextBoxCZ" runat="server"></asp:TextBox>
            </td>
             <td style="white-space:nowrap;" align="left">
              
            </td>
            <td style="white-space:nowrap;" align="left">
               
            </td>
        </tr>
    </table>
    </asp:Panel>
    
    <asp:Panel ID="PanelBody" runat="server"  style="position:static;overflow:auto; margin:2px" Width="98%" Height="250px">
    
    <table width="100%" align="center" cellpadding="4" cellspacing="1" class="toptable grid" border="1">
        <asp:Repeater ID="RepeaterLL" runat="server" OnItemDataBound="RepeaterLL_ItemDataBound">
            <HeaderTemplate>
            <tr align="center" class="tableTitle1" >
                <td ><strong></strong></td>
                <td width="70px"><strong>日期</strong></td>
                <td width="60px"><strong>审核状态</strong></td>
                <td width="70px"><strong>单据编号</strong></td>
                <td width="70px"><strong>领料部门</strong></td>
                <td width="60px"><strong>发料仓库</strong></td>
                <td width="130px"><strong>计划跟踪号</strong></td>                
                <td width="70px"><strong>物料代码</strong></td>
                <td width="90px"><strong>物料名称</strong></td>
                <td width="60px"><strong>型号规格</strong></td>
                <td width="60px"><strong>材质</strong></td>
                <td width="100px"><strong>批号</strong></td>
                <td width="30px"><strong>单位</strong></td>
                <td width="60px"><strong>实发数量</strong></td>                
                <td  width="30px"><strong>单价</strong></td>                  
                <td  width="30px"><strong>金额</strong></td>
                <td width="100px"><strong>备注</strong></td>                          
            </tr>
            </HeaderTemplate>
             <ItemTemplate>
            <tr align="center" class="baseGadget" onmousemove="this.className='highlight'" onmouseout="this.className='baseGadget'" ondblclick="openout(this)">
                <td ><%#Container.ItemIndex+1%></td>
                <td ><asp:Label ID="LabelDate" runat="server" Text='<%#Eval("Date")%>' width="70px"></asp:Label></td>
                <td><asp:Label ID="LabelState" runat="server" Text='<%#convertState((string)Eval("State"))%>' width="60px"></asp:Label></td>                
                <td><asp:Label ID="LabelCode" runat="server" Text='<%#Eval("Code")%>' width="70px"></asp:Label></td>
                <td><asp:Label ID="LabelDep" runat="server" Text='<%#Eval("Dep")%>' width="70px"></asp:Label></td>
                <td><asp:Label ID="LabelWarehouse" runat="server" Text='<%#Eval("Warehouse")%>' width="60px"></asp:Label>
                    <asp:Label ID="LabelWarehouseCode" runat="server" Text='<%#Eval("WarehouseCode")%>' Visible="false"></asp:Label></td>
                <td><asp:Label ID="LabelPTC" runat="server" Text='<%#Eval("PTC")%>' width="130px"></asp:Label></td>                
                <td>
                    <asp:Label ID="LabelMaterialCode" runat="server" Text='<%#Eval("MaterialCode")%>' width="70px"></asp:Label>
                    <asp:Label ID="LabelUniqueID" runat="server" Text='<%#Eval("UniqueID")%>' Visible="false"></asp:Label>                    
                </td>
                <td><asp:Label ID="LabelMaterialName" runat="server" Text='<%#Eval("MaterialName")%>' width="90px"></asp:Label></td>
                <td><asp:Label ID="LabelMaterialStandard" runat="server" Text='<%#Eval("MaterialStandard")%>' width="60px"></asp:Label></td>
                <td><asp:Label ID="LabelAttribute" runat="server" Text='<%#Eval("Attribute")%>' width="60px"></asp:Label></td>
                <td><asp:Label ID="LabelLotNumber" runat="server" Text='<%#Eval("LotNumber")%>' width="100px"></asp:Label></td>
                <td><asp:Label ID="LabelUnit" runat="server" Text='<%#Eval("Unit")%>' width="30px"></asp:Label></td>
                <td><asp:Label ID="LabelRN" runat="server" Text='<%#Eval("RN")%>' width="60px"></asp:Label></td>
                <td><asp:Label ID="LabelUnitPrice" runat="server" Text='<%#Eval("UnitPrice")%>'  width="30px"></asp:Label></td>
                <td><asp:Label ID="LabelAmount" runat="server" Text='<%#Eval("Amount")%>'  width="30px"></asp:Label></td>
                <td><asp:Label ID="LabelDetailNote" runat="server" Text='<%#Eval("Note")%>' width="100px"></asp:Label></td>
            </tr>
            </ItemTemplate>
            <FooterTemplate>
            <tr class="tableTitle1">
                <td ></td>
                <td >合计:</td>
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
                <td align="center"><asp:Label ID="LabelTotalRN" runat="server"></asp:Label></td>
                <td></td>
                <td align="center"><asp:Label ID="LabelTotalAmount" runat="server"></asp:Label></td>
                <td></td>
            </tr>
            </FooterTemplate>
        </asp:Repeater>
        <asp:Panel ID="NoDataPanel" runat="server" Visible="false">没有相关记录!</asp:Panel>
    </table>
    </asp:Panel>
    
    </div><!--box-outer END -->
    </div> <!--box-wrapper END -->            
    
    </ContentTemplate>
    </cc1:TabPanel>
    
    <cc1:TabPanel ID="Tab2" runat="server" HeaderText="委外加工">
    <ContentTemplate>
    
    <div class="box-inner">
    <div class="box_right">
    <div class="box-title">
    <table width="100%">
        <tr>
            <td align="left">
                <asp:Label ID="LabelMessage2" runat="server" ForeColor="Red"></asp:Label>
            </td>
            <td align="right">
                <input id="Display2" type="button" value="隐藏条件" onclick="Disp2()" />&nbsp;&nbsp;&nbsp;
                <asp:Button ID="Query2" runat="server" Text="查询" OnClick="Query2_Click" />&nbsp;&nbsp;&nbsp;
                <asp:Button ID="Refresh2" runat="server" Text="重置" OnClick="Refresh2_Click" />&nbsp;&nbsp;&nbsp;                                          
                <input id="Export2" type="button" value="导出" onclick="OutExport2()" />&nbsp;&nbsp;&nbsp;
                <input id="ToStorage2" type="button" value="到库存" onclick="tostorage()" />&nbsp;&nbsp;&nbsp;
            </td>
        </tr>
    </table>
    </div>
    </div>
    </div>
    
    <div class="box-wrapper">
    <div class="box-outer">
    <asp:Panel ID="PanelCondition2" runat="server" Width="100%" Visible="true">
    <table width="100%">
        <tr>
            <td style="white-space:nowrap;" align="left">
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;状态：<asp:DropDownList ID="DropDownListState2" runat="server" AutoPostBack ="true" OnSelectedIndexChanged="DropDownListState2_SelectedIndexChanged">
                    <asp:ListItem Text="--请选择--" Value=""></asp:ListItem>
                    <asp:ListItem Text="未审核" Value="1"></asp:ListItem>
                    <asp:ListItem Text="已审核" Value="2"></asp:ListItem>
                </asp:DropDownList>
            </td>
            <td style="white-space:nowrap;" align="left">
                &nbsp;&nbsp;&nbsp;红蓝字：<asp:DropDownList ID="DropDownListColour2" runat="server"  AutoPostBack ="true" OnSelectedIndexChanged="DropDownListColour2_SelectedIndexChanged">
                    <asp:ListItem Text="--请选择--" Value=""></asp:ListItem>
                    <asp:ListItem Text="蓝字" Value="0"></asp:ListItem>
                    <asp:ListItem Text="红字" Value="1" style="color:Red;"></asp:ListItem>                    
                </asp:DropDownList>     
            </td>
            <td style="white-space:nowrap;" align="left">
            </td>            
            <td style="white-space:nowrap;" align="center">
            </td>
        </tr>
        <tr>
            <td style="white-space:nowrap;" align="left">
                领料单编号：<asp:TextBox ID="TextBoxCode2" runat="server"></asp:TextBox>
            </td> 
            <td style="white-space:nowrap;" align="left">
                加工单位：<asp:TextBox ID="TextBoxCompany2" runat="server"></asp:TextBox>
            </td>
            <td style="white-space:nowrap;" align="left">
                发料仓库：<asp:TextBox ID="TextBoxWarehouse2" runat="server"></asp:TextBox>
            </td>
            <td style="white-space:nowrap;" align="left">
               &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;日&nbsp;&nbsp;&nbsp;期：<asp:TextBox ID="TextBoxDate2" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="white-space:nowrap;" align="left">
                &nbsp;&nbsp;&nbsp;物料代码：<asp:TextBox ID="TextBoxMaterialCode2" runat="server"></asp:TextBox>
            </td>
            <td style="white-space:nowrap;" align="left">
                物料名称：<asp:TextBox ID="TextBoxMaterialName2" runat="server"></asp:TextBox>
            </td>
             <td style="white-space:nowrap;" align="left">
                规格型号：<asp:TextBox ID="TextBoxStandard2" runat="server"></asp:TextBox>
            </td>
            <td style="white-space:nowrap;" align="left">
                计划跟踪号：<asp:TextBox ID="TextBoxPTC2" runat="server"></asp:TextBox>
            </td>
        </tr>
        
        <tr>
            <td style="white-space:nowrap;" align="left">
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;制单人：<asp:TextBox ID="TextBoxZDR2" runat="server"></asp:TextBox>
            </td>
            <td style="white-space:nowrap;" align="left">
                &nbsp;&nbsp;&nbsp;材&nbsp;&nbsp;&nbsp;质：<asp:TextBox ID="TextBoxCZ2" runat="server"></asp:TextBox>
            </td>
             <td style="white-space:nowrap;" align="left">
              
            </td>
            <td style="white-space:nowrap;" align="left">
               
            </td>
        </tr>
    </table>
    </asp:Panel>
    
    <asp:Panel ID="PanelBody2" runat="server" style="position:static;overflow:auto; margin:2px" Width="98%" Height="250px">
    <table width="100%" align="center" cellpadding="4" cellspacing="1" class="toptable grid" border="1">
        <asp:Repeater ID="RepeaterWW" runat="server" OnItemDataBound="RepeaterWW_ItemDataBound">
            <HeaderTemplate>
            <tr align="center" class="tableTitle1">
                <td><strong></strong></td>
                <td width="70px"><strong>日期</strong></td>
                <td width="60px"><strong>审核状态</strong></td>
                <td width="70px"><strong>单据编号</strong></td>
                <td width="120px"><strong>加工单位</strong></td>  
                <td width="60px"><strong>委外类型</strong></td>
                <td width="60px"><strong>发料仓库</strong></td>
                <td width="130px"><strong>计划跟踪号</strong></td>     
                <td width="70px"><strong>物料代码</strong></td>
                <td width="90px"><strong>物料名称</strong></td>
                <td width="60px"><strong>型号规格</strong></td>
                <td width="30px"><strong>单位</strong></td>
                <td width="60px"><strong>实发数量</strong></td>                
                <td  width="30px"><strong>单价</strong></td>                  
                <td  width="30px"><strong>金额</strong></td>
                <td width="100px"><strong>备注</strong></td>                                           
            </tr>
            </HeaderTemplate>
             <ItemTemplate>
            <tr align="center" class="baseGadget" onmousemove="this.className='highlight'" onmouseout="this.className='baseGadget'" ondblclick="openout2(this)">
                <td><%#Container.ItemIndex+1%></td>
                <td><asp:Label ID="LabelDate" runat="server" Text='<%#Eval("Date")%>'  width="70px"></asp:Label></td>
                <td><asp:Label ID="LabelState" runat="server" Text='<%#convertState2((string)Eval("State"))%>' width="60px"></asp:Label></td> 
                <td><asp:Label ID="LabelCode" runat="server" Text='<%#Eval("Code")%>' width="70px"></asp:Label></td>               
                <td><asp:Label ID="LabelCompany" runat="server" Text='<%#Eval("Company")%>' width="120px"></asp:Label></td>                
                <td><asp:Label ID="LabelProcessType" runat="server" Text='<%#Eval("ProcessType")%>' width="60px"></asp:Label></td>
                <td><asp:Label ID="LabelWarehouse" runat="server" Text='<%#Eval("Warehouse")%>' width="60px"></asp:Label>
                    <asp:Label ID="LabelWarehouseCode" runat="server" Text='<%#Eval("WarehouseCode")%>' Visible="false"></asp:Label></td>         
                
                <td><asp:Label ID="LabelPTC" runat="server" Text='<%#Eval("PTC")%>' width="130px"></asp:Label></td>
                <td>
                    <asp:Label ID="LabelMaterialCode" runat="server" Text='<%#Eval("MaterialCode")%>' width="70px"></asp:Label>
                    <asp:Label ID="LabelUniqueID" runat="server" Text='<%#Eval("UniqueID")%>' Visible="false"></asp:Label>                    
                </td>
                <td><asp:Label ID="LabelMaterialName" runat="server" Text='<%#Eval("MaterialName")%>' width="90px"></asp:Label></td>
                <td><asp:Label ID="LabelMaterialStandard" runat="server" Text='<%#Eval("MaterialStandard")%>' width="60px"></asp:Label></td>
                <td><asp:Label ID="LabelUnit" runat="server" Text='<%#Eval("Unit")%>' width="30px"></asp:Label></td>
                <td><asp:Label ID="LabelRN" runat="server" Text='<%#Eval("RN")%>' width="60px"></asp:Label></td>           
                <td><asp:Label ID="LabelUnitPrice" runat="server" Text='<%#Eval("UnitPrice")%>' width="30px"></asp:Label></td>
                <td><asp:Label ID="LabelAmount" runat="server" Text='<%#Eval("Amount")%>' width="30px"></asp:Label></td>
                <td><asp:Label ID="LabelDetailNote" runat="server" Text='<%#Eval("Note")%>' width="100px"></asp:Label></td>
            </tr>
            </ItemTemplate>
            <FooterTemplate>
            <tr class="tableTitle1">
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
                <td align="center"><asp:Label ID="LabelTotalRN" runat="server"></asp:Label></td>
                <td></td>
                <td align="center"><asp:Label ID="LabelTotalAmount" runat="server"></asp:Label></td>
                <td></td>
            </tr>
            </FooterTemplate>
        </asp:Repeater>
        <asp:Panel ID="NoDataPanel2" runat="server" Visible="false">没有相关记录!</asp:Panel>
    </table>
    </asp:Panel>
    
    </div><!--box-outer END -->
    </div> <!--box-wrapper END -->        
    
    </ContentTemplate>
    </cc1:TabPanel>
    
    <cc1:TabPanel ID="Tab3" runat="server" HeaderText="销售出库">
    <ContentTemplate>
    
    <div class="box-inner">
    <div class="box_right">
    <div class="box-title">
    <table width="100%">
        <tr>
            <td align="left">
                <asp:Label ID="LabelMessage3" runat="server" ForeColor="Red"></asp:Label>
            </td>
            <td align="right">
                <input id="Display3" type="button" value="隐藏条件" onclick="Disp3()" />&nbsp;&nbsp;&nbsp;
                <asp:Button ID="Query3" runat="server" Text="查询" OnClick="Query3_Click" />&nbsp;&nbsp;&nbsp;
                <asp:Button ID="Refresh3" runat="server" Text="重置" OnClick="Refresh3_Click" />&nbsp;&nbsp;&nbsp;                                          
                <input id="Export3" type="button" value="导出" onclick="OutExport3()" />&nbsp;&nbsp;&nbsp;
                <input id="ToStorage3" type="button" value="到库存" onclick="tostorage()" />&nbsp;&nbsp;&nbsp;
            </td>
        </tr>
    </table>
    </div>
    </div>
    </div>
    
    <div class="box-wrapper">
    <div class="box-outer">
    <asp:Panel ID="PanelCondition3" runat="server" Width="100%" Visible="true">
    <table width="100%">
        <tr>
            <td style="white-space:nowrap;" align="left">
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;状态：<asp:DropDownList ID="DropDownListState3" runat="server" AutoPostBack ="true" OnSelectedIndexChanged="DropDownListState3_SelectedIndexChanged">
                    <asp:ListItem Text="--请选择--" Value=""></asp:ListItem>
                    <asp:ListItem Text="未审核" Value="1"></asp:ListItem>
                    <asp:ListItem Text="已审核" Value="2"></asp:ListItem>
                </asp:DropDownList>
            </td>
            <td style="white-space:nowrap;" align="left">
                &nbsp;&nbsp;&nbsp;红蓝字：<asp:DropDownList ID="DropDownListColour3" runat="server"  AutoPostBack ="true" OnSelectedIndexChanged="DropDownListColour3_SelectedIndexChanged">
                    <asp:ListItem Text="--请选择--" Value=""></asp:ListItem>
                    <asp:ListItem Text="蓝字" Value="0"></asp:ListItem>
                    <asp:ListItem Text="红字" Value="1" style="color:Red;"></asp:ListItem>                    
                </asp:DropDownList>     
            </td>
            <td style="white-space:nowrap;" align="left">
            </td>            
            <td style="white-space:nowrap;" align="center">
            </td>
        </tr>
        <tr>
            <td style="white-space:nowrap;" align="left">
                领料单编号：<asp:TextBox ID="TextBoxCode3" runat="server"></asp:TextBox>
            </td> 
            <td style="white-space:nowrap;" align="left">
                购货单位：<asp:TextBox ID="TextBoxCompany3" runat="server"></asp:TextBox>
            </td>
            <td style="white-space:nowrap;" align="left">
                发料仓库：<asp:TextBox ID="TextBoxWarehouse3" runat="server"></asp:TextBox>
            </td>
            <td style="white-space:nowrap;" align="left">
               &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;日&nbsp;&nbsp;&nbsp;期：<asp:TextBox ID="TextBoxDate3" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="white-space:nowrap;" align="left">
                &nbsp;&nbsp;&nbsp;物料代码：<asp:TextBox ID="TextBoxMaterialCode3" runat="server"></asp:TextBox>
            </td>
            <td style="white-space:nowrap;" align="left">
                物料名称：<asp:TextBox ID="TextBoxMaterialName3" runat="server"></asp:TextBox>
            </td>
             <td style="white-space:nowrap;" align="left">
                规格型号：<asp:TextBox ID="TextBoxStandard3" runat="server"></asp:TextBox>
            </td>
            <td style="white-space:nowrap;" align="left">
                计划跟踪号：<asp:TextBox ID="TextBoxPTC3" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="white-space:nowrap;" align="left">
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;制单人：<asp:TextBox ID="TextBoxZDR3" runat="server"></asp:TextBox>
            </td>
            <td style="white-space:nowrap;" align="left">
                &nbsp;&nbsp;&nbsp;材&nbsp;&nbsp;&nbsp;质：<asp:TextBox ID="TextBoxCZ3" runat="server"></asp:TextBox>
            </td>
             <td style="white-space:nowrap;" align="left">
              
            </td>
            <td style="white-space:nowrap;" align="left">
               
            </td>
        </tr>
    </table>
    </asp:Panel>
    
    <asp:Panel ID="Panel2" runat="server"  style="position:static; overflow:auto;" Width="100%" Height="250px">
    <table width="100%" align="center" cellpadding="4" cellspacing="1" class="toptable grid" border="1">
        <asp:Repeater ID="RepeaterXS" runat="server" OnItemDataBound="RepeaterXS_ItemDataBound">
            <HeaderTemplate>
            <tr align="center" class="tableTitle1">
                <td ><strong></strong></td>
                <td width="70px"><strong>日期</strong></td>
                <td width="60px"><strong>审核状态</strong></td>
                <td width="70px"><strong>单据编号</strong></td>               
                <td width="150px"><strong>购货单位</strong></td>  
                <td width="60px"><strong>发料仓库</strong></td>
                <td width="130px"><strong>计划跟踪号</strong></td>     
                <td width="70px"><strong>物料代码</strong></td>
                <td width="90px"><strong>物料名称</strong></td>
                <td width="60px"><strong>型号规格</strong></td>
                <td width="60px"><strong>材质</strong></td>
                <td width="30px"><strong>单位</strong></td>
                <td width="100px"><strong>批号</strong></td>                
                <td width="60px"><strong>销售数量</strong></td>
                <td width="30px"><strong>单价</strong></td>                  
                <td width="30px"><strong>金额</strong></td>                              
                <td width="50px"><strong>部门</strong></td>                  
                <td width="40px"><strong>业务员</strong></td>
                <td width="60px"><strong>销售单价</strong></td>
                <td width="60px"><strong>销售金额</strong></td>      
                <td width="100px"><strong>备注</strong></td>                                                     
            </tr>
            </HeaderTemplate>
             <ItemTemplate>
            <tr align="center" class="baseGadget" onmousemove="this.className='highlight'" onmouseout="this.className='baseGadget'" ondblclick="openout3(this)">
                <td><%#Container.ItemIndex+1%></td>
                <td><asp:Label ID="LabelDate" runat="server" Text='<%#Eval("Date")%>' width="70px"></asp:Label></td>
                <td><asp:Label ID="LabelState" runat="server" Text='<%#convertState3((string)Eval("State"))%>' width="60px"></asp:Label></td>                
                <td><asp:Label ID="LabelCode" runat="server" Text='<%#Eval("Code")%>' width="70px"></asp:Label></td>
                <td><asp:Label ID="LabelCompany" runat="server" Text='<%#Eval("Company")%>' width="150px"></asp:Label></td>
                <td><asp:Label ID="LabelWarehouse" runat="server" Text='<%#Eval("Warehouse")%>' width="60px"></asp:Label>
                    <asp:Label ID="LabelWarehouseCode" runat="server" Text='<%#Eval("WarehouseCode")%>' Visible="false"></asp:Label></td>         
                <td>
                    <asp:Label ID="LabelPTC" runat="server" Text='<%#Eval("PTC")%>' width="130px"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="LabelMaterialCode" runat="server" Text='<%#Eval("MaterialCode")%>' width="70px"></asp:Label>
                    <asp:Label ID="LabelUniqueID" runat="server" Text='<%#Eval("UniqueID")%>' Visible="false"></asp:Label>                    
                </td>
                <td><asp:Label ID="LabelMaterialName" runat="server" Text='<%#Eval("MaterialName")%>' width="90px"></asp:Label></td>
                
                <td><asp:Label ID="LabelMaterialStandard" runat="server" Text='<%#Eval("MaterialStandard")%>' width="60px"></asp:Label></td>
                <td><asp:Label ID="LabelAttribute" runat="server" Text='<%#Eval("Attribute")%>' width="60px"></asp:Label></td>
                <td><asp:Label ID="LabelUnit" runat="server" Text='<%#Eval("Unit")%>' width="30px"></asp:Label></td>
                <td><asp:Label ID="LabelLotNumber" runat="server" Text='<%#Eval("LotNumber")%>' width="100px"></asp:Label></td>
                <td><asp:Label ID="LabelRN" runat="server" Text='<%#Eval("RN")%>' width="60px"></asp:Label></td>
                <td><asp:Label ID="LabelUnitCost" runat="server" Text='<%#Eval("UnitPrice")%>' width="30px"></asp:Label></td>
                <td><asp:Label ID="LabelCost" runat="server" Text='<%#Eval("Amount")%>' width="30px"></asp:Label></td>
                <td><asp:Label ID="LabelDep" runat="server" Text='<%#Eval("Dep")%>' width="50px"></asp:Label></td>
                <td><asp:Label ID="LabelClerk" runat="server" Text='<%#Eval("Clerk")%>' width="40px"></asp:Label></td>
                <td><asp:Label ID="LabelUnitPrice" runat="server" Text='<%#Eval("UnitCost")%>' width="60px"></asp:Label></td>
                <td><asp:Label ID="LabelAmount" runat="server" Text='<%#Eval("Cost")%>' width="60px"></asp:Label></td>
                <td><asp:Label ID="LabelDetailNote" runat="server" Text='<%#Eval("Note")%>' width="100px"></asp:Label></td>
            </tr>
                 
            </ItemTemplate>
            <FooterTemplate>
            <tr class="tableTitle1">
                <td ></td>
                <td>合计:</td>
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
                <td><asp:Label ID="LabelTotalRN" runat="server"></asp:Label></td>
                <td></td>
                <td><asp:Label ID="LabelTotalCost" runat="server"></asp:Label></td>
                <td></td>
                <td></td>
                <td></td>
                <td><asp:Label ID="LabelTotalAmount" runat="server"></asp:Label></td>
                <td></td>
            </tr>
            </FooterTemplate>
        </asp:Repeater>
        <asp:Panel ID="NoDataPanel3" runat="server" Visible="false">没有相关记录!</asp:Panel>
    </table>
    </asp:Panel>
    
    </div><!--box-outer END -->
    </div> <!--box-wrapper END -->
    
    </ContentTemplate>
    </cc1:TabPanel>
    
    <cc1:TabPanel ID="Tab4" runat="server" HeaderText="其他出库">
    
    <ContentTemplate>
    
    <div class="box-inner">
    <div class="box_right">
    <div class="box-title">
    <table width="100%">
        <tr>
            <td align="left">
                <asp:Label ID="LabelMessage4" runat="server" ForeColor="Red"></asp:Label>
            </td>
            <td align="right">
                <input id="Display4" type="button" value="隐藏条件" onclick="Disp4()" />&nbsp;&nbsp;&nbsp;
                <asp:Button ID="Query4" runat="server" Text="查询" OnClick="Query4_Click" />&nbsp;&nbsp;&nbsp;
                <asp:Button ID="Refresh4" runat="server" Text="重置" OnClick="Refresh4_Click" />&nbsp;&nbsp;&nbsp;                                          
                <input id="Export4" type="button" value="导出" onclick="OutExport4()" />&nbsp;&nbsp;&nbsp;
                <input id="backred" type="button" value="退库" onclick="tobackred()" />&nbsp;&nbsp;&nbsp;
            </td>
        </tr>
    </table>
    </div>
    </div>
    </div>
    
    <div class="box-wrapper">
    <div class="box-outer">
    <asp:Panel ID="PanelCondition4" runat="server" Width="100%" Visible="true">
    <table width="100%">
        <tr>
            <td style="white-space:nowrap;" align="left">
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;状态：<asp:DropDownList ID="DropDownListState4" runat="server" AutoPostBack ="true" OnSelectedIndexChanged="DropDownListState4_SelectedIndexChanged">
                    <asp:ListItem Text="--请选择--" Value=""></asp:ListItem>
                    <asp:ListItem Text="未审核" Value="1"></asp:ListItem>
                    <asp:ListItem Text="已审核" Value="2"></asp:ListItem>
                </asp:DropDownList>
            </td>
            <td style="white-space:nowrap;" align="left">
                   
            </td>
            <td style="white-space:nowrap;" align="left">
            </td>            
            <td style="white-space:nowrap;" align="center">
            </td>
        </tr>
        <tr>
            <td style="white-space:nowrap;" align="left">
                领料单编号：<asp:TextBox ID="TextBoxCode4" runat="server"></asp:TextBox>
            </td> 
            <td style="white-space:nowrap;" align="left">
                领料部门：<asp:TextBox ID="TextBoxDep4" runat="server"></asp:TextBox>
            </td>
            <td style="white-space:nowrap;" align="left">
                发料仓库：<asp:TextBox ID="TextBoxWarehouse4" runat="server"></asp:TextBox>
            </td>
            <td style="white-space:nowrap;" align="left">
               &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;日&nbsp;&nbsp;&nbsp;期：<asp:TextBox ID="TextBoxDate4" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="white-space:nowrap;" align="left">
                &nbsp;&nbsp;&nbsp;物料代码：<asp:TextBox ID="TextBoxMCode4" runat="server"></asp:TextBox>
            </td>
            <td style="white-space:nowrap;" align="left">
                物料名称：<asp:TextBox ID="TextBoxMName4" runat="server"></asp:TextBox>
            </td>
             <td style="white-space:nowrap;" align="left">
                规格型号：<asp:TextBox ID="TextBoxMStandard4" runat="server"></asp:TextBox>
            </td>
            <td style="white-space:nowrap;" align="left">
                计划跟踪号：<asp:TextBox ID="TextBoxMPTC4" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="white-space:nowrap;" align="left">
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;制单人：<asp:TextBox ID="TextBoxZDR4" runat="server"></asp:TextBox>
            </td>
            <td style="white-space:nowrap;" align="left">
                &nbsp;&nbsp;&nbsp;材&nbsp;&nbsp;&nbsp;质：<asp:TextBox ID="TextBoxCZ4" runat="server"></asp:TextBox>
            </td>
             <td style="white-space:nowrap;" align="left">
              
            </td>
            <td style="white-space:nowrap;" align="left">
               
            </td>
        </tr>
    </table>
    </asp:Panel>
    
    <asp:Panel ID="Panel3" runat="server"  style="position:static; overflow:auto;" Width="100%" Height="250px">
    <table width="100%" align="center" cellpadding="4" cellspacing="1" class="toptable grid" border="1">
        <asp:Repeater ID="RepeaterQT" runat="server" OnItemDataBound="RepeaterQT_ItemDataBound">
            <HeaderTemplate>
            <tr align="center" class="tableTitle1">
                <td><strong></strong></td>
                <td  width="70px"><strong>日期</strong></td>
                <td width="60px"><strong>审核状态</strong></td>
                <td width="70px"><strong>单据编号</strong></td>
                <td width="60px"><strong>领料部门</strong></td>
                <td width="60px"><strong>发料仓库</strong></td>
                <td width="120px"><strong>计划跟踪号</strong></td>     
                <td width="70px"><strong>物料代码</strong></td>
                <td width="90px"><strong>物料名称</strong></td>
                <td width="150px"><strong>型号规格</strong></td>
                <td width="60px"><strong>材质</strong></td>
                <td width="100px"><strong>批号</strong></td>
                <td width="30px"><strong>单位</strong></td>
                <td width="60px"><strong>实发数量</strong></td>                
                <td  width="30px"><strong>单价</strong></td>                  
                <td  width="30px"><strong>金额</strong></td>
                <td width="100px"><strong>备注</strong></td>                        
            </tr>
            </HeaderTemplate>
             <ItemTemplate>
            <tr align="center" class="baseGadget" onmousemove="this.className='highlight'" onmouseout="this.className='baseGadget'" ondblclick="openout4(this)">
                <td><%#Container.ItemIndex+1%></td>
                <td><asp:Label ID="LabelDate" runat="server" Text='<%#Eval("Date")%>' width="70px"></asp:Label></td>
                <td><asp:Label ID="LabelState" runat="server" Text='<%#convertState4((string)Eval("State"))%>' width="60px"></asp:Label></td>                
                <td><asp:Label ID="LabelCode" runat="server" Text='<%#Eval("Code")%>' width="70px"></asp:Label></td>
                <td><asp:Label ID="LabelDep" runat="server" Text='<%#Eval("Dep")%>' width="60px"></asp:Label></td>
                <td><asp:Label ID="LabelWarehouse" runat="server" Text='<%#Eval("Warehouse")%>' width="60px"></asp:Label>
                    <asp:Label ID="LabelWarehouseCode" runat="server" Text='<%#Eval("WarehouseCode")%>' Visible="false"></asp:Label></td>
                <td><asp:Label ID="LabelPTC" runat="server" Text='<%#Eval("PTC")%>' width="120px"></asp:Label></td>                
                <td>
                    <asp:Label ID="LabelMaterialCode" runat="server" Text='<%#Eval("MaterialCode")%>' width="70px"></asp:Label>
                    <asp:Label ID="LabelUniqueID" runat="server" Text='<%#Eval("UniqueID")%>' Visible="false"></asp:Label>                    
                </td>
                <td><asp:Label ID="LabelMaterialName" runat="server" Text='<%#Eval("MaterialName")%>' width="90px"></asp:Label></td>
                <td><asp:Label ID="LabelMaterialStandard" runat="server" Text='<%#Eval("MaterialStandard")%>' width="150px"></asp:Label></td>
                <td><asp:Label ID="LabelAttribute" runat="server" Text='<%#Eval("Attribute")%>' width="60px"></asp:Label></td>
                <td><asp:Label ID="LabelLotNumber" runat="server" Text='<%#Eval("LotNumber")%>'  width="100px"></asp:Label></td>
                <td><asp:Label ID="LabelUnit" runat="server" Text='<%#Eval("Unit")%>' width="30px"></asp:Label></td>
                <td><asp:Label ID="LabelRN" runat="server" Text='<%#Eval("RN")%>'  width="60px"></asp:Label></td>
                <td><asp:Label ID="LabelUnitPrice" runat="server" Text='<%#Eval("UnitPrice")%>'  width="30px"></asp:Label></td>
                <td><asp:Label ID="LabelAmount" runat="server" Text='<%#Eval("Amount")%>' width="30px"></asp:Label></td>
                <td><asp:Label ID="LabelDetailNote" runat="server" Text='<%#Eval("Note")%>'  width="100px"></asp:Label></td>
            </tr>
            </ItemTemplate>
            <FooterTemplate>
            <tr class="tableTitle1">
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
                <td align="center"><asp:Label ID="LabelTotalRN" runat="server"></asp:Label></td>
                <td></td>
                <td align="center"><asp:Label ID="LabelTotalAmount" runat="server"></asp:Label></td>
                <td></td>
            </tr>
            </FooterTemplate>
        </asp:Repeater>
        <asp:Panel ID="NoDataPanel4" runat="server" Visible="false">没有相关记录!</asp:Panel>
    </table>
    </asp:Panel>
    
    </div><!--box-outer END -->
    </div> <!--box-wrapper END -->   
    
    
    
    
    </ContentTemplate>
    </cc1:TabPanel>
    
    </cc1:TabContainer>
 
          

</asp:Content>
