<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SM_Warehouse_ProjTempManage.aspx.cs" MasterPageFile="~/Masters/SMBaseMaster.Master" Inherits="ZCZJ_DPF.SM_Data.SM_Warehouse_ProjTempManage" 
Title="项目结转备库管理"
%>



<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<%@ Register src="../Controls/UCPaging.ascx" tagname="UCPaging" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PrimaryContent" runat="server">


<script src="SM_JS/SelectCondition.js" type="text/javascript"></script>

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
           fixedCols : 5,
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
                                            window.open("SM_Warehouse_ProjTemp.aspx?FLAG=OPEN&&ID=" + id+"&&tm="+time);
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

    window.open('SM_Warehouse_Query.aspx?FLAG=PUSHPROJTEMP');
}

function projtempexport() {
    var retVal = window.showModalDialog("SM_Warehouse_ProjTemp_Export.aspx", "", "dialogWidth=780px;dialogHeight=600px;status=no;help=no;scroll=yes");
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
      
    <table width="98%">
        <tr>
            <td align="left" style="width:auto">
             <asp:CheckBox ID="CheckBoxShow" runat="server" AutoPostBack="true" Text="单据头完整显示" OnCheckedChanged="CheckBoxShow_CheckedChanged" />
                <asp:Label ID="LabelMessage" runat="server" ForeColor="Red" Visible="false" ></asp:Label>
                &nbsp;&nbsp;
            </td>
            <td style="width:auto" >
            
               <asp:CheckBox runat="server" ID="MyTask" Text="我的任务" AutoPostBack="true" Checked="true" OnCheckedChanged="MyTask_CheckedChanged" /> 
              
              </td>
              <td style="width:auto">
               
               <asp:RadioButtonList ID="RadioButtonListState" runat="server" OnSelectedIndexChanged="RadioButtonListState_SelectedIndexChanged" AutoPostBack="true" RepeatDirection="Horizontal">
                                <asp:ListItem Text="全部" Value=""></asp:ListItem>
                                <asp:ListItem Text="未提交" Value="0" Selected="True"></asp:ListItem>
                                <asp:ListItem Text="已提交" Value="1"></asp:ListItem>
                                <asp:ListItem Text="待审批" Value="2"></asp:ListItem>
                                <asp:ListItem Text="已审批" Value="3"></asp:ListItem>                               
                                
                            </asp:RadioButtonList>                            
                         
             </td>
             <td align="right" style="width:auto">
                <asp:Button ID="btnShowPopup" runat="server" Text="筛选" OnClientClick="viewCondition()"/>&nbsp;&nbsp;
                <asp:ModalPopupExtender ID="ModalPopupExtenderSearch" runat="server" 
                    TargetControlID="btnShowPopup" PopupControlID="UpdatePanelCondition"  Drag="false"  
                    Enabled="True"  DynamicServicePath=""  Y="30" >
                </asp:ModalPopupExtender> 
                
                <input id="ProjtempExport" type="button" value="导出" onclick="projtempexport()" runat="server" visible="false"/>&nbsp;&nbsp;
                
                <asp:Button runat="server" ID="BtnShowExport" OnClick="BtnShowExport_Click" Text="导 出" />&nbsp;&nbsp;
                
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
            <td style="white-space:nowrap;width:25%" align="left">&nbsp;</td>
            <td style="white-space:nowrap;width:25%" align="left">&nbsp;</td>
            <td style="white-space:nowrap;width:25%" align="left">&nbsp;</td>
            <td style="white-space:nowrap;width:25%" align="center">
               <asp:Button ID="QueryButton" runat="server" Text="查询"  OnClick="QueryButton_Click" />&nbsp;
               <asp:Button ID="btnClose" runat="server" Text="取消" OnClick="btnClose_Click" />&nbsp;
               <asp:Button ID="btnReset" runat="server" Text="重置" OnClick="btnReset_Click"/>&nbsp;
            </td>   
         </tr>
       
       <tr>
        <td colspan="2" style="width:50%">         
           <table width="100%">     
            <tr>
                <td style="width:50%;white-space:nowrap;" align="left">
                  项目结转单号：<asp:TextBox ID="TextBoxPTCode" runat="server"></asp:TextBox>
                </td>
                <td style="width:50%;white-space:nowrap;" align="left">
                  &nbsp;&nbsp;&nbsp;生&nbsp;产&nbsp;制&nbsp;号：<asp:TextBox ID="TextBoxSCZH" runat="server"></asp:TextBox>
                </td>
               
            </tr>
            <tr>
               <td style="width:50%;white-space:nowrap;" align="left">
                  &nbsp;&nbsp;&nbsp;项&nbsp;目&nbsp;名&nbsp;称：<asp:TextBox ID="TextBoxProjname" runat="server"></asp:TextBox>
                </td>
                <td style="width:50%;white-space:nowrap;" align="left">
                   &nbsp;&nbsp;&nbsp;工&nbsp;程&nbsp;名&nbsp;称：<asp:TextBox ID="TextBoxEngname" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="width:50%;white-space:nowrap;" align="left">
                   到计划跟踪号：<asp:TextBox ID="TextBoxToPTC" runat="server"></asp:TextBox>
                </td>
                <td style="width:50%;white-space:nowrap;" align="left">
                  从计划跟踪号：<asp:TextBox ID="TextBoxFromPTC" runat="server"></asp:TextBox>
                </td> 
             </tr>
              <tr>
                 <td style="width:50%;white-space:nowrap;" align="left">
                   &nbsp;&nbsp;&nbsp;物&nbsp;料&nbsp;代&nbsp;码：<asp:TextBox ID="TextBoxCode" runat="server"></asp:TextBox>
                </td>
                <td style="width:50%;white-space:nowrap;" align="left">
                  &nbsp;&nbsp;&nbsp;物&nbsp;料&nbsp;名&nbsp;称：<asp:TextBox ID="TextBoxName" runat="server"></asp:TextBox>
                </td>
             </tr>
            <tr>                
                <td style="width:50%;white-space:nowrap;" align="left">
                   &nbsp;&nbsp;&nbsp;规&nbsp;格&nbsp;型&nbsp;号：<asp:TextBox ID="TextBoxStandard" runat="server"></asp:TextBox>
                </td>
                <td style="width:50%;white-space:nowrap;" align="left">
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;材&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;质：<asp:TextBox ID="TextBoxcaizhi" runat="server"></asp:TextBox>
                </td>
            </tr>
              <tr>                
                <td style="width:50%;white-space:nowrap;" align="left">
                     &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;国&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;标：<asp:TextBox ID="TextBoxGB" runat="server"></asp:TextBox>
                </td>
                <td style="width:50%;white-space:nowrap;" align="left">
                   &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;批&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;号：<asp:TextBox ID="TextBoxPihao" runat="server"></asp:TextBox>
                </td>
            </tr>          
            
            <tr>
               <td style="width:50%;white-space:nowrap;" align="left">
                 &nbsp;&nbsp;&nbsp;调&nbsp;整&nbsp;数&nbsp;量：<asp:TextBox ID="TextBoxTnum" runat="server"></asp:TextBox>
                </td>
                 <td style="width:50%;white-space:nowrap;" align="left">
                   调整张(支)数：<asp:TextBox ID="TextBoxTFnum" runat="server"></asp:TextBox>
                </td>
            </tr>
             <tr>
               <td style="width:50%;white-space:nowrap;" align="left">
                  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;长：<asp:TextBox ID="TextBoxLength" runat="server"></asp:TextBox>
                </td>
                 <td style="width:50%;white-space:nowrap;" align="left">
                  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;宽：<asp:TextBox ID="TextBoxWidth" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr> 
                <td style="width:50%;white-space:nowrap;" align="left">
                    &nbsp;&nbsp;&nbsp;是&nbsp;否&nbsp;定&nbsp;尺：<asp:TextBox ID="TextBoxFixed" runat="server"></asp:TextBox>
                </td>                
                <td style="width:50%;white-space:nowrap;" align="left">
                    &nbsp;&nbsp;&nbsp;审&nbsp;批&nbsp;意&nbsp;见：<asp:TextBox ID="TextBoxSuggestion" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="width:50%;white-space:nowrap;" align="left">
                   &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;制&nbsp;&nbsp;单&nbsp;&nbsp;人：<asp:TextBox ID="TextBoxDoc" runat="server"></asp:TextBox>
                </td>
                <td style="width:50%;white-space:nowrap;" align="left">
                  &nbsp;&nbsp;&nbsp;制&nbsp;单&nbsp;日&nbsp;期：<asp:TextBox ID="TextBoxDate" runat="server"></asp:TextBox>
                </td>                
            </tr>
             <tr> 
                <td style="width:50%;white-space:nowrap;" align="left">
                   &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;技&nbsp;&nbsp;术&nbsp;&nbsp;员：<asp:TextBox ID="TextBoxVerifier" runat="server"></asp:TextBox>
                </td>
                <td style="width:50%;white-space:nowrap;" align="left">
                  &nbsp;&nbsp;&nbsp;审&nbsp;核&nbsp;日&nbsp;期：<asp:TextBox ID="TextBoxVerifyDate" runat="server"></asp:TextBox>
                </td>
             </tr>         
             
            <tr>          
                <td style="width:50%;white-space:nowrap;" align="left">
                   &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;仓&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;库：<asp:TextBox ID="TextBoxWarehouse" runat="server"></asp:TextBox>
                </td>
                <td style="width:50%;white-space:nowrap;" align="left">
                 &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;仓&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;位：<asp:TextBox ID="TextBoxLocation" runat="server"></asp:TextBox>
                </td>                      
            </tr>
            <tr>             
                <td style="width:50%;white-space:nowrap;" align="left">
                  &nbsp;&nbsp;&nbsp;剩&nbsp;余&nbsp;原&nbsp;因：<asp:TextBox ID="TextBoxShengYuNote" runat="server"></asp:TextBox>
                </td>
                
                <td style="width:50%;white-space:nowrap;" align="left">
                &nbsp;&nbsp;&nbsp;审&nbsp;批&nbsp;时&nbsp;间：<asp:TextBox ID="TextBoxShenPiDate" runat="server"></asp:TextBox>
                </td>                      
            </tr>
             <tr>   
                <td style="width:50%;white-space:nowrap;" align="left">
                </td>
                <td style="width:50%;white-space:nowrap;" align="left">
                   &nbsp;&nbsp;&nbsp;&nbsp;逻&nbsp;&nbsp;&nbsp;辑：
                  <asp:DropDownList ID="DropDownListFatherLogic" runat="server">
                    <asp:ListItem Value="AND" Selected="True">并且</asp:ListItem>
                    <asp:ListItem Value="OR">或者</asp:ListItem>
                    <asp:ListItem></asp:ListItem>
                 </asp:DropDownList>
                </td>                      
            </tr>
         </table> 
        </td> 
        <td  colspan="2" style="width:50%">
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
                <td>到计划跟踪号</td>
                <td>项目名称</td>
                <td>工程名称</td>                
                <td>物料代码</td>
                <td>物料名称</td>
                <td>型号规格</td>
                <td>国标</td>
                <td>材质</td>
                <td>是否定尺</td>
                <td>长</td>
                <td>宽</td>
                <td>批号</td>
                <td>单位</td>
                <td>调整数量</td>
                <td>调整张数</td>
                <td>从计划跟踪号</td>                
                <td>可调整数量</td>
                <td>可调整张数</td>
                <td>剩余原因</td>
                <td>仓库</td>
                <td>仓位</td>
                <td>制单人</td>
                <td>制单日期</td>                                
                <td>技术员</td>  
                <td>技术员审核日期</td> 
                <td>审批人</td>
                <td>审批日期</td>               
                <td>状态</td>                
                <td>审批意见</td>    
            </tr>
            </HeaderTemplate>
             <ItemTemplate>
            <tr>
                <td><%#Container.ItemIndex+1%></td>         
                <td><asp:Label ID="LabelCode" runat="server" Text='<%#Eval("Code")%>'></asp:Label></td>
                <td><asp:Label ID="LabelPTCTo" runat="server" Text='<%#Eval("PTCTo")%>'></asp:Label></td>  
                 <td><%#Eval("Pjname")%></td>
                <td><%#Eval("Engname")%></td>    
                <td><%#Eval("MaterialCode")%></td>
                <td><%#Eval("MaterialName")%></td>
                <td><%#Eval("MaterialStandard")%></td>
                <td><%#Eval("GB")%></td>
                <td><%#Eval("Attribute")%></td>
                <td><%#Eval("Fixed")%></td>
                <td><%#Eval("Length")%></td>
                <td><%#Eval("Width")%></td>
                <td><%#Eval("LotNumber")%></td>
                <td><%#Eval("Unit")%></td>
                <td><asp:Label ID="LabelAJ" runat="server" Text='<%#Eval("AJNUM")%>'></asp:Label></td>
                <td><asp:Label ID="LabelFAJ" runat="server" Text='<%#Eval("FAJNUM")%>'></asp:Label></td>                
                <td><%#Eval("PTCFrom")%></td>               
                <td><asp:Label ID="LabelNumber" runat="server" Text='<%#Eval("Number")%>'></asp:Label></td>
                <td><asp:Label ID="LabelQuantity" runat="server" Text='<%#Eval("Quantity")%>'></asp:Label></td>
                <td><%#Eval("shengyunote")%></td>
                <td><%#Eval("Warehouse")%></td>
                <td><%#Eval("Location")%></td>
                <td><%#Eval("Doc")%></td>
                <td><%#Eval("Date")%></td>
                <td><%#Eval("Verifier")%></td>
                <td><%#Eval("VerifyDate")%></td>
                <td><%#Eval("Managername")%></td>                
                <td><%#Eval("ManagerDate")%></td>                
                <td><%#convertState((string)Eval("State"))%></td>                
                <td><%#Eval("PT_MANAGERNOTE")%></td>                
                     
            </tr>
            </ItemTemplate>
            <FooterTemplate>
            <tr>
                <td></td>
                <td></td>
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
                <td> <asp:label runat="server" ID="labelsumaj"></asp:label></td>
                <td> <asp:label runat="server" ID="labelsumfaj"></asp:label></td>
                <td></td>
                <td><asp:Label ID="LabelTotalNumber" runat="server"></asp:Label></td>
                <td><asp:Label ID="LabelTotalQuantity" runat="server"></asp:Label></td>  
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
           fixedCols : 5,
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
                                          window.open("SM_Warehouse_ProjTemp.aspx?FLAG=OPEN&&ID=" + id+"&&tm="+time);
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

